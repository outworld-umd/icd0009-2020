import { Router } from 'aurelia-router';
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { QuizSessionService } from "../../service/quiz-session-service";
import { IAlertData } from "../../types/IAlertData";
import { AlertType } from "../../types/AlertType";
import { IQuizSessionView } from "../../domain/IQuizSession";

@autoinject
export class SessionsIndex {

    private _loading = false;

    private _alert: IAlertData | null = null;
    private _sessions: IQuizSessionView[] | null = null;

    constructor(private sessionService: QuizSessionService, private appState: AppState, private router: Router) {
    }

    async getSessions() {
        this._loading = !this._loading;
        await this.sessionService.getAll().then(
            response => {
                console.log("updated", response.data)
                if (response.isSuccessful) {
                    this._alert = null;
                    this._sessions = response.data;
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

    async attached() {
        await this.getSessions();
    }

    getDate(createdAt: Date): string {
        return new Date(createdAt).toLocaleString('ru-RU')
    }
}
