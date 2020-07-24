import { IFetchResponse } from "@/types/IFetchResponse";
import { IAddress, IAddressCreate } from "@/domain/IAddress";
import { BaseAPI } from "@/services/BaseAPI";

export abstract class AddressAPI extends BaseAPI {
    static url = 'Address/';

    static async getAll(): Promise<IFetchResponse<IAddress[]>> {
        return super.baseGetAll<IAddress>(this.url);
    }

    static async get(id: string): Promise<IFetchResponse<IAddress>> {
        return super.baseGet<IAddress>(this.url, id);
    }

    static async post(address: IAddressCreate): Promise<IFetchResponse<IAddress>> {
        return super.basePost<IAddressCreate, IAddress>(this.url, address);
    }

    static async put(id: string, address: IAddress): Promise<IFetchResponse> {
        return super.basePut<IAddress>(this.url, id, address);
    }

    static async delete(id: string): Promise<IFetchResponse> {
        return super.baseDelete(this.url, id);
    }
}
