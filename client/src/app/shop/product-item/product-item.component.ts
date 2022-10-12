import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct;

  constructor() { }

  ngOnInit(): void {
  }

}





/* 
    product-item is our child component
    We use it for not displaying cards for each element -> for example if we have 100 products we do not want to create 100 html cards
    That's why we are creating the child component that get all necesary info from the parent component


    In our have the shop.component is the PARENT 
    and the product-item.component is the Child



    We need to create the product of type product, and the most important here is the @Input -> we show to the app what is this , and that it takes info from parent

    And to make it to take information -> 
    1: we need to go to the shop.component.html
    2: we find out the html tag for our child: in our case is : app-product-item
    3: we write [product]="product"
     3.1 [] brackets means that we are sending to the child
     3.2 [product] -> variable inside the brackets -> we use the variable that we created in this file; I would use the naming otherelse
     3.3 ="product" -> we refer to the *ngFor that we created in the html file to use the product from the products

*/
