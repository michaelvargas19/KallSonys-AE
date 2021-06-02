package co.edu.javeriana.pica.proveedor.core.servicio;

import co.edu.javeriana.pica.proveedor.core.modelo.Proveedor;
import co.edu.javeriana.pica.proveedor.infra.repository.ProveedorEntity;
import co.edu.javeriana.pica.proveedor.infra.repository.ProveedorRepository;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import java.util.List;
import java.util.logging.Logger;
import java.util.stream.Collectors;

@ApplicationScoped
public class ServicioQueryProveedor {

    @Inject
    ProveedorRepository proveedorRepository;

    private static Logger LOGGER = Logger.getLogger(ServicioQueryProveedor.class.getName());

    public List<Proveedor> fetchAll() {
        List<Proveedor> result = null;
        List<ProveedorEntity> listaProveedores = proveedorRepository.encontrarTodos();
        if(listaProveedores != null && listaProveedores.size() > 0){
            LOGGER.info("Proveedores encontrados: " + listaProveedores.size());
            result = listaProveedores.stream().map(proveedorEntity -> new Proveedor(proveedorEntity.getId(), 
            proveedorEntity.getTipoDocumento(), 
            proveedorEntity.getCodigo(), 
            proveedorEntity.getRazonSocial(), 
            proveedorEntity.getCorreo(), 
            proveedorEntity.getCelular())).collect(Collectors.toList());
            LOGGER.info("Proveedores parseados: " + result.size());
        }       
        return result;
    }

    public Proveedor buscarPorId(Long id) {
        Proveedor result = null;
        ProveedorEntity proveedor = proveedorRepository.encontrarPorId(id);  
        if(proveedor != null){
            LOGGER.info("Proveedor encontrado: " + proveedor.getCodigo());    
            result = new Proveedor(proveedor.getId(), 
            proveedor.getTipoDocumento(), 
            proveedor.getCodigo(), 
            proveedor.getRazonSocial(), 
            proveedor.getCorreo(), 
            proveedor.getCelular());     
        } 
        return result;
    }

    public Proveedor buscarPorTipoDocumento(String tipoDocumento, String documento) {
        Proveedor result = null;
        LOGGER.info("Parametros: " + tipoDocumento + " - " + documento);    
        ProveedorEntity proveedor = proveedorRepository.encontrarPorTipoDocumento(tipoDocumento, documento);   
        if(proveedor != null){            
            LOGGER.info("Proveedor encontrado: " + proveedor.getCodigo());       
            result = new Proveedor(proveedor.getId(), 
            proveedor.getTipoDocumento(), 
            proveedor.getCodigo(), 
            proveedor.getRazonSocial(), 
            proveedor.getCorreo(), 
            proveedor.getCelular());       
        }         
        return result;
    }


}
