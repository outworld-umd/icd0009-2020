export interface IChoice {
    id: string;
    value: string;
}

export interface IChoiceView {
    id: string;
    value: string;
    questionId: string;
    timesAnswered: number;
}
