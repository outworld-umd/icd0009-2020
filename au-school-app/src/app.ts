import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';

@autoinject
export class App {
  router?: Router;

  constructor(private appState: AppState) {

  }

  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;

    config.title = "Animals";

    config.map([
        { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },

        { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
        { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },


        { route: ['%name%', '%name%/index'], name: '%name%-index', moduleId: PLATFORM.moduleName('views/%name%/index'), nav: true, title: '%name%' },
        { route: ['%name%/details/:id?'], name: '%name%-details', moduleId: PLATFORM.moduleName('views/%name%/details'), nav: false, title: '%name% Details' },
        { route: ['%name%/edit/:id?'], name: '%name%-edit', moduleId: PLATFORM.moduleName('views/%name%/edit'), nav: false, title: '%name% Edit' },
        { route: ['%name%/delete/:id?'], name: '%name%-delete', moduleId: PLATFORM.moduleName('views/%name%/delete'), nav: false, title: '%name% Delete' },
        { route: ['%name%/create'], name: '%name%-create', moduleId: PLATFORM.moduleName('views/%name%/create'), nav: false, title: '%name% Create' },
      ]
    );

    config.mapUnknownRoutes('views/home/index');
  }

  logoutOnClick(){
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }

}
