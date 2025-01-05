import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocalStorageService } from './common/services/local-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Epic.CRM';

  constructor(
    private translateService: TranslateService,
    private localStorageService: LocalStorageService) {
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
