import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IItem, IItemCreate, IItemEdit } from "../domain/IItem";

@autoinject
export class ItemService extends BaseService {
    url = "Items/";

    async get(id: string): Promise<IFetchResponse<IItem>> {
        return super.baseGet<IItem>(this.url, id);
    }

    async post(address: IItemCreate): Promise<IFetchResponse<IItem>> {
        return super.basePost<IItem>(this.url, address);
    }

    async put(id: string, address: IItemEdit): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    async delete(id: string): Promise<IFetchResponse<IItem>> {
        return super.baseDelete<IItem>(this.url, id);
    }
}
