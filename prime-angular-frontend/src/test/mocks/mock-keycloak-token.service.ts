import { Token } from '@auth/shared/services/token.interface';

export class MockKeycloakTokenService implements Token {
  decodeToken() {
    throw new Error("Method not implemented.");
  } clearToken(): void {
    throw new Error("Method not implemented.");
  }
  isTokenExpired(): boolean {
    throw new Error("Method not implemented.");
  }
  login(): Promise<void> {
    throw new Error("Method not implemented.");
  }
  logout(redirectUri: string): Promise<void> {
    throw new Error("Method not implemented.");
  }
  isLoggedIn(): Promise<boolean> {
    throw new Error("Method not implemented.");
  }
  getUserId(): Promise<string> {
    throw new Error("Method not implemented.");
  }
  getUser(forceReload?: boolean): Promise<import("../../app/modules/auth/shared/models/user.model").User> {
    throw new Error("Method not implemented.");
  }
  getAdmin(forceReload?: boolean): Promise<import("../../app/modules/auth/shared/models/admin.model").Admin> {
    throw new Error("Method not implemented.");
  }
  getUserRoles(): string[] {
    throw new Error("Method not implemented.");
  }
  isUserInRole(role: string): boolean {
    throw new Error("Method not implemented.");
  }

}
