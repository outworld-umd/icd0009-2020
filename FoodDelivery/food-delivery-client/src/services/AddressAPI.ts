import { IFetchResponse } from "@/types/IFetchResponse";
import { IAddress, IAddressCreate } from "@/domain/IAddress";
import { BaseAPI } from "@/services/BaseAPI";

export class AddressAPI extends BaseAPI {
    static url = 'Addresses/';

    static async getAll(): Promise<IFetchResponse<IAddress[]>> {
        return super.baseGetAll<IAddress>(this.url);
    }

    static async get(id: string): Promise<IFetchResponse<IAddress>> {
        return super.baseGet<IAddress>(this.url, id);
    }

    static async post(address: IAddressCreate): Promise<IFetchResponse<IAddress>> {
        return super.basePost<IAddress>(this.url, address);
    }

    static async put(id: string, address: IAddress): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    static async delete(id: string): Promise<IFetchResponse<IAddress>> {
        return super.baseDelete<IAddress>(this.url, id);
    }
}
