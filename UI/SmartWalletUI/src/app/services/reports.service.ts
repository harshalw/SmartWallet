import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ReportSummary {
  category: string;
  totalAmount: number;
}

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private baseUrl = 'http://localhost:5125/api/reports';

  constructor(private http: HttpClient) {}

  getIncomeReport(userId: number): Observable<ReportSummary[]> {
    return this.http.get<ReportSummary[]>(`${this.baseUrl}/income/${userId}`);
  }

  getExpenseReport(userId: number): Observable<ReportSummary[]> {
    return this.http.get<ReportSummary[]>(`${this.baseUrl}/expense/${userId}`);
  }
}