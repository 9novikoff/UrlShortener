import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = '/api/users';

  constructor(private http: HttpClient) {}

  getUserData(): Observable<any> {
    return this.http.get(`${this.apiUrl}/me`); // Assuming there's an endpoint to fetch the logged-in user
  }

  updateUser(userData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/me`, userData); // Assuming PUT endpoint for updating user data
  }
}
