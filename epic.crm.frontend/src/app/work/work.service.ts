import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PageResult, Result } from '../common/models/result.model';
import { AppConfig } from '../common/services/app-config.service';
import { BaseService } from '../common/services/base.service';
import { WorkDto, WorkEditRegisterDto } from './work.model';

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
    : Observable<PageResult<WorkDto[]>> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + 'api/work';
    let params = new HttpParams();
    params = this.setPageParams(params, pageIndex, pageSize);
    params = this.setSortParams(params, sortColumn, sortOrder);
    params = this.setFilterParams(params, filter);
    return this.http.get<PageResult<WorkDto[]>>(url, { params, headers: { skipLoading: skipLoading.toString() } });
  }

  post(form: WorkEditRegisterDto): Observable<Result> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + "api/work/create";
    return this.http.post<Result>(url, form);
  }

  put(id: number, form: WorkEditRegisterDto): Observable<Result> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + "api/work/" + form.workId;
    return this.http.put<Result>(url, form);
  }

  delete(id: number): Observable<Result> {
    const url = AppConfig.settings.epicCRM.apiBaseUrl + "api/work/" + id.toString();
    return this.http.delete<Result>(url);
  }
}
