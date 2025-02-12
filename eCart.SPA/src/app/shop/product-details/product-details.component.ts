import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/products';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrls: ['./product-details.component.scss'],
    standalone: false
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;
  quantity = 1;
  quantityInBasket = 0;
  
  constructor(private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService){
      // Added this to set the product name in breadcrumb to null and once loading will done, name will be refreshed as in below method.
      this.bcService.set('@productDetails',' ');
    }

  ngOnInit(): void{
    this.loadProduct();
  }

  loadProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    // + sign casted id into number from string
    if(id) this.shopService.getProduct(+id).subscribe({
      next: product => 
      {
        this.product = product,
        // Will show the name in the breadcrumb
        this.bcService.set('@productDetails',product.name);
        this.basketService.basketSource$.pipe(take(1)).subscribe({
          next: basket => {
            // +id will convert the string into number
            const item = basket?.items.find(x => x.id === +id);
            if(item){
              this.quantity = item.quantity;
              this.quantityInBasket = item.quantity;
            }
          }
        })
      },
      error: error => console.log(error)
    })
  }

  incrementQuantity(){
    this.quantity++;
  }

  decrementQuantity(){
    this.quantity--;
  }

  updateBasket(){
    if(this.product){
      if(this.quantity > this.quantityInBasket){
        const itemsToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemsToAdd;
        this.basketService.addItemToBasket(this.product, itemsToAdd);
      }
      else
      {
        const itemsToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= itemsToRemove;
        this.basketService.removeItemsFromBasket(this.product.id, itemsToRemove);
      }
    }
  }

  get buttonText(){
    return this.quantityInBasket === 0 ? 'Add to Basket' : 'Update Basket';
  }
}