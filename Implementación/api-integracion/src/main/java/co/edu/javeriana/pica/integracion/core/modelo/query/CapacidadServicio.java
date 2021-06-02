package co.edu.javeriana.pica.integracion.core.modelo.query;

import java.util.List;

public class CapacidadServicio {
    private Long id;
    private String nombreServicio;
    private String descripcionServicio;
    private String ruta;
    private String protocolo;
    private String idProveedor;
    private String estado;
    private Long idCapacidad;
    private String nombreCapacidad;
    private String descripcionCapacidad;
    private String metodoHTTP;
    private String path;
    private String plantillaRequest;
    private String plantillaResponse;
    private String estadoCapacidad;

    public CapacidadServicio(){

    }

    public CapacidadServicio(Long id, String nombreServicio, String descripcionServicio, String ruta, String protocolo, String idProveedor, String estado, Long idCapacidad, String nombreCapacidad, String descripcionCapacidad, String metodoHTTP, String path, String plantillaRequest, String plantillaResponse, String estadoCapacidad) {
        this.id = id;
        this.nombreServicio = nombreServicio;
        this.descripcionServicio = descripcionServicio;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estado = estado;
        this.idCapacidad = idCapacidad;
        this.nombreCapacidad = nombreCapacidad;
        this.descripcionCapacidad = descripcionCapacidad;
        this.metodoHTTP = metodoHTTP;
        this.path = path;
        this.plantillaRequest = plantillaRequest;
        this.plantillaResponse = plantillaResponse;
        this.estadoCapacidad = estadoCapacidad;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
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

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public Long getIdCapacidad() {
        return idCapacidad;
    }

    public void setIdCapacidad(Long idCapacidad) {
        this.idCapacidad = idCapacidad;
    }

    public String getNombreCapacidad() {
        return nombreCapacidad;
    }

    public void setNombreCapacidad(String nombreCapacidad) {
        this.nombreCapacidad = nombreCapacidad;
    }

    public String getDescripcionCapacidad() {
        return descripcionCapacidad;
    }

    public void setDescripcionCapacidad(String descripcionCapacidad) {
        this.descripcionCapacidad = descripcionCapacidad;
    }

    public String getMetodoHTTP() {
        return metodoHTTP;
    }

    public void setMetodoHTTP(String metodoHTTP) {
        this.metodoHTTP = metodoHTTP;
    }

    public String getPath() {
        return path;
    }

    public void setPath(String path) {
        this.path = path;
    }

    public String getPlantillaRequest() {
        return plantillaRequest;
    }

    public void setPlantillaRequest(String plantillaRequest) {
        this.plantillaRequest = plantillaRequest;
    }

    public String getPlantillaResponse() {
        return plantillaResponse;
    }

    public void setPlantillaResponse(String plantillaResponse) {
        this.plantillaResponse = plantillaResponse;
    }

    public String getEstadoCapacidad() {
        return estadoCapacidad;
    }

    public void setEstadoCapacidad(String estadoCapacidad) {
        this.estadoCapacidad = estadoCapacidad;
    }
}
