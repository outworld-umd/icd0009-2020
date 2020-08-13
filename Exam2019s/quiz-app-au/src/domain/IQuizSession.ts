import {IQuizDisplay, IQuizView} from "./IQuiz";
import {IAnswer} from "./IAnswer";

export interface IQuizSession {
    id: string;
    quizId: string;
    createdAt: Date;
    quiz: IQuizView;
    answers: IAnswer[];
}

export interface IQuizSessionView {
    id: string;
    createdAt: Date;
    quiz: IQuizDisplay;
}
