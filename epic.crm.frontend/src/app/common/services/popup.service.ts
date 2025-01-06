import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { PopupType } from '../models/popup.model';

@Injectable({
  providedIn: 'root'
})
export class PopupService {
  constructor(
    private translateService: TranslateService,
    private toastrService: ToastrService,
  ) { }

  showPopup(message: string, popupType: PopupType) {
    if (message !== null && message.length !== 0) {
      this.translateService.get(message).subscribe((result: string) => {
        if (result !== null && result.length > 0) {
          switch (popupType) {
            case PopupType.Error:
              this.toastrService.error(result);
              break;
            case PopupType.Info:
              this.toastrService.info(result);
              break;
            case PopupType.Success:
              this.toastrService.success(result);
              break;
            case PopupType.Warning:
              this.toastrService.warning(result);
              break;
          }
        }
      });
    }
  }
}
