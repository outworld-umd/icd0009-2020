import { IRestaurantView } from "@/domain/IRestaurant";

export interface ICategory {
    id: string;
    name: string;
    restaurants?: IRestaurantView[]
}
