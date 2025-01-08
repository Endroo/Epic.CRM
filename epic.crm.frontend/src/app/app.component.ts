import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { EpicCRMCookieService } from './common/services/cookie.service';
import { LocalStorageService } from './common/services/local-storage.service';
import { LoginService } from './login/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Epic.CRM';

  constructor(
    private translateService: TranslateService,
    private localStorageService: LocalStorageService,
    private accountService: LoginService) {
    this.setLanguage();
  }

  setLanguage() {
    const userLanguage = this.localStorageService.getLanguage();
    if (userLanguage) {
      this.translateService.setDefaultLang(userLanguage);
      this.translateService.use(userLanguage);
    }
    else {
      this.localStorageService.setLanguage('en');
      this.translateService.setDefaultLang('en');
    }
  }
}
