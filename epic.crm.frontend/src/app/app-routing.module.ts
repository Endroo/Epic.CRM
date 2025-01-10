import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { WorkComponent } from './work/work.component';
import { CustomerComponent } from './customers/customers.component';
import { UserComponent } from './user/user.component';
import { LoginService } from './login/login.service';
import { WorkCalendarComponent } from './work-calendar/work-calendar.component';

export const ROUTES: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UserComponent, canActivate: [LoginService] },
  { path: 'works', component: WorkComponent, canActivate: [LoginService] },
  { path: 'works/calendar', component: WorkCalendarComponent, canActivate: [LoginService] },
  { path: 'customers', component: CustomerComponent, canActivate: [LoginService] }
]

@NgModule({
  imports: [RouterModule.forRoot(ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
