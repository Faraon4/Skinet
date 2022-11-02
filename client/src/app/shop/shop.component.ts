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
 brandIdSelected: number = 0; // We add = 0 to select from the beginning to be ALL brand selected
 typeIdSelected: number = 0; // We add = 0 to select from the beginning to be ALL type selected

 sortSelected = 'name'

 // Speling is very important, because only such way we get information from our backend
 sortOptions = [
  {name: 'Alphabetical', value: 'name'},
  {name: 'Price: Low to High', value: 'priceAsc'},
  {name: 'Price: High to Low', value: 'priceDesc'}
 ]


  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }



  getProducts(){
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe(response => {
      this.products = response.data;
    }, error => {
      console.log(error)
    })
  }


  getBrands(){
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id: 0, name : 'All'},...response] // if we just want to populate the array with other array, we have to specify it in the service as well
                                                        // response is an array, we did like this, to spread up the objects from inside the response and add infront of them the {object}
                                                        // simply add, ALL in site to choose it
    }, error => {
      console.log(error)
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id: 0, name : 'All'},...response]; // if we just want to populate the array with other array, we have to specify it in the service as well
    }, error => {
      console.log(error)
    })
  }



  onBrandSelected(brandId: number){
    this.brandIdSelected = brandId;
    this.getProducts();

  }


  onTypeSelected(typeId: number){
    this.typeIdSelected = typeId;
    this.getProducts();
  }


onSortSelected(sort: string){
  this.sortSelected = sort;
  this.getProducts();
}


}





// This class is used for diplaying the units, paginate , fileter etc.