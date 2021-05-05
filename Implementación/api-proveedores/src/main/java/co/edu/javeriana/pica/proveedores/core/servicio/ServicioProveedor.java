package co.edu.javeriana.pica.proveedores.core.servicio;

import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

import org.eclipse.microprofile.faulttolerance.Fallback;
import org.eclipse.microprofile.faulttolerance.Retry;
import co.edu.javeriana.pica.proveedores.core.modelo.*;
import co.edu.javeriana.pica.proveedores.infra.repository.*;
import java.util.logging.Logger;

@ApplicationScoped
public class ServicioProveedor {

    @Inject
    ProveedorRepository proveedorRepository;

    private static Logger LOGGER = Logger.getLogger(ServicioProveedor.class.getName());

    
    @Retry(delay = 500, jitter = 150, maxRetries = 5, maxDuration = 3500)
    @Fallback(fallbackMethod = "fetchAllFallback")
    public List<Proveedor> fetchAll() {
        List<ProveedorEntity> listaProveedores = proveedorRepository.encontrarTodos();
        LOGGER.info("Proveedores encontrados: " + listaProveedores.size());
        List<Proveedor> result = listaProveedores.stream().map(proveedorEntity -> new Proveedor(proveedorEntity.getId(), 
        proveedorEntity.getTipoDocumento(), 
        proveedorEntity.getCodigo(), 
        proveedorEntity.getRazonSocial(), 
        proveedorEntity.getCorreo(), 
        proveedorEntity.getCelular())).collect(Collectors.toList());
        LOGGER.info("Proveedores parseados: " + result.size());
        return result;
    }

    private List<Proveedor> fetchAllFallback() {
        return Collections.emptyList();
    }

    public Proveedor buscarPorId(Long id) {
        ProveedorEntity proveedor = proveedorRepository.encontrarPorId(id);    
        LOGGER.info("Proveedor encontrado: " + proveedor.getCodigo());    
        Proveedor result = new Proveedor(proveedor.getId(), 
        proveedor.getTipoDocumento(), 
        proveedor.getCodigo(), 
        proveedor.getRazonSocial(), 
        proveedor.getCorreo(), 
        proveedor.getCelular());        
        return result;
    }

    public Proveedor buscarPorTipoDocumento(String tipoDocumento, String documento) {
        LOGGER.info("Parametros: " + tipoDocumento + " - " + documento);    
        ProveedorEntity proveedor = proveedorRepository.encontrarPorTipoDocumento(tipoDocumento, documento);   
        LOGGER.info("Proveedor encontrado: " + proveedor.getCodigo());       
        Proveedor result = new Proveedor(proveedor.getId(), 
        proveedor.getTipoDocumento(), 
        proveedor.getCodigo(), 
        proveedor.getRazonSocial(), 
        proveedor.getCorreo(), 
        proveedor.getCelular());        
        return result;
    }

    
    public void guardarProveedor(Proveedor proveedor){
        ProveedorEntity entity = new ProveedorEntity(
        proveedor.getTipoDocumento(), 
        proveedor.getCodigo(), 
        proveedor.getRazonSocial(), 
        proveedor.getCorreo(), 
        proveedor.getCelular()); 
        proveedorRepository.insertarProveedor(entity);
    } 

    public void actualizarProveedor(Proveedor proveedor){
        ProveedorEntity entity = new ProveedorEntity(
        proveedor.getId(), 
        proveedor.getRazonSocial(), 
        proveedor.getCorreo(), 
        proveedor.getCelular()); 
        proveedorRepository.actualizarProveedor(entity);
    } 


}
