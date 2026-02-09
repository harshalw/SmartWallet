import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export interface ExpenseType {
  id: number;
  name: string;
}

export interface NewExpense {
  userId: number;
  typeId: number;
  amount: number;
  description?: string;
  expenseDate: string;
}

@Injectable({ providedIn: 'root' })
export class ExpenseService {
  private apiUrl = 'http://localhost:5125/api';

  constructor(private http: HttpClient) {}

  getTypes(): Observable<ExpenseType[]> {
    return this.http.get<any>(`${this.apiUrl}/types/expenses`).pipe(
      map(response => {
        const typesArray = Array.isArray(response) ? response : (response?.$values || []);
        return typesArray.map((item: { typeId: number; typeName: string }) => ({
          id: item.typeId,
          name: item.typeName
        }));
      })
    );
  }

  createExpense(payload: NewExpense): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/expenses`, payload);
  }
}
