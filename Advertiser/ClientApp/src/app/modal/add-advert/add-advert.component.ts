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
  modal: FormGroup;
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
    this.modal = this.formBuilder.group({
      price: [null, [Validators.required]],
      description: [null, [DescriptionValidators.notOnlySpace]]
    });
  }
  beforeUpload = (file: File )=> {

  }
  handleChange(info: { file: UploadFile }): void {

  }
  onFile = (file: UploadFile) : UploadFile => {
    this.file = file;
    this.file.filename = "1111111";
    this.file.name = "111111111.gif";
    console.log(this.file);
    return this.file;
  }
}