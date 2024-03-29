import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: true}) searchTerm: ElementRef;


 products: IProduct[];
 brands: IBrand[];
 types: IType[];
 shopParams = new ShopParams();

 totalCount: number

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
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1 // reseting it to 1, because we get bug that is described in video nr 105
    this.getProducts();

  }


  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1 // reseting it to 1, because we get bug that is described in video nr 105
    this.getProducts();
  }


onSortSelected(sort: string){
  this.shopParams.sort = sort;
  this.getProducts();
}


onPageChanged(event: any){
  // We did this because we have bug that is described in video number 105
  if(this.shopParams.pageNumber !== event)
  {
    
  this.shopParams.pageNumber = event;
  this.getProducts()
  }
}



onSearch(){
  this.shopParams.search = this.searchTerm.nativeElement.value;
  this.shopParams.pageNumber = 1 // reseting it to 1, because we get bug that is described in video nr 105
  this.getProducts();
}


onReset(){
  this.searchTerm.nativeElement.value = "";
  this.shopParams = new ShopParams();
  this.getProducts();
}


}





// This class is used for diplaying the units, paginate , fileter etc.