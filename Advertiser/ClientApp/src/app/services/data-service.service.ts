import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {catchError} from 'rxjs/operators'
import { throwError } from 'rxjs';
import { AppError } from '../app-errors/app-error';
import { NotFoundError } from '../app-errors/not-found-error';
import { BadInput } from '../app-errors/bad-input';

@Injectable({
  providedIn: 'root'
})
export class DataServiceService {
private url;
  constructor(private httpsService: HttpClient) { }
  getAll(){
    return this.httpsService.get(this.url)
      .pipe(
        catchError(this.handleError)
      )
  }
  private handleError(error: Response){
    if(error.status === 400)
    return throwError(new BadInput(error.json()));
  
    if(error.status === 404)
    return throwError(new NotFoundError());
  
  return throwError(new AppError(error));
  }
}
