import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { Address, Customer, CustomerDto } from '../../customers/customers.model';
import { CustomerService } from '../../customers/customers.service';
import { AddAddressDialogComponent } from '../add-address-dialog/add-address-dialog.component';
import { Work, WorkEditRegisterDto, WorkStatusEnum } from '../work.model';

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
  selectedDefaultAddress!: boolean;
  newAddress!: Address;

  constructor(
    private matDialogRef: MatDialogRef<ModifyWorkDialogComponent>,
    private formBuilder: FormBuilder,
    private customerService: CustomerService,
    private dialog: MatDialog,
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
      name: [this.selectedRow?.name, [Validators.required, Validators.maxLength(250)]],
      customer: [this.selectedRow?.customerId, Validators.required],
      description: [this.selectedRow?.description, Validators.required],
      address: [this.selectedRow?.addressId, Validators.required],
      workStatus: [this.selectedRow?.workStatusId, Validators.required],
      price: [this.selectedRow?.price, Validators.required],
    });
  }

  getCustomers(skipLoading: boolean) {
    if (this.selectedRow) {
      this.getCustomerById(this.selectedRow.customerId!);
    }

    this.customerService.getData(0, 0, '', '', '', skipLoading).subscribe(customersList => {
      if (customersList.data && customersList.data.length > 0) {
        this.customers = customersList.data;
      }
    });
  }

  checkDefaultAddress(checked: MatCheckboxChange) {
    this.selectedDefaultAddress = checked.checked;
    if (checked.checked) {
      this.formGroup.controls['address'].setValue(this.selectedCustomer.address.addressId);
    }
  }

  selectCustomer(selected: MatSelectChange) {
    var customerId = selected.value;
    this.getCustomerById(customerId);
  }

  getCustomerById(id: number) {
    this.customerService.get(id).subscribe(result => {
      if (result && result.data) {
        this.selectedCustomer = result.data!;
        this.address = this.selectedCustomer.address;
      }
    });
  }

  onAddAddress() {
    var addressDialogRef = this.dialog.open(AddAddressDialogComponent, {
      width: '20%',
      hasBackdrop: false,
      disableClose: true,
      autoFocus: true,
    });

    this.matDialogRef.disableClose = true;

    addressDialogRef.afterClosed().subscribe((filledData: Address) => {
      if (filledData) {
        this.newAddress = filledData;
      }
    });
  }

  onSubmit() {
    if (this.formGroup.invalid || this.formGroup.pristine) {
      return;
    }

    var data = new WorkEditRegisterDto();
    data.workId = this.selectedRow!?.workId;
    data.workDateTime = this.formGroup.controls['workDateTime'].value;
    data.name = this.formGroup.controls['name'].value;
    data.customerId = this.formGroup.controls['customer'].value;
    data.description = this.formGroup.controls['description'].value;
    data.customer = this.selectedCustomer;

    data.zipCode = this.selectedCustomer.address.zipCode!;
    data.city = this.selectedCustomer.address.city;
    data.houseAddress = this.selectedCustomer.address.houseAddress;
    data.addressId = this.selectedCustomer.address.addressId;

    if (!this.selectedDefaultAddress && this.newAddress) {
      data.zipCode = this.newAddress.zipCode!;
      data.city = this.newAddress.city;
      data.houseAddress = this.newAddress.houseAddress;
      data.addressId = this.newAddress.addressId;

      data.addressId = null;
      data.address = this.newAddress;
      data.customer.address = this.newAddress;
    } else {
      data.addressId = this.formGroup.controls['address'].value;
    }

    data.workStatusId = this.formGroup.controls['workStatus'].value;
    data.price = this.formGroup.controls['price'].value;

    this.matDialogRef.close(data);
  }
}
