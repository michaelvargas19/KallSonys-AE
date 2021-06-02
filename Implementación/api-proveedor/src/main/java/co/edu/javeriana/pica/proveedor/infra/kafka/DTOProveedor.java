package co.edu.javeriana.pica.proveedor.infra.kafka;

import co.edu.javeriana.pica.proveedor.core.modelo.Proveedor;

import java.util.Date;

public class DTOProveedor {
    private String evento;
    private String topico;
    private String origen;
    private Date fecha;
    private Proveedor data;

    public DTOProveedor(){

    }

    public String getEvento() {
        return evento;
    }

    public void setEvento(String evento) {
        this.evento = evento;
    }

    public String getTopico() {
        return topico;
    }

    public void setTopico(String topico) {
        this.topico = topico;
    }

    public String getOrigen() {
        return origen;
    }

    public void setOrigen(String origen) {
        this.origen = origen;
    }

    public Date getFecha() {
        return fecha;
    }

    public void setFecha(Date fecha) {
        this.fecha = fecha;
    }

    public Proveedor getData() {
        return data;
    }

    public void setData(Proveedor data) {
        this.data = data;
    }
}
