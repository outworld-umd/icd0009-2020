import { Router } from 'aurelia-router';
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from "../../types/IAlertData";
import { AlertType } from "../../types/AlertType";
import { IQuizDisplay } from "../../domain/IQuiz";
import { QuizService } from "../../service/quiz-service";

@autoinject
export class QuizzesIndex {

    private _loading = false;

    private _alert: IAlertData | null = null;
    private _quizzes: IQuizDisplay[] | null = null;
    private _searchByName = '';
    private _typeFilter = 2;

    constructor(private quizService: QuizService, private appState: AppState, private router: Router) {
    }

    get loggedIn(): boolean {
        return this.appState.jwt != null;
    }

    async getQuizzes() {
        this._loading = !this._loading;
        await this.quizService.getAll().then(
            response => {
                console.log("updated", response.data)
                if (response.isSuccessful) {
                    this._alert = null;
                    this._quizzes = response.data;
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

    get quizzes(): IQuizDisplay[] {
        return this._quizzes.filter(r => {
            return r.title.toLowerCase().includes(this._searchByName.toLowerCase()) && r.quizType !== this._typeFilter
        })
    }

    async attached() {
        await this.getQuizzes();
    }

    getDate(createdAt: Date): string {
        return new Date(createdAt).toLocaleString('ru-RU')
    }
}
