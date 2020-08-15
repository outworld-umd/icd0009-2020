import { INutritionInfo } from "@/domain/INutritionInfo";
import { IOption } from "@/domain/IOption";

export interface IItemView {
    id: string;
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
    nutritionInfos: INutritionInfo[];
    options: IOption[];
}
