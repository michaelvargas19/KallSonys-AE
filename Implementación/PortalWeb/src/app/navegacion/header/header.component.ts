import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { MediaMatcher } from '@angular/cdk/layout'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass']
})
export class HeaderComponent implements OnInit {

  private _mobileQueryListener: () => void;

  // variables Globales
  usuario: string="";
  mobileQuery: MediaQueryList;

  fillerNav = Array.from({length: 50}, (_, i) => `Nav Item ${i + 1}`);
  
  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher,
              router: Router) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }


  
  logout() {
    sessionStorage.clear();
    //this.router.navigate(['login']);
  }

  datosUsuario(usuario: any) {
    this.usuario = usuario;
  }
}
