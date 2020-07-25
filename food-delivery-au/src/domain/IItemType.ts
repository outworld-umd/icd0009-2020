import { IItemView } from "./IItem";

export interface IItemType {
    id: string;
    name: string;
    isSpecial: boolean;
    description?: string;
    items: IItemView[];
}
