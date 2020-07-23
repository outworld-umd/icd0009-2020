import {IOrderItemChoice, IOrderItemChoiceCreate, IOrderItemChoiceTemp} from "@/domain/IOrderItemChoice";

export interface IOrderRow {
    id: string;
    itemId: string;
    itemName: string;
    orderId: string;
    amount: number;
    cost: number;
    orderItemChoices: IOrderItemChoice[];
    comment?: string;
}

export interface IOrderRowTemp {
    itemId: string;
    amount: number;
    name: string;
    cost: number;
    comment?: string;
    choices: IOrderItemChoiceTemp[];
}

export interface IOrderRowCreate {
    itemId: string;
    amount: number;
    cost: number;
    comment?: string;
    choices: IOrderItemChoiceCreate[];
}

export interface IOrderRowEdit {
    id: string;
    itemId: string;
    orderId: string;
    amount: number;
    cost: number;
    comment?: string;
}
