import { IChoice } from "./IChoice";

export interface IOption {
    id: string;
    name: string;
    isRequired: boolean;
    isSingle: boolean;
    choices: IChoice[];
}

export interface IOptionCreate {
    name: string;
    isRequired: boolean;
    isSingle: boolean;
    itemId: string;
}

export interface IOptionEdit {
    id: string;
    name: string;
    isRequired: boolean;
    isSingle: boolean;
    itemId: string;
}
