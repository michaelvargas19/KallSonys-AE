export class SubmenuDto {
    id_submenu: number;
    id_menu: number;
    descripcion: string;
    ruta_componente: string;
    estado: boolean;
    
    constructor(){
        this.id_submenu=0;
        this.id_menu=0;
        this.descripcion="";
        this.ruta_componente="";
        this.estado=false;
    }
}