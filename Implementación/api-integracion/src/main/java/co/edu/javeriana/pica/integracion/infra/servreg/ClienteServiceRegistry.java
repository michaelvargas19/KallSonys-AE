package co.edu.javeriana.pica.integracion.infra.servreg;

import co.edu.javeriana.pica.integracion.core.modelo.query.CapacidadServicio;
import org.eclipse.microprofile.rest.client.inject.RestClient;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import java.util.logging.Logger;

@ApplicationScoped
public class ClienteServiceRegistry {
    @Inject
    @RestClient
    IServiceRegistry serviceRegistry;

    private static Logger LOGGER = Logger.getLogger(ClienteServiceRegistry.class.getName());

    public CapacidadServicio obtenerServicioProveedor(Long idProveedor, String capacidad){
        CapacidadServicio result = null;
        try {
            LOGGER.info("Antes de ejecutar el cliente");
            result = serviceRegistry.obtenerCapacidadServicio(idProveedor.toString(), capacidad);
            LOGGER.info("Despues de ejecutar el cliente: " + result.getDescripcionCapacidad());
        }catch (Exception ex){
            LOGGER.warning("Error consumiendo servicio service registry: " + ex.getMessage());
        }

        return result;
    }

}
