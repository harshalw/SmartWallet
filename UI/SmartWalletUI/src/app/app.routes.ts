import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { IncomeNewComponent } from './components/income-new/income-new.component';
import { ExpenseNewComponent } from './components/expense-new/expense-new.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'income/new', component: IncomeNewComponent },
  { path: 'expense/new', component: ExpenseNewComponent },
  { path: '**', redirectTo: '/login' }
];
