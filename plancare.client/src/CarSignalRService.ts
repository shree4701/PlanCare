import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarSignalRService {
  private hubConnection: HubConnection;
  private carRegistrationStatusSubject: Subject<{ carId: string, isExpired: boolean }> = new Subject();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:64508/carRegistrationHub')  // Adjust the URL to your backend SignalR hub URL
      .build();

    // Start the connection and set up the listeners
    this.startConnection();

  }

  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR connection established.');
      })
      .catch(err => console.error('Error starting SignalR connection: ', err));

    // Listen for registration status updates from the server
    this.hubConnection.on('ReceiveCarRegistrationStatus', (carId: string, isExpired: boolean) => {
      this.carRegistrationStatusSubject.next({ carId, isExpired });
    });
  }

  // Method to add a listener for the registration status updates
  public addRegistrationStatusListener(): Observable<{ carId: string, isExpired: boolean }> {
    return this.carRegistrationStatusSubject.asObservable();
  }
}

