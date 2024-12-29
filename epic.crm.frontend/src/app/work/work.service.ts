import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfig } from '../common/services/app-config.service';
import { BaseService } from '../common/services/base.service';
import { Customer } from '../customers/customers.model';
import { Result } from '../common/models/result.model';
import { WorkEditRegisterDto } from './work.model';
 
@Injectable({
  providedIn: 'root'
})
export class WorkService extends BaseService {
  constructor(
    http: HttpClient,
    private datePipe: DatePipe
  ) {
    super(http);
  }


  getData<ApiResult>(pageIndex: number, pageSize: number, sortColumn: string, sortOrder: string, filter: any, skipLoading: boolean = false)
    : Observable<ApiResult> {
    const url = AppConfig.settings.data.apiBaseUrl + 'api/customer';
    let params = new HttpParams();
    params = this.setPageParams(params, pageIndex, pageSize);
    params = this.setSortParams(params, sortColumn, sortOrder);
    params = this.setFilterParams(params, filter);
    return this.http.get<ApiResult>(url, { params, headers: { skipLoading: skipLoading.toString() } });
  }

  post(form: WorkEditRegisterDto): Observable<Result> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/customer/create";
    return this.http.post<Result>(url, form);
  }

  put(id: number): Observable<Result> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/customer";
    let params = new HttpParams();
    params = params.set('id', id);
    return this.http.put<Result>(url, { params });
  }

  delete(id: number): Observable<Result> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/customer";
    let params = new HttpParams();
    params = params.set('id', id);
    return this.http.delete<Result>(url, { params });
  }
}
