package co.edu.javeriana.pica.proveedores.core.modelo;

public class Proveedor {
    private Long id;
    private String tipoDocumento;
    private String codigo;
    private String razonSocial;
    private String correo;
    private String celular;

    public Proveedor(){

    }

    public Proveedor(Long id){
        this.id = id;
    }

    public Proveedor(Long id, String tipoDocumento, String codigo, String razonSocial, String correo, String celular){
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
