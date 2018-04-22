import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router'
import {FormsModule} from '@angular/forms'

import { AppComponent } from './app.component';
import { DataService } from './shared/dataService';
import { ProductList } from './shop/productList.component'
import { Shop } from './shop/shop.component'
import { Login } from './login/login.component'
import { Cart } from './shop/cart.component'
import {Checkout} from './checkout/checkout.component'



let routes = [
    { path: "", component: Shop },
    { path: "login", component: Login },
    {path:"checkout", component: Checkout}

]

@NgModule({
  declarations: [
      AppComponent,
      ProductList,
      Shop,
      Cart,
      Checkout,
      Login
  ],
  imports: [
      BrowserModule,
      HttpModule,
      FormsModule,
      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing:false //for debugging the routes(see in console each call for routes)
      })
  ],
  providers: [
      DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
