import { Component, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ExpenseService, ExpenseType, NewExpense } from '../../services/expense.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-expense-new',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './expense-new.component.html',
  styleUrl: './expense-new.component.css'
})
export class ExpenseNewComponent implements OnInit {
  types = signal<ExpenseType[]>([]);
  form!: FormGroup;
  loading = signal(false);
  error = signal('');
  success = signal('');

  constructor(
    private fb: FormBuilder,
    private expenseService: ExpenseService,
    private authService: AuthService,
    public router: Router
  ) {
    this.form = this.fb.group({
      selectedTypeId: [null, Validators.required],
      amount: [null, Validators.required],
      expenseDate: ['', Validators.required],
      description: ['']
    });
  }

  ngOnInit(): void {
    this.fetchTypes();
    this.form.patchValue({
      expenseDate: new Date().toISOString().slice(0, 10)
    });
  }

  fetchTypes(): void {
    this.expenseService.getTypes().subscribe({
      next: (types) => {
        console.log('Expense types loaded:', types);
        this.types.set(types);
      },
      error: (err) => {
        console.error('Failed to load expense types', err);
        this.error.set('Failed to load expense types');
      }
    });
  }

  onSubmit(): void {
    this.error.set('');
    this.success.set('');

    if (!this.form.valid) {
      this.error.set('Please fill in all required fields');
      return;
    }

    const user = this.authService.getCurrentUser();
    if (!user) {
      this.error.set('You must be logged in');
      this.router.navigate(['/login']);
      return;
    }

    const formValue = this.form.value;
    const payload: NewExpense = {
      userId: Number(user?.id ?? user?.userId ?? 1),
      typeId: Number(formValue.selectedTypeId),
      amount: Number(formValue.amount),
      description: formValue.description || '',
      expenseDate: new Date(formValue.expenseDate).toISOString()
    };

    this.loading.set(true);
    this.expenseService.createExpense(payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.success.set('Expense recorded successfully');
        setTimeout(() => this.router.navigate(['/dashboard']), 900);
      },
      error: (err) => {
        this.loading.set(false);
        console.error('Create expense failed', err);
        this.error.set(err.error?.message || 'Failed to create expense');
      }
    });
  }
}
