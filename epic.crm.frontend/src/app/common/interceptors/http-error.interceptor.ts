import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BadRequestError, BadRequestErrorParameters } from '../models/bad-request-error.model';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(
    private toastrService: ToastrService,
    private translateService: TranslateService,
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(error => {
      if (error.status === 400) {
        this.processValidationError(error);
      } else if (error.status === 401) {
        this.processValidationError(error);
      }
      else {
        this.translateService.get(error.error).subscribe((result: string) => {
          this.toastrService.error(result);
        });
      }
      return throwError(error);
    }));
  }

  processValidationError(error: any): void {
    const badRequestError: BadRequestError = error as BadRequestError;

    if (badRequestError) {
      let translateParameters: any = {};
      if (badRequestError.error && badRequestError.error.length > 0) {
        translateParameters = badRequestError.error;
      }

      this.translateService
        .get(translateParameters)
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
