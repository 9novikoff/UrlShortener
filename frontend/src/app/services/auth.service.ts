import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, firstValueFrom, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _isAuthenticated = new BehaviorSubject<boolean>(this.isAuthenticated());
  private tokenKey = 'token';
  
  constructor(private http: HttpClient, private router: Router) {}
  
  async register(email: string, password: string) {
    const registerPayload = { email: email, password: password };
    try{
      await firstValueFrom(this.http.post<any>(`${environment.apiUrl}/register`, registerPayload));
      this.router.navigate(['login']);
    }
    catch(error){
      return (error as HttpErrorResponse).error;
    }
  }

  async login(email: string, password: string) {
    const loginPayload = { email: email, password: password };
    try {
      const token = await firstValueFrom(this.http.post(`${environment.apiUrl}/login`, loginPayload, {responseType: 'text'}));
      this.saveToken(token!);
      this.router.navigate(['']);
      return '';
    } catch (error) {
      return (error as HttpErrorResponse).error;
  }
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

  getId(): string | null {
    const token = this.getToken();
    if (token) {
      const decoded = this.decodeToken(token);
      return decoded?.sub ?? null;
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


