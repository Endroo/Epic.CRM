import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataResult, PageResult, Result } from '../common/models/result.model';
import { AppConfig } from '../common/services/app-config.service';
import { BaseService } from '../common/services/base.service';
import { CustomerDto, CustomerEditRegisterDto } from './customers.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService extends BaseService {
  constructor(
    http: HttpClient,
    private datePipe: DatePipe
  ) {
    super(http);
  }

  getData<ApiResult>(pageIndex: number, pageSize: number, sortColumn: string, sortOrder: string, filter: any, skipLoading: boolean = false)
    : Observable<PageResult<CustomerDto[]>> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + 'api/customer';
    let params = new HttpParams();
    params = this.setPageParams(params, pageIndex, pageSize);
    params = this.setSortParams(params, sortColumn, sortOrder);
    params = this.setFilterParams(params, filter);
    return this.http.get<PageResult<CustomerDto[]>>(url, { params, headers: { skipLoading: skipLoading.toString() } });
  }

  get(id: number): Observable<DataResult<CustomerDto>> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + "api/customer/" + id.toString();
    return this.http.get<DataResult<CustomerDto>>(url);
  }


  post(form: CustomerEditRegisterDto): Observable<Result> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + "api/customer/create";
    return this.http.post<Result>(url, form);
  }

  put(id: number, form: CustomerEditRegisterDto): Observable<Result> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + "api/customer" + form.customerId;
    return this.http.put<Result>(url, form);
  }

  delete(id: number): Observable<Result> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + "api/customer/" + id.toString();
    return this.http.delete<Result>(url);
  }
}
