import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  private readonly LANGUAGE = 'language';

  constructor() { }

  public getLanguage(): string | null {
    return localStorage.getItem(this.LANGUAGE);
  }

  public setLanguage(value: string) {
    localStorage.setItem(this.LANGUAGE, value)
  }
}
