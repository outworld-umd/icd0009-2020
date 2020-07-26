import { IItemView } from "./IItem";

export interface IItemType {
    id: string;
    name: string;
    isSpecial: boolean;
    description?: string;
    items: IItemView[];
}

export interface IItemTypeCreate {
    name: string;
    isSpecial: boolean;
    description?: string;
    restaurantId: string;
}

export interface IItemTypeEdit {
    id: string;
    name: string;
    isSpecial: boolean;
    description?: string;
    restaurantId: string;
}
