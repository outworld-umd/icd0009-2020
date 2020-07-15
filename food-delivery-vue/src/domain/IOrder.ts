import { IOrderRow } from "@/domain/IOrderRow";

export interface IOrder {
    id: string;
    orderStatus: OrderStatus;
    foodCost: number;
    deliveryCost: number;
    restaurantName?: string;
}

export interface IOrderSingle {
    id: string;
    comment?: string;
    address: string;
    apartment?: string;
    paymentMethod: PaymentMethod;
    orderRows: IOrderRow[];
    orderStatus: OrderStatus;
    foodCost: number;
    deliveryCost: number;
    restaurantId?: string;
    restaurantName?: string;
}

export interface IOrderSingleCreate {
    comment?: string;
    address: string;
    apartment?: string;
    paymentMethod: PaymentMethod;
    orderStatus: OrderStatus;
    foodCost: number;
    deliveryCost: number;
    restaurantId: string;
}

export interface IOrderSingleEdit {
    id: string;
    comment?: string;
    address: string;
    apartment?: string;
    paymentMethod: PaymentMethod;
    orderStatus: OrderStatus;
    foodCost: number;
    deliveryCost: number;
    restaurantId: string;
}

export enum OrderStatus {
    UNFINISHED,
    COOKING,
    DELIVERING,
    DELIVERED
}

export enum PaymentMethod {
    CASH,
    CARD,
    IN_APP
}
