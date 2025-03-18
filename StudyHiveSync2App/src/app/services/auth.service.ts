import { HttpClient, HttpHeaders } from '@angular/common/http';                    //Angular's HTTP module for making HTTP requests.
import { Injectable } from '@angular/core';                                        //Injectable decorator to define a service.
import { Router } from '@angular/router';                                          //Router service to navigate between routes.
import { JwtHelperService } from '@auth0/angular-jwt';                             //JWT helper service to work with JWT tokens.
import { Observable } from 'rxjs';                                                 //Observable from rxjs for handling asynchronous operations.//asynchonous to perform another task without waiting for the previous task to complete.simultaniously
                                                                                                                  //RxJS (Reactive Extensions for JavaScript)-library composing of asynchronous and event-based programs using observable sequences.
@Injectable({                                                                      //This decorator marks the class as a service that can be injected into other components or services.
  providedIn: 'root'                                                               //This line specifies that the service should available application-wide
})
export class AuthService {

  private baseUrl = "https://localhost:7162/Account";
 
  constructor(private http: HttpClient, private router: Router,private jwtHelper: JwtHelperService) {}    //The constructor injects the HttpClient, Router, and JwtHelperService services into AuthService.
 
  login(login: { email: string, password: string, accountType: string }): Observable<any> {               //This method sends a POST request to the login endpoint with the login credentials.
    return this.http.post<any>(`${this.baseUrl}/login`, login, {                                          //The method returns an Observable that emits the response from the server. //${this.baseUrl} placeholder that gets replaced with the value of this.baseUrl. // (`) denote a template literal
      headers: new HttpHeaders({                                                                          //The request headers are set to specify the content type as JSON.
        "Content-Type": "application/json"
      })
    });
  }
 
  storeToken(token: string): void {                                                                       //This method stores the JWT token in the local storage.
    localStorage.setItem('jwtToken', token);                                                              
  }
 
  logout(): void {                                                                                        //This method removes the JWT token from the local storage and navigates to the login page.
    localStorage.removeItem('jwtToken');
    this.router.navigate(['/login']);
  }
 
  signup(signup: {name: string,  email: string, password: string, accountType: string}): Observable<any> {  //This method sends a POST request to the register endpoint with the signup details.
    return this.http.post<any>(`${this.baseUrl}/register`, signup, {                                        //The method returns an Observable that emits the response from the server.
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  //new
  getToken(): string | null {                                                                               //This method retrieves the JWT token from the local storage.
    return localStorage.getItem('jwtToken');                                                                //The method returns the token or null if it is not present.
  }

  getCurrentUserDetails(): any {                                                                            //This method retrieves the user details from the JWT token.
    const token = this.getToken();                                                                          //The method decodes the token using the JwtHelperService.
    if(token) {                                                                                             //If the token is present, the decoded user details are returned.
      return this.jwtHelper.decodeToken(token);
    }
    return null;                                                                                            //If the token is not present, null is returned.
  }
 
}