import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface MonthlyReport {
  userId: number;
  month: number;
  year: number;
  totalIncome: number;
  totalExpense: number;
  netBalance: number;
}

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  private apiUrl = 'http://localhost:5125/api/reports';

  constructor(private http: HttpClient) {}

  getMonthlyReports(userId: number): Observable<MonthlyReport[]> {
    return this.http.get<MonthlyReport[]>(
      `${this.apiUrl}/monthly/${userId}`
    );
  }
}

