import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAppConfig } from '../models/app-config.model';

@Injectable({
  providedIn: 'root'
})
export class AppConfig {
  private http: HttpClient;
  static settings: IAppConfig;

  constructor(
    httpBackend: HttpBackend
  ) {
    this.http = new HttpClient(httpBackend);
  }

  load() {
    const jsonFile = 'assets/config.json'

    return new Promise<void>((resolve, reject) => {
      this.http.get(jsonFile, { withCredentials: true }).toPromise().then((response) => {
        AppConfig.settings = <IAppConfig>response;
        resolve();
      }).catch((response: any) => {
        reject(`Could not load file '${jsonFile}': ${JSON.stringify(response)}`);
      });
    });
  }
}
