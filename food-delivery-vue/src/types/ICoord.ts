export interface ICoord {
    lat: number;
    lng: number;
}

export interface IFetchLocation {
    results: IResult[];
}

export interface IResult {
    formatted: string;
    geometry: {
        lat: number;
        lng: number;
    }
}
