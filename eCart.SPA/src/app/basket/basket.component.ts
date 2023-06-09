import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  constructor(public basketServive: BasketService){}

  incrementQuantity(item: BasketItem){
    this.basketServive.addItemToBasket(item);
  }

  removeItem(id: number, quantity: number){
    this.basketServive.removeItemsFromBasket(id, quantity);
  }
}