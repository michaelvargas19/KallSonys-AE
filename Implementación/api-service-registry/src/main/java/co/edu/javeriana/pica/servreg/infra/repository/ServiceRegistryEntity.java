package co.edu.javeriana.pica.servreg.infra.repository;

import java.util.List;

import javax.persistence.*;

@Entity
@Table(name = "service_registry")
public class ServiceRegistryEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @Column(name = "nombre")
    private String nombre;

    @Column(name = "descripcion")
    private String descripcion;

    @Column(name = "ruta")
    private String ruta;

    @Column(name = "protocolo")
    private String protocolo;

    @Column(name = "idProveedor")
    private String idProveedor;

    @Column(name = "estado")
    private String estado;

    @OneToMany(cascade = CascadeType.ALL, mappedBy = "id")
    private List<ServiceCapabilityEntity> serviceCapabilityEntities;


    public ServiceRegistryEntity(){

    }
    

    public ServiceRegistryEntity(String nombre, String descripcion, String ruta, String protocolo, String idProveedor,
            String estado) {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estado = estado;
    }

    public ServiceRegistryEntity(Long id, String nombre, String descripcion, String ruta, String protocolo,
            String idProveedor, String estado) {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.ruta = ruta;
        this.protocolo = protocolo;
        this.idProveedor = idProveedor;
        this.estado = estado;
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

    public List<ServiceCapabilityEntity> getServiceCapabilityEntities() {
        return serviceCapabilityEntities;
    }

    public void setServiceCapabilityEntities(List<ServiceCapabilityEntity> serviceCapabilityEntities) {
        this.serviceCapabilityEntities = serviceCapabilityEntities;
    }
}
