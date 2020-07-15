import { IFetchResponse } from "@/types/IFetchResponse";
import { BaseAPI } from "@/services/BaseAPI";
import { IOrderRow, IOrderRowCreate, IOrderRowEdit } from "@/domain/IOrderRow";

export abstract class OrderRowAPI extends BaseAPI {
    static url = 'orderrows/';

    static async post(order: IOrderRowCreate): Promise<IFetchResponse<IOrderRow>> {
        return super.basePost<IOrderRowCreate, IOrderRow>(this.url, order);
    }

    static async put(id: string, order: IOrderRowEdit): Promise<IFetchResponse> {
        return super.basePut<IOrderRowEdit>(this.url, id, order);
    }

    static async delete(id: string): Promise<IFetchResponse> {
        return super.baseDelete(this.url, id);
    }
}
