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
  private expirationDate: Date;

  constructor(private httpClient: HttpClient) {
    this.token = localStorage.getItem("token");
    if (null != this.token) {
      let helper = new JwtHelperService();
      try {
        this.expirationDate = helper.getTokenExpirationDate(this.token);
      }
      catch {
        this.expirationDate = null;
      }
    }
  }

  isLoggedIn(): boolean {
    if (null == this.token || null == this.expirationDate) {
      return false;
    }
    if (+this.expirationDate > +new Date()) {
      return true;
    }
    return false;
  }

  async login(name: string) {
    try {
      let res = await this.httpClient.post<{ token: string }>(`${environment.apiUrl}/token`, { name }).toPromise();
      this.token = res.token;
      let helper = new JwtHelperService();
      this.expirationDate = helper.getTokenExpirationDate(this.token);
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
        'Authorization': `Bearer ${this.token}`
      });
      return this.httpClient.get<UserInfo>(`${environment.apiUrl}/oauth2/userinfo`, { headers: requestHeaders, observe: 'response' });
    }
    catch {
      throw new Error("Can't get userinfo");
    }
  }

  getExpiration() {
    return this.expirationDate;
  }

  async logout() {
    localStorage.removeItem("token");
    this.token = null;
    this.expirationDate = null;
    if (window["PasswordCredential"]) {
      await navigator.credentials.preventSilentAccess();
    }
  }
}

export interface UserInfo {
  name: string;
}