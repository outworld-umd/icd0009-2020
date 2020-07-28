export interface IChoice {
    id: string;
    name: string;
    additionalPrice: number;
}

export interface IChoiceCreate {
    name: string;
    additionalPrice: number;
    itemOptionId: string;
}

export interface IChoiceEdit {
    id: string;
    name: string;
    additionalPrice: number;
    itemOptionId: string;
}
