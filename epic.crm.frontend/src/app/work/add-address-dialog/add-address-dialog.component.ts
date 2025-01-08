import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Address } from '../../customers/customers.model';

@Component({
  selector: 'app-add-address-dialog',
  templateUrl: './add-address-dialog.component.html',
  styleUrls: ['./add-address-dialog.component.scss']
})
export class AddAddressDialogComponent {
  formGroup!: FormGroup;

  constructor(
    private addressDialogRef: MatDialogRef<AddAddressDialogComponent>,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.formGroup = this.formBuilder.group({
      zipCode: ['', Validators.required],
      city: ['', [Validators.required, Validators.maxLength(250)]],
      houseAddress: ['', [Validators.required, Validators.maxLength(250)]],
    });
  }

  onSaveAddress() {
    if (this.formGroup.invalid) {
      return;
    }

    var data = new Address();
    data.zipCode = this.formGroup.controls['zipCode'].value;
    data.city = this.formGroup.controls['city'].value;
    data.houseAddress = this.formGroup.controls['houseAddress'].value;
    this.addressDialogRef.close(data);
  }
}
