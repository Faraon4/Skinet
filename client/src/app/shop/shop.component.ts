import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
 products: IProduct[];
 brands: IBrand[];
 types: IType[];


  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }



  getProducts(){
    this.shopService.getProducts().subscribe(response => {
      this.products = response.data;
    }, error => {
      console.log(error)
    })
  }


  getBrands(){
    this.shopService.getBrands().subscribe(response => {
      this.brands = response; // if we just want to populate the array with other array, we have to specify it in the service as well
    }, error => {
      console.log(error)
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response => {
      this.types = response; // if we just want to populate the array with other array, we have to specify it in the service as well
    }, error => {
      console.log(error)
    })
  }


}





// This class is used for diplaying the units, paginate , fileter etc.