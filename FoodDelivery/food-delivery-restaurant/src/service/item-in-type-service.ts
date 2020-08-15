import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IItemInType, IItemInTypeCreate } from "../domain/IItemInType";

@autoinject
export class ItemInTypeService extends BaseService {
    url = "ItemInTypes/";

    async post(address: IItemInTypeCreate): Promise<IFetchResponse<IItemInType>> {
        return super.basePost<IItemInType>(this.url, address);
    }

    async put(id: string, address: IItemInType): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    async delete(id: string): Promise<IFetchResponse<IItemInType>> {
        return super.baseDelete<IItemInType>(this.url, id);
    }
}
