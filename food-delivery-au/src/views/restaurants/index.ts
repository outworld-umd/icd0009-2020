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

    private loading = false;

    private _alert: IAlertData | null = null;
    private _restaurants: IRestaurantView[] | null = null;
    private _categories: ICategory[] = [];

    private _isEdited = false;
    private _selected: string[] = [];
    private _initial: ICategoryView[] = [];
    private _currentId: string | null = null;

    constructor(private restaurantService: RestaurantService, private restaurantCategoryService: RestaurantCategoryService, private categoryService: CategoryService, private appState: AppState, private router: Router) {

    }

    async getRestaurants() {
        await this.restaurantService.getAll().then(
            response => {
                console.log("updated", response.data)
                if (response.isSuccessful) {
                    this._alert = null;
                    this._restaurants = response.data;
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
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

    async saveEdit(): Promise<void> {
        this.loading = true;
        for (const view of this._initial) {
            if (!this._selected.includes(view.id)) {
                console.log("delete restaurantcategory", view.restaurantCategoryId);
                const response = await this.restaurantCategoryService.delete(view.restaurantCategoryId);
                console.log(response)
            }
        }
        for (const id of this._selected) {
            if (!this._initial.map(c => c.id).includes(id)) {
                console.log("add category", id, "to restaurant", this._currentId)
                const response = await this.restaurantCategoryService.post({ categoryId: id, restaurantId: this._currentId });
                console.log(response)
            }
        }
        await this.getRestaurants();
        this.closeEdit();
        this.loading = false;
    }

    async attached() {
        await this.getRestaurants();
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
                }

            });

    }

}
