import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IOption, IOptionCreate, IOptionEdit } from "../domain/IOption";

@autoinject
export class ItemOptionService extends BaseService {
    url = "ItemOptions/";

    async post(address: IOptionCreate): Promise<IFetchResponse<IOption>> {
        return super.basePost<IOption>(this.url, address);
    }

    async put(id: string, address: IOptionEdit): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    async delete(id: string): Promise<IFetchResponse<IOption>> {
        return super.baseDelete<IOption>(this.url, id);
    }
}
