import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiDummyService {

  private readonly apiUrl = 'https://svct.cartasur.com.ar/api/dummy';

  constructor(private http: HttpClient) {}

  getDummyData(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}