import { ErrorHandler, OnInit, Injectable } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { AppError } from './app-error';
import { BadInput } from './bad-input';
import { NotFoundError } from './not-found-error';
import { UserWarning } from './userWarning';


@Injectable({
    providedIn: 'root'
  })
export class AppErrorHandler implements ErrorHandler, OnInit{
    constructor(private msg: NzMessageService){}
    ngOnInit() {
    }
    handleError(error: AppError) {
        if(
            (error instanceof BadInput) || 
            (error instanceof NotFoundError)
        ){
            this.msg.error(error.error);
        }
        if(error instanceof UserWarning){
            this.msg.warning(error.error);
        }
        
        console.log(error);
    }

}