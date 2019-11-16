import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DescriptionValidators } from 'src/app/validators/description.validators';
import { Constants } from 'src/app/constants';
import { UploadFile, UploadChangeParam } from 'ng-zorro-antd';

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
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.initForm();
  }
  initForm(){
    this.form = this.formBuilder.group({
      price: [null, [Validators.required]],
      description: [null, [DescriptionValidators.notOnlySpace]],
      file: [null, [Validators.required]]
    });
  }

  onChange(info: { file: UploadFile }){
    console.log(info);

    this.form.controls['file'].setValue('asdfasdfasdfasdf');
  }
}