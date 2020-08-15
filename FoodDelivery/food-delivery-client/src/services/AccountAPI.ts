import { BaseAPI } from "@/services/BaseAPI";
import { ILoginRequest } from "@/domain/identity/ILoginRequest";
import { ILoginResponse } from "@/domain/identity/ILoginResponse";
import { IFetchResponse } from "@/types/IFetchResponse";
import { IRegisterRequest } from "@/domain/identity/IRegisterRequest";

export abstract class AccountAPI extends BaseAPI {
    static url = "Accounts/";

    static async login(loginRequest: ILoginRequest): Promise<IFetchResponse<ILoginResponse>> {
        return super.basePost<ILoginResponse>(this.url + "Login", loginRequest, false);
    }

    static async register(registerRequest: IRegisterRequest): Promise<IFetchResponse<ILoginResponse>> {
        return super.basePost<ILoginResponse>(this.url + "Register", registerRequest, false);
    }
}
