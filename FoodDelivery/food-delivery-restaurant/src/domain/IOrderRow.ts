import { IOrderItemChoice } from "./IOrderItemChoice";

export interface IOrderRow {
    name: string;
    amount: number;
    cost: number;
    choices: IOrderItemChoice[];
    comment?: string;
}
