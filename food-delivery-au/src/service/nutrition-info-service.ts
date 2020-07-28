import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { INutritionInfo, INutritionInfoCreate, INutritionInfoEdit } from "../domain/INutritionInfo";

@autoinject
export class NutritionInfoService extends BaseService {
    url = "NutritionInfos/";

    async post(address: INutritionInfoCreate): Promise<IFetchResponse<INutritionInfo>> {
        return super.basePost<INutritionInfo>(this.url, address);
    }

    async put(id: string, address: INutritionInfoEdit): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    async delete(id: string): Promise<IFetchResponse<INutritionInfo>> {
        return super.baseDelete<INutritionInfo>(this.url, id);
    }
}
