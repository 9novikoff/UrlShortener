import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _isAuthenticated = new BehaviorSubject<boolean>(this.isAuthenticated());
  private authUrl = 'http://localhost:5168';
  private tokenKey = 'token';
  
  constructor(private http: HttpClient, private router: Router) {}
  
  register(email: string, password: string) {
    const registerPayload = { email: email, password: password };
    return this.http.post<any>(`${this.authUrl}/register`, registerPayload).subscribe(t => t);
  }

  login(email: string, password: string) {
    const loginPayload = { email: email, password: password };

    this.http.post<string>(`${this.authUrl}/login`, loginPayload).subscribe(t => this.saveToken(t));
  }

  onLoginSuccess(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    this._isAuthenticated.next(true);
  }

  private saveToken(token: string): void {
    localStorage.setItem('token', token);
  }


  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this._isAuthenticated.next(false);
    this.router.navigate(['/login']); 
  }


  isAuthenticated(): boolean {
    const token = this.getToken();
    return token != null && !this.isTokenExpired(token);
  }


  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getEmail(): string | null {
    const token = this.getToken();
    if (token) {
      const decoded = this.decodeToken(token);
      return decoded?.email ?? null;
    }
    return null;
  }

  getRole(): string | null {
    const token = this.getToken();
    if (token) {
      const decoded = this.decodeToken(token);
      return decoded?.role ?? null;
    }
    return null;
  }

  private isTokenExpired(token: string): boolean {
    try {
      const decoded = this.decodeToken(token);
      if (!decoded.exp) {
        return false;
      }
      const expirationDate = new Date(decoded.exp * 1000);
      return expirationDate < new Date();
    } catch (e) {
      return true; 
    }
  }

  private decodeToken(token: string): any {
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  get isAuthenticated$(): Observable<boolean> {
    return this._isAuthenticated.asObservable();
  }
}
