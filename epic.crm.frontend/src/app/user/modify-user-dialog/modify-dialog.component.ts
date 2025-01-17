import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppUserRegisterDto } from '../user.model';

@Component({
  selector: 'app-modify-dialog',
  templateUrl: './modify-dialog.component.html',
  styleUrls: ['./modify-dialog.component.scss']
})
export class ModifyUserDialogComponent {
  formGroup!: FormGroup;
  selectedRow?: AppUserRegisterDto;

  constructor(
    private matDialogRef: MatDialogRef<ModifyUserDialogComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: AppUserRegisterDto
  ) {
    this.selectedRow = data;
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.formGroup = this.formBuilder.group({
      name: [this.selectedRow?.name, [Validators.required, Validators.maxLength(250)]],
      password: [this.selectedRow?.password, Validators.required],
      email: [this.selectedRow?.email, [Validators.required, Validators.email, Validators.maxLength(250)]],
      profession: [this.selectedRow?.profession, [Validators.required, Validators.maxLength(50)]],
      isAdmin: [this.selectedRow?.isAdmin],
    });

    //felhasznalo szerkesztesekor nem lehet jelszot modositani.
    if (this.selectedRow) {
      this.formGroup.controls['password'].clearValidators();
    }
  }

  onSubmit() {
    if (this.formGroup.invalid) {
      return;
    }

    var data = new AppUserRegisterDto();
    data.appUserId = this.selectedRow!?.appUserId;
    data.name = this.formGroup.controls['name'].value;
    data.email = this.formGroup.controls['email'].value;
    data.profession = this.formGroup.controls['profession'].value;
    data.isAdmin = this.formGroup.controls['isAdmin'].value ?? false;
    data.password = this.formGroup.controls['password'].value;

    this.matDialogRef.close(data);
  }
}
