import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Work, WorkStatusEnum } from '../work.model';
import { Address, Customer } from '../../customers/customers.model';

@Component({
  selector: 'app-modify-work-dialog',
  templateUrl: './modify-work-dialog.component.html',
  styleUrls: ['./modify-work-dialog.component.scss']
})
export class ModifyWorkDialogComponent {
  formGroup!: FormGroup;
  selectedRow?: Work;
  WorkStatuses = WorkStatusEnum;
  customers: Customer[] = [];
  addresses: Address[] = [];
  constructor(
    private matDialogRef: MatDialogRef<ModifyWorkDialogComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: Work
  ) {
    this.selectedRow = data;
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.formGroup = this.formBuilder.group({
      workDateTime: [this.selectedRow?.workDateTime, Validators.required],
      name: [this.selectedRow?.name, [Validators.required, Validators.maxLength(200)]],
      customer: [this.selectedRow?.customerId, Validators.required],
      description: [this.selectedRow?.description, Validators.required],
      address: [this.selectedRow?.addressId, Validators.required],
      workStatus: [this.selectedRow?.workStatusId, Validators.required],
      price: [this.selectedRow?.price, Validators.required],
    });
  }

  onSubmit() {
    if (this.formGroup.invalid) {
      return;
    }

    var data = new Work();
    data.workId = this.selectedRow!.workId;
    data.name = this.formGroup.controls['name'].value;
    data.customerId = this.formGroup.controls['customer'].value;
    data.description = this.formGroup.controls['description'].value;
    data.addressId = this.formGroup.controls['address'].value;
    data.workStatusId = this.formGroup.controls['workStatus'].value;
    data.price = this.formGroup.controls['price'].value;

    this.matDialogRef.close(data);
  }
}
