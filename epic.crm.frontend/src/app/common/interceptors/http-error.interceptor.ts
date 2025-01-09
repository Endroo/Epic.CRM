import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { BadRequestError, BadRequestErrorParameters } from '../models/bad-request-error.model';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(
    private toastrService: ToastrService,
    private translateService: TranslateService,
    private router: Router
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(error => {
      if (error.error instanceof ErrorEvent) {
        this.translateService.get('error.clientSide').subscribe((result: string) => {
          this.toastrService.error(result);
        });
      }
      else {
        // Bad Request
        if (error.status === 400) {
          this.processValidationError(error);
        }
        // Unauthorized
        else if (error.status === 401) {
          this.translateService.get('error.unauthorized').subscribe((result: string) => {
            this.toastrService.error(result);
          });
          this.router.navigate(['/']);
        }
        // Forbidden
        else if (error.status === 403) {
          this.router.navigate(['/']);
        }
        // Not Found
        else if (error.status === 404) {
          this.translateService.get('error.notFound').subscribe((result: string) => {
            this.toastrService.error(result);
          });
        }
        // Internal Server Error
        else if (error.status === 500) {
          this.translateService.get('error.internalServerError').subscribe((result: string) => {
            this.toastrService.error(result);
          });
        }
        // Service Unavailable
        else if (error.status === 503) {
          this.translateService.get('error.serviceUnavailable').subscribe((result: string) => {
            this.toastrService.error(result);
          });
        }
        else {
          this.translateService.get('error.unspecified').subscribe((result: string) => {
            this.toastrService.error(result);
          });
        }
      }

      return throwError(error);
    }));
  }

  processValidationError(error: any): void {
    const badRequestError: BadRequestError = error.error as BadRequestError;

    if (badRequestError.errorLabel) {
      let translateParameters: any = {};
      if (badRequestError.error && badRequestError.error.length > 0) {
        badRequestError.error.forEach((element: BadRequestErrorParameters) => {
          translateParameters[element.key] = element.value;
        });
      }

      this.translateService
        .get(badRequestError.errorLabel, translateParameters)
        .subscribe((result: string) => {
    
          this.toastrService.error(result);
        });
    } else {
      this.translateService
        .get('error.badRequest')
        .subscribe((result: string) => {
          this.toastrService.error(result);
        });
    }
  }
}
