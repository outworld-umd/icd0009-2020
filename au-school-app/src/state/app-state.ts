export class AppState {
  constructor(){
  }

  public readonly baseUrl = 'https://localhost:8080/api/';

  // JavaScript Object Notation Web Token
  // to keep track of logged in status
  // https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage
  get jwt(): string | null {
    return localStorage.getItem('jwt');
  }

  set jwt(value: string | null) {
    value ? localStorage.setItem('jwt', value) : localStorage.removeItem('jwt');
  }


}
