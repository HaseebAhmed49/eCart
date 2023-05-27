import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, Type } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/products';
import { ShopParams } from '../shared/models/shopParams';
import { Types } from '../shared/models/type';

@Injectable({
  // loaded when app module is routed
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7167/api/';

  constructor(private http: HttpClient) { }

    getProducts(shopParams: ShopParams){
      let params = new HttpParams();
  
      if(shopParams.brandId > 0) params = params.append('brandId',shopParams.brandId);
      if(shopParams.typeId > 0) params = params.append('typeId',shopParams.typeId);
      params = params.append('sort',shopParams.sort);
      params = params.append('pageIndex',shopParams.pageNumber);
      params = params.append('pageSize',shopParams.pageSize);
      if(shopParams.search) params = params.append('search',shopParams.search);
  
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {params});
  }

  getProduct(id: number){
    console.log('into service');
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl+'products/brands')
  }

  getTypes(){
    return this.http.get<Types[]>(this.baseUrl+'Products/types')
  }

}
