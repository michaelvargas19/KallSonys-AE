import { Component, OnInit, ViewChild  } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { Router } from "@angular/router";
import { ModalComponent } from '../compartido/modal/modal.component';
import { Seguridad } from '../guards/seguridad';
import { AutenticacionService } from '../compartido/servicios/autenticacion.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {

  @ViewChild(ModalComponent,{ static: false }) modal: ModalComponent;

  loginForm = new FormGroup({
    usuario: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  })

  tituloAlerta: string = "";
  //sendData: Login;

  constructor(private router: Router,
              private Seguridad: Seguridad,
              private Autenticacion: AutenticacionService)
              {
                this.modal= new ModalComponent();
              }

  ngOnInit(): void {  }


  iniciarSesion() {
    //this.modal.mostraCargando();
    this.cargaSesiones("");
    this.loginForm
    this.router.navigate(['inicio']);
    // if (this.captchaValido) {
    //Arma el objeto para enviar al servicio post de login
    /*this.sendData = {
      abreviacionAPP: "",
      usuario: this.loginForm.controls["usuario"].value,
      contrasena: this.loginForm.controls["password"].value,
      nuevaContrasena: "",
      tokenJWT: ""
    };

    this.modal.mostraCargando();
    //Llama al servicio y se le pasa el objeto sendata
    this.LoginService.login(this.sendData).then((res: any) => {
      if (res.data != null) {
        if (res.data.autenticacion) {
          swal.close();
          this.cargaSesiones(res.data.tokenJWT);
          this.consultaEstadisticasNacionales();
          this.router.navigate(['inicio']);
        } else {
          if (res.data.bloqueado) {
            let url = '<a target="_blank" href="' + res.data.urLdesbloqueo + '">Desbloquear usuario</a>'
            this.modal.modalRedireccion("Inicio Sesión", res.data.mensaje, "error", url);
          } else {
            this.modal.modalGeneral("Inicio Sesión", res.data.mensaje, "warning");
          }
        }
      } else {
        this.modal.ocultarModal();
      }
    });*/
    // }else{
    //   this.modal.modalGeneral("Inicio Sesión", "Por favor valide el captcha para continuar", "warning");
    // }
  }

  cargaSesiones(token: any) {
    
    sessionStorage.setItem('login', 'ok');
    sessionStorage.setItem('general', token.toString());
    sessionStorage.setItem('bit', token.toString());
    //sessionStorage.setItem('user', this.Seguridad.encriptar(this.sendData.usuario));
    
  }



}
