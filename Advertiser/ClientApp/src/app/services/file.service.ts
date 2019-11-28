import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../constants';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class FileService extends DataService{

  constructor(httpService: HttpClient) {
    super(Constants.deleteFileUrl, httpService)
   }
   
}
