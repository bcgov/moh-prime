import { User } from '@auth/shared/models/user.model';
import { Admin } from '../models/admin.model';

export interface Token {
  decodeToken();
  clearToken(): void;
  isTokenExpired(): boolean;
  login(): Promise<void>;
  logout(redirectUri: string): Promise<void>;
  isLoggedIn(): Promise<boolean>;

  getUserId(): Promise<string>;
  getUser(forceReload?: boolean): Promise<User>;
  getAdmin(forceReload?: boolean): Promise<Admin>;
  getUserRoles(): string[];
  isUserInRole(role: string): boolean;
}
