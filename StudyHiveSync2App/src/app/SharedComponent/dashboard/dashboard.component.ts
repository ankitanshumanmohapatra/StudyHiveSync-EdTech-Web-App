import { Component } from '@angular/core';
import { NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
// import { User } from '../../models/user.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class DashboardComponent {
  // user = {
  //   firstName: 'Ankit',
  //   lastName: 'Mohapatra',
  //   email: 'ankitanshumanm@gmail.com',
  //   phone: '+91-6372709525',
  //   dob: '16-09-2023',
  //   about: 'I am a result-oriented C# Programmer.'
  // };

  constructor(private auth: AuthService, private http: HttpClient) { }
  isEditing = false;

  ngOnInit() {
    this.getCurrentUser();
    this.getCurrentUserDetails();
  }

  editProfile() {
    this.isEditing = true;
  }

  // saveProfile() {
  //   this.isEditing = false;
  //   alert('Profile updated successfully!');
  // }

  deleteAccount() {
    if (confirm('Are you sure you want to delete your account? This action cannot be undone.')) {
      const url = 'https://localhost:7162/api/Users';
      this.http.delete(`${url}/${this.userId}`).subscribe({
        next: (response) => {
          console.log('Account deleted successfully');
          this.auth.logout();
        },
        error: (err) => {
          console.error('Error deleting account:', err);
        }
      });
    }
  }

  //new
  userId: number = 0;
  user: any = null;

  editName: string = "";
  editEmail: string = "";

  getCurrentUser(): void {
    this.userId = Number(this.auth.getCurrentUserDetails().nameid);
    // console.log(this.userId);

    // console.log(this.auth.getCurrentUserDetails().nameid);
  }

  getCurrentUserDetails(): void {
    const url = 'https://localhost:7162/api/Users/GetUserById'
    this.http.get(`${url}/${this.userId}`).subscribe({
      next: (data) => {
        
        this.user = data;
        this.editName = this.user?.name;
        this.editEmail = this.user?.email;
        console.log('User details:', this.user); // Log user details (optional)
      },
      error: (err) => {
        console.error('Error fetching user details:', err); // Handle errors
      }
    });
  }

  saveProfile() {
    const updateUser = {
      userId: this.user?.userId,
      name: this.editName,
      email: this.editEmail
    }

    console.log(updateUser);

    const url = 'https://localhost:7162/api/Users/UpdateUserById';
    this.http.put(`${url}/${this.userId}`, updateUser).subscribe({
      next: () => {
        this.getCurrentUserDetails();
        this.isEditing = false;
        alert('Profile updated successfully!');
      },
      error: (err) => {
        console.error('Error updating user:', err);
      }
    });
  }
}





// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-dashboard',
//   imports: [],
//   templateUrl: './dashboard.component.html',
//   styleUrl: './dashboard.component.css'
// })
// export class DashboardComponent {

// }
