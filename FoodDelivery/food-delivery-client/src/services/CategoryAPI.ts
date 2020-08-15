import { BaseAPI } from "@/services/BaseAPI";
import { IFetchResponse } from "@/types/IFetchResponse";
import { ICategory } from "@/domain/ICategory";
import { getModule } from "vuex-module-decorators";
import UserModule from "@/store/modules/UserModule";
import store from '@/store'

export abstract class CategoryAPI extends BaseAPI {
    static url = `Categories/?culture=${getModule(UserModule, store).lang}`;

    static async getAll(): Promise<IFetchResponse<ICategory[]>> {
        console.log(this.url)
        return super.baseGetAll<ICategory>(this.url);
    }
}
