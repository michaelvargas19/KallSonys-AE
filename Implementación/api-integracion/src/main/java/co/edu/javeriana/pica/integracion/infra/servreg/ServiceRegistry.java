package co.edu.javeriana.pica.integracion.infra.servreg;

import java.util.List;

public class ServiceRegistry {
    private Long idServicio;
    private String nombreServicio;
    private String descripcionServicio;
    private String ruta;
    private String protocolo;
    private String idProveedor;
    private String estadoServicio;
    private List<ServiceCapability> capacidades;

    public ServiceRegistry(){

    }
    
    public ServiceRegistry(String nombreServicio, String descripcionServicio, String ruta, String protocolo, String idProveedor,
            String estadoServicio) {
        this.nombreServicio = nombreServicio;
        this.descripcionServicio = descripcionServicio;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estadoServicio = estadoServicio;
    }



    public ServiceRegistry(Long idServicio, String nombreServicio, String descripcionServicio, String ruta, String protocolo,
            String idProveedor, String estadoServicio) {
        this.idServicio = idServicio;
        this.nombreServicio = nombreServicio;
        this.descripcionServicio = descripcionServicio;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estadoServicio = estadoServicio;
    }

    

    public ServiceRegistry(Long idServicio, String nombreServicio, String descripcionServicio, String ruta, String protocolo,
            String idProveedor, String estadoServicio, List<ServiceCapability> capacidades) {
        this.idServicio = idServicio;
        this.nombreServicio = nombreServicio;
        this.descripcionServicio = descripcionServicio;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estadoServicio = estadoServicio;
        this.capacidades = capacidades;
    }

    public Long getIdServicio() {
        return idServicio;
    }

    public void setIdServicio(Long idServicio) {
        this.idServicio = idServicio;
    }

    public String getNombreServicio() {
        return nombreServicio;
    }

    public void setNombreServicio(String nombreServicio) {
        this.nombreServicio = nombreServicio;
    }

    public String getDescripcionServicio() {
        return descripcionServicio;
    }

    public void setDescripcionServicio(String descripcionServicio) {
        this.descripcionServicio = descripcionServicio;
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

    public String getEstadoServicio() {
        return estadoServicio;
    }

    public void setEstadoServicio(String estadoServicio) {
        this.estadoServicio = estadoServicio;
    }

    public List<ServiceCapability> getCapacidades() {
        return capacidades;
    }

    public void setCapacidades(List<ServiceCapability> capacidades) {
        this.capacidades = capacidades;
    }
}
