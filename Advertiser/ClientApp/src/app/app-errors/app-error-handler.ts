import { ErrorHandler } from '@angular/core';

export class AppErrorHandler implements ErrorHandler{
    handleError(error) {
        alert('An unxepected error occured.');
        console.log(error);
    }

}