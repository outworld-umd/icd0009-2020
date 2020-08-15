export interface INutritionInfo {
    id: string;
    name: string;
    amount: number;
    unit: string;
}

export interface INutritionInfoCreate {
    name: string;
    amount: number;
    unit: string;
    itemId: string;
}

export interface INutritionInfoEdit {
    id: string;
    name: string;
    amount: number;
    unit: string;
    itemId: string;
}
