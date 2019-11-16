import { ErrorHandler, OnInit, Injectable } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';


@Injectable({
    providedIn: 'root'
  })
export class AppErrorHandler implements ErrorHandler, OnInit{
    constructor(private msg: NzMessageService){}
    ngOnInit() {
    }
    handleError(error) {
        this.msg.error('Что-то пошло не так!');
        console.log(error);
    }

}