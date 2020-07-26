import { Router } from 'aurelia-router';
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { RestaurantService } from "../../service/restaurant-service";
import { IAlertData } from "../../types/IAlertData";
import { IRestaurantView } from "../../domain/IRestaurant";
import { AlertType } from "../../types/AlertType";
import { RestaurantCategoryService } from "../../service/restaurant-category-service";
import { CategoryService } from "../../service/category-service";
import { ICategory } from "../../domain/ICategory";

@autoinject
export class RestaurantIndex {

    private _alert: IAlertData | null = null;
    private _restaurants: IRestaurantView[] | null = null;
    private _isCategoryBeingEdited = false;
    private _selectedCategories: string[] = [];
    private _initialCategories: string[] = [];
    private _currentRestaurantId: string | null = null;
    private _categories: ICategory[] = [];

    constructor(private restaurantService: RestaurantService, private restaurantCategoryService: RestaurantCategoryService, private categoryService: CategoryService, private appState: AppState, private router: Router) {

    }

    editCategories(id: string, categories: ICategory[]): void {
        this._isCategoryBeingEdited = true;
        this._currentRestaurantId = id;
        this._selectedCategories = categories.map(c => c.restaurantCategoryId);
        this._initialCategories = categories.map(c => c.restaurantCategoryId);
    }

    closeEdit(): void {
        this._isCategoryBeingEdited = false;
        this._selectedCategories = [];
        this._initialCategories = [];
        this._currentRestaurantId = null;
    }

    saveEdit(): void {
        this._initialCategories.forEach(id => {
            if (!this._selectedCategories.includes(id)) {
                console.log("delete " + id);
                this.restaurantCategoryService.delete(id);
            }
        })
        this._selectedCategories.forEach(id => {
            if (!this._initialCategories.includes(id)) {
                console.log("add " + id)
                this.restaurantCategoryService.post({ categoryId: id, restaurantId: this._currentRestaurantId });
            }
        })
        this.closeEdit();
    }

    attached() {
        this.restaurantService.getAll().then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._restaurants = response.data;
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                    this._restaurants = [
                        {
                            id: "1",
                            name: "Testing Food",
                            deliveryCost: 2.34,
                            isOpen: true,
                            categories: [
                                {
                                    restaurantCategoryId: "450",
                                    id: "1",
                                    name: "Mexican food"
                                }
                            ]
                        }
                    ]
                }
            });
        this.categoryService.getAll().then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._categories = response.data;
                } else {
                    console.log("nerjhbejhfjebf")
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                    this._categories = [
                        {
                            restaurantCategoryId: "450",
                            id: "1",
                            name: "Mexican food"
                        },
                        {
                            restaurantCategoryId: "890",
                            id: "2",
                            name: "Mom is fucked"
                        }
                    ]

                }

            });

    }

}
