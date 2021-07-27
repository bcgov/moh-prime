import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public get length(): number {
    return localStorage.length;
  }

  public set(key: string, value: string): any {
    localStorage.setItem(key, value);
  }

  public get(key: string): string {
    return localStorage.getItem(key);
  }

  public getInteger(key: string): number {
    return (localStorage.getItem(key)) ? parseInt(localStorage.getItem(key)) : null;
  }

  public clear(): void {
    localStorage.clear();
  }

  public removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  public key(index: number): string {
    return localStorage.key(index);
  }
}
