import { IOrderRow, IOrderRowCreate } from "@/domain/IOrderRow";

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
    restaurantId?: string;
    restaurantName?: string;
    createdAt: string;
}

export interface IOrderTemp {
    comment?: string;
    address: string;
    apartment?: string;
    paymentMethod: PaymentMethod;
    orderStatus: OrderStatus;
}

export interface IOrderCreate {
    comment?: string;
    address: string;
    apartment?: string;
    paymentMethod: PaymentMethod;
    orderStatus: OrderStatus;
    foodCost: number;
    orderRows: IOrderRowCreate[];
    deliveryCost: number;
    restaurantId?: string;
    restaurantName?: string;
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
