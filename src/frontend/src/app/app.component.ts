import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {

  }

  ngOnInit(): void {
    let self = this;
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(["/login"]);
    } else {
      function checkToken() {
        const ONE_HOUR_MS = 1000 * 3600;
        let exp = self.authService.getExpiration();
        let expiresMs = +exp - +new Date();
        if (expiresMs < ONE_HOUR_MS) {
          setTimeout(() => { this.router.navigate(["/login"]); }, expiresMs);
        }
        else {
          setTimeout(() => { checkToken(); }, ONE_HOUR_MS);
        }
      }
      checkToken();
    }
  }
  async logout() {
    await this.authService.logout();
    this.router.navigate(["/loggedout"]);
  }
}
