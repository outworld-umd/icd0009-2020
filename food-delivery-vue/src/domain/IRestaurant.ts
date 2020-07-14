import { IItemType } from "@/domain/IItemType";
import { IWorkingHours } from "@/domain/IWorkingHours";

export interface IRestaurant {
    id: string;
    name: string;
    deliveryCost: number;
    categories: string[];
    isOpen: boolean;
}

export interface IRestaurantSingle {
    id: string;
    name: string;
    deliveryCost: string;
    workingHours: IWorkingHours[];
    phone: string;
    address: string;
    description?: string;
    itemTypes: IItemType[];
}
