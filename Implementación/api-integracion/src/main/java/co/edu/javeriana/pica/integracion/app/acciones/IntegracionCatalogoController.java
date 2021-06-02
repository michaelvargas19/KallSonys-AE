package co.edu.javeriana.pica.integracion.app.acciones;

import co.edu.javeriana.pica.integracion.core.servicio.ServicioQueryProductos;

import javax.inject.Inject;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import java.util.logging.Logger;

@Path("/integracion/catalogo")
public class IntegracionCatalogoController {
    @Inject
    ServicioQueryProductos servicioQueryProductos;

    private static Logger LOGGER = Logger.getLogger(IntegracionCatalogoController.class.getName());

    @GET
    @Path("query")
    @Produces(MediaType.APPLICATION_JSON)
    public String obtenerTodos(@QueryParam("idProveedor") Long idProveedor) {

        String catalogo = servicioQueryProductos.obtenerTodos(idProveedor);

        return catalogo;

    }

    @GET
    @Path("find")
    @Produces(MediaType.APPLICATION_JSON)
    public String buscarProducto(@QueryParam("idProveedor") Long idProveedor, @QueryParam("codigo") String codigo) {

        String catalogo = servicioQueryProductos.buscarProducto(idProveedor, codigo);

        return catalogo;

    }

}