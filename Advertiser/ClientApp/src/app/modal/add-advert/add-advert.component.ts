import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DescriptionValidators } from 'src/app/validators/description.validators';
import { Constants } from 'src/app/constants';
import { UploadFile, NzMessageService } from 'ng-zorro-antd';
import { UserWarning } from 'src/app/app-errors/userWarning';

@Component({
  selector: 'app-add-advert',
  templateUrl: './add-advert.component.html',
  styleUrls: ['./add-advert.component.less']
})
export class AddAdvertComponent implements OnInit {
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

  beforeUpload = (file: File) : boolean => {
    const maxDimension = 1000;
    var width: number, height: number;
    const img = new Image();
    img.src = URL.createObjectURL(file);

    img.onload = () => {
      width = img.naturalWidth;
      height = img.naturalHeight;    console.log(width, height);
      if( width >= maxDimension || height >= maxDimension ) {
        return true
      } else {
        throw new UserWarning(`Разрешение должно быть не меньше ${maxDimension}px`);
      }
      URL.revokeObjectURL(img.src);
    }

  }



}