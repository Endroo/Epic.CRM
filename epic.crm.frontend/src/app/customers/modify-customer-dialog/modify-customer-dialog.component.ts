import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CustomerEditRegisterDto } from '../customers.model';

@Component({
  selector: 'app-modify-customer-dialog',
  templateUrl: './modify-customer-dialog.component.html',
  styleUrls: ['./modify-customer-dialog.component.scss']
})
export class ModifyCustomerDialogComponent {
  formGroup!: FormGroup;
  selectedRow?: CustomerEditRegisterDto;

  constructor(
    private matDialogRef: MatDialogRef<ModifyCustomerDialogComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: CustomerEditRegisterDto
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
      zipCode: [this.selectedRow?.zipCode, Validators.required],
      city: [this.selectedRow?.city, Validators.required],
      houseAddress: [this.selectedRow?.houseAddress, Validators.required],
    });
  }

  onSubmit() {
    if (this.formGroup.invalid) {
      return;
    }

    var data = new CustomerEditRegisterDto();
    data.customerId = this.selectedRow!?.customerId;
    data.name = this.formGroup.controls['name'].value;
    data.email = this.formGroup.controls['email'].value;
    data.zipCode = this.formGroup.controls['zipCode'].value;
    data.city = this.formGroup.controls['city'].value;
    data.houseAddress = this.formGroup.controls['houseAddress'].value;

    this.matDialogRef.close(data);
  }
}
