import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  constructor(private http:HttpClient) { }

  products:any=[];

  modalTitle=""
  Id=0
  Name=""
  Description=""
  Price=""
  Stock=""


  ngOnInit(): void {
    this.refreshList();
  }

  refreshList(){
    this.http.get<any>(environment.API_URL+'products')
    .subscribe(data=>{
      this.products=data;
    });
  }

  addClick(){
    this.modalTitle="Add Products";
    this.Id=0
    this.Name=""
    this.Description=""
    this.Price=""
    this.Stock=""
    }

  editClick(dep:any){
    this.modalTitle="Edit Products";
    this.Id=dep.Id;
    this.Name=dep.Name
    this.Description=dep.Description
    this.Price=dep.Price
    this.Stock=dep.Stock

  }

  createClick(){
    var val={Name:this.Name,
      Description:this.Description,
      Price:this.Price,
      Stock:this.Stock,
};
    

    this.http.post(environment.API_URL+"products",val).subscribe(res=>{
      alert(res.toString());
      this.refreshList();
    });
  }


  updateClick(){
    var val={Id:this.Id,
      Name:this.Name,
      Description:this.Description,
      Price:this.Price,
      Stock:this.Stock,
  };

    this.http.put(environment.API_URL+"products/"+this.Id,val).subscribe(res=>{
      alert(res.toString());
      this.refreshList();
    });
  }

  deleteClick(id:any){
    if(confirm("Are You sure to Delete? ")){      
      this.http.delete(environment.API_URL+'products/'+id).subscribe(res=>{
        alert(res.toString());
        this.refreshList();
      });
    };
    
      
      
  }

}

