import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/products';

@Component({
    selector: 'app-product-item',
    templateUrl: './product-item.component.html',
    styleUrls: ['./product-item.component.scss'],
    standalone: false
})
export class ProductItemComponent {
  @Input() product?: Product;

  constructor(private basketService: BasketService){}

  addItemToBasket(){
    // Only gets executed if there is any product, otherwise will not execute
    this.product && this.basketService.addItemToBasket(this.product);
  }
}