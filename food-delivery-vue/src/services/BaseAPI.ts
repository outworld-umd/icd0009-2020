import Axios, {AxiosError, AxiosResponse} from "axios";
import {IFetchResponse} from "@/types/IFetchResponse";
import { getModule } from "vuex-module-decorators";
import store, { UserModule } from "@/store";
import { IMessage } from "@/types/IMessage";

export abstract class BaseAPI {
    protected static axios = Axios.create({
        baseURL: "https://placeholder.com/api/",
        headers: {
            common: {
                'Content-Type': 'application/json'
            }
        }
    })

    static async baseGetAll<TEntity>(url: string): Promise<IFetchResponse<TEntity[]>> {
        return this.axios.get<TEntity[]>(url, {
            headers: {'Authorization': `Bearer ${getModule(UserModule, store).jwt}`}
        }).then(function (response: AxiosResponse) {
            return {
                isSuccessful: response.status === 200,
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

    static async baseGet<TEntity>(url: string, id: string): Promise<IFetchResponse<TEntity>> {
        return this.axios.get<TEntity>(url + id, {
            headers: {'Authorization': `Bearer ${getModule(UserModule, store).jwt}`}
        }).then(function (response: AxiosResponse) {
            return {
                isSuccessful: response.status === 200,
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

    static async basePost<TEntityCreate, TEntity>(url: string, entity: TEntityCreate): Promise<IFetchResponse<TEntity>> {
        return this.axios.post<TEntityCreate>(url, entity, {
            headers: {'Authorization': `Bearer ${getModule(UserModule, store).jwt}`}
        }).then(function (response: AxiosResponse) {
            return {
                isSuccessful: response.status === 201,
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

    static async basePut<TEntityEdit>(url: string, id: string, entity: TEntityEdit): Promise<IFetchResponse> {
        return this.axios.put(url + id, entity, {
            headers: {'Authorization': `Bearer ${getModule(UserModule, store).jwt}`}
        }).then(function (response: AxiosResponse) {
            return {
                isSuccessful: response.status === 204,
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

    static async baseDelete(url: string, id: string): Promise<IFetchResponse> {
        return this.axios.delete(url + id, {
            headers: {'Authorization': `Bearer ${getModule(UserModule, store).jwt}`}
        }).then(function (response: AxiosResponse) {
            return {
                isSuccessful: response.status === 200,
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
