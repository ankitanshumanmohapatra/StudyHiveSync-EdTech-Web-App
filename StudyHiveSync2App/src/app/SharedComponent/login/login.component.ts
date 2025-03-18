import { Component } from '@angular/core';  //Imports the Component decorator from Angular's core library, used to define a component.
import { AuthService } from '../../services/auth.service';  //Imports the AuthService, which handles authentication logic.
import { Router } from '@angular/router';  // Imports the Router service, used for navigation between views.
import { response } from 'express';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Imports modules for handling forms in Angular.

@Component({                                     //Decorator that marks the class as an Angular component.
  selector: 'app-login',                         //Defines the custom HTML tag for this component.
  imports: [FormsModule, ReactiveFormsModule],   //Specifies the modules to be imported for this component.
  templateUrl: './login.component.html',         //Specifies the HTML template file for the component.
  styleUrl: './login.component.css'              
})
export class LoginComponent {                   //Defines the LoginComponent class.
  email: string = ""                            //Defines the email property as a string.
  password: string = ""                         //Defines the password property as a string.
  accountType: string = ""                      //Defines the accountType property as a string.

  constructor(private authService: AuthService, private router: Router) { } //Constructor that injects the AuthService and Router services. 

  onLogin(): void {                             //Method that handles the login process.
    const login = { email: this.email, password: this.password, accountType: this.accountType }  //Creates a login object with email, password, and accountType properties.
    
    this.authService.login(login).subscribe({   //Calls the login method of the AuthService and subscribes to the response.
      next: (response) => {                     //Callback function for successful response.
        const token = response.token;           //Extracts the token from the response.
        this.authService.storeToken(token);     //Stores the token in local storage.
        this.router.navigate(['/home']);        //Navigates to the home route.
        console.log("User Authenticated ", token);   //Logs the token to the console.
      },
      error: (error) => {                       //Callback function for error response.
        console.log("Error ", error);           //Logs the error to the console.
      } 
    }) 
  }
}