import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IRestaurant, IRestaurantView } from "../domain/IRestaurant";

@autoinject
export class RestaurantService extends BaseService {
    url = "Restaurants";

    async getAll(): Promise<IFetchResponse<IRestaurantView[]>> {
        return super.baseGetAll<IRestaurantView>(this.url + `?culture=en-GB`);
    }

    async get(id: string): Promise<IFetchResponse<IRestaurant>> {
        return super.baseGet<IRestaurant>(this.url + "/", id);
    }
}
