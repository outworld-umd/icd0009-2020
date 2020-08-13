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
                    this._sessions = [
                        {
                            "id": "c625c542-dbcc-4dd7-a8c5-2b3e19de4b71",
                            "createdAt": new Date("2020-08-13T11:12:02.6678768"),
                            "quiz": {
                                "id": "f772f4ee-44c7-4504-b091-1f8b8954e500",
                                "title": "123",
                                "description": null,
                                "quizType": 0,
                                "createdAt": "2020-08-13T11:11:55.1555373",
                                "timesTaken": 1
                            }
                        }
                    ]
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
}
