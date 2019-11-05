import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FlatService extends DataService {
  
  constructor(httpService: HttpClient) {
    super('https://localhost:44332/api/flat/getAllFlats', httpService)
   }
}
