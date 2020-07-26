import { Router } from 'aurelia-router';
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { RestaurantService } from "../../service/restaurant-service";
import { IAlertData } from "../../types/IAlertData";
import { IRestaurantView } from "../../domain/IRestaurant";
import { AlertType } from "../../types/AlertType";
import { RestaurantCategoryService } from "../../service/restaurant-category-service";
import { CategoryService } from "../../service/category-service";
import { ICategory, ICategoryView } from "../../domain/ICategory";

@autoinject
export class RestaurantIndex {

    private _alert: IAlertData | null = null;
    private _restaurants: IRestaurantView[] | null = null;
    private _categories: ICategory[] = [];

    private _isEdited = false;
    private _selected: string[] = [];
    private _initial: ICategoryView[] = [];
    private _currentId: string | null = null;

    constructor(private restaurantService: RestaurantService, private restaurantCategoryService: RestaurantCategoryService, private categoryService: CategoryService, private appState: AppState, private router: Router) {

    }

    editCategories(id: string, categories: ICategoryView[]): void {
        this._isEdited = true;
        this._currentId = id;
        this._selected = categories.map(c => c.id);
        this._initial = categories;
    }

    closeEdit(): void {
        this._isEdited = false;
        this._selected = [];
        this._initial = [];
        this._currentId = null;
    }

    saveEdit(): void {
        this._initial.forEach(view => {
            if (!this._selected.includes(view.id)) {
                console.log("delete restaurantcategory", view.restaurantCategoryId);
                this.restaurantCategoryService.delete(view.restaurantCategoryId);
            }
        })
        this._selected.forEach(id => {
            if (!this._initial.map(c => c.id).includes(id)) {
                console.log("add category", id, "to restaurant", this._currentId)
                this.restaurantCategoryService.post({ categoryId: id, restaurantId: this._currentId });
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
                            id: "1",
                            name: "Mexican food"
                        },
                        {
                            id: "2",
                            name: "Mom is fucked"
                        }
                    ]

                }

            });

    }

}
