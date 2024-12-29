import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkComponent } from '../work/work.component';
import { SharedModule } from '../shared.module';



@NgModule({
  declarations: [
    WorkComponent
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
