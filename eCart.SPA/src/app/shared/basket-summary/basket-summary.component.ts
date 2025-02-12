import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BasketItem } from '../models/basket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
    selector: 'app-basket-summary',
    templateUrl: './basket-summary.component.html',
    styleUrls: ['./basket-summary.component.scss'],
    standalone: false
})
export class BasketSummaryComponent {
  @Output() addItem = new EventEmitter<BasketItem>();
  @Output() removeItem = new EventEmitter<{id: number, quantity: number}>();
  @Input() isBasket = true;

  // public to use async pipe
  constructor(public basketService: BasketService){}

  addBasketItem(item: BasketItem){
    this.addItem.emit(item);
  }

  removeBasketItem(id: number, quantity = 1){
    this.removeItem.emit({id, quantity});
  }
}
