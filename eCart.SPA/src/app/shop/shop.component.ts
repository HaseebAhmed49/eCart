import { HttpParams } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/products';
import { ShopParams } from '../shared/models/shopParams';
import { Types } from '../shared/models/type';
import { ShopService } from './shop.service';
import { ExcelService } from '../core/export/excel.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{
  @ViewChild('search') searchTerm? : ElementRef;
  products: Product[] = [];  
  brands: Brand[] = [];
  types: Types[] = [];
  shopParams: ShopParams;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ];
  totalCount = 0;

  constructor(private shopService: ShopService, private excelService: ExcelService){
    this.shopParams = shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  exportData() {

    this.excelService.exportToExcel(this.products, 'data.xlsx', 'Sheet1');
  }

  getProducts(){
    this.shopService.getProducts().subscribe({
      next: response => {
        this.products = response.data;
        this.totalCount = response.count;
      },
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
    const params = this.shopService.getShopParams();
    params.brandId = brandId;
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.shopParams = params;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    const params = this.shopService.getShopParams();
    params.typeId = typeId;
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.shopParams = params;
    this.getProducts();
  }

  onSortSelected(event: any){
    const params = this.shopService.getShopParams();
    params.sort = event.target.value;
    this.shopService.setShopParams(params);
    this.shopParams = params;
    this.getProducts();
  }

  onPageChanged(event: any){
    const params = this.shopService.getShopParams();
    if(params.pageNumber != event){
      params.pageNumber = event;
      this.shopService.setShopParams(params);
      this.shopParams = params;
        this.getProducts();
    }
  }

  onSearch(){
    const params = this.shopService.getShopParams();
    params.search = this.searchTerm?.nativeElement.value;
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.shopParams = params;
  this.getProducts();
  }

  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }
}
