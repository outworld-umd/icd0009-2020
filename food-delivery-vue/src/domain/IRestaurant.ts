import { IItemType } from "@/domain/IItemType";
import { IWorkingHours } from "@/domain/IWorkingHours";
import { ICategory } from "@/domain/ICategory";

export interface IRestaurantView {
    id: string;
    name: string;
    deliveryCost: number;
    categories: ICategory[];
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
