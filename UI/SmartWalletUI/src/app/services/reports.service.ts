import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  private baseUrl = 'http://localhost:5125/api/reports';

  constructor(private http: HttpClient) {}

  getMonthlyReport(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/monthly/${userId}`);
  }
}
