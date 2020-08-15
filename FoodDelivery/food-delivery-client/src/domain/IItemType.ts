import { IItemView } from "@/domain/IItem";

export interface IItemType {
    id: string;
    name: string;
    isSpecial: boolean;
    description?: string;
    items: IItemView[];
}
