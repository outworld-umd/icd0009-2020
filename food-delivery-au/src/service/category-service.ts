import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { ICategory } from "../domain/ICategory";

@autoinject
export class CategoryService extends BaseService {
    url = "Categories/";

    async getAll(): Promise<IFetchResponse<ICategory[]>> {
        return super.baseGetAll<ICategory>(this.url);
    }
}
