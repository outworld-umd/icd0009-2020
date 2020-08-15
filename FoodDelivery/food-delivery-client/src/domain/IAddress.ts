export interface IAddress {
    id: string;
    name: string;
    county: string;
    city: string;
    street: string;
    buildingNumber: string;
    apartment?: string;
    comment?: string;
}

export interface IAddressCreate {
    name: string;
    county: string;
    city: string;
    street: string;
    buildingNumber: string;
    apartment?: string;
    comment?: string;
}
