import { CommonModule, DatePipe } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ConfirmationDialogComponent } from './common/components/confirmation-dialog/confirmation-dialog.component';
import { LanguageSelectorComponent } from './common/components/language-selector/language-selector.component';
import { CustomInterceptor } from './common/interceptors/custom.interceptor';
import { HttpErrorInterceptor } from './common/interceptors/http-error.interceptor';
import { EnumToArrayPipe } from './common/pipes/enum-to-array-pipe';
import { AppConfig } from './common/services/app-config.service';
import { MaterialModule } from './material.module';

@NgModule({
  declarations: [
    LanguageSelectorComponent,
    ConfirmationDialogComponent,
    EnumToArrayPipe
  ],
  imports: [
    MaterialModule,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    ToastrModule.forRoot({
      closeButton: true
    }),
  ],
  exports: [
    MaterialModule,
    ReactiveFormsModule,
    TranslateModule,
    LanguageSelectorComponent,
    ConfirmationDialogComponent,
    EnumToArrayPipe,
    ToastrModule,
    FormsModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: loadSettings,
      deps: [AppConfig],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CustomInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    DatePipe,
    EnumToArrayPipe,
    ToastrService
  ]
})
export class SharedModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
export function loadSettings(appConfig: AppConfig) {
  return () => appConfig.load();
}
