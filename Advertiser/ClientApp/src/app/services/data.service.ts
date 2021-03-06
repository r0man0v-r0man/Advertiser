import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators'
import { throwError, Observable } from 'rxjs';
import { AppError } from '../app-errors/app-error';
import { NotFoundError } from '../app-errors/not-found-error';
import { BadInput } from '../app-errors/bad-input';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  headers = new HttpHeaders().set('content-type', 'application/json'); 
  constructor(
    private url: string, 
    private httpService: HttpClient) { }
  getAll(){
    return this.httpService.get<any[]>(this.url)
      .pipe(
        catchError(this.handleError)
      )
  }
  create(url: string, resourse){
    return this.httpService.post<any>(url, resourse, {headers: this.headers})
      .pipe(
        catchError(this.handleError)
      )
  }
  delete(id){
    return this.httpService.delete(this.url + '/' + id)
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
