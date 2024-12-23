import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DetailedUrl } from '../models/detailed-url.model';
import { Url } from '../models/url.model';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  private apiUrl = environment.apiUrl + '/urls';

  constructor(private http: HttpClient) {}

  getUrls(): Observable<Url[]> {
    return this.http.get<Url[]>(`${this.apiUrl}`);
  }

  getUrlById(id: string): Observable<DetailedUrl> {
    return this.http.get<DetailedUrl>(`${this.apiUrl}/${id}`);
  }

  createUrl(originalUrl: string): Observable<any> {
    return this.http.post(`${this.apiUrl}`, { originalUrl: originalUrl });
  }

  deleteUrl(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
