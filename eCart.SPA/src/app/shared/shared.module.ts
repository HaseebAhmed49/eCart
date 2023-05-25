import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule} from 'ngx-bootstrap/pagination';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    // forRoot is used to be as a singleton. Shared by other components as well.
    PaginationModule.forRoot()
  ],
  exports: [
    PaginationModule
  ]
})
export class SharedModule { }
