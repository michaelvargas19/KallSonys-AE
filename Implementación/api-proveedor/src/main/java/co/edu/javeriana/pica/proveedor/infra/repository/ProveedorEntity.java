package co.edu.javeriana.pica.proveedor.infra.repository;

import javax.persistence.*;

@Entity
@Table(name = "proveedor")
public class ProveedorEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @Column(name = "tipo_documento")
    private String tipoDocumento;

    @Column(name = "codigo")
    private String codigo;

    @Column(name = "razon_social")
    private String razonSocial;

    @Column(name = "correo")
    private String correo;

    @Column(name = "celular")
    private String celular;

    public ProveedorEntity(){

    }

    

    public ProveedorEntity(Long id) {
        this.id = id;
    }

    public ProveedorEntity(Long id, String razonSocial, String correo, String celular) {
        this.id = id;
        this.razonSocial = razonSocial;
        this.correo = correo;
        this.celular = celular;
    }

    public ProveedorEntity(String tipoDocumento, String codigo, String razonSocial, String correo,
        String celular) {
        this.tipoDocumento = tipoDocumento;
        this.codigo = codigo;
        this.razonSocial = razonSocial;
        this.correo = correo;
        this.celular = celular;
    }


    public ProveedorEntity(Long id, String tipoDocumento, String codigo, String razonSocial, String correo,
            String celular) {
        this.id = id;
        this.tipoDocumento = tipoDocumento;
        this.codigo = codigo;
        this.razonSocial = razonSocial;
        this.correo = correo;
        this.celular = celular;
    }


    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getTipoDocumento() {
        return tipoDocumento;
    }

    public void setTipoDocumento(String tipoDocumento) {
        this.tipoDocumento = tipoDocumento;
    }

    public String getCodigo() {
        return codigo;
    }

    public void setCodigo(String codigo) {
        this.codigo = codigo;
    }

    public String getRazonSocial() {
        return razonSocial;
    }

    public void setRazonSocial(String razonSocial) {
        this.razonSocial = razonSocial;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public String getCelular() {
        return celular;
    }

    public void setCelular(String celular) {
        this.celular = celular;
    }

    
}
