import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  constructor(private http:HttpClient) { }

  customers:any=[];

  modalTitle=""
  Id=0
  FirstName=""
  LastName=""
  Identification=""
  Birthdate=""
  Email=""
  Age=""


  ngOnInit(): void {
    this.refreshList();
  }

  refreshList(){
    this.http.get<any>(environment.API_URL+'customers')
    .subscribe(data=>{
      this.customers=data;
    });
  }

  addClick(){
    this.modalTitle="Add Customers";
    this.Id=0
    this.FirstName=""
    this.LastName=""
    this.Identification=""
    this.Birthdate=""
    this.Email=""
    this.Age=""
  }

  editClick(dep:any){
    this.modalTitle="Edit Customers";
    this.Id=dep.Id;
    this.FirstName=dep.FirstName
    this.LastName=dep.LastName
    this.Identification=dep.Identification
    this.Birthdate=dep.Birthdate
    this.Email=dep.Email
    this.Age=dep.Age
  }

  createClick(){
    var val={FirstName:this.FirstName,
      LastName:this.LastName,
      Identification:this.Identification,
      Birthdate:this.Birthdate,
      Email:this.Email,
      Age:this.Age};
    

    this.http.post(environment.API_URL+"customers",val).subscribe(res=>{
      alert(res.toString());
      this.refreshList();
    });
  }


  updateClick(){
    var val={Id:this.Id,
      FirstName:this.FirstName,
      LastName:this.LastName,
      Identification:this.Identification,
      Birthdate:this.Birthdate,
      Email:this.Email,
      Age:this.Age    
    };

    this.http.put(environment.API_URL+"customers/"+this.Id,val).subscribe(res=>{
      alert(res.toString());
      this.refreshList();
    });
  }

  deleteClick(id:any){
    if(confirm("Are You sure to Delete? ")){      
      this.http.delete(environment.API_URL+'customers/'+id).subscribe(res=>{
        alert(res.toString());
        this.refreshList();
      });
    };
    
      
      
  }

}
