import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/products';

@Injectable({
  // loaded when app module is routed
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7167/api/';

  constructor(private http: HttpClient) { }

  getProducts(){
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products?pageSize=50');
  }
}
