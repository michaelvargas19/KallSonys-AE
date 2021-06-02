package co.edu.javeriana.pica.servreg.core.modelo.cmd;

import java.util.List;

public class ServiceRegistry {
    private Long id;
    private String nombre;
    private String descripcion;
    private String ruta;
    private String protocolo;
    private String idProveedor;
    private String estado;
    private List<ServiceCapability> capacidades;

    public ServiceRegistry(){

    }
    
    public ServiceRegistry(String nombre, String descripcion, String ruta, String protocolo, String idProveedor,
            String estado) {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estado = estado;
    }



    public ServiceRegistry(Long id, String nombre, String descripcion, String ruta, String protocolo,
            String idProveedor, String estado) {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estado = estado;
    }

    

    public ServiceRegistry(Long id, String nombre, String descripcion, String ruta, String protocolo,
            String idProveedor, String estado, List<ServiceCapability> capacidades) {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estado = estado;
        this.capacidades = capacidades;
    }



    public Long getId() {
        return id;
    }
    public void setId(Long id) {
        this.id = id;
    }
    public String getNombre() {
        return nombre;
    }
    public void setNombre(String nombre) {
        this.nombre = nombre;
    }
    public String getDescripcion() {
        return descripcion;
    }
    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }
    public String getRuta() {
        return ruta;
    }
    public void setRuta(String ruta) {
        this.ruta = ruta;
    }
    public String getProtocolo() {
        return protocolo;
    }
    public void setProtocolo(String protocolo) {
        this.protocolo = protocolo;
    }
    public String getIdProveedor() {
        return idProveedor;
    }
    public void setIdProveedor(String idProveedor) {
        this.idProveedor = idProveedor;
    }
    public String getEstado() {
        return estado;
    }
    public void setEstado(String estado) {
        this.estado = estado;
    }



    public List<ServiceCapability> getCapacidades() {
        return capacidades;
    }



    public void setCapacidades(List<ServiceCapability> capacidades) {
        this.capacidades = capacidades;
    }

    

}
