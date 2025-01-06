import { CommonModule, DatePipe } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { LanguageSelectorComponent } from './common/components/language-selector/language-selector.component';
import { AppConfig } from './common/services/app-config.service';
import { MaterialModule } from './material.module';
import { ConfirmationDialogComponent } from './common/components/confirmation-dialog/confirmation-dialog.component';
import { EnumToArrayPipe } from './common/pipes/enum-to-array-pipe';
import { ToastrModule, ToastrService } from 'ngx-toastr';

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
    ToastrModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: loadSettings,
      deps: [AppConfig],
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
