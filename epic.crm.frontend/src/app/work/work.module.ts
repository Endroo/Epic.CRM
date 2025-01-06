import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkComponent } from '../work/work.component';
import { SharedModule } from '../shared.module';
import { ModifyWorkDialogComponent } from './modify-work-dialog/modify-work-dialog.component';

@NgModule({
  declarations: [
    WorkComponent,
    ModifyWorkDialogComponent
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
