import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IOrder, IOrderView } from "../domain/IOrder";

@autoinject
export class OrderService extends BaseService {
    url = "Orders/";

    async getAll(id: string): Promise<IFetchResponse<IOrderView[]>> {
        return super.baseGetAll<IOrderView>(this.url + "restaurant/" + id);
    }

    async get(id: string): Promise<IFetchResponse<IOrder>> {
        return super.baseGet<IOrder>(this.url, id);
    }

    // async patch()
}
