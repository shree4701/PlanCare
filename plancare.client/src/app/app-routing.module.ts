import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './registration/registration.component';
import { CarComponent } from './car/car.component';

const routes: Routes = [
  { path: '', component: CarComponent },
  { path: 'registration', component: RegistrationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
