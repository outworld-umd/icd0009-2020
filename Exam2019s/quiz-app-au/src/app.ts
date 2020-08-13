import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';
import JwtDecode from "jwt-decode";

@autoinject
export class App {
    router?: Router;

    constructor(private appState: AppState) {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = "Quiz Application";

        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },

            { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
            { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

            { route: ['sessions', 'sessions/index'], name: 'session-index', moduleId: PLATFORM.moduleName('views/sessions/index'), nav: false, title: 'Sessions' },
            { route: ['session/:id?/details'], name: 'session-details', moduleId: PLATFORM.moduleName('views/sessions/details'), nav: false, title: 'Session Details' },

            { route: ['quizzes', 'quizzes/index'], name: 'quiz-index', moduleId: PLATFORM.moduleName('views/quizzes/index'), nav: true, title: 'Quizzes' },
            { route: ['quizzes/:id?/details'], name: 'quiz-details', moduleId: PLATFORM.moduleName('views/quizzes/details'), nav: false, title: 'Quiz Details' },
            { route: ['quizzes/:id?/take'], name: 'quiz-take', moduleId: PLATFORM.moduleName('views/quizzes/take'), nav: false, title: 'Take a Quiz' }
        ]);

        config.mapUnknownRoutes('views/home/index');
    }

    logoutOnClick(){
        this.appState.jwt = null;
        this.router!.navigateToRoute('account-login');
    }

    get userEmail(): string {
        const jwt = this.appState.jwt
        if (jwt) {
            const decoded = JwtDecode(jwt) as Record<string, string>;
            return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        }
        return 'null';
    }

}
