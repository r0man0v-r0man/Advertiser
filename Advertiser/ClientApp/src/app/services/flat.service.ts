import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class FlatService extends DataService {
  
  constructor(httpService: HttpClient) {
    super(Constants.getAllFlats, httpService)
   }
}
