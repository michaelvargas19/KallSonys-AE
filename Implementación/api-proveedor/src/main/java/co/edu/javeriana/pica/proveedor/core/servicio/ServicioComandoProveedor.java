package co.edu.javeriana.pica.proveedor.core.servicio;


import co.edu.javeriana.pica.proveedor.core.modelo.Proveedor;
import co.edu.javeriana.pica.proveedor.infra.kafka.DTOProveedor;
import co.edu.javeriana.pica.proveedor.infra.kafka.KafkaConnector;
import co.edu.javeriana.pica.proveedor.infra.repository.ProveedorEntity;
import co.edu.javeriana.pica.proveedor.infra.repository.ProveedorRepository;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import javax.transaction.Transactional;
import java.util.Date;
import java.util.logging.Logger;

@ApplicationScoped
@Transactional
public class ServicioComandoProveedor {

    @Inject
    ProveedorRepository proveedorRepository;
    @Inject
    KafkaConnector kafkaConnector;
    @Inject
    ServicioQueryProveedor servicioQueryProveedor;

    private static Logger LOGGER = Logger.getLogger(ServicioComandoProveedor.class.getName());


    public boolean guardarProveedor(Proveedor proveedor){
        boolean result = false;
        ProveedorEntity entity = new ProveedorEntity(
        proveedor.getTipoDocumento(), 
        proveedor.getCodigo(), 
        proveedor.getRazonSocial(), 
        proveedor.getCorreo(), 
        proveedor.getCelular()); 
        result = proveedorRepository.insertarProveedor(entity);
        if(result == true){
            Proveedor proveedorAlmacenado = servicioQueryProveedor.buscarPorTipoDocumento(proveedor.getTipoDocumento(), proveedor.getCodigo());
            DTOProveedor dtoProveedor = new DTOProveedor();
            dtoProveedor.setEvento("ProveedorAgregado");
            dtoProveedor.setFecha(new Date());
            dtoProveedor.setTopico("TP_Proveedores");
            dtoProveedor.setOrigen("api-proveedores");
            dtoProveedor.setData(proveedorAlmacenado);
            kafkaConnector.notificar(dtoProveedor);
        }
        return result;
    } 

    public boolean actualizarProveedor(Proveedor proveedor){
        boolean result = false;
        ProveedorEntity entity = new ProveedorEntity(
        proveedor.getId(), 
        proveedor.getRazonSocial(), 
        proveedor.getCorreo(), 
        proveedor.getCelular());
        LOGGER.info("Antes de actualizar proveedor");
        result = proveedorRepository.actualizarProveedor(entity);
        LOGGER.info("Despues de actualizar proveedor: " + result);
        if(result == true){
            LOGGER.info("Antes de notificar kafka");
            DTOProveedor dtoProveedor = new DTOProveedor();
            dtoProveedor.setEvento("ProveedorModificado");
            dtoProveedor.setFecha(new Date());
            dtoProveedor.setTopico("TP_Proveedores");
            dtoProveedor.setOrigen("api-proveedores");
            dtoProveedor.setData(proveedor);
            kafkaConnector.notificar(dtoProveedor);
            LOGGER.info("despues de notificar kafka");
        }
        return result;
    } 


}
