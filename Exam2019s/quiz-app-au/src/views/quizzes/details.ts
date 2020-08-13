import { NavigationInstruction, RouteConfig, Router } from "aurelia-router";
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from "../../types/IAlertData";
import { AlertType } from "../../types/AlertType";
import { QuizService } from "../../service/quiz-service";
import { IQuizView } from "../../domain/IQuiz";

@autoinject
export class QuizDetails {

    private _loading = false;
    private _spoiler = false;

    private _alert: IAlertData | null = null;
    private _quiz: IQuizView | null = null;

    constructor(private quizService: QuizService, private appState: AppState, private router: Router) {
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
                    this._quiz = {
                            id: "2",
                            title: "Test your knowledge",
                            quizType: 0,
                            questions: [
                               {
                                   id: "1",
                                   title: "Why?",
                                   quizId: "2",
                                   timesAnswered: 30,
                                   description: "easy question",
                                   correctChoiceId: "374",
                                   choices: [
                                       {
                                           id: "35244",
                                           value: "Because",
                                           questionId: "1",
                                           timesAnswered: 20
                                       },
                                       {
                                           id: "374",
                                           value: "Because!",
                                           questionId: "1",
                                           timesAnswered: 10
                                       },
                                       {
                                           id: "304",
                                           value: "Because#",
                                           questionId: "1",
                                           timesAnswered: 0
                                       }
                                   ]
                               },
                               {
                                   id: "2",
                                   title: "Why why?",
                                   quizId: "2",
                                   description: "easy question",
                                   correctChoiceId: '348',
                                   timesAnswered: 33,
                                   choices: [
                                       {
                                           id: "344",
                                           value: "Because",
                                           questionId: "1",
                                           timesAnswered: 20
                                       },
                                       {
                                           id: "345",
                                           value: "Because!",
                                           questionId: "1",
                                           timesAnswered: 10
                                       },
                                       {
                                           id: "348",
                                           value: "Because#",
                                           questionId: "1",
                                           timesAnswered: 3
                                       }
                                   ]
                               }
                           ],
                           timesTaken: 23
                    }
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

    toggleSpoiler(): void {
        this._spoiler = !this._spoiler;
    }
}
