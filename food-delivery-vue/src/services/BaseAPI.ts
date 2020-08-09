import Axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { IFetchResponse } from "@/types/IFetchResponse";
import { getModule } from "vuex-module-decorators";
import store from "@/store";
import UserModule from '@/store/modules/UserModule'
import { IMessage } from "@/types/IMessage";

export abstract class BaseAPI {
    protected static axios = Axios.create({
        baseURL: "coltfood.azurewebsites.net",
        headers: {
            common: {
                'Content-Type': 'application/json'
            }
        }
    })

    static getConfig(isAuth: boolean): AxiosRequestConfig | undefined {
        const config: AxiosRequestConfig = {
            headers: {
                Authorization: `Bearer ${getModule(UserModule, store).jwt}`
            }
        }
        return isAuth ? config : undefined;
    }

    static async baseGetAll<TEntity>(url: string, isAuth = true): Promise<IFetchResponse<TEntity[]>> {
        return this.axios.get<TEntity[]>(url, BaseAPI.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                console.log(error.response)
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 400,
                    messages: (error.response?.data as IMessage).messages
                }
            })
    }

    static async baseGet<TEntity>(url: string, id: string, isAuth = true): Promise<IFetchResponse<TEntity>> {
        return this.axios.get<TEntity>(url + id, BaseAPI.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 400,
                    messages: (error.response?.data as IMessage).messages
                }
            })
    }

    static async basePost<TEntity>(url: string, entity: any, isAuth = true): Promise<IFetchResponse<TEntity>> {
        return this.axios.post<TEntity>(url, entity, BaseAPI.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 400,
                    messages: (error.response?.data as IMessage).messages
                }
            })
    }

    static async basePut(url: string, id: string, entity: any, isAuth = true): Promise<IFetchResponse> {
        return this.axios.put(url + id, entity, BaseAPI.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 400,
                    messages: (error.response?.data as IMessage).messages
                }
            })
    }

    static async baseDelete<TEntity>(url: string, id: string, isAuth = true): Promise<IFetchResponse<TEntity>> {
        return this.axios.delete<TEntity>(url + id, BaseAPI.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 400,
                    messages: (error.response?.data as IMessage).messages
                }
            })
    }
}
