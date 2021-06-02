package co.edu.javeriana.pica.integracion.core.modelo.cmd;

import java.util.Date;

public class EventBase {
    private String evento;
    private String topico;
    private String origen;
    private String usuario;
    private Date fecha;
    private String data;

    public EventBase(){

    }

    public EventBase(String evento, String topico, String origen, String usuario, Date fecha, String data) {
        this.evento = evento;
        this.topico = topico;
        this.origen = origen;
        this.usuario = usuario;
        this.fecha = fecha;
        this.data = data;
    }

    public void setEvento(String evento) {
        this.evento = evento;
    }

    public void setTopico(String topico) {
        this.topico = topico;
    }

    public void setOrigen(String origen) {
        this.origen = origen;
    }

    public void setUsuario(String usuario) {
        this.usuario = usuario;
    }

    public void setFecha(Date fecha) {
        this.fecha = fecha;
    }

    public void setData(String data) {
        this.data = data;
    }
}
