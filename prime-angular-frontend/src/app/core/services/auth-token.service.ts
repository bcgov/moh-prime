import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

import { Token } from '../models/token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthTokenService {
  private tokenKey: string;
  private refreshTokenKey: string;
  private jwtHelper: JwtHelperService;

  constructor() {
    this.tokenKey = 'token';
    this.jwtHelper = new JwtHelperService();
  }

  public setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  public hasToken(): boolean {
    return (this.getToken()) ? true : false;
  }

  public tokenHasNotExpired(): boolean {
    return !this.hasTokenExpired();
  }

  public hasTokenExpired(): boolean {
    const token = this.getToken();

    if (token) {
      return this.jwtHelper.isTokenExpired(token);
    }

    return true;
  }

  public decodeToken(): Token | null {
    const token = this.getToken();

    if (token) {
      return this.jwtHelper.decodeToken(token);
    }

    return null;
  }

  public removeToken(): void {
    localStorage.removeItem(this.tokenKey);
  }
}
