import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

import { Token } from '@auth/shared/models/token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthTokenService {
  private tokenKey: string;
  private jwtHelper: JwtHelperService;

  constructor() {
    this.tokenKey = 'token';
    this.jwtHelper = new JwtHelperService();
  }

  /**
   * Set the token.
   *
   * @memberof TokenService
   */
  public setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  /**
   * Get the JWT token.
   *
   * @readonly
   * @type {(string | null)}
   * @memberof TokenService
   */
  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  /**
   * Checks if a token exists.
   *
   * @memberof TokenService
   */
  public hasToken(): boolean {
    return (this.getToken()) ? true : false;
  }

  /**
   * Checks if the token has not expired.
   *
   * @memberof TokenService
   */
  public tokenHasNotExpired(): boolean {
    return !this.hasTokenExpired();
  }

  /**
   * Checks if the token has expired.
   *
   * @memberof TokenService
   */
  public hasTokenExpired(): boolean {
    const token = this.getToken();

    if (token) {
      return this.jwtHelper.isTokenExpired(token);
    }

    return true;
  }

  /**
   * Decodes the token.
   *
   * @memberof TokenService
   */
  public decodeToken(): Token | null {
    const token = this.getToken();

    if (token) {
      return this.jwtHelper.decodeToken(token);
    }

    return null;
  }

  /**
   * Remove the token.
   *
   * @memberof TokenService
   */
  public removeToken(): void {
    localStorage.removeItem(this.tokenKey);
  }
}
