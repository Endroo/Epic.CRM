import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PageResult } from '../models/result.model';

@Injectable()
export abstract class BaseService {
  constructor(
    protected http: HttpClient
  ) { }

  abstract getData<ApiResult>(pageIndex: number, pageSize: number, sortColumn: string, sortOrder: string, filter: any)
    : Observable<PageResult<any[]>>;

  protected setPageParams(params: HttpParams, pageIndex: number, pageSize: number): HttpParams {
    if (pageIndex) {
      params = params.set("pageIndex", pageIndex.toString());
    }

    if (pageSize) {
      params = params.set("pageSize", pageSize.toString());
    }

    return params;
  }

  protected setSortParams(params: HttpParams, sortColumn: string, sortOrder: string): HttpParams {
    params = params.set("sortColumn", sortColumn)
    params = params.set("sortOrder", sortOrder);

    return params;
  }

  protected setFilterParams(params: HttpParams, filter: any): HttpParams {
    if (!filter) {
      return new HttpParams();
    }

    const filterPropNames = Object.getOwnPropertyNames(filter);
    for (let filterPropName of filterPropNames) {
      let filterPropValue = filter[filterPropName];

      if (filterPropValue instanceof Date) {
        params = params.set(filterPropName, new Date(filterPropValue).toUTCString());
      }
      else if (Array.isArray(filterPropValue)) {
        if (filterPropValue.length > 0) {
          filterPropValue.forEach(arrayItem => {
            params = params.append(filterPropName, arrayItem);
          });
        }
      }
      else if (typeof filterPropValue === 'boolean') {
        params = params.set(filterPropName, filterPropValue.toString());
      }
      else if (filterPropValue) {
        params = params.set(filterPropName, filterPropValue);
      }
    }

    return params;
  }
}

export interface ApiResult<T> {
  data: T[];
  sortOrder: string;
  sortColumn: string;
  searchString: string;
  pageSize: number;
  pageIndex: number;
}
