package co.edu.javeriana.pica.servreg.app.acciones;

import java.util.List;
import java.util.logging.Logger;

import javax.inject.Inject;
import javax.json.JsonArray;
import javax.json.JsonObject;
import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceCapability;
import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceRegistry;
import co.edu.javeriana.pica.servreg.core.modelo.query.ServiceRegistryCapability;
import co.edu.javeriana.pica.servreg.core.servicio.ComandoServicioRegistry;
import co.edu.javeriana.pica.servreg.core.servicio.QueryServicioRegistry;

@Path("services")
public class ServiceRegistryController {
    @Inject
    ComandoServicioRegistry comandoRegistroServicio;
    @Inject
    QueryServicioRegistry queryServicioRegistry;
    
    private static Logger LOGGER = Logger.getLogger(ServiceRegistryController.class.getName());


    @GET
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject obtenerTodos() {
        LOGGER.info("Antes de ejecutar servicio");
        List<ServiceRegistryCapability> result = queryServicioRegistry.obtenerServicios();
        JsonObject dto = ModeloToDTO.obtenerTodosToPayload(result);
        LOGGER.info("Retorna: " + dto.toString());
        return dto;
    }

    @GET
    @Path("{id}")
    @Produces(MediaType.APPLICATION_JSON)
    public JsonArray buscarPorId(@PathParam("id") Long id) {
        ServiceRegistry result = queryServicioRegistry.obtenerServicio(id);
        return ModeloToDTO.buscarPorIdToPayload(result);
    }

    @GET
    @Path("query")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject buscarPorNombreCapacidad(@QueryParam("idProveedor") String idProveedor, @QueryParam("nombreCapacidad") String nombreCapacidad) {
        
        ServiceRegistryCapability result = queryServicioRegistry.obtenerServicioPorCapacidad(idProveedor, nombreCapacidad);
        
        
        return ModeloToDTO.buscarPorNombreCapacidadToPayload(result);
    }

    @POST
    @Path("add")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject adicionarServicio(ServiceRegistry request) {
        LOGGER.info("Antes de ejecutar servicio");
        boolean result = comandoRegistroServicio.adicionarServicio(request);

        return ModeloToDTO.respuestaPersistencia(result);
    }

    @PUT
    @Path("update")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject actualizarServicio(ServiceRegistry request) {
        LOGGER.info("Antes de ejecutar servicio");
        boolean result = comandoRegistroServicio.actualizarServicio(request);
                
        return ModeloToDTO.respuestaActualizacion(result);
    }

    @POST
    @Path("capability/add")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject adicionarCapacidad(ServiceCapability request) {
        LOGGER.info("Antes de ejecutar servicio");
        boolean result = comandoRegistroServicio.adicionarCapacidad(request);
        return ModeloToDTO.respuestaPersistencia(result);
    }

    @PUT
    @Path("capability/update")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public JsonObject actualizarCapacidad(ServiceCapability request) {
        LOGGER.info("Antes de ejecutar servicio");
        boolean result = comandoRegistroServicio.actualizarCapacidad(request);
                
        return ModeloToDTO.respuestaActualizacion(result);
    }

}
