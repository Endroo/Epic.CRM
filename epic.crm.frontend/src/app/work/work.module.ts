import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { WorkComponent } from '../work/work.component';
import { AddAddressDialogComponent } from './add-address-dialog/add-address-dialog.component';
import { ModifyWorkDialogComponent } from './modify-work-dialog/modify-work-dialog.component';
import { WorkCalendarComponent } from '../work-calendar/work-calendar.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';

@NgModule({
  declarations: [
    WorkComponent,
    ModifyWorkDialogComponent,
    AddAddressDialogComponent,
    WorkCalendarComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
  ],
  exports: [
    SharedModule
  ]
})
export class WorkModule { }
