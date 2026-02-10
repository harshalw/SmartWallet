import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';

export interface LoginRequest {
  username: string;
  passwordHash: string;
}

export interface LoginResponse {
  message?: string;
  success?: boolean;
}

export interface RegisterRequest {
  username: string;
  email: string;
  passwordHash: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5125/api/users';
  private currentUserSubject = new BehaviorSubject<any>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    const storedUser = localStorage.getItem('currentUser');
    if (storedUser) {
      this.currentUserSubject.next(JSON.parse(storedUser));
    }
  }

  login(credentials: LoginRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, credentials)
      .pipe(
        tap((response: any) => {
          // Extract user info from response or generate a user object
          const user = response?.data || response || { 
            username: credentials.username, 
            passwordHash: credentials.passwordHash 
          };
          // Ensure userId is available
          if (!user.userId) {
            user.userId = response?.userId || 1; // Default to 1 if not in response
          }
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }),
        map(() => ({ success: true })) // Return a success object for the component
      );
  }

  register(data: RegisterRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, data)
      .pipe(
        tap(() => {
          // If we get a 200 response (even empty), consider it successful
          const user = { username: data.username, passwordHash: data.passwordHash };
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }),
        map(() => ({ success: true })) // Return a success object for the component
      );
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  isLoggedIn(): boolean {
    return this.currentUserSubject.value !== null;
  }

  getCurrentUser() {
    return this.currentUserSubject.value;
  }
}
