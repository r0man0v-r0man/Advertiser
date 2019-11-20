import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { FlatsComponent } from './components/flat/flats/flats.component';
import { FlatDetailComponent } from './components/flat/flat-detail/flat-detail.component';


const routes: Routes = [
  {  
    path: '', 
    component: HomeComponent
  },
  {  
    path: 'flat', 
    component: FlatsComponent
  },
  {
    path: 'flat/:id',
    component: FlatDetailComponent
  },
  {  
    path: '**', 
    component: NotFoundComponent
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
