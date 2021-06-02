package co.edu.javeriana.pica.servreg.core.servicio;

import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;
import java.util.stream.Collectors;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceCapability;
import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceRegistry;
import co.edu.javeriana.pica.servreg.core.modelo.query.ServiceRegistryCapability;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceCapabilityEntity;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceCapabilityRepository;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceRegistryEntity;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceRegistryRepository;



@ApplicationScoped
public class QueryServicioRegistry {
    @Inject
    ServiceRegistryRepository serviceRegistryRepository;

    @Inject
    ServiceCapabilityRepository serviceCapabilityRepository;

    private static Logger LOGGER = Logger.getLogger(QueryServicioRegistry.class.getName());


    public List<ServiceRegistryCapability> obtenerServicios(){
        List<ServiceRegistryCapability> result = new ArrayList<ServiceRegistryCapability>();
        
            List<ServiceCapabilityEntity> servicios = serviceCapabilityRepository.listAll();
            
            result = servicios.stream().map(serviceEntity -> new ServiceRegistryCapability(serviceEntity.getServicio().getId(),
                serviceEntity.getServicio().getNombre(),
                serviceEntity.getServicio().getDescripcion(),
                serviceEntity.getServicio().getRuta(),
                serviceEntity.getServicio().getProtocolo(),
                serviceEntity.getServicio().getIdProveedor(),
                serviceEntity.getServicio().getEstado(),
                serviceEntity.getId(),
                serviceEntity.getNombre(),
                serviceEntity.getDescripcion(),
                serviceEntity.getMetodoHTTP(),
                serviceEntity.getPlantillaRequest(),
                serviceEntity.getPlantillaResponse(),
                serviceEntity.getEstado(),
                serviceEntity.getPath())
            ).collect(Collectors.toList());

        return result;
    }

    private List<ServiceCapability> transformaCapacidades(List<ServiceCapabilityEntity> capacidades){
        List<ServiceCapability> result = null;
        LOGGER.info("cantidad de capacidades: " + capacidades.size());
        result = capacidades.stream().map(serviceCapabilityEntity -> new ServiceCapability(serviceCapabilityEntity.getId(),
                serviceCapabilityEntity.getServicio().getId(),
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
            transformaCapacidades(serviceEntity.getServiceCapabilityEntities()));
        }
        return result;
    }

    public ServiceRegistryCapability obtenerServicioPorCapacidad(String idProveedor, String nombreCapacidad){
        ServiceRegistryCapability result = null;
        ServiceCapabilityEntity serviceEntity = serviceCapabilityRepository.encontrarPorNombre(idProveedor, nombreCapacidad);
        if(serviceEntity != null){
            result = new ServiceRegistryCapability(serviceEntity.getServicio().getId(),
                    serviceEntity.getServicio().getNombre(),
                    serviceEntity.getServicio().getDescripcion(),
                    serviceEntity.getServicio().getRuta(),
                    serviceEntity.getServicio().getProtocolo(),
                    serviceEntity.getServicio().getIdProveedor(),
                    serviceEntity.getServicio().getEstado(),
                    serviceEntity.getId(),
                    serviceEntity.getNombre(),
                    serviceEntity.getDescripcion(),
                    serviceEntity.getMetodoHTTP(),
                    serviceEntity.getPlantillaRequest(),
                    serviceEntity.getPlantillaResponse(),
                    serviceEntity.getEstado(),
                    serviceEntity.getPath());
        }

        return result;
    }

}
