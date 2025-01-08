import { Component, Injectable, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { QueryParams } from '../models/result.model';

@Injectable({
  providedIn: 'root'
})
@Component({
  selector: '',
  template: ''
})

export abstract class BaseComponent   {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator | undefined;
  @ViewChild(MatSort, { static: false }) sort: MatSort | undefined;

  filter = new QueryParams();

  constructor() {}

  abstract getData(skipLoading: boolean) : any;

  onSearch() {
    this.paginator!.pageIndex = 0;
    this.getData(false);
  }

  onRefresh() {
    this.getData(false);
  }

  onChangeSort() {
    this.paginator!.pageIndex = 0;
    this.getData(false);
  }
}
