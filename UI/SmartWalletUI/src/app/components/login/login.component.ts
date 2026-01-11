import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username = signal('');
  passwordHash = signal('');
  loading = signal(false);
  error = signal('');
  showRegister = signal(false);

  // Register form fields
  registerUsername = signal('');
  registerEmail = signal('');
  registerPassword = signal('');
  registerError = signal('');

  constructor(private authService: AuthService, private router: Router) {}

  onLogin(): void {
    this.error.set('');
    
    if (!this.username() || !this.passwordHash()) {
      this.error.set('Please enter username and password');
      return;
    }

    this.loading.set(true);
    this.authService.login({
      username: this.username(),
      passwordHash: this.passwordHash()
    }).subscribe({
      next: (response) => {
        this.loading.set(false);
        // If we get a response (200 status), login was successful
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.loading.set(false);
        this.error.set(err.error?.message || 'An error occurred during login');
        console.error('Login error:', err);
      }
    });
  }

  onRegister(): void {
    this.registerError.set('');

    if (!this.registerUsername() || !this.registerEmail() || !this.registerPassword()) {
      this.registerError.set('Please fill in all fields');
      return;
    }

    if (!this.registerEmail().includes('@')) {
      this.registerError.set('Please enter a valid email');
      return;
    }

    this.loading.set(true);
    this.authService.register({
      username: this.registerUsername(),
      email: this.registerEmail(),
      passwordHash: this.registerPassword()
    }).subscribe({
      next: (response) => {
        this.loading.set(false);
        // If we get a response (200 status), registration was successful
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.loading.set(false);
        this.registerError.set(err.error?.message || 'An error occurred during registration');
        console.error('Registration error:', err);
      }
    });
  }

  toggleRegisterForm(): void {
    this.showRegister.update(v => !v);
    this.error.set('');
    this.registerError.set('');
  }
}
