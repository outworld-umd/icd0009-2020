import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";
import { IWorkingHours, IWorkingHoursCreate } from "../domain/IWorkingHours";

@autoinject
export class WorkingHoursService extends BaseService {
    url = "WorkingHours/";

    async post(address: IWorkingHoursCreate): Promise<IFetchResponse<IWorkingHours>> {
        return super.basePost<IWorkingHours>(this.url, address);
    }

    async put(id: string, address: IWorkingHours): Promise<IFetchResponse> {
        return super.basePut(this.url, id, address);
    }

    async delete(id: string): Promise<IFetchResponse<IWorkingHours>> {
        return super.baseDelete<IWorkingHours>(this.url, id);
    }
}
