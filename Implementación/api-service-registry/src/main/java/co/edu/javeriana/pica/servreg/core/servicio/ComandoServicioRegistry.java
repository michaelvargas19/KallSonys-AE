package co.edu.javeriana.pica.servreg.core.servicio;

import java.util.logging.Logger;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import javax.transaction.Transactional;

import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceCapability;
import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceRegistry;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceCapabilityEntity;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceCapabilityRepository;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceRegistryEntity;
import co.edu.javeriana.pica.servreg.infra.repository.ServiceRegistryRepository;

@ApplicationScoped
@Transactional
public class ComandoServicioRegistry {
    @Inject
    ServiceRegistryRepository serviceRegistryRepository;
    @Inject
    ServiceCapabilityRepository serviceCapabilityRepository;

    private static Logger LOGGER = Logger.getLogger(ComandoServicioRegistry.class.getName());

    public boolean adicionarServicio(ServiceRegistry model){
        ServiceRegistryEntity entity = new ServiceRegistryEntity(
            model.getNombre(), 
        model.getDescripcion(), 
        model.getRuta(), 
        model.getProtocolo(), 
        model.getIdProveedor(),
        model.getEstado()); 
        return serviceRegistryRepository.insertarServiceRegistry(entity);
    }

    public boolean actualizarServicio(ServiceRegistry model){
        ServiceRegistryEntity entity = new ServiceRegistryEntity(model.getId(),
            model.getNombre(), 
        model.getDescripcion(), 
        model.getRuta(), 
        model.getProtocolo(), 
        model.getIdProveedor(),
        model.getEstado()); 
        return serviceRegistryRepository.actualizarServiceRegistry(entity);
    }

    public boolean adicionarCapacidad(ServiceCapability model){

        //buscar por servicio por ID
        ServiceRegistryEntity serviceRegistryEntity = serviceRegistryRepository.encontrarPorId(model.getIdServicio());

        ServiceCapabilityEntity entity = new ServiceCapabilityEntity(serviceRegistryEntity,
        model.getNombre(), 
        model.getDescripcion(), 
        model.getMetodoHTTP(), 
        model.getPlantillaRequest(), 
        model.getPlantillaResponse(), 
        model.getEstado(), model.getPath());
        return serviceCapabilityRepository.insertarServiceCapability(entity);
    }    

    public boolean actualizarCapacidad(ServiceCapability model){

        //buscar por servicio por ID
        ServiceRegistryEntity serviceRegistryEntity = serviceRegistryRepository.encontrarPorId(model.getIdServicio());

        ServiceCapabilityEntity entity = new ServiceCapabilityEntity(model.getId(),
                serviceRegistryEntity,
        model.getNombre(), 
        model.getDescripcion(), 
        model.getMetodoHTTP(), 
        model.getPlantillaRequest(), 
        model.getPlantillaResponse(), 
        model.getEstado(),
                model.getPath());
        return serviceCapabilityRepository.actualizarServiceCapability(entity);
    }

}
