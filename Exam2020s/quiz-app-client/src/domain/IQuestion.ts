import { IChoice, IChoiceView } from "./IChoice";

export interface IQuestion {
    id: string;
    title: string;
    description?: string;
    choices: IChoice[];
}

export interface IQuestionView {
    id: string;
    title: string;
    description?: string;
    correctChoiceId?: string;
    choices: IChoiceView[];
    quizId: string;
    timesAnswered: number;
}
