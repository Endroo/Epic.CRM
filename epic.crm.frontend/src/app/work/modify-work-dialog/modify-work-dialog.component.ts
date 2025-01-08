import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Work, WorkStatusEnum } from '../work.model';
import { Address, Customer, CustomerDto } from '../../customers/customers.model';
import { CustomerService } from '../../customers/customers.service';
import { MatSelectChange } from '@angular/material/select';
import { MatCheckboxChange } from '@angular/material/checkbox';

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
  address!: Address;
  selectedCustomer!: CustomerDto;

  constructor(
    private matDialogRef: MatDialogRef<ModifyWorkDialogComponent>,
    private formBuilder: FormBuilder,
    private customerService: CustomerService,
    @Inject(MAT_DIALOG_DATA) public data: Work
  ) {
    this.selectedRow = data;
  }

  ngOnInit(): void {
    this.getCustomers(false);
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

  getCustomers(skipLoading: boolean) {
    this.customerService.getData(0, 0, '', '', '', skipLoading).subscribe(customersList => {
      if (customersList.data && customersList.data.length > 0) {
        this.customers = customersList.data;

      }
    });
  }

  checkDefaultAddress(checked: MatCheckboxChange) {
    if (checked.checked) {
      this.formGroup.controls['address'].setValue(this.selectedCustomer.address.addressId);
    }
  }

  selectCustomer(selected: MatSelectChange) {
    this.selectedCustomer = selected.value;
    this.address = this.selectedCustomer.address;
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
