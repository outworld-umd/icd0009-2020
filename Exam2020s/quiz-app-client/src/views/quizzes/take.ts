import { NavigationInstruction, RouteConfig, Router } from "aurelia-router";
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { QuizSessionService } from "../../service/quiz-session-service";
import { IAlertData } from "../../types/IAlertData";
import { AlertType } from "../../types/AlertType";
import { IQuizSessionCreate } from "../../domain/IQuizSession";
import { QuizService } from "../../service/quiz-service";
import { IQuizView } from "../../domain/IQuiz";
import { IAnswerCreate } from "../../domain/IAnswer";
import {Choice} from "../../types/IMessage";

@autoinject
export class QuizTake {

    private _loading = false;
    private _submitting = false;

    private _alert: IAlertData | null = null;
    private _quiz: IQuizView | null = null;
    private _chosenRadios: Choice = {};

    constructor(private quizService: QuizService, private quizSessionService: QuizSessionService, private appState: AppState, private router: Router) {
    }

    async getQuiz(id: string) {
        this._loading = !this._loading;
        await this.quizService.get(id).then(
            response => {
                console.log("updated", response.data)
                if (response.isSuccessful) {
                    this._alert = null;
                    this._quiz = response.data;
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        this._loading = !this._loading;
    }

    async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            await this.getQuiz(params.id);
        }
    }

    get validate(): boolean {
        if (this._quiz) {
            for (const question of this._quiz.questions) {
                if (!this._chosenRadios[question.id]) return false;
            }
        }
        return true;
    }

    async submit(): Promise<void> {
        this._submitting = true;
        const answers: IAnswerCreate[] = [];
        for (const option of this._quiz.questions) {
            console.log(option.id, this._chosenRadios)
            const choice = this._chosenRadios[option.id];
            const otherChoice: IAnswerCreate = {
                choiceId: choice,
                isCorrect: option.correctChoiceId === choice
            }
            answers.push(otherChoice)
        }
        const quizSession: IQuizSessionCreate = {
            quizId: this._quiz.id,
            answers: answers
        }
        console.log(quizSession)
        await this.quizSessionService.post(quizSession).then(
            response => {
                console.log("sent", response.data)
                if (response.isSuccessful) {
                    this._alert = null;
                    this.router.navigate(`session/${response.data.id}/details`)
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        this._submitting = false;
    }
}
