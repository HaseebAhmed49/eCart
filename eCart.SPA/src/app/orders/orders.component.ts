import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/order';
import { OrdersService } from './orders.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit{
  orders: Order[] = [];

  ngOnInit(): void {
    this.getOrders();
  }

  constructor(private orderService: OrdersService, private toastr: ToastrService){}

  getOrders(){
    this.orderService.getOrders().subscribe({
      next: response => {
        this.orders = response;
      },
      error: error => console.log(error)
    })
  }
}
