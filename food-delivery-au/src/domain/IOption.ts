import { IChoice } from "./IChoice";

export interface IOption {
    id: string;
    name: string;
    isRequired: boolean;
    isSingle: boolean;
    choices: IChoice[];
}
