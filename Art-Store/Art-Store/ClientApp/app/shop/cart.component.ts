import { Component } from '@angular/core';
import { DataService } from "../shared/dataService";
import { Router } from "@angular/router";
@Component({
    selector: 'the-cart',
    templateUrl: 'cart.component.html'
})
export class Cart {

    constructor(public data: DataService, private router: Router) { }

    onCheckout() {

        if (this.data.loginRequired) {

            return this.router.navigate(["login"]);
        }
        else return this.router.navigate(["checkout"]);
    }

   
}