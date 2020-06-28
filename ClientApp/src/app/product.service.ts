import { Injectable } from '@angular/core';
import { Model, Product } from './Model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {


  baseUrl:string = "http://localhost:5000/";

  model= new Model();

  constructor(private http:HttpClient) { }


  addProduct(product:Product): Observable<Product>
  {
   return  this.http.post<Product>(this.baseUrl + 'api/products',product);
  }


  updateProduct(product:Product){
    return this.http.put<Product>(this.baseUrl + 'api/products/' + product.id, product);
  }


  getProducts() :Observable<Product[]>{
    return  this.http.get<Product[]>(this.baseUrl + 'api/products');
  }

  deleteProduct(product :Product): Observable<Product>{
    return this.http.delete<Product>(this.baseUrl + 'api/products/' + product.id);
  }

  getProductbyId(id:number)
  {
    return this.model.products.find(i=>i.id == id);
  }

  saveProduct(product:Product)
  {

 

    if(product.id==0){
    
      this.model.products.push(product);
    }
    else{

      const p=this.getProductbyId(product.id);
    
      console.log(p);
      p.name=product.name
      p.price=product.price
      p.isactive=product.isactive

    }


  }
 


}
