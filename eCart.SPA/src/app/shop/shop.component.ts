import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/products';
import { Types } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{
  products: Product[] = [];  
  brands: Brand[] = [];
  types: Types[] = [];
  brandIdSelected = 0;
  typeIdSelected = 0;
  sortSelected = 'name';
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ]

  constructor(private shopService: ShopService){}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
    console.log(this.types);
  }

  getProducts(){
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe({
      next: response => this.products = response.data,
      error: error => console.log(error)
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe({
      next: response => this.brands = [{id: 0, name: 'All'}, ...response],
      error: error => console.log(error)
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
      // next: response => this.types = response,
      // It will append 0,All in the types array with the data from the API using response
      next: response => this.types = [{id: 0, name: 'All'}, ...response],
      error: error => console.log(error)
    });
  }

  onBrandSelected(brandId: number){
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(event: any){
    this.sortSelected = event.target.value;
    this.getProducts();
  }

}
