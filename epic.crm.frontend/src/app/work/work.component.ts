import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmationDialogComponent } from '../common/components/confirmation-dialog/confirmation-dialog.component';
import { PopupType } from '../common/models/popup.model';
import { PageResult, Result, ResultStatusEnum } from '../common/models/result.model';
import { PopupService } from '../common/services/popup.service';
import { ModifyWorkDialogComponent } from './modify-work-dialog/modify-work-dialog.component';
import { WorkDto, WorkEditRegisterDto } from './work.model';
import { WorkService } from './work.service';

@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrls: ['./work.component.scss']
})
export class WorkComponent {
  displayedColumns: string[] = [
    'WorkId',
    'WorkDateTime',
    'Name',
    'CustomerName',
    'Description',
    'AddressLiteral',
    'WorkStatus',
    'Price'
  ];

  dataSource: MatTableDataSource<WorkDto> = new MatTableDataSource<WorkDto>();
  selectedDataRow?: WorkDto;

  constructor(
    private service: WorkService,
    private popupService: PopupService,
    private translateService: TranslateService,
    private dialog: MatDialog
  ) {
  }
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getData(0, 0, '', '', null, true).subscribe((result: PageResult<WorkDto[]>) => {
      if (result.data) {
        this.dataSource.data = result.data;
      }
    });
  }

  selectRow(selectedRow: WorkDto) {
    if (this.selectedDataRow === selectedRow) {
      this.selectedDataRow = undefined;
    }
    else {
      this.selectedDataRow = selectedRow;
    }
  }

  add() {
    const dialogRef = this.dialog.open(ModifyWorkDialogComponent, {
      disableClose: true
    });

    dialogRef.afterClosed().subscribe((filledData: WorkEditRegisterDto) => {
      if (filledData) {
        this.service.post(filledData).subscribe((result: Result) => {
          if (result.resultStatus === ResultStatusEnum.Success) {
            this.popupService.showPopup('common.addSuccessful', PopupType.Success);
          } else {
            this.popupService.showPopup('common.addFailed', PopupType.Error);
          }
          this.selectedDataRow = undefined;
        });
      }
    });
  }

  edit() {
    if (this.selectedDataRow) {
      const dialogRef = this.dialog.open(ModifyWorkDialogComponent, {
        disableClose: true,
        data: this.selectedDataRow
      });

      dialogRef.afterClosed().subscribe((filledData: WorkDto) => {
        if (filledData) {
          this.service.put(filledData.workId, filledData).subscribe((result: Result) => {
            if (result.resultStatus === ResultStatusEnum.Success) {
              this.popupService.showPopup('common.editSuccessful', PopupType.Success);
            } else {
              this.popupService.showPopup('common.editFailed', PopupType.Error);
            }
            this.selectedDataRow = undefined;
          });
        }
      });
    }
  }

  delete() {
    if (!this.selectedDataRow) {
      return;
    }

    let confirmMessage: string = '';
    this.translateService.get('common.deleteQuestion', { name: this.selectedDataRow.name }).subscribe((result: string) => {
      confirmMessage = result;
    });

    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      {
        width: 'fit-content',
        data: confirmMessage,
        disableClose: true
      }
    );

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.service.delete(this.selectedDataRow!.appUserId).subscribe((deleteResult: Result) => {
          if (deleteResult.resultStatus == ResultStatusEnum.Success) {
            this.popupService.showPopup('common.deleteSuccessful', PopupType.Success);
          } else {
            this.popupService.showPopup('common.deleteFailed', PopupType.Error);
          }
          this.selectedDataRow = undefined;
        });
      }
    });
  }
}
