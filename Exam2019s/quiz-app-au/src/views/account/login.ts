import { Router } from 'aurelia-router';
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { ILoginRequest } from "../../domain/identity/ILoginRequest";

@autoinject
export class AccountLogin {

    private _email: string = "";
    private _password: string = "";
    private _loginWasOk: boolean | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    loginOnClick() {
        console.log(this._email, this._password);
        const loginDTO: ILoginRequest = {
            email: this._email,
            password: this._password
        }

        this.accountService.login(loginDTO).then(
            response => {
                console.log(response);
                if (response.isSuccessful) {
                    this.appState.jwt = response.data.token;
                    this._loginWasOk = true;
                    this.router!.navigateToRoute('home');
                } else {
                    this._loginWasOk = false;
                }
            }
        );
    }

}
