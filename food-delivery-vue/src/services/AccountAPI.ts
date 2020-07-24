import { BaseAPI } from "@/services/BaseAPI";
import { ILoginRequest } from "@/domain/identity/ILoginRequest";
import { ILoginResponse } from "@/domain/identity/ILoginResponse";

export abstract class AccountAPI extends BaseAPI {
    static url = "Account/";

    static async login(loginDTO: ILoginRequest): Promise<string | null> {
        const url = "Login";
        try {
            const response = await this.axios.post<ILoginResponse>(this.url + url, loginDTO);
            console.log('getJwt response', response);
            if (response.status === 200) return response.data.token;
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }
}
