import * as crypto from 'crypto-js'; 
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';


@Injectable({
    providedIn: 'root'
})

export class Seguridad {
    encriptar(texto: string): string{
        return crypto.AES.encrypt(texto, `${environment.keyEncript}`).toString()
    }

    desencriptar(textoEncriptado: string){
        return crypto.AES.decrypt(textoEncriptado, `${environment.keyEncript}`).toString(crypto.enc.Utf8)
    }    
}