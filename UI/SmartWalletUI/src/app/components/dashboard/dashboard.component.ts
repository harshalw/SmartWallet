import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ReportsService, MonthlyReport } from '../../services/reports.service';
import { timeout } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  currentUser: any = null;
  monthlyReports: MonthlyReport[] = [];
  loading: boolean = false;

  constructor(
    private authService: AuthService,
    public router: Router,
    private reportsService: ReportsService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    if (!this.currentUser) {
      this.router.navigate(['/login']);
    } else {
      this.loadMonthlyReports();
    }
  }

  loadMonthlyReports(): void {
    console.log('Starting to load reports for user:', this.currentUser);
    const userId = this.currentUser?.userId || 1;
    this.fetchReports(userId);
  }

  private fetchReports(userId: number): void {
    console.log('Fetching reports for userId:', userId);
    this.loading = true;
    
    this.reportsService.getMonthlyReports(userId).pipe(
      timeout(10000) 
    ).subscribe({
      next: (data) => {
        console.log('Reports loaded successfully:', data);
        console.log('Data length:', data?.length);
        console.log('Loading before false:', this.loading);
        this.monthlyReports = data || [];
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Loading after false:', this.loading);
      },
      error: (error) => {
        console.error('Error loading reports:', error);
        console.error('Full error object:', JSON.stringify(error, null, 2));
        this.monthlyReports = [];
        this.loading = false;
        this.cdr.detectChanges();
      },
      complete: () => {
        console.log('Reports subscription completed');
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  getMonthName(month: number): string {
    const monthNames = [
      'January', 'February', 'March', 'April', 'May', 'June',
      'July', 'August', 'September', 'October', 'November', 'December'
    ];
    return monthNames[month - 1] || '';
  }
}
