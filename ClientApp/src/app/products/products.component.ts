import { Component, OnInit } from '@angular/core';
import { Model, Product } from '../Model';
import { ProductFormComponent } from '../product-form/product-form.component';
import { ProductService } from '../product.service';
import { observable } from 'rxjs';

@Component({
  selector: 'products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products:Product[];
  selecttedproduct:Product;
 

  constructor(private productService: ProductService) { }

  ngOnInit(): void {

    this.getProducts();
  }


  getProducts(){

     this.productService.getProducts().subscribe(p=>
      {
      this.products=p;
      });

  } 

  deleteProduct(product:Product){
    this.productService.deleteProduct(product).subscribe(
      p=>{ this.products.splice(this.products.findIndex(p=>p.id==product.id),1)}
    );
  }

  onSelectedProduct(product:Product){
    this.selecttedproduct=product;
  }
 
  

 

  
}
