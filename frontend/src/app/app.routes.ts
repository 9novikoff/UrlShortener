import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ShortUrlTableComponent } from './short-url-table/short-url-table.component';
import { ShortUrlInfoComponent } from './short-url-info/short-url-info.component';
import { AboutComponent } from './about/about.component';
import { RegisterComponent } from './register/register.component';
import { ShortUrlComponent } from './short-url/short-url.component';

export const routes: Routes = [
    { path: '', component: ShortUrlComponent },
    { path: 'short-urls', component: ShortUrlTableComponent },
    { path: 'short-url-info/:id', component: ShortUrlInfoComponent },
    { path: 'about', component: AboutComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
];
