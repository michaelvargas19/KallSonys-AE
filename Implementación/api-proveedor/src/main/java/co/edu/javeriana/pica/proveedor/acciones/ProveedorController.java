package co.edu.javeriana.pica.proveedor.acciones;

import co.edu.javeriana.pica.proveedor.core.modelo.Proveedor;
import co.edu.javeriana.pica.proveedor.core.servicio.ServicioComandoProveedor;
import co.edu.javeriana.pica.proveedor.core.servicio.ServicioQueryProveedor;

import javax.inject.Inject;
import javax.json.JsonObject;
import javax.transaction.Transactional;
import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import java.util.List;
import java.util.logging.Logger;

@Path("proveedores")
@Transactional
public class ProveedorController {
    @Inject
    ServicioQueryProveedor servicioQueryProveedor;

    @Inject
    ServicioComandoProveedor servicioComandoProveedor;
    private static Logger LOGGER = Logger.getLogger(ProveedorController.class.getName());

    @GET
    @Path("list")
    @Produces(MediaType.APPLICATION_JSON)
    public JsonObject obtenerTodos() {
        LOGGER.info("Antes de ejecutar servicio");
        List<Proveedor> result = servicioQueryProveedor.fetchAll();
        
        return ModelToDTO.obtenerTodosToPayload(result);
    }

    @GET
    @Path("{id}")
    @Produces(MediaType.APPLICATION_JSON)
    public JsonObject buscarPorId(@PathParam("id") Long id) {
        
        Proveedor result = servicioQueryProveedor.buscarPorId(id);
        
        
        return ModelToDTO.buscarPorIdToPayload(result);
    }

    @GET
    @Path("find")
    @Produces(MediaType.APPLICATION_JSON)
    public JsonObject buscarporTipoDocumento(@QueryParam("tipoDocumento") String tipoDocumento, @QueryParam("documento") String documento) {
        LOGGER.info("Antes de ejecutar servicio: " + tipoDocumento + ":" + documento);
        Proveedor result = servicioQueryProveedor.buscarPorTipoDocumento(tipoDocumento, documento);
        
        return ModelToDTO.buscarPorIdToPayload(result);
    }

    @POST
    @Path("add")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject adicionarProveedor(Proveedor request) {
        LOGGER.info("Antes de ejecutar servicio");
        boolean result = servicioComandoProveedor.guardarProveedor(request);

        return ModelToDTO.respuestaAdicionarProveedor(result);
    }

    @PUT
    @Path("update")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject actualizarProveedor(Proveedor request) {
        LOGGER.info("Antes de actualizar");
        boolean result = servicioComandoProveedor.actualizarProveedor(request);
        LOGGER.info("despues de actualizar");
        return ModelToDTO.respuestaAdicionarProveedor(result);
    }

}
