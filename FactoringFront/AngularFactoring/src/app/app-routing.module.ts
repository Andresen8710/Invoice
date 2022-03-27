import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { CustomersComponent } from './customers/customers.component';
import { ProductsComponent } from './products/products.component';
import { InvoicesComponent } from './invoices/invoices.component';

const routes: Routes = [
  {path:'home',component:HomeComponent},
  {path:'customers',component:CustomersComponent},
  {path:'products',component:ProductsComponent},
  {path:'invoices',component:InvoicesComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
