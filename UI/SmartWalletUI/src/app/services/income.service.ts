import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export interface IncomeType {
  id: number;
  name: string;
}

export interface NewIncome {
  userId: number;
  typeId: number;
  amount: number;
  description?: string;
  incomeDate: string;
}

@Injectable({ providedIn: 'root' })
export class IncomeService {
  private apiUrl = 'http://localhost:5125/api';

  constructor(private http: HttpClient) {}

  getTypes(): Observable<IncomeType[]> {
    return this.http.get<any>(`${this.apiUrl}/types`).pipe(
      map(response => {
        const typesArray = Array.isArray(response) ? response : (response?.$values || []);
        return typesArray.map((item: { typeId: number; typeName: string }) => ({
          id: item.typeId,
          name: item.typeName
        }));
      })
    );
  }

  createIncome(payload: NewIncome): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/income`, payload);
  }
}
