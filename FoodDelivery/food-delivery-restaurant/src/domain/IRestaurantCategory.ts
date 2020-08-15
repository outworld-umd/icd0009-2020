export interface IRestaurantCategoryCreate {
    restaurantId: string;
    categoryId: string;
}

export interface IRestaurantCategory {
    id: string;
    restaurantId: string;
    categoryId: string;
}
