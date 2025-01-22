import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Car } from '../../car.model';
import { CarSignalRService } from '../../CarSignalRService';

@Component({
  selector: 'app-car',
  standalone: false,
  
  templateUrl: './car.component.html',
  styleUrl: './car.component.css'
})
export class CarComponent implements OnInit {
  public cars: Car[] = [];
  public filteredMake: string = ''; // Optional make filter

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute, private carSignalRService: CarSignalRService) { }

  ngOnInit() {
    // On initialization, get the query parameter 'make' if it exists
    this.activatedRoute.queryParams.subscribe(params => {
      this.filteredMake = params['make'] || ''; // Default to empty string if 'make' is not found
      this.getCars(); // Load cars based on the filtered make
    });
  }

  getCars() {

    let params = new HttpParams();

    // If 'make' is provided, add it as a query parameter
    if (this.filteredMake) {
      params = params.set('make', this.filteredMake);
    }

    this.http.get<Car[]>('/car', { params }).subscribe(
      (result: Car[]) => {
        this.cars = result;
      },
      (error: any) => {
        console.error(error);
      }
    );
  }

  title = 'plancare.client';
}

