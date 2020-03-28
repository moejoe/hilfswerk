import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http"
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private token: string;

  constructor(private httpClient: HttpClient) {
    this.token = localStorage.getItem("token");
  }

  isLoggedIn(): boolean {
    let token = localStorage.getItem("token");
    if (null == token) {
      return false;
    }
    let helper = new JwtHelperService();
    try {
      return !helper.isTokenExpired(token);
    }
    catch {
      return false;
    }
  }

  async login(name: string) {
    try {
      let res = await this.httpClient.post<{ token: string }>(`${environment.apiUrl}/token`, { name }).toPromise();
      this.token = res.token;
      localStorage.setItem("token", res.token);
    }
    catch {
      throw new Error("Login error");
    }
  }

  getToken() {
    return this.token;
  }

  getUserInfo(): Observable<HttpResponse<UserInfo>> {
    if (!this.isLoggedIn()) {
      throw new Error("User not logged in.");
    }
    try {
      let requestHeaders = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("token")
      });
      return this.httpClient.get<UserInfo>(`${environment.apiUrl}/oauth2/userinfo`, { headers: requestHeaders, observe: 'response' } );
    }
    catch {
      throw new Error("Can't get userinfo");
    }
  }

  async logout() {
    localStorage.removeItem("token");
    this.token = null;
    if (window["PasswordCredential"]) {
      await navigator.credentials.preventSilentAccess();
    }
  }
}
export interface UserInfo {
  name: string;
}