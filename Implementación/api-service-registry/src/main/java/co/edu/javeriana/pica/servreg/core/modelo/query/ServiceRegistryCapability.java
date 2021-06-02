package co.edu.javeriana.pica.servreg.core.modelo.query;

public class ServiceRegistryCapability {
    private Long idServicio;
    private String nombreServicio;
    private String descripcionServicio;
    private String ruta;
    private String protocolo;
    private String idProveedor;
    private String estadoServicio;
    private Long idCapacidad;
    private String nombreCapacidad;
    private String descripcionCapacidad;
    private String metodoHTTP;
    private String plantillaRequest;
    private String plantillaResponse;
    private String estadoCapacidad;
    private String path;

    public ServiceRegistryCapability(){

    }

    public ServiceRegistryCapability(Long idServicio, String nombreServicio, String descripcionServicio, String ruta, String protocolo, String idProveedor, String estadoServicio, Long idCapacidad, String nombreCapacidad, String descripcionCapacidad, String metodoHTTP, String plantillaRequest, String plantillaResponse, String estadoCapacidad, String path) {
        this.idServicio = idServicio;
        this.nombreServicio = nombreServicio;
        this.descripcionServicio = descripcionServicio;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estadoServicio = estadoServicio;
        this.idCapacidad = idCapacidad;
        this.nombreCapacidad = nombreCapacidad;
        this.descripcionCapacidad = descripcionCapacidad;
        this.metodoHTTP = metodoHTTP;
        this.plantillaRequest = plantillaRequest;
        this.plantillaResponse = plantillaResponse;
        this.estadoCapacidad = estadoCapacidad;
        this.path = path;
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

    public String getPath() {
        return path;
    }

    public void setPath(String path) {
        this.path = path;
    }
}
