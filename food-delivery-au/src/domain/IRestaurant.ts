import { IItemType } from "./IItemType";
import { IWorkingHours } from "./IWorkingHours";

export interface IRestaurantView {
    id: string;
    name: string;
    deliveryCost: number;
    categories: string[];
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
