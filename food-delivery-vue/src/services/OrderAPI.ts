import { IFetchResponse } from "@/types/IFetchResponse";
import { IOrder, IOrderCreate, IOrderEdit, IOrderView } from "@/domain/IOrder";
import { BaseAPI } from "@/services/BaseAPI";

export abstract class OrderAPI extends BaseAPI {
    static url = 'orders/';

    static async getAll(): Promise<IFetchResponse<IOrderView[]>> {
        return super.baseGetAll<IOrderView>(this.url);
    }

    static async get(id: string): Promise<IFetchResponse<IOrder>> {
        return super.baseGet<IOrder>(this.url, id);
    }

    static async post(order: IOrderCreate): Promise<IFetchResponse<IOrder>> {
        return super.basePost<IOrderCreate, IOrder>(this.url, order);
    }

    static async put(id: string, order: IOrderEdit): Promise<IFetchResponse> {
        return super.basePut<IOrderEdit>(this.url, id, order);
    }

    static async delete(id: string): Promise<IFetchResponse> {
        return super.baseDelete(this.url, id);
    }
}
