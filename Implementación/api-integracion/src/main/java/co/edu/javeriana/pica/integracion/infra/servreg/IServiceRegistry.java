package co.edu.javeriana.pica.integracion.infra.servreg;

import co.edu.javeriana.pica.integracion.core.modelo.query.CapacidadServicio;
import org.eclipse.microprofile.rest.client.inject.RegisterRestClient;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.QueryParam;
import java.util.List;

@RegisterRestClient
public interface IServiceRegistry {

    @GET
    @Path("/query")
    public CapacidadServicio obtenerCapacidadServicio(@QueryParam("idProveedor") String idProveedor, @QueryParam("nombreCapacidad") String nombreCapacidad);
}
