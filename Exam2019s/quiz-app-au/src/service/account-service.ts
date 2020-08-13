import { autoinject } from 'aurelia-framework';
import { BaseService } from "./base-service";
import { ILoginRequest } from "../domain/identity/ILoginRequest";
import { ILoginResponse } from "../domain/identity/ILoginResponse";
import { IFetchResponse } from "../types/IFetchResponse";
import { IRegisterRequest } from "../domain/identity/IRegisterRequest";

@autoinject
export class AccountService extends BaseService {
    url = "Accounts/";

    async login(loginRequest: ILoginRequest): Promise<IFetchResponse<ILoginResponse>> {
        return super.basePost<ILoginResponse>(this.url + "Login", loginRequest, false);
    }

    async register(registerRequest: IRegisterRequest): Promise<IFetchResponse<ILoginResponse>> {
        return super.basePost<ILoginResponse>(this.url + "Register", registerRequest, false);
    }
}
