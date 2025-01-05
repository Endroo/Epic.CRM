import { Component, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocalStorageService } from '../../services/local-storage.service';

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.css']
})
export class LanguageSelectorComponent implements AfterViewChecked {
  selectedLanguage!: string | null;

  constructor(
    private translateService: TranslateService,
    private changeDetectorRef: ChangeDetectorRef,
    private localStorageService: LocalStorageService) {
    this.selectedLanguage = localStorageService.getLanguage();
  }

  ngAfterViewChecked() {
    this.changeDetectorRef.detectChanges();
  }

  useLanguage(language: string) {
    this.localStorageService.setLanguage(language);
    this.translateService.use(language);
    this.selectedLanguage = language;
  }
}
