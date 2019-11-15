import { ErrorHandler, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';

export class AppErrorHandler implements ErrorHandler, OnInit{
    constructor(private msg: NzMessageService){}
    ngOnInit() {
    }
    handleError(error) {
        this.msg.error('Что-то пошло не так!');
        console.log(error);
    }

}