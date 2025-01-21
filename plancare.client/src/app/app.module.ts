//import { HttpClientModule } from '@angular/common/http';
//import { NgModule } from '@angular/core';
//import { BrowserModule } from '@angular/platform-browser';
//import { AppRoutingModule } from './app-routing.module';
//import { AppComponent } from './app.component';
//import { CarSignalRService } from '../CarSignalRService';
//import { RegistrationComponent } from '../registration/registration.component';
//import { HomeComponent } from '../home/home.component';
//import { RouterModule } from '@angular/router';

//@NgModule({
//  declarations: [
//    AppComponent,
//  ],
//  imports: [
//    BrowserModule,
//    HttpClientModule,
//    AppRoutingModule,
//    HomeComponent,
//    RegistrationComponent,
//    RouterModule.forRoot([
//      { path: '', component: HomeComponent },
//      { path: 'registration', component: RegistrationComponent },
//    ])
//  ],
//  providers: [CarSignalRService],
//})
//export class AppModule { }
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }



