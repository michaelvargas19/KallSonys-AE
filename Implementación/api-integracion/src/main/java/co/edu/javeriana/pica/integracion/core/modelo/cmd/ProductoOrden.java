package co.edu.javeriana.pica.integracion.core.modelo.cmd;

public class ProductoOrden {
    private String idDetalleOrden;
    private String idOrden;
    private Integer cantidad;
    private String sku;
    private String nombreProducto;
    private Double precio;


    ProductoOrden(){

    }

    public ProductoOrden(String idDetalleOrden, String idOrden, Integer cantidad, String sku, String nombreProducto, Double precio) {
        this.idDetalleOrden = idDetalleOrden;
        this.idOrden = idOrden;
        this.cantidad = cantidad;
        this.sku = sku;
        this.nombreProducto = nombreProducto;
        this.precio = precio;
    }

    public String getIdDetalleOrden() {
        return idDetalleOrden;
    }

    public void setIdDetalleOrden(String idDetalleOrden) {
        this.idDetalleOrden = idDetalleOrden;
    }

    public String getIdOrden() {
        return idOrden;
    }

    public void setIdOrden(String idOrden) {
        this.idOrden = idOrden;
    }

    public Integer getCantidad() {
        return cantidad;
    }

    public void setCantidad(Integer cantidad) {
        this.cantidad = cantidad;
    }

    public String getSku() {
        return sku;
    }

    public void setSku(String sku) {
        this.sku = sku;
    }

    public String getNombreProducto() {
        return nombreProducto;
    }

    public void setNombreProducto(String nombreProducto) {
        this.nombreProducto = nombreProducto;
    }

    public Double getPrecio() {
        return precio;
    }

    public void setPrecio(Double precio) {
        this.precio = precio;
    }
}
