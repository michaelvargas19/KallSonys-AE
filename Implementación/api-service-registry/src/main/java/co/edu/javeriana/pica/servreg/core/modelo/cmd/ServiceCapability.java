package co.edu.javeriana.pica.servreg.core.modelo.cmd;

public class ServiceCapability {
    private Long id;
    private Long idServicio;
    private String nombre;
    private String descripcion;
    private String metodoHTTP;
    private String plantillaRequest;
    private String plantillaResponse;
    private String estado;
    private String path;

    public ServiceCapability(){

    }

    public ServiceCapability(Long id, Long idServicio, String nombre, String descripcion, String metodoHTTP, String plantillaRequest,
            String plantillaResponse, String estado, String path) {
        this.id = id;
        this.idServicio = idServicio;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.metodoHTTP = metodoHTTP;
        this.plantillaRequest = plantillaRequest;
        this.plantillaResponse = plantillaResponse;
        this.estado = estado;
        this.path = path;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getIdServicio() {
        return idServicio;
    }

    public void setIdServicio(Long idServicio) {
        this.idServicio = idServicio;
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

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public String getPath() {
        return path;
    }

    public void setPath(String path) {
        this.path = path;
    }
}
