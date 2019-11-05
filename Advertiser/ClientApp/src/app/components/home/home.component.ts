import { Component, OnInit } from '@angular/core';
import { FlatService } from 'src/app/services/flat.service';
import { FlatModel } from 'src/app/models/flatModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {
  flats:FlatModel[];
  constructor(private flatService: FlatService) { }

  ngOnInit() {
    this.flatService.getAll()
      .subscribe(
        response => {
          this.flats = response as FlatModel[]
          console.table(this.flats)

         }
      )
  }

}
