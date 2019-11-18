import { Component, OnInit } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd';
import { AddAdvertComponent } from 'src/app/modal/add-advert/add-advert.component';
import { FlatService } from 'src/app/services/flat.service';
import { Constants } from 'src/app/constants';
import { FlatModel } from 'src/app/models/flatModel';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.less']
})
export class NavbarComponent implements OnInit {
  constructor(private modalService: NzModalService, private flatService: FlatService) { }

  ngOnInit() {
  }

  showAddAdvertModal(){
  const modal = this.modalService.create({
      nzTitle: 'Добавить объявление',
      nzContent: AddAdvertComponent,
      nzFooter:[
        {
          type: 'primary',
          label: 'Добавить',
          disabled: ()=> !modal.getContentComponent().form.valid,
          onClick: ()=>{
            const modalForm = modal.getContentComponent().form;
            if(modalForm.valid){
              let newFlatAdvert = new FlatModel(modalForm.value)
              this.flatService
                .create( 
                  Constants.createFlatAdvertURL, 
                  newFlatAdvert)
                .subscribe(response => {
                  console.log(response);
                })
              modal.destroy();
            }
          }
        }
      ]
    });
  }
}
