import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { WorkComponent } from '../work/work.component';
import { AddAddressDialogComponent } from './add-address-dialog/add-address-dialog.component';
import { ModifyWorkDialogComponent } from './modify-work-dialog/modify-work-dialog.component';

@NgModule({
  declarations: [
    WorkComponent,
    ModifyWorkDialogComponent,
    AddAddressDialogComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    SharedModule
  ]
})
export class WorkModule { }
