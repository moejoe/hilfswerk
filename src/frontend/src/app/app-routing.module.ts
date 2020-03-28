import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './auth-guard';
import { LoggedoutComponent } from './components/loggedout/loggedout.component';
import { CreateHelferComponent } from './components/createHelfer/createHelfer.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'index',
    pathMatch: 'full'
  },
  {
    path: 'index',
    component: IndexComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'loggedout',
    component: LoggedoutComponent
  },
  {
    path: 'createHelfer',
    component: CreateHelferComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "**",
    redirectTo: 'login'
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
