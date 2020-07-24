import { BaseAPI } from "@/services/BaseAPI";
import { IFetchResponse } from "@/types/IFetchResponse";
import { IItem } from "@/domain/IItem";

export abstract class ItemAPI extends BaseAPI {
    static url = 'Items/';

    static async get(id: string): Promise<IFetchResponse<IItem>> {
        return super.baseGet<IItem>(this.url, id);
    }
}
