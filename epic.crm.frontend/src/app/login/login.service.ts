import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { catchError, map, Observable, of } from 'rxjs';
import { AppConfig } from '../common/services/app-config.service';
import { EpicCRMCookieService } from '../common/services/cookie.service';
import { LoggedUserDto, LoginDto } from './login.model';
import { LocalStorageService } from '../common/services/local-storage.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  static loginUser?: LoggedUserDto;

  constructor(
    private cookieServie: EpicCRMCookieService,
    private localStorageService: LocalStorageService,
    private router: Router,
    private http: HttpClient
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    this.getCurrentUser().subscribe(result => {
      LoginService.loginUser = result;
    });

    if (LoginService.loginUser) {
      return of(true);
    } else {
      this.router.navigate(['/']);
      return of(false);
    }
  }

  login(username: string, password: string): Observable<LoggedUserDto | null> {
    const url = `${AppConfig.settings.epicCRM.apiBaseUrl}api/account/login`;
    return this.http.post<LoggedUserDto>(url, {
      username,
      password
    }, httpOptions).pipe(
      map((result: LoggedUserDto) => {
        if (result) {
          LoginService.loginUser = result;
          this.router.navigate(['/works']);
        }
        return result;
      }),
      catchError((error: any, caught: Observable<LoggedUserDto>) => {
        return of(null);
      }));
  }

  getCurrentUser() {
    const url = `${AppConfig.settings.epicCRM.apiBaseUrl}api/account/getCurrentUser`;
    return this.http.get<LoggedUserDto>(url);
  }


  logout() {
    const url = `${AppConfig.settings.epicCRM.apiBaseUrl}api/account/logout`;

    this.http.post<boolean>(url, {}).subscribe(result => {
      if (result) {
        LoginService.loginUser = undefined;
        this.router.navigate(['/']);
      }
    }),
      catchError((error: any, caught: Observable<boolean>) => {
        this.router.navigate(['/']);
        return of(false);
    });
  }
}
