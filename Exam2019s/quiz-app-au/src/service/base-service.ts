import Axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { IFetchResponse } from "../types/IFetchResponse";
import { autoinject } from 'aurelia-framework';
import { AppState } from "../state/app-state";
import { IMessage } from "../types/IMessage";
import { Router } from "aurelia-router";

@autoinject
export class BaseService {

    constructor(private appState: AppState, private router: Router) {
    }


    protected axios = Axios.create({
        baseURL: "https://localhost:5001/api/v1.0/",
        headers: {
            common: {
                'Content-Type': 'application/json'
            }
        }
    })

    getConfig(isAuth: boolean): AxiosRequestConfig | undefined {
        const config: AxiosRequestConfig = {
            headers: {
                Authorization: `Bearer ${this.appState.jwt}`
            }
        }
        return isAuth ? config : undefined;
    }

    async baseGetAll<TEntity>(url: string, isAuth = true): Promise<IFetchResponse<TEntity[]>> {
        return this.axios.get<TEntity[]>(url, this.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 500,
                    messages: (error.response?.data as IMessage)?.messages ?? ["Server side error!"]
                }
            })
    }

    async baseGet<TEntity>(url: string, id: string, isAuth = true): Promise<IFetchResponse<TEntity>> {
        return this.axios.get<TEntity>(url + id, this.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 500,
                    messages: (error.response?.data as IMessage)?.messages ?? ["Server side error!"]
                }
            })
    }

    async basePost<TEntity>(url: string, entity: any, isAuth = true): Promise<IFetchResponse<TEntity>> {
        return this.axios.post<TEntity>(url, entity, this.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 500,
                    messages: (error.response?.data as IMessage)?.messages ?? ["Server side error!"]
                }
            })
    }

    async basePut(url: string, id: string, entity: any, isAuth = true): Promise<IFetchResponse<null>> {
        return this.axios.put(url + id, entity, this.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 500,
                    messages: (error.response?.data as IMessage)?.messages ?? ["Server side error!"]
                }
            })
    }

    async baseDelete<TEntity>(url: string, id: string, isAuth = true): Promise<IFetchResponse<TEntity>> {
        return this.axios.delete<TEntity>(url + id, this.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status,
                    data: response.data
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 500,
                    messages: (error.response?.data as IMessage)?.messages ?? ["Server side error!"]
                }
            })
    }

    async basePatch(url: string, id: string, entity: any, isAuth = true): Promise<IFetchResponse<null>> {
        return this.axios.patch(url + id, entity, this.getConfig(isAuth))
            .then(function (response: AxiosResponse) {
                return {
                    isSuccessful: response.status < 300,
                    statusCode: response.status
                }
            }).catch(function (error: AxiosError) {
                return {
                    isSuccessful: false,
                    statusCode: error.response?.status ?? 500,
                    messages: (error.response?.data as IMessage)?.messages ?? ["Server side error!"]
                }
            })
    }
}
