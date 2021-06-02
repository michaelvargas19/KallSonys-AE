package co.edu.javeriana.pica.servreg.infra.repository;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Logger;

import javax.enterprise.context.ApplicationScoped;
import io.quarkus.hibernate.orm.panache.PanacheRepository;

@ApplicationScoped
public class ServiceRegistryRepository implements PanacheRepository<ServiceRegistryEntity>{
    
    private static Logger LOGGER = Logger.getLogger(ServiceRegistryRepository.class.getName());

    public boolean insertarServiceRegistry(ServiceRegistryEntity entity){
        boolean result = false;
        try{
            persist(entity);
            result = true;
        }catch(Exception ex){
            result = false;
            LOGGER.warning("No se guardo el servicio: " + ex.getMessage());
        }
        return result;
    }

    public boolean actualizarServiceRegistry(ServiceRegistryEntity entity){
        boolean result = false;
        Map<String, Object> params = new HashMap<>();
        params.put("nombre", entity.getNombre());
        params.put("descripcion", entity.getDescripcion());
        params.put("ruta", entity.getRuta());
        params.put("protocolo", entity.getProtocolo());
        params.put("idProveedor", entity.getIdProveedor());
        params.put("estado", entity.getEstado());
        params.put("id", entity.getId());
        
        try{
            String strUpdate = "nombre = :nombre, descripcion = :descripcion, ruta = :ruta, protocolo = :protocolo, idProveedor = :idProveedor, estado = :estado where id = :id";
            if(update(strUpdate, params) > 0 ){
                result = true;
            }

        }catch(Exception ex){
            result = false;
            LOGGER.warning("No se actualizo el servicio: " + ex.getMessage());
        }


        return result;
    }

    public List<ServiceRegistryEntity> encontrarTodos(){
        List<ServiceRegistryEntity> result = null;
        try{
            result = listAll();
            LOGGER.info("Servicio encontrados: " + result.size());
        }catch(Exception ex){
            LOGGER.warning("No se encontraron los servicios: " + ex.getMessage());
        }
        return result;
    }

    public ServiceRegistryEntity encontrarPorId(Long id){
        ServiceRegistryEntity result = null;
        try{
            result = findById(id);
        }catch(Exception ex){
            LOGGER.warning("No se encontro el servicio: " + ex.getMessage());
        }
        return result;
    }


    public ServiceRegistryEntity encontrarPorNombre(String idProveedor, String nombre){
        ServiceRegistryEntity result = null;
        Map<String, Object> params = new HashMap<>();
        params.put("nombre", nombre);
        params.put("idProveedor", idProveedor);
        try{
            result = find("select sr from ServiceRegistryEntity sr, ServiceCapabilityEntity sc " +
                    "where sc.idServicio = sr.id and sc.nombre = :nombre and sr.idProveedor = :idProveedor", params).firstResult();


        }catch(Exception ex){
            result = null;
            LOGGER.warning("No se encontro el servicio: " + ex.getMessage());
        }
        return result;
    }
    
}
