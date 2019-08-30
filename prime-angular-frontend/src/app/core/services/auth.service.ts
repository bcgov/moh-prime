import { Injectable } from '@angular/core';

// import { User } from '@shared/models/user.model';
// import { Role } from '@shared/enums/role.enum';
import { AuthTokenService } from './auth-token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private tokenService: AuthTokenService
  ) { }

  // public get user(): User {
  //   return this.tokenService.decodeToken().user;
  // }

  // public isLoggedIn(): boolean {
  //   return this.tokenService.tokenHasNotExpired();
  // }

  // public isApplicant(): boolean {
  //   const role = this.getRole();
  //   return (role) ? !!(role === Role.APPLICANT) : false;
  // }

  // public isAdmin(): boolean {
  //   const role = this.getRole();
  //   return (role) ? !!(role === Role.ADMIN) : false;
  // }

  // private getRole(): string {
  //   const claim = this.tokenService.decodeToken();
  //   return (claim && claim.user) ? claim.user.role : '';
  // }
}
