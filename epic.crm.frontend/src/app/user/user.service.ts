import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfig } from '../common/services/app-config.service';
import { BaseService } from '../common/services/base.service';
import { AppUserDto } from './user.model';
import { DataResult, Result } from '../common/models/result.model';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  constructor(
    http: HttpClient,
    private datePipe: DatePipe
  ) {
    super(http);
  }


  getData<ApiResult>(pageIndex: number, pageSize: number, sortColumn: string, sortOrder: string, filter: any, skipLoading: boolean = false)
    : Observable<ApiResult> {
    const url = AppConfig.settings.data.apiBaseUrl + 'api/user';
    let params = new HttpParams();
    params = this.setPageParams(params, pageIndex, pageSize);
    params = this.setSortParams(params, sortColumn, sortOrder);
    params = this.setFilterParams(params, filter);
    return this.http.get<ApiResult>(url, { params, headers: { skipLoading: skipLoading.toString() } });
  }


  getById(id: number): Observable<DataResult<AppUserDto[]>> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/user";
    let params = new HttpParams();
    params = params.set('id', id);
    return this.http.get<DataResult<AppUserDto[]>>(url, { params });
  }

  getByUserName(username: string): Observable<DataResult<AppUserDto[]>> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/user";
    let params = new HttpParams();
    params = params.set('username', username);
    return this.http.get<DataResult<AppUserDto[]>>(url, { params });
  }


  post(form: AppUserDto): Observable<Result> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/user/register";
    return this.http.post<Result>(url, form);
  }

  put(id: number): Observable<Result> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/user";
    let params = new HttpParams();
    params = params.set('id', id);
    return this.http.put<Result>(url, { params });
  }

  delete(id: number): Observable<Result> {
    const url = AppConfig.settings.data.apiBaseUrl + "api/user";
    let params = new HttpParams();
    params = params.set('id', id);
    return this.http.delete<Result>(url, { params });
  }
}
