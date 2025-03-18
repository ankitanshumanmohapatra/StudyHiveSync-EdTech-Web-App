import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { inject } from '@angular/core/testing';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  imports: [FormsModule, ReactiveFormsModule]
})
export class SignupComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  accountType: string = '';
  image: string = '';
  router = inject(Router);
  constructor(private authService: AuthService) { }

  onSubmit(): void {
    if (this.password !== this.confirmPassword) {
      console.log("Passwords do not match!");
      return;
    }

    const signup = { name: this.name, email: this.email, password: this.password, accountType: this.accountType, image: this.image };
    this.authService.signup(signup).subscribe({
      next: (response) => {
        console.log("User Registered ", response);
        this.router.navigate(['/login']);
      },
      error: (error) => {
        console.log("Error ", error);
      }
    });
  }
}


// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-signup',
//   imports: [],
//   templateUrl: './signup.component.html',
//   styleUrl: './signup.component.css'
// })
// export class SignupComponent {

// }
