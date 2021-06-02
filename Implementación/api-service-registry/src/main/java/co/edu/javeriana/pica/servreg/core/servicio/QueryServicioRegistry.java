package co.edu.javeriana.pica.servreg.core.servicio;

import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;
import java.util.stream.Collectors;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceCapability;
import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceRegistry;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceCapabilityEntity;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceRegistryEntity;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceRegistryRepository;



@ApplicationScoped
public class QueryServicioRegistry {
    @Inject
    ServiceRegistryRepository serviceRegistryRepository;

    private static Logger LOGGER = Logger.getLogger(QueryServicioRegistry.class.getName());


    public List<ServiceRegistry> obtenerServicios(){
        List<ServiceRegistry> result = new ArrayList<ServiceRegistry>();
        
            List<ServiceRegistryEntity> servicios = serviceRegistryRepository.encontrarTodos();
            
            result = servicios.stream().map(serviceEntity -> new ServiceRegistry(serviceEntity.getId(), 
                serviceEntity.getNombre(), 
                serviceEntity.getDescripcion(), 
                serviceEntity.getRuta(), 
                serviceEntity.getProtocolo(), 
                serviceEntity.getIdProveedor(),
                serviceEntity.getEstado(),
                transformaCapacidades(serviceEntity.getCapacidades())
            )).collect(Collectors.toList());      

        
        //devolver resultado
        return result;
    }

    private List<ServiceCapability> transformaCapacidades(List<ServiceCapabilityEntity> capacidades){
        List<ServiceCapability> result = null;
        LOGGER.info("cantidad de capacidades: " + capacidades.size());
        result = capacidades.stream().map(serviceCapabilityEntity -> new ServiceCapability(serviceCapabilityEntity.getId(),
                serviceCapabilityEntity.getIdServicio(),
                serviceCapabilityEntity.getNombre(),
                serviceCapabilityEntity.getDescripcion(),
                serviceCapabilityEntity.getMetodoHTTP(),
                serviceCapabilityEntity.getPlantillaRequest(),
                serviceCapabilityEntity.getPlantillaResponse(),
                serviceCapabilityEntity.getEstado(),
                serviceCapabilityEntity.getPath())).collect(Collectors.toList());
        return result;
    }


    public ServiceRegistry obtenerServicio(Long id){
        ServiceRegistry result = null;
        ServiceRegistryEntity serviceEntity = serviceRegistryRepository.encontrarPorId(id);
        if(serviceEntity != null){
            result = new ServiceRegistry(serviceEntity.getId(), 
            serviceEntity.getNombre(), 
            serviceEntity.getDescripcion(), 
            serviceEntity.getRuta(), 
            serviceEntity.getProtocolo(), 
            serviceEntity.getIdProveedor(),
            serviceEntity.getEstado(),
            transformaCapacidades(serviceEntity.getCapacidades()));
        }
        return result;
    }

    public ServiceRegistry obtenerServicioPorCapacidad(String idProveedor, String nombreCapacidad){
        ServiceRegistry result = null;
        ServiceRegistryEntity serviceEntity = serviceRegistryRepository.encontrarPorNombre(idProveedor, nombreCapacidad);
        if(serviceEntity != null){
            result = new ServiceRegistry(serviceEntity.getId(), 
            serviceEntity.getNombre(), 
            serviceEntity.getDescripcion(), 
            serviceEntity.getRuta(), 
            serviceEntity.getProtocolo(), 
            serviceEntity.getIdProveedor(),
            serviceEntity.getEstado(),
            transformaCapacidades(serviceEntity.getCapacidades()));
        }

        return result;
    }

}
