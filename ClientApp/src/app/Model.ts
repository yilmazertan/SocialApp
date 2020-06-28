export class Model{
    categoryName:string;
    products:Array<Product>;
 
    constructor() {

         this.categoryName="Telefon";
         this.products=[

            new Product(1,"Samsung S1",10000,true),
            new Product(2,"Samsung S2",20000,false),
            new Product(3,"Samsung S3",30000,true),
            new Product(4,"Samsung S4",40000,true),
            new Product(5,"Samsung S5",50000,false),
            new Product(6,"Samsung S6",60000,true)
          
          ];
        
    }

    }

    export class Product {

        id:number;
        name:string;
        price:number;
        isactive:boolean;

        constructor(id:number,name:string,price:number,isactive:boolean) {
            this.id=id;
            this.name=name;
            this.price=price;
            this.isactive=isactive;
        }
    }