import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { WorkComponent } from './work/work.component';
import { CustomerComponent } from './customers/customers.component';

export const ROUTES: Routes = [
  { path: '', component: LoginComponent },
  { path: 'users', component: UserComponent },
  { path: 'works', component: WorkComponent },
  { path: 'customers', component: CustomerComponent }
]

@NgModule({
  imports: [RouterModule.forRoot(ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
