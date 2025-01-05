import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { catchError, map, Observable, of } from 'rxjs';
import { AppConfig } from '../common/services/app-config.service';
import { EpicCRMCookieService } from '../common/services/cookie.service';
import { LoginDto } from './login.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(
    private cookieServie: EpicCRMCookieService,
    private router: Router,
    private http: HttpClient
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    var userName = this.cookieServie.getUsername();
    if (userName) {
      this.cookieServie.setUsername(userName);
      this.router.navigate(['/home']);
      return of(true);
    } else {
      this.router.navigate(['/login']);
      return of(false);
    }
  }

  login(username: string, password: string): Observable<LoginDto | null> {
    const url = `${AppConfig.settings.epicCRM.apiBaseUrl}api/security/login`;
    return this.http.post<LoginDto>(url, {
      username,
      password
    }, httpOptions).pipe(
      map((result: LoginDto) => {
        if (result) {
          this.cookieServie.setUsername(result.UserName);
          this.router.navigate(['/home']);
        }
        return result;
      }),
      catchError((error: any, caught: Observable<LoginDto>) => {
        return of(null);
      }));
  }

  logout(): Observable<any> {
    const url = `${AppConfig.settings.epicCRM.apiBaseUrl}api/security/logout`;
    return this.http.post<boolean>(url, {}, httpOptions).pipe(
      map((result: boolean) => {
        this.cookieServie.setUsername(undefined);
        this.cookieServie.setToken(undefined);
        return result;
      }),
      catchError((error: any, caught: Observable<boolean>) => {
        this.cookieServie.setUsername(undefined);
        this.cookieServie.setToken(undefined);
        this.router.navigate(['/login']);
        return of(false);
      }));
  }
}
