export interface IOrderItemChoice {
    id: string;
    orderRowId: string;
    itemChoiceId: string;
    itemChoiceName: string;
    amount: number;
    cost: number;
}

export interface IOrderItemChoiceTemp {
    itemChoiceId: string;
    amount: number;
    cost: number;
}

export interface IOrderItemChoiceCreate {
    orderRowId?: string;
    itemChoiceId: string;
    amount: number;
    cost: number;
}

export interface IOrderItemChoiceEdit {
    id: string;
    orderRowId: string;
    itemChoiceId: string;
    amount: number;
    cost: number;
}
