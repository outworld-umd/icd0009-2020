import {IQuestion, IQuestionView} from "./IQuestion";

export interface IQuiz {
    id: string;
    title: string;
    description?: string;
    quizType: IQuizType;
    questions: IQuestion[];
}

export interface IQuizView {
    id: string;
    title: string;
    description?: string;
    quizType: IQuizType;
    questions: IQuestionView[];
    timesTaken: number;
}

export interface IQuizDisplay {
    id: string;
    title: string;
    description?: string;
    quizType: IQuizType;
    createdAt: string;
    timesTaken: number;
}

export enum IQuizType {
    Quiz,
    Poll
}
