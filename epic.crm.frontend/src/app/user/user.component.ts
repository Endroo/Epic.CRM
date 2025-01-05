import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AppUserDto } from './user.model';
import { UserService } from './user.service';
import { PageResult } from '../common/models/result.model';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  displayedColumns: string[] = [
    'AppUserId',
    'Name',
    'Email',
    'Profession',
    'WorkCount',
    'CustomerCount'
  ];

  dataSource: MatTableDataSource<AppUserDto> = new MatTableDataSource<AppUserDto>();
  selectedRowId?: number;

  constructor(private service: UserService) {

  }
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getData(0, 0, '', '', null, true).subscribe((result: PageResult<AppUserDto[]>) => {
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
