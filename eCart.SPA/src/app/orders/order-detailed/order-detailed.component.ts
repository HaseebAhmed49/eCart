import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/shared/models/order';
import { OrdersService } from '../orders.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit{
  order?: Order;

  constructor(private orderService: OrdersService, 
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService){
      // Added this to set the product name in breadcrumb to null and once loading will done, name will be refreshed as in below method.
      this.bcService.set('@orderDetails',' ');
    }

  ngOnInit(): void {
    this.loadOrder();
  }

  loadOrder(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    // + sign casted id into number from string
    if(id) this.orderService.getOrderById(+id).subscribe({
      next: order => 
      {
        this.order = order,
        // Will show the name in the breadcrumb
        this.bcService.set('@orderDetails',order.id.toString());
      },
      error: error => console.log(error)
    })
  }
}
