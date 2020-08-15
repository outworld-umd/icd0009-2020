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

    getDate(createdAt: Date): string {
        return new Date(createdAt).toLocaleString('ru-RU')
    }
}
