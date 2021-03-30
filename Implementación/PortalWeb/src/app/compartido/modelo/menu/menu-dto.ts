import { SubmenuDto } from './submenu-dto';

export class MenuDto {
    id_menu: number;
    descripcion: string;
    cabecera: string;
    icon: string;
    estado: boolean;
    submenus: SubmenuDto[];

    constructor(){
        this.id_menu=0;
        this.descripcion="";
        this.cabecera="";
        this.icon="";
        this.estado=false;
        this.submenus=[];
    }
}