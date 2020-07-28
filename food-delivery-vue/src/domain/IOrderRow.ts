import { IOrderItemChoice, IOrderItemChoiceCreate, IOrderItemChoiceTemp } from "@/domain/IOrderItemChoice";

export interface IOrderRow {
    id: string;
    name: string;
    amount: number;
    cost: number;
    choices: IOrderItemChoice[];
    comment?: string;
}

export interface IOrderRowCreate {
    itemId: string;
    amount: number;
    name: string;
    cost: number;
    comment?: string;
    choices: IOrderItemChoiceTemp[];
}
