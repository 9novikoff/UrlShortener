import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ShortUrlTableComponent } from './short-url-table/short-url-table.component';
import { ShortUrlInfoComponent } from './short-url-info/short-url-info.component';
import { AboutComponent } from './about/about.component';
import { RegisterComponent } from './register/register.component';
import { ShortUrlComponent } from './short-url/short-url.component';
import { AuthGuard } from './auth-guard';
import { InvalidUrlComponent } from './invalid-url/invalid-url.component';

export const routes: Routes = [
    { path: '', component: ShortUrlTableComponent, canActivate: [AuthGuard] },
    { path: 'short-urls', component: ShortUrlTableComponent },
    { path: 'short-url-info/:id', component: ShortUrlInfoComponent, canActivate: [AuthGuard] },
    { path: 'about', component: AboutComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'invalid-url', component: InvalidUrlComponent },
    { path: '**', component: InvalidUrlComponent },
];
