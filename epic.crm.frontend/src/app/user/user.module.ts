import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { ModifyUserDialogComponent } from './modify-user-dialog/modify-dialog.component';
import { UserComponent } from './user.component';

@NgModule({
  declarations: [
    UserComponent,
    ModifyUserDialogComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    SharedModule
  ]
})
export class UserModule { }
