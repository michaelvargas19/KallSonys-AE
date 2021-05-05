package co.edu.javeriana.pica.proveedores.app.acciones;

import co.edu.javeriana.pica.proveedores.core.modelo.Proveedor;
import co.edu.javeriana.pica.proveedores.core.servicio.ServicioProveedor;
import javax.inject.Inject;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;

import org.eclipse.microprofile.metrics.annotation.Counted;
import org.eclipse.microprofile.metrics.annotation.Metered;
import org.eclipse.microprofile.metrics.annotation.Timed;
import javax.ws.rs.core.MediaType;
import javax.json.JsonObject;
import javax.transaction.Transactional;

import java.util.List;
import java.util.logging.Logger;

@Path("proveedores")
@Transactional
public class ProveedorController {
    @Inject
    ServicioProveedor servicioProveedor;
    private static Logger LOGGER = Logger.getLogger(ProveedorController.class.getName());

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    @Timed(name = "ProveedoresResource_obtenerTodosRateTime", absolute = true, description = "Tiempo en obtener todos los proveedores")
    @Counted(name = "Proveedoresesource_obtenerTodosRateCount", absolute = true, description = "Numero de invocaciones de obtencion de todos los proveedores")
    @Metered(name = "ProveedoresResource_obtenerTodosRateMetered", tags = {"endpoint=rest"}, description = "Rendimiento de obtencion de todos los proveedores")
    public JsonObject obtenerTodos() {
        LOGGER.info("Antes de ejecutar servicio");
        List<Proveedor> result = servicioProveedor.fetchAll();
        
        return ModelToDTO.obtenerTodosToPayload(result);
    }

    @GET
    @Path("{id}")
    @Produces(MediaType.APPLICATION_JSON)
    @Timed(name = "ProveedoresResource_buscarPorIdRateTime", absolute = true, description = "Tiempo en buscar proveedor por ID")
    @Counted(name = "Proveedoresesource_buscarPorIdRateCount", absolute = true, description = "Numero de invocaciones de buscar proveedor por ID")
    @Metered(name = "ProveedoresResource_buscarPorIdRateMetered", tags = {"endpoint=rest"}, description = "Rendimiento de buscar proveedor por ID")
    public JsonObject buscarPorId(@PathParam("id") Long id) {
        
        Proveedor result = servicioProveedor.buscarPorId(id);
        
        
        return ModelToDTO.buscarPorIdToPayload(result);
    }

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    @Timed(name = "ProveedoresResource_buscarporTipoDocumentoRateTime", absolute = true, description = "Tiempo en buscar proveedor tipo de documento")
    @Counted(name = "Proveedoresesource_buscarporTipoDocumentoRateCount", absolute = true, description = "Numero de invocaciones de buscar por tipo de documento")
    @Metered(name = "ProveedoresResource_buscarporTipoDocumentoRateMetered", tags = {"endpoint=rest"}, description = "Rendimiento de buscar proveedor por tipo de documento")
    public JsonObject buscarporTipoDocumento(@QueryParam("tipoDocumento") String tipoDocumento, @QueryParam("documento") String documento) {
        LOGGER.info("Antes de ejecutar servicio: " + tipoDocumento + ":" + documento);
        Proveedor result = servicioProveedor.buscarPorTipoDocumento(tipoDocumento, documento);
        
        return ModelToDTO.buscarPorIdToPayload(result);
    }

    @POST
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    @Timed(name = "ProveedoresResource_adicionarProveedorRateTime", absolute = true, description = "Tiempo en adicionar un proveedor")
    @Counted(name = "Proveedoresesource_adicionarProveedorRateCount", absolute = true, description = "Numero de invocaciones de adicionar proveedor")
    @Metered(name = "ProveedoresResource_adicionarProveedorRateMetered", tags = {"endpoint=rest"}, description = "Rendimiento adición proveedor")
    public JsonObject adicionarProveedor(Proveedor request) {
        LOGGER.info("Antes de ejecutar servicio");
        servicioProveedor.guardarProveedor(request);

        return ModelToDTO.respuestaAdicionarProveedor();
    }

    @PUT
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    @Timed(name = "ProveedoresResource_actualizarProveedorRateTime", absolute = true, description = "Tiempo en actualizar un proveedor")
    @Counted(name = "Proveedoresesource_actualizarProveedorRateCount", absolute = true, description = "Numero de invocaciones de actualizar proveedor")
    @Metered(name = "ProveedoresResource_actualizarProveedorRateMetered", tags = {"endpoint=rest"}, description = "Rendimiento actualizar proveedor")
    public JsonObject actualizarProveedor(Proveedor request) {
        LOGGER.info("Antes de ejecutar servicio");
        servicioProveedor.actualizarProveedor(request);
                
        return ModelToDTO.respuestaAdicionarProveedor();
    }

}
