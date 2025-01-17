import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '../common/components/base.component';
import { ConfirmationDialogComponent } from '../common/components/confirmation-dialog/confirmation-dialog.component';
import { PopupType } from '../common/models/popup.model';
import { PageResult, Result, ResultStatusEnum } from '../common/models/result.model';
import { PopupService } from '../common/services/popup.service';
import { CustomerDto, CustomerEditRegisterDto } from './customers.model';
import { CustomerService } from './customers.service';
import { ModifyCustomerDialogComponent } from './modify-customer-dialog/modify-customer-dialog.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomerComponent extends BaseComponent{
  displayedColumns: string[] = [
    'customerId',
    'Name',
    'Email',
    'AddressLiteral'
  ];

  dataSource: MatTableDataSource<CustomerDto> = new MatTableDataSource<CustomerDto>();
  selectedRowId?: number;

  selectedDataRow?: CustomerEditRegisterDto;

  constructor(
    private service: CustomerService,
    private dialog: MatDialog,
    private popupService: PopupService,
    private translateService: TranslateService
  ) {
    super();
  }
  ngOnInit(): void {
    this.getData(false);
  }

  getData(skipLoading: boolean) {
    this.service.getData(
      this.paginator!?.pageIndex,
      this.paginator!?.pageSize,
      this.sort!?.active,
      this.sort!?.direction,
      this.filter,
      skipLoading
    ).subscribe((result: PageResult<CustomerDto[]>) => {
      if (result.data) {
        this.paginator!.length = result.itemCount;
        this.paginator!.pageIndex = result.queryParams.pageIndex;
        this.paginator!.pageSize = result.queryParams.pageSize;
        this.dataSource.data = result.data;
      }
    });
  }

  selectRow(selectedRow: CustomerEditRegisterDto) {
    if (this.selectedDataRow === selectedRow) {
      this.selectedDataRow = undefined;
    }
    else {
      this.selectedDataRow = selectedRow;
    }
  }

  add() {
    const dialogRef = this.dialog.open(ModifyCustomerDialogComponent, {
      disableClose: true
    });

    dialogRef.afterClosed().subscribe((filledData: CustomerEditRegisterDto) => {
      if (filledData) {
        this.service.post(filledData).subscribe((result: Result) => {
          if (result.resultStatus === ResultStatusEnum.Success) {
            this.popupService.showPopup('common.addSuccessful', PopupType.Success);
            this.getData(false);
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
      const dialogRef = this.dialog.open(ModifyCustomerDialogComponent, {
        disableClose: true,
        data: this.selectedDataRow
      });

      dialogRef.afterClosed().subscribe((filledData: CustomerEditRegisterDto) => {
        if (filledData) {
          this.service.put(filledData.appUserId, filledData).subscribe((result: Result) => {
            if (result.resultStatus === ResultStatusEnum.Success) {
              this.popupService.showPopup('common.editSuccessful', PopupType.Success);
              this.getData(false);
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
        this.service.delete(this.selectedDataRow!.customerId).subscribe((deleteResult: Result) => {
          if (deleteResult.resultStatus == ResultStatusEnum.Success) {
            this.popupService.showPopup('common.deleteSuccessful', PopupType.Success);
            this.getData(false);
          } else {
            this.popupService.showPopup('common.deleteFailed', PopupType.Error);
          }
          this.selectedDataRow = undefined;
        });
      }
    });
  }
}
