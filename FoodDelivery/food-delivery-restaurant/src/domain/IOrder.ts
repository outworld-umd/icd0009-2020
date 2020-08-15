import { IOrderRow } from "./IOrderRow";

export interface IOrderView {
    id: string;
    orderStatus: OrderStatus;
    foodCost: number;
    deliveryCost: number;
    restaurantName?: string;
    createdAt: Date;
}

export interface IOrder {
    id: string;
    comment?: string;
    address: string;
    apartment?: string;
    paymentMethod: PaymentMethod;
    orderRows: IOrderRow[];
    orderStatus: OrderStatus;
    foodCost: number;
    deliveryCost: number;
    restaurantId: string;
    restaurantName?: string;
    createdAt: Date;
}

export interface IOrderEdit {
    id: string;
    comment?: string;
    address: string;
    paymentMethod: PaymentMethod;
    orderStatus: OrderStatus;
    foodCost: number;
    deliveryCost: number;
    restaurantId: string;
}

export enum OrderStatus {
    SENT,
    COOKING,
    DELIVERING,
    DELIVERED
}

export enum PaymentMethod {
    CASH,
    CARD,
    IN_APP
}
