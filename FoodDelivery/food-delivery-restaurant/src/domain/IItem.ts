import { INutritionInfo } from "./INutritionInfo";
import { IOption } from "./IOption";

export interface IItemView {
    id: string;
    itemInTypeId: string;
    name: string;
    pictureLink?: string;
    price: number;
}

export interface IItem {
    id: string;
    name: string;
    pictureLink?: string;
    price: number;
    description?: string;
    restaurantId: string;
    nutritionInfos: INutritionInfo[];
    options: IOption[];
}

export interface IItemCreate {
    name: string;
    pictureLink?: string;
    price: number;
    restaurantId: string;
    description?: string;
}

export interface IItemEdit {
    id: string;
    name: string;
    pictureLink?: string;
    price: number;
    restaurantId: string;
    description?: string;
}
