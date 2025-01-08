import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  private readonly LANGUAGE = 'language';

  constructor() { }

  public get(key: string) {
    return localStorage.getItem(key);
  }

  public set(key: string, value: string) {
    return localStorage.setItem(key, value);

  }

  public remove(key: string) {
    return localStorage.removeItem(key);
  }

  public getLanguage(): string | null {
    return localStorage.getItem(this.LANGUAGE);
  }

  public setLanguage(value: string) {
    localStorage.setItem(this.LANGUAGE, value)
  }
}
