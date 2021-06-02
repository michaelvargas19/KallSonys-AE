package co.edu.javeriana.pica.integracion.core.modelo.cmd;

import java.util.Date;
import java.util.List;

public class Orden {
    private String idOrden;
    private String ordenPadre;
    private String estadoOrden;
    private String estadoPago;
    private String moneda;
    private Double tasaCambio;
    private String fuente;
    private String direccion;
    private Long idCliente;
    private Date fechaCreacion;
    private Integer itemsOrden;
    private Long idProveedor;
    private List<ProductoOrden> productosOrden;

    public Orden(){

    }

    public Orden(String idOrden, String ordenPadre, String estadoOrden, String estadoPago, String moneda, Double tasaCambio, String fuente, String direccion, Long idCliente, Date fechaCreacion, Integer itemsOrden, Long idProveedor, List<ProductoOrden> productosOrden) {
        this.idOrden = idOrden;
        this.ordenPadre = ordenPadre;
        this.estadoOrden = estadoOrden;
        this.estadoPago = estadoPago;
        this.moneda = moneda;
        this.tasaCambio = tasaCambio;
        this.fuente = fuente;
        this.direccion = direccion;
        this.idCliente = idCliente;
        this.fechaCreacion = fechaCreacion;
        this.itemsOrden = itemsOrden;
        this.idProveedor = idProveedor;
        this.productosOrden = productosOrden;
    }

    public String getIdOrden() {
        return idOrden;
    }

    public void setIdOrden(String idOrden) {
        this.idOrden = idOrden;
    }

    public String getOrdenPadre() {
        return ordenPadre;
    }

    public void setOrdenPadre(String ordenPadre) {
        this.ordenPadre = ordenPadre;
    }

    public String getEstadoOrden() {
        return estadoOrden;
    }

    public void setEstadoOrden(String estadoOrden) {
        this.estadoOrden = estadoOrden;
    }

    public String getEstadoPago() {
        return estadoPago;
    }

    public void setEstadoPago(String estadoPago) {
        this.estadoPago = estadoPago;
    }

    public String getMoneda() {
        return moneda;
    }

    public void setMoneda(String moneda) {
        this.moneda = moneda;
    }

    public Double getTasaCambio() {
        return tasaCambio;
    }

    public void setTasaCambio(Double tasaCambio) {
        this.tasaCambio = tasaCambio;
    }

    public String getFuente() {
        return fuente;
    }

    public void setFuente(String fuente) {
        this.fuente = fuente;
    }

    public String getDireccion() {
        return direccion;
    }

    public void setDireccion(String direccion) {
        this.direccion = direccion;
    }

    public Long getIdCliente() {
        return idCliente;
    }

    public void setIdCliente(Long idCliente) {
        this.idCliente = idCliente;
    }

    public Date getFechaCreacion() {
        return fechaCreacion;
    }

    public void setFechaCreacion(Date fechaCreacion) {
        this.fechaCreacion = fechaCreacion;
    }

    public Integer getItemsOrden() {
        return itemsOrden;
    }

    public void setItemsOrden(Integer itemsOrden) {
        this.itemsOrden = itemsOrden;
    }

    public Long getIdProveedor() {
        return idProveedor;
    }

    public void setIdProveedor(Long idProveedor) {
        this.idProveedor = idProveedor;
    }

    public List<ProductoOrden> getProductosOrden() {
        return productosOrden;
    }

    public void setProductosOrden(List<ProductoOrden> productosOrden) {
        this.productosOrden = productosOrden;
    }
}
