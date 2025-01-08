import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class EpicCRMCookieService {
  setToken(undefined: undefined) {
    throw new Error('Method not implemented.');
  }
  private readonly USERNAME = 'userName';
  private readonly TOKEN = 'USER_TOKEN';

  constructor(private cookieService: CookieService) { }

  public getToken() {
    return this.cookieService.get(this.TOKEN);
  }

  public getUsername(): string {
    return this.cookieService.get(this.USERNAME);
  }

  public setUsername(value?: string) {
    if (value) {
      this.cookieService.set(this.USERNAME, value);
    } else {
      this.cookieService.delete(this.USERNAME);
    }
  }
}
