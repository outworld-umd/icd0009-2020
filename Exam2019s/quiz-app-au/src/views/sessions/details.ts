import { NavigationInstruction, RouteConfig, Router } from "aurelia-router";
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { QuizSessionService } from "../../service/quiz-session-service";
import { IAlertData } from "../../types/IAlertData";
import { AlertType } from "../../types/AlertType";
import { IQuizSession } from "../../domain/IQuizSession";

@autoinject
export class SessionsDetails {

    private _loading = false;

    private _alert: IAlertData | null = null;
    private _session: IQuizSession | null = null;

    constructor(private sessionService: QuizSessionService, private appState: AppState, private router: Router) {
    }

    async getSession(id: string) {
        this._loading = !this._loading;
        await this.sessionService.get(id).then(
            response => {
                console.log("updated", response.data)
                if (response.isSuccessful) {
                    this._alert = null;
                    this._session = response.data;
                } else {
                    this._session = {
                        answers: [
                            {
                                id: '5',
                                choiceId: '374',
                                isCorrect: true,
                                quizSessionId: '1'
                            },
                            {
                                id: '54',
                                choiceId: '344',
                                isCorrect: false,
                                quizSessionId: '1'
                            }
                        ],
                        createdAt: new Date(),
                        id: "1",
                        quiz: {
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
                        },
                        quizId: "2"}
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
            await this.getSession(params.id);
        }
    }

    isAnswered(id: string): boolean {
        return this._session.answers.map(e => e.choiceId).includes(id);
    }

    isCheckOrTimes(correctId: string, id: string): string {
        return (correctId === id || this._session.quiz.quizType === 1 ? 'fa-check' : 'fa-times') + (this.isAnswered(id) ? '' : 'invisible');
    }
}
