package co.edu.javeriana.pica.integracion.infra.servreg;

public class ServiceCapability {
    private Long idCapacidad;
    private String nombreCapacidad;
    private String descripcionCapacidad;
    private String metodoHTTP;
    private String plantillaRequest;
    private String plantillaResponse;
    private String estadoCapacidad;
    private String path;

    public ServiceCapability(){

    }

    public ServiceCapability(Long idCapacidad, String nombreCapacidad, String descripcionCapacidad, String metodoHTTP, String plantillaRequest, String plantillaResponse, String estadoCapacidad, String path) {
        this.idCapacidad = idCapacidad;
        this.nombreCapacidad = nombreCapacidad;
        this.descripcionCapacidad = descripcionCapacidad;
        this.metodoHTTP = metodoHTTP;
        this.plantillaRequest = plantillaRequest;
        this.plantillaResponse = plantillaResponse;
        this.estadoCapacidad = estadoCapacidad;
        this.path = path;
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
