import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.css']
})
export class InvoicesComponent implements OnInit {

  constructor(private http:HttpClient) { }

  invoices:any=[];

  modalTitle=""
  Id=0
  Date=""
  CustomerId=""
  Quantity=""
  TaxValue=""
  SubTotal=""
  Total=""
  
  ngOnInit(): void {
    this.refreshList();
  }

  refreshList(){
    this.http.get<any>(environment.API_URL+'invoices')
    .subscribe(data=>{
      this.invoices=data;
    });
  }

  addClick(){
    this.modalTitle="Add invoices";
    this.Id=0
    this.Date=""
    this.CustomerId=""
    this.Quantity=""
    this.TaxValue=""
    this.SubTotal=""
    this.Total=""
    }

  editClick(dep:any){
    this.modalTitle="Edit invoices";
    this.Id=dep.Id;
    this.Date=dep.Date
    this.CustomerId=dep.CustomerId
    this.Quantity=dep.Quantity
    this.TaxValue=dep.TaxValue
    this.SubTotal=dep.SubTotal
    this.Total=dep.Total
  }

  createClick(){
    var val={Date:this.Date,
      CustomerId:this.CustomerId,
      Quantity:this.Quantity,
      TaxValue:this.TaxValue,
      SubTotal:this.SubTotal,
      Total:this.Total
      
};
    

    this.http.post(environment.API_URL+"invoices",val).subscribe(res=>{
      alert(res.toString());
      this.refreshList();
    });
  }


  updateClick(){
    var val={Id:this.Id,
      Date:this.Date,
      CustomerId:this.CustomerId,
      Quantity:this.Quantity,
      TaxValue:this.TaxValue,
      SubTotal:this.SubTotal,
      Total:this.Total
  };

    this.http.put(environment.API_URL+"invoices/"+this.Id,val).subscribe(res=>{
      alert(res.toString());
      this.refreshList();
    });
  }

  deleteClick(id:any){
    if(confirm("Are You sure to Delete? ")){      
      this.http.delete(environment.API_URL+'invoices/'+id).subscribe(res=>{
        alert(res.toString());
        this.refreshList();
      });
    };
    
      
      
  }

}


