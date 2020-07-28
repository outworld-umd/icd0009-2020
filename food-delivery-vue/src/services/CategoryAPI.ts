import { BaseAPI } from "@/services/BaseAPI";
import { IFetchResponse } from "@/types/IFetchResponse";
import { ICategory } from "@/domain/ICategory";

export abstract class CategoryAPI extends BaseAPI {
    static url = 'Categories/';

    static async getAll(): Promise<IFetchResponse<ICategory[]>> {
        return super.baseGetAll<ICategory>(this.url);
    }
}
