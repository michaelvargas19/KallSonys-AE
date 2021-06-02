package co.edu.javeriana.pica.integracion.infra.servreg;

import co.edu.javeriana.pica.integracion.core.modelo.query.CapacidadServicio;
import org.eclipse.microprofile.rest.client.inject.RestClient;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.Invocation;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.Response;
import java.util.List;
import java.util.logging.Logger;

@ApplicationScoped
public class ClienteServiceRegistry {
    @Inject
    @RestClient
    IServiceRegistry serviceRegistry;

    private static Logger LOGGER = Logger.getLogger(ClienteServiceRegistry.class.getName());

    public CapacidadServicio obtenerServicioProveedor(Long idProveedor, String capacidad){
        CapacidadServicio servicio = null;
        List<CapacidadServicio> result = null;
        try {
            LOGGER.info("Antes de ejecutar el cliente");
            result = serviceRegistry.obtenerCapacidadServicio(idProveedor.toString(), capacidad);
            for (CapacidadServicio  capacidadServicio: result ) {
                if (capacidadServicio.getNombreCapacidad().equals(capacidad) == true){
                    servicio = capacidadServicio;
                    break;
                }
            }

            LOGGER.info("Despues de ejecutar el cliente: " + result.size());
        }catch (Exception ex){
            LOGGER.warning("Error consumiendo servicio service registry: " + ex.getMessage());
        }

        return servicio;
    }

    private CapacidadServicio obtenerDataPrueba(){
        CapacidadServicio result = new CapacidadServicio();
        result.setId(1L);
        result.setIdProveedor("1");
        result.setNombreServicio("test");
        result.setDescripcionServicio("servicio de prueba");
        result.setRuta("http://localhost:8080/catalog/");
        result.setProtocolo("REST");
        result.setEstado("ACTIVO");

        result.setIdCapacidad(1L);
        result.setNombreCapacidad("Buscar por texto");
        result.setMetodoHTTP("GET");
        result.setDescripcionCapacidad("Permite la busqueda de producto texto ");
        result.setPath("/query");
        result.setPlantillaRequest("{ \"texto\" : \"$texto\"}");
        result.setPlantillaResponse("{ " +
                "productos [ " +
                "#foreach($producto in $arregloProducto)" +
                "{" +
                "\"sku\" : \"$producto.SKU\" ," +
                "\"nombre\" : \"$producto.nombre\" " +
                "}" +
                "#end" +
                "]" +
                "}");
        result.setEstado("ACTIVO");
        return result;
    }
}
