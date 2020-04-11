import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { IndexComponent } from './components/index/index.component';
import { AuthGuard } from './auth-guard';
import { LoggedoutComponent } from './components/loggedout/loggedout.component';
import { CreateHelferComponent } from './components/createHelfer/createHelfer.component';
import { SearchHelferComponent } from './components/searchHelfer/searchHelfer.component';
import { EditHelferComponent } from './components/editHelfer/editHelfer.component';
import { MatTableModule } from "@angular/material/table";
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddEinsatzComponent } from './components/addEinsatz/addEinsatz.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule } from '@angular/material/card';
import { HelferDetailComponent } from './components/helfer-detail/helfer-detail.component';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { registerLocaleData } from '@angular/common';
import localeDeAT from '@angular/common/locales/de-AT';
import { DateFnsDateAdapter, MAT_DATE_FNS_DATE_FORMATS } from './date-fns-date-adapter';

registerLocaleData(localeDeAT);


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    IndexComponent,
    LoggedoutComponent,
    CreateHelferComponent,
    AddEinsatzComponent,
    SearchHelferComponent,
    EditHelferComponent,
    HelferDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatListModule,
    MatCardModule,
    MatDatepickerModule
  ],
  providers: [
    AuthGuard,
    {
      provide: DateAdapter,
      useClass: DateFnsDateAdapter
    },
    {
      provide: MAT_DATE_FORMATS,
      useValue: MAT_DATE_FNS_DATE_FORMATS
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }