import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppUserDto } from '../user.model';

@Component({
  selector: 'app-modify-dialog',
  templateUrl: './modify-dialog.component.html',
  styleUrls: ['./modify-dialog.component.scss']
})
export class ModifyUserDialogComponent {
  formGroup!: FormGroup;
  selectedRow?: AppUserDto;

  constructor(
    private matDialogRef: MatDialogRef<ModifyUserDialogComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: AppUserDto
  ) {
    this.selectedRow = data;
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.formGroup = this.formBuilder.group({
      name: [this.selectedRow?.name, [Validators.required, Validators.maxLength(200)]],
      email: [this.selectedRow?.email, [Validators.required, Validators.email]],
      profession: [this.selectedRow?.profession, Validators.required],
      isAdmin: [this.selectedRow?.isAdmin],
    });
  }

  onSubmit() {
    if (this.formGroup.invalid) {
      return;
    }

    var data = new AppUserDto();
    data.appUserId = this.selectedRow!.appUserId;
    data.name = this.formGroup.controls['name'].value;
    data.email = this.formGroup.controls['email'].value;
    data.profession = this.formGroup.controls['profession'].value;
    data.isAdmin = this.formGroup.controls['isAdmin'].value;

    this.matDialogRef.close(data);
  }
}
