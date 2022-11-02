import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams){
    let params = new HttpParams();

    if(shopParams.brandId) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if(shopParams.typeId) {
      params = params.append('typeId',shopParams.typeId.toString());
    }

    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})   // We get an observable , and we can manioulate it and get into pagination form
      .pipe( // wrapper around rxjs opperator, inside this pipe operator, we can chain other funct as well (for example delay)
        map(response => {
          return response.body; // type of ipagination object
        })
      );
  }



  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
  }

  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + "products/types")
  }
}
