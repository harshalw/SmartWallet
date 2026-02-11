import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
<<<<<<< HEAD

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
=======
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

export interface MonthlyReport {
  userId: number;
  year: number;
  month: number;
  totalIncome: number;
  totalExpense: number;
  netBalance: number;
}

@Injectable({ providedIn: 'root' })
export class ReportsService {
  private apiUrl = 'http://localhost:5125/api/reports';

  constructor(private http: HttpClient) {}

  getMonthlyReports(userId: number): Observable<MonthlyReport[]> {
    console.log(`Fetching reports for userId: ${userId}`);
    return this.http.get<any>(`${this.apiUrl}/monthly/${userId}`).pipe(
      map(response => {
        console.log('API Response:', response);
        let reportsArray = Array.isArray(response) ? response : (response?.$values || response?.data || []);
        console.log('Parsed reports:', reportsArray);
        return reportsArray;
      }),
      catchError(error => {
        console.error('HTTP Error fetching reports:', error);
        return of([]); // Return empty array on error
      })
    );
  }
}
>>>>>>> master
