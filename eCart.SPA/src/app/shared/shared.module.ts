import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule} from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';



@NgModule({
  declarations: [
    PagingHeaderComponent
  ],
  imports: [
    CommonModule,
    // forRoot is used to be as a singleton. Shared by other components as well.
    PaginationModule.forRoot()
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent
  ]
})
export class SharedModule { }
