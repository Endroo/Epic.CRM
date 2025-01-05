import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { PageResult } from '../common/models/result.model';
import { AppUserDto } from '../user/user.model';
import { WorkDto } from './work.model';
import { WorkService } from './work.service';

@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrls: ['./work.component.scss']
})
export class WorkComponent {
  displayedColumns: string[] = [
    'WorkId',
    'WorkDateTime',
    'Name',
    'CustomerName',
    'Description',
    'AddressLiteral',
    'WorkStatus',
    'Price'
  ];

  dataSource: MatTableDataSource<WorkDto> = new MatTableDataSource<WorkDto>();
  selectedRowId?: number;

  constructor(private service: WorkService) {

  }
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getData(0, 0, '', '', null, true).subscribe((result: PageResult<WorkDto[]>) => {
      if (result.data) {
        this.dataSource.data = result.data;
      }
    });
  }

  selectRow(selectedRow: AppUserDto) {
    if (this.selectedRowId === selectedRow.appUserId) {
      this.selectedRowId = undefined;
    }
    else {
      this.selectedRowId = selectedRow.appUserId;
    }
  }

  add() {

  }

  edit() {

  }

  delete() {

  }
}
