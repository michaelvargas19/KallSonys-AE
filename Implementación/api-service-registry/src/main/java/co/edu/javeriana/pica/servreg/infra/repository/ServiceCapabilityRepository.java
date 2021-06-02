package co.edu.javeriana.pica.servreg.infra.repository;

import java.util.HashMap;
import java.util.Map;
import java.util.logging.Logger;

import javax.enterprise.context.ApplicationScoped;
import io.quarkus.hibernate.orm.panache.PanacheRepository;

@ApplicationScoped
public class ServiceCapabilityRepository implements PanacheRepository<ServiceCapabilityEntity>{
    
    private static Logger LOGGER = Logger.getLogger(ServiceCapabilityRepository.class.getName());

    public boolean insertarServiceCapability(ServiceCapabilityEntity entity){
        boolean result = false;
        try{
            persist(entity);
            result = true;
        }catch(Exception ex){
            result = false;
            LOGGER.warning("No guardo la capacidad del servicio: " + ex.getMessage());
        }
        return result;
    }

    public boolean actualizarServiceCapability(ServiceCapabilityEntity entity){
        boolean result = false;
        Map<String, Object> params = new HashMap<>();
        params.put("idServicio", entity.getIdServicio());
        params.put("nombre", entity.getNombre());
        params.put("descripcion", entity.getDescripcion());
        params.put("metodoHTTP", entity.getMetodoHTTP());
        params.put("plantillaRequest", entity.getPlantillaRequest());
        params.put("plantillaResponse", entity.getPlantillaResponse());
        params.put("estado", entity.getEstado());
        params.put("path", entity.getPath());
        params.put("id", entity.getId());
        try{
            String strUpdate = "idServicio = :idServicio, nombre = :nombre, descripcion = :descripcion, metodoHTTP = :metodoHTTP, plantillaRequest = :plantillaRequest, plantillaResponse = :plantillaResponse, estado = :estado, path = :path where id = :id";
            if(update(strUpdate, params) > 0){
                result = true;
            }
        }catch(Exception ex){
            result = false;
            LOGGER.warning("No actualizo la capacidad del servicio: " + ex.getMessage());
        }    
        return result;
    }
}
