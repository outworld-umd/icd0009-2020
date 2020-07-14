export interface IAddress {
    id: string;
    county: string;
    city: string;
    street: string;
    buildingNumber: string;
    comment?: string;
}

export interface IAddressCreate {
    county: string;
    city: string;
    street: string;
    buildingNumber: string;
    comment?: string;
}
