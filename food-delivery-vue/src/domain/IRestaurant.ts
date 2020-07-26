import { IItemType } from "@/domain/IItemType";
import { IWorkingHours } from "@/domain/IWorkingHours";
import { ICategoryView } from "@/domain/ICategory";

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
