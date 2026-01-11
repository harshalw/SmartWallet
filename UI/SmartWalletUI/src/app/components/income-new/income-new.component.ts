import { Component, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IncomeService, IncomeType, NewIncome } from '../../services/income.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-income-new',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './income-new.component.html',
  styleUrl: './income-new.component.css'
})
export class IncomeNewComponent implements OnInit {
  types = signal<IncomeType[]>([]);
  form!: FormGroup;
  loading = signal(false);
  error = signal('');
  success = signal('');

  constructor(
    private fb: FormBuilder,
    private incomeService: IncomeService,
    private authService: AuthService,
    public router: Router
  ) {
    this.form = this.fb.group({
      selectedTypeId: [null, Validators.required],
      amount: [null, Validators.required],
      incomeDate: ['', Validators.required],
      description: ['']
    });
  }

  ngOnInit(): void {
    this.fetchTypes();
    this.form.patchValue({
      incomeDate: new Date().toISOString().slice(0, 10)
    });
  }

  fetchTypes(): void {
    this.incomeService.getTypes().subscribe({
      next: (types) => {
        console.log('Types loaded:', types);
        this.types.set(types);
      },
      error: (err) => {
        console.error('Failed to load types', err);
        this.error.set('Failed to load income types');
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
    const payload: NewIncome = {
      userId: Number(user?.id ?? user?.userId ?? 1),
      typeId: Number(formValue.selectedTypeId),
      amount: Number(formValue.amount),
      description: formValue.description || '',
      incomeDate: new Date(formValue.incomeDate).toISOString()
    };

    this.loading.set(true);
    this.incomeService.createIncome(payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.success.set('Income recorded successfully');
        setTimeout(() => this.router.navigate(['/dashboard']), 900);
      },
      error: (err) => {
        this.loading.set(false);
        console.error('Create income failed', err);
        this.error.set(err.error?.message || 'Failed to create income');
      }
    });
  }
}
