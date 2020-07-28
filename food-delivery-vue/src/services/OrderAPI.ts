import { IFetchResponse } from "@/types/IFetchResponse";
import { IOrder, IOrderCreate, IOrderView } from "@/domain/IOrder";
import { BaseAPI } from "@/services/BaseAPI";

export abstract class OrderAPI extends BaseAPI {
    static url = 'Orders/';

    static async getAll(): Promise<IFetchResponse<IOrderView[]>> {
        return super.baseGetAll<IOrderView>(this.url);
    }

    static async get(id: string): Promise<IFetchResponse<IOrder>> {
        return super.baseGet<IOrder>(this.url, id);
    }

    static async post(order: IOrderCreate): Promise<IFetchResponse<IOrder>> {
        return super.basePost<IOrder>(this.url, order);
    }
}
