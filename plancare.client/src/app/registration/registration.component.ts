import { Component, OnInit } from '@angular/core';
import { CarSignalRService } from '../../CarSignalRService';
import { Car } from '../../car.model';

@Component({
  selector: 'app-registration',
  standalone: false,  
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent implements OnInit {

  public carStatuses: { carId: string, isExpired: boolean }[] = [];

  constructor(private signalRService: CarSignalRService) { }

  ngOnInit(): void {
    // Subscribe to registration status updates from the SignalR service
    this.signalRService.addRegistrationStatusListener().subscribe((status) => {
      const existingCar = this.carStatuses.find(car => car.carId === status.carId);

      if (existingCar) {
        // If car exists, update its registration status
        existingCar.isExpired = status.isExpired;
      } else {
        // If it's a new car, add it to the list
        this.carStatuses.push(status);
      }
    });
  }

}
