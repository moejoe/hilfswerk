import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './auth-guard';
import { LoggedoutComponent } from './components/loggedout/loggedout.component';
import { CreateHelferComponent } from './components/createHelfer/createHelfer.component';
import { SearchHelferComponent } from './components/searchHelfer/searchHelfer.component';
import { EditHelferComponent } from './components/editHelfer/editHelfer.component';
import { AddEinsatzComponent } from './components/addEinsatz/addEinsatz.component';
import { EditEinsatzComponent } from './components/editEinsatz/editEinsatz.component';


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
    path: 'helfer_innen/new',
    component: CreateHelferComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'helfer_innen',
    component: SearchHelferComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'helfer_innen/:helferId',
    component: EditHelferComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'helfer_innen/:helferId/einsaetze/new',
    component: AddEinsatzComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'helfer_innen/:helferId/einsaetze/:einsatzId',
    component: EditEinsatzComponent,
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
