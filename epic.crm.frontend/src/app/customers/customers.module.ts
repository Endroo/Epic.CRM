import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CustomerComponent } from '../customers/customers.component';
import { SharedModule } from '../shared.module';
import { ModifyCustomerDialogComponent } from './modify-customer-dialog/modify-customer-dialog.component';

@NgModule({
  declarations: [
    CustomerComponent,
    ModifyCustomerDialogComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    SharedModule
  ]
})
export class CustomerModule { }
