export interface IItemType {
    id: string;
    name: string;
    isSpecial: boolean;
    description?: string;
    items: ItemsDTO[];
}
