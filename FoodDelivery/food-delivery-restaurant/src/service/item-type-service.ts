import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IItemType, IItemTypeCreate, IItemTypeEdit } from "../domain/IItemType";

@autoinject
export class ItemTypeService extends BaseService {
    url = "ItemTypes/";

    async post(address: IItemTypeCreate): Promise<IFetchResponse<IItemType>> {
        return super.basePost<IItemType>(this.url, address);
    }

    async put(id: string, address: IItemTypeEdit): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    async delete(id: string): Promise<IFetchResponse<IItemType>> {
        return super.baseDelete<IItemType>(this.url, id);
    }
}
