import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IItem, IItemCreate, IItemEdit } from "../domain/IItem";

@autoinject
export class ItemService extends BaseService {
    url = "Items/";

    async getAll(id: string): Promise<IFetchResponse<IItem[]>> {
        return super.baseGetAll<IItem>(this.url + "restaurant/" + id);
    }

    async get(id: string): Promise<IFetchResponse<IItem>> {
        return super.baseGet<IItem>(this.url, id);
    }

    async post(itemCreate: IItemCreate): Promise<IFetchResponse<IItem>> {
        return super.basePost<IItem>(this.url, itemCreate);
    }

    async put(id: string, itemEdit: IItemEdit): Promise<IFetchResponse> {
        return super.basePut(this.url, id, itemEdit);
    }

    async delete(id: string): Promise<IFetchResponse<IItem>> {
        return super.baseDelete<IItem>(this.url, id);
    }
}
