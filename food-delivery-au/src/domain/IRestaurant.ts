import { IItemType } from "./IItemType";
import { IWorkingHours } from "./IWorkingHours";
import { ICategoryView } from "./ICategory";

export interface IRestaurantView {
    id: string;
    name: string;
    deliveryCost: number;
    categories: ICategoryView[];
    isOpen: boolean;
}

export interface IRestaurant {
    id: string;
    name: string;
    deliveryCost: number;
    workingHours: IWorkingHours[];
    phone: string;
    address: string;
    description?: string;
    itemTypes: IItemType[];
}

export interface IRestaurantEdit {
    id: string;
    name: string;
    phone: string;
    address: string;
    description?: string;
    deliveryCost: number;
}
