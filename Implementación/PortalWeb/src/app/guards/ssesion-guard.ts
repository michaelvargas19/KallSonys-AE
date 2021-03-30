import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';


@Injectable({
    providedIn: 'root'
})

export class SessionGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(){    
        if(sessionStorage.getItem('login') === 'ok'){
            return true;
        }else{
            this.router.navigate(['login']);
            return false;
        }        
    }
}