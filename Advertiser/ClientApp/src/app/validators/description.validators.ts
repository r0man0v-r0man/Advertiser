import { FormControl } from '@angular/forms';

export class DescriptionValidators {
   static notOnlySpace = (control: FormControl) : {[s:string]:boolean} => {
        if(!control.value){
            return { error:true,  required: true }
          } else if(!(control.value as string).trim()){
            return { error: true, notOnlySpace: true }
          }
        return {};
    }
}