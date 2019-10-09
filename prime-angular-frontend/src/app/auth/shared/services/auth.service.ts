import { Injectable } from '@angular/core';

import { Role } from '@auth/shared/enum/role.enum';
import { User } from '@auth/shared/models/user.model';
import { AuthTokenService } from '@auth/shared/services/auth-token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private tokenService: AuthTokenService
  ) { }

  public get user(): User {
    // TODO: update this to reflect the claim
    return this.tokenService.decodeToken().user;
  }

  public isLoggedIn(): boolean {
    return this.tokenService.tokenHasNotExpired();
  }

  public isApplicant(): boolean {
    const role = this.getRole();
    return (role) ? !!(role === Role.APPLICANT) : false;
  }

  public isAdmin(): boolean {
    const role = this.getRole();
    return (role) ? !!(role === Role.ADMIN) : false;
  }

  private getRole(): string {
    const claim = this.tokenService.decodeToken();
    return (claim && claim.user) ? claim.user.role : '';
  }
}
