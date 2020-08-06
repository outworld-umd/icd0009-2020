import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IOrder, IOrderEdit, IOrderView } from "../domain/IOrder";
import { IItemEdit } from "../domain/IItem";

@autoinject
export class OrderService extends BaseService {
    url = "Orders/";

    async getAll(id: string): Promise<IFetchResponse<IOrderView[]>> {
        return super.baseGetAll<IOrderView>(this.url + "Restaurant/" + id);
    }

    async get(id: string): Promise<IFetchResponse<IOrder>> {
        return super.baseGet<IOrder>(this.url, id);
    }

    async put(id: string, itemEdit: IOrderEdit): Promise<IFetchResponse> {
        return super.basePut(this.url, id, itemEdit);
    }
}
