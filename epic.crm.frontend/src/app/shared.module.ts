import { CommonModule, DatePipe } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { LanguageSelectorComponent } from './common/components/language-selector/language-selector.component';
import { AppConfig } from './common/services/app-config.service';
import { MaterialModule } from './material.module';

@NgModule({
  declarations: [
    LanguageSelectorComponent
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
    })
  ],
  exports: [
    MaterialModule,
    ReactiveFormsModule,
    TranslateModule,
    LanguageSelectorComponent
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: loadSettings,
      deps: [AppConfig],
      multi: true
    },
    DatePipe
  ]
})
export class SharedModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
export function loadSettings(appConfig: AppConfig) {
  return () => appConfig.load();
}
