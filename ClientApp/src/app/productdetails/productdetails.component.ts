import { Component, OnInit, Input } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../product.service';

@Component({
  selector: 'productdetails',
  templateUrl: './productdetails.component.html',
  styleUrls: ['./productdetails.component.css']
})
export class ProductdetailsComponent implements OnInit {


  @Input() product: Product;
  @Input() products:Product[];

  constructor( private productService:ProductService) { }

  ngOnInit(): void {
  }

  addProduct(id:number,name:string,price:number,isactive:boolean)
  {
      

      const p= new Product(id,name,price,isactive);
      this.productService.updateProduct(p).subscribe(result=>{
        this.products.splice(this.products.findIndex(x=>x.id==p.id),1,p);
      });
  }

}
