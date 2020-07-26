import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IChoice, IChoiceCreate, IChoiceEdit } from "../domain/IChoice";

@autoinject
export class ItemChoiceService extends BaseService {
    url = "ItemChoices/";

    async post(address: IChoiceCreate): Promise<IFetchResponse<IChoice>> {
        return super.basePost<IChoice>(this.url, address);
    }

    async put(id: string, address: IChoiceEdit): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    async delete(id: string): Promise<IFetchResponse<IChoice>> {
        return super.baseDelete<IChoice>(this.url, id);
    }
}
