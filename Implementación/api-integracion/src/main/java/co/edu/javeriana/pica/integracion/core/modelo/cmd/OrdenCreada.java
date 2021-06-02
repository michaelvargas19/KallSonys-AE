package co.edu.javeriana.pica.integracion.core.modelo.cmd;

import java.util.Date;
import java.util.List;

public class OrdenCreada extends Orden{


    public OrdenCreada(){
        super();
    }

    public OrdenCreada(String idOrden,String ordenPadre, String estadoOrden, String estadoPago, String moneda, Double tasaCambio, String fuente, String direccion, Long idCliente, Date fechaCreacion, Integer itemsOrden, Long idProveedor, List<ProductoOrden> productosOrden) {
        super(idOrden, ordenPadre, estadoOrden, estadoPago, moneda, tasaCambio, fuente, direccion, idCliente, fechaCreacion, itemsOrden, idProveedor, productosOrden);
    }


}
