import { Injectable } from '@angular/core';                                      //module to make service injectable
import { HttpClient, HttpHeaders } from '@angular/common/http';                  //module to make http requests
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'                                                             //'root' syntax makes the service available application-wide.
})
export class ReviewService {
  private apiUrl = 'https://localhost:7162/api/RatingAndReviews';                // url for api endpoint

  constructor(private http: HttpClient) { }                                      //constructor to inject HttpClient service into reviewservice

  getReviews(): Observable<any[]> {
    // const headers = new HttpHeaders({
    //   'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` || ''
    // });
    return this.http.get<any[]>(this.apiUrl);                                  //GET request to the end point to retrieve all reviews
  }

  getReview(id: number): Observable<any> {                                     //GET request to the end point to retrieve a review by id
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` || ''
    });
    return this.http.get<any>(`${this.apiUrl}/${id}`, { headers });
  }

  addReview(review: any): Observable<any> {                                    //POSt request to the end point to add a review
    // const headers = new HttpHeaders({
    //   'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` || ''
    // });
    return this.http.post<any>(this.apiUrl, review);            
  }

  updateReview(id: number, review: any): Observable<any> {                     //PUT request to the end point to update a review
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` || ''
    });
    return this.http.put<any>(`${this.apiUrl}/${id}`, review, { headers });
  }

  deleteReview(id: number): Observable<any> {                                  //DELETE request to the end point to delete a review
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` || ''
    });
    return this.http.delete<any>(`${this.apiUrl}/${id}`, { headers });
  }
}
