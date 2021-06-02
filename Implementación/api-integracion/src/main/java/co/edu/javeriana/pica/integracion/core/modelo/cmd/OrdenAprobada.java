package co.edu.javeriana.pica.integracion.core.modelo.cmd;

import java.util.Date;
import java.util.List;

public class OrdenAprobada extends Orden{
    private String codigoOrden;

    private List<ProductoOrdenAprobado> productoOrdenAprobados;

    public OrdenAprobada(){
        super();
    }

    public OrdenAprobada(String idOrden, String ordenPadre, String estadoOrden, String estadoPago, String moneda, Double tasaCambio, String fuente, String direccion, Long idCliente, Date fechaCreacion, Integer itemsOrden, Long idProveedor, List<ProductoOrden> productosOrden, String codigoOrden, List<ProductoOrdenAprobado> productoOrdenAprobados) {
        super(idOrden, ordenPadre, estadoOrden, estadoPago, moneda, tasaCambio, fuente, direccion, idCliente, fechaCreacion, itemsOrden, idProveedor, productosOrden);
        this.codigoOrden = codigoOrden;
        this.productoOrdenAprobados = productoOrdenAprobados;
    }

    public String getCodigoOrden() {
        return codigoOrden;
    }

    public void setCodigoOrden(String codigoOrden) {
        this.codigoOrden = codigoOrden;
    }

    public List<ProductoOrdenAprobado> getProductoOrdenAprobados() {
        return productoOrdenAprobados;
    }

    public void setProductoOrdenAprobados(List<ProductoOrdenAprobado> productoOrdenAprobados) {
        this.productoOrdenAprobados = productoOrdenAprobados;
    }
}
