import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { IFetchResponse } from "../types/IFetchResponse";

@autoinject
export class ItemService extends BaseService {
    url = "Items/";

    // async login(loginRequest: ILoginRequest): Promise<IFetchResponse<ILoginResponse>> {
    //     return super.basePost<ILoginResponse>(this.url + "Login", loginRequest, false);
    // }
    //
    // async register(registerRequest: IRegisterRequest): Promise<IFetchResponse<ILoginResponse>> {
    //     return super.basePost<ILoginResponse>(this.url + "Register", registerRequest, false);
    // }
}
