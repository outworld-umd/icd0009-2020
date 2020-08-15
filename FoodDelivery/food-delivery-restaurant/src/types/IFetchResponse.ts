export interface IFetchResponse<TData = null> {
    isSuccessful: boolean;
    statusCode: number;
    messages?: string[];
    data?: TData;
}
