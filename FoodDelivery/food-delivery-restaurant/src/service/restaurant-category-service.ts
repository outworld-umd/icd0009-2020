import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IRestaurantCategory, IRestaurantCategoryCreate } from "../domain/IRestaurantCategory";

@autoinject
export class RestaurantCategoryService extends BaseService {
    url = "RestaurantCategories/";

    async post(address: IRestaurantCategoryCreate): Promise<IFetchResponse<IRestaurantCategory>> {
        return super.basePost<IRestaurantCategory>(this.url, address);
    }

    async delete(id: string): Promise<IFetchResponse<IRestaurantCategory>> {
        return super.baseDelete<IRestaurantCategory>(this.url, id);
    }
}
