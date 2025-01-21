import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Car } from './car.model';

@Injectable({
  providedIn: 'root'
})
export class CarSignalRService {
  private hubConnection: signalR.HubConnection;
  private carStatusSubject: BehaviorSubject<Car[]> = new BehaviorSubject<Car[]>([]);

  constructor() {
    // Set up SignalR connection to the server
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/carHub') // Update with your API URL
      .build();

    // Receive updates from the server
    this.hubConnection.on('ReceiveCarRegistrationStatusUpdate', (car: Car) => {
      this.updateCarStatus(car);
    });

    // Start the connection
    this.startConnection();
  }

  private async startConnection() {
    try {
      await this.hubConnection.start();
      console.log('SignalR connection established');
    } catch (err) {
      console.error('Error establishing SignalR connection:', err);
      setTimeout(() => this.startConnection(), 5000);
    }
  }

  private updateCarStatus(updatedCar: Car) {
    // Update car registration status in the list
    const currentCars = this.carStatusSubject.value;
    const carIndex = currentCars.findIndex(car => car.id === updatedCar.id);
    if (carIndex >= 0) {
      currentCars[carIndex] = updatedCar;
      this.carStatusSubject.next([...currentCars]);
    } else {
      this.carStatusSubject.next([...currentCars, updatedCar]);
    }
  }

  // Get live car registration status
  getCarRegistrationStatus() {
    return this.carStatusSubject.asObservable();
  }
}
