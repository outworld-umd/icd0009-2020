export interface IOrderItemChoice {
    name: string;
    amount: number;
    cost: number;
}

export interface IOrderItemChoiceTemp {
    itemChoiceId: string;
    name: string;
    amount: number;
    cost: number;
}

export interface IOrderItemChoiceCreate {
    itemChoiceId: string;
    amount: number;
    cost: number;
}
