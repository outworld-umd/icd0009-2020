import {autoinject} from "aurelia-framework";
import {BaseService} from "./base-service";
import {IFetchResponse} from "../types/IFetchResponse";
import { IQuizSession, IQuizSessionCreate, IQuizSessionView } from "../domain/IQuizSession";

@autoinject
export class QuizSessionService extends BaseService {
    url = "QuizSessions/";

    async getAll(): Promise<IFetchResponse<IQuizSessionView[]>> {
        return super.baseGetAll<IQuizSessionView>(this.url);
    }

    async get(id: string): Promise<IFetchResponse<IQuizSession>> {
        return super.baseGet<IQuizSession>(this.url, id);
    }

    async post(order: IQuizSessionCreate): Promise<IFetchResponse<IQuizSession>> {
        return super.basePost<IQuizSession>(this.url, order);
    }
}
