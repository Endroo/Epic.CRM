import { Component } from '@angular/core';
import { CustomerService } from './customers.service';
import { MatTableDataSource } from '@angular/material/table';
import { PageResult } from '../common/models/result.model';
import { CustomerDto } from './customers.model';


@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomerComponent {
  displayedColumns: string[] = [
    'Name',
    'Email', 
    'AddressLiteral'
  ];

  dataSource: MatTableDataSource<CustomerDto> = new MatTableDataSource<CustomerDto>();
  selectedRowId?: number;


  constructor(private service: CustomerService) {

  }
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getData(0, 0, '', '', null, true).subscribe((result: PageResult<CustomerDto[]>) => {
      if (result.data) {
        this.dataSource.data = result.data;
      }
    });
  }

  selectRow(selectedRow: CustomerDto) {
    if (this.selectedRowId === selectedRow.customerId) {
      this.selectedRowId = undefined;
    }
    else {
      this.selectedRowId = selectedRow.customerId;
    }
  }

  add() {

  }

  edit() {

  }

  delete() {

  }
}
