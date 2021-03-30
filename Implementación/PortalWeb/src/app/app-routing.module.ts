import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InicioComponent } from './inicio/inicio.component';
import { LoginComponent } from './login/login.component';
import { NavegacionComponent } from './navegacion/navegacion.component'
import { SessionGuard } from './guards/ssesion-guard';


const routes: Routes = [
  { path: '', component: LoginComponent },  
  {
    path: 'inicio', component: NavegacionComponent,
    canActivate: [SessionGuard],
    children: [
      { path: '', component: InicioComponent },
    ]
  },
  { path: '**', redirectTo: '' },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
