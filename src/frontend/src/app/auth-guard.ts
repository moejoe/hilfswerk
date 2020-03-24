import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { AuthService } from './services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
    var hasAccessToken = this.authService.isLoggedIn();
    if (hasAccessToken) {
      return true;
    }
    else {
      this.router.navigate(["login"], { queryParamsHandling: "merge" });
      return false;
    }
  }
}
