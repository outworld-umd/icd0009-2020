import { BaseAPI } from "@/services/BaseAPI";
import { IFetchResponse } from "@/types/IFetchResponse";
import { IRestaurant, IRestaurantView } from "@/domain/IRestaurant";

export abstract class RestaurantAPI extends BaseAPI {
    static url = 'Restaurants/';

    static async getAll(): Promise<IFetchResponse<IRestaurantView[]>> {
        return super.baseGetAll<IRestaurantView>(this.url);
    }

    static async get(id: string): Promise<IFetchResponse<IRestaurant>> {
        return super.baseGet<IRestaurant>(this.url, id);
    }
}
