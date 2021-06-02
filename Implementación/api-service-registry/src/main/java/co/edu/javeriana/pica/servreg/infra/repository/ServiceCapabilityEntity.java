package co.edu.javeriana.pica.servreg.infra.repository;

import javax.persistence.*;

@Entity
@Table(name = "service_capability")
public class ServiceCapabilityEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @ManyToOne
    @JoinColumn(name = "id_servicio", referencedColumnName = "id")
    private ServiceRegistryEntity servicio;

    @Column(name = "nombre")
    private String nombre;

    @Column(name = "descripcion")
    private String descripcion;

    @Column(name = "metodoHTTP")
    private String metodoHTTP;

    @Column(name = "plantillaRequest")
    private String plantillaRequest;

    @Column(name = "plantillaResponse")
    private String plantillaResponse;

    @Column(name = "estado")
    private String estado;

    @Column(name = "path")
    private String path;


    public ServiceCapabilityEntity(){

    }

    public ServiceCapabilityEntity(ServiceRegistryEntity servicio, String nombre, String descripcion, String metodoHTTP, String plantillaRequest, String plantillaResponse, String estado, String path) {
        this.servicio = servicio;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.metodoHTTP = metodoHTTP;
        this.plantillaRequest = plantillaRequest;
        this.plantillaResponse = plantillaResponse;
        this.estado = estado;
        this.path = path;
    }

    public ServiceCapabilityEntity(Long id, ServiceRegistryEntity servicio, String nombre, String descripcion, String metodoHTTP, String plantillaRequest, String plantillaResponse, String estado, String path) {
        this.id = id;
        this.servicio = servicio;
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

    public ServiceRegistryEntity getServicio() {
        return servicio;
    }

    public void setServicio(ServiceRegistryEntity servicio) {
        this.servicio = servicio;
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
