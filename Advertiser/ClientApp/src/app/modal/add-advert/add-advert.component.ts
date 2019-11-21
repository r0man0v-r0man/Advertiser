import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DescriptionValidators } from 'src/app/validators/description.validators';
import { Constants } from 'src/app/constants';
import { UploadFile, NzMessageService } from 'ng-zorro-antd';
import { UserWarning } from 'src/app/app-errors/userWarning';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-advert',
  templateUrl: './add-advert.component.html',
  styleUrls: ['./add-advert.component.less']
})
export class AddAdvertComponent implements OnInit {
  minDimension = 1000;
  maxFileSize = 3;
  price = 200;
  formatterDollar = (value: number) => `$ ${value}`;
  parserDollar = (value: string) => value.replace('$ ', '');
  form: FormGroup;
  uploadUrl = Constants.uploadFileUrl;
  fileList : UploadFile[]= [];
  file: UploadFile;
  showUploadList = {
    showPreviewIcon: false,
    showRemoveIcon: true
  }
  constructor(private formBuilder: FormBuilder, private messageService: NzMessageService) { }

  ngOnInit() {
    this.initForm();
  }
  initForm(){
    this.form = this.formBuilder.group({
      price: [null, [Validators.required]],
      description: [null, [DescriptionValidators.notOnlySpace]],
      file: [this.file, [Validators.required]]
    });
  }

  onChange(info: { file: UploadFile }){
    if(info.file.status === 'done' && info.file.response) 
      this.form.controls['file'].setValue(info.file.response);
  }

  checkImageResolution(file: File, resolution: number) : Observable<boolean>{
    return new Observable(observer=>{
      let width: number, height: number;
      const img = new Image();
      img.src = window.URL.createObjectURL(file);
      img.onload = () => {
        width = img.naturalWidth;
        height = img.naturalHeight;    
        const isMinDimension = (width >= resolution && height >= resolution)
        if(!isMinDimension) throw new UserWarning('Разрешение изображения меньше 1000px');
        
          observer.next(isMinDimension);
          observer.complete();
          window.URL.revokeObjectURL(img.src);
          return;
      }
    })
  }

  beforeUpload = (file: File) : Observable<boolean> => {
    return new Observable(observer => {
      const isSizeLimit = file.size / 1024 / 1024 < this.maxFileSize;
      if (!isSizeLimit) {
        this.messageService.warning(`Максимальный размер изображения ${this.maxFileSize}mb`);
        observer.complete();
        return;
      }

      this.checkImageResolution(file, this.minDimension).subscribe(
        isResolutionLimit => {
          observer.next(isSizeLimit && isResolutionLimit);
          observer.complete();
        }
      );
    });
  }


}