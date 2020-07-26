import { IRestaurantView } from "@/domain/IRestaurant";

export interface ICategory {
    id: string;
    name: string;
    restaurants: IRestaurantView[]
}

export interface ICategoryView {
    restaurantCategoryId: string;
    id: string;
    name: string;
}
