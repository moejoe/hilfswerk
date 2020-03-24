import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  username: string;
  loginFehler: boolean;

  constructor(private authService: AuthService, private router: Router) {
    this.loginFehler = false;
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(["index"]);
    }
    else {
      if (window["PasswordCredential"]) {
        navigator.credentials.get(<any>{
          password: true,
          mediation: "optional"
        }).then(c => {
          if (null != c) {
            return this._login(c.id);
          }
          return Promise.resolve();
        });
      }
    }
  }

  private async _login(username: string) {
    this.loginFehler = false;
    let self = this;
    try {
      await this.authService.login(username);
      if (window["PasswordCredential"]) {
        var cred = new window["PasswordCredential"]({
          id: username,
          password: "none",
          name: "Nachbarschaftshilfe"
        });
        navigator.credentials.store(cred)
          .then(function () {
            self.router.navigate(["index"]);
          });
      }
      else {
        self.router.navigate(["index"]);
      }
    }
    catch {
      this.loginFehler = true;
    }
  }

  async login() {
    await this._login(this.username)
  }

}
