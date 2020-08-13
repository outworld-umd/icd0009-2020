import {autoinject} from "aurelia-framework";
import {BaseService} from "./base-service";
import {IFetchResponse} from "../types/IFetchResponse";
import {IQuizDisplay, IQuizView} from "../domain/IQuiz";

@autoinject
export class QuizService extends BaseService {
    url = "Quizzes/";

    async getAll(): Promise<IFetchResponse<IQuizDisplay[]>> {
        return super.baseGetAll<IQuizDisplay>(this.url);
    }

    async get(id: string): Promise<IFetchResponse<IQuizView>> {
        return super.baseGet<IQuizView>(this.url, id);
    }
}
