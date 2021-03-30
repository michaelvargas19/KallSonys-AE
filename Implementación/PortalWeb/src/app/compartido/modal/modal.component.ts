import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import swal from 'sweetalert2';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.sass']
})
export class ModalComponent implements OnInit {
  

  @Output() confirmacion = new EventEmitter<boolean>();
  titulo:string="";
  texto:string="";

  constructor() { }

  ngOnInit(): void {
  }

  modalGeneral(titulo: string, texto: string, icon: any){
    swal.fire({
      title: titulo,
      text: texto,
      icon: icon
    });
  }

  modalRedireccion(titulo: string, texto: string, icon: any, footer: string){
    swal.fire({
      title: titulo,
      text: texto,
      footer: footer,
      icon: icon
    });
  }

  mostraCargando(){
    swal.fire({
      allowOutsideClick: false,
      allowEscapeKey: false,
    })
    swal.showLoading();
  }

  ocultarModal(){
    swal.close();
  }

  modalConfirmacion(titulo: string, textoBotonConfirmacion: string, texto: string){
    swal.fire({
      title: titulo, 
      text: texto,     
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: textoBotonConfirmacion,
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.confirmacion.emit(true);
      }else{
        this.confirmacion.emit(false);
      }
    })
  }

}
