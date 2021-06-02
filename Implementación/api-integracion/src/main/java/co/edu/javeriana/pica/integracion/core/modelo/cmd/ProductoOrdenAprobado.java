package co.edu.javeriana.pica.integracion.core.modelo.cmd;

public class ProductoOrdenAprobado extends ProductoOrden{
    private String idProductoOrdenAprobado;
    private String codigoProducto;

    public ProductoOrdenAprobado(){
        super();
    }

    public ProductoOrdenAprobado(String idProductoOrdenAprobado, String codigoProducto) {
        this.idProductoOrdenAprobado = idProductoOrdenAprobado;
        this.codigoProducto = codigoProducto;
    }

    public ProductoOrdenAprobado(String idDetalleOrden, String idOrden, Integer cantidad, String sku, String nombreProducto, Double precio, String idProductoOrdenAprobado, String codigoProducto) {
        super(idDetalleOrden, idOrden, cantidad, sku, nombreProducto, precio);
        this.idProductoOrdenAprobado = idProductoOrdenAprobado;
        this.codigoProducto = codigoProducto;
    }

    public String getIdProductoOrdenAprobado() {
        return idProductoOrdenAprobado;
    }

    public void setIdProductoOrdenAprobado(String idProductoOrdenAprobado) {
        this.idProductoOrdenAprobado = idProductoOrdenAprobado;
    }

    public String getCodigoProducto() {
        return codigoProducto;
    }

    public void setCodigoProducto(String codigoProducto) {
        this.codigoProducto = codigoProducto;
    }
}
