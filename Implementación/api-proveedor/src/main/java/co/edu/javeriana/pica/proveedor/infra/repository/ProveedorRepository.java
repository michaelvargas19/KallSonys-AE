package co.edu.javeriana.pica.proveedor.infra.repository;

import io.quarkus.hibernate.orm.panache.PanacheRepository;

import javax.enterprise.context.ApplicationScoped;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Logger;

@ApplicationScoped
public class ProveedorRepository implements PanacheRepository<ProveedorEntity>{
    
    private static Logger LOGGER = Logger.getLogger(ProveedorRepository.class.getName());

    public ProveedorEntity encontrarPorId(Long id){
        ProveedorEntity result = null;
        try{
            result = findById(id);
        }catch(Exception ex){
            LOGGER.warning("No se encontro el prooveedor: " + ex.getMessage());
        }
        return result;
    }

    public List<ProveedorEntity> encontrarTodos(){
        List<ProveedorEntity> result = null;
        try{
            result = listAll();
        }catch(Exception ex){
            LOGGER.warning("No se encontraron proveedores: " + ex.getMessage());
        }
        return result;
    }

    public ProveedorEntity encontrarPorTipoDocumento(String tipoDocumento, String codigo){
        ProveedorEntity result = null;        
        Map<String, Object> params = new HashMap<>();
        params.put("tipoDocumento", tipoDocumento);
        params.put("codigo", codigo);
        try{
            result = find("tipoDocumento = :tipoDocumento and codigo = :codigo", params).firstResult();
        }catch(Exception ex){
            LOGGER.warning("No se encontro el prooveedor: " + ex.getMessage());
        }
        return result;    
    }

    public boolean insertarProveedor(ProveedorEntity entity){
        boolean result = false;
        try{
            persist(entity);
            result = true;
        }catch(Exception ex){
            LOGGER.warning("No se persistio el prooveedor: " + ex.getMessage());
        }
        return result;
    }

    public boolean actualizarProveedor(ProveedorEntity entity){
        boolean result = false;
        Map<String, Object> params = new HashMap<>();
        params.put("razonSocial", entity.getRazonSocial());
        params.put("correo", entity.getCorreo());
        params.put("celular", entity.getCelular());
        params.put("id", entity.getId());
        try{
            String strUpdate = "razonSocial = :razonSocial, correo = :correo, celular = :celular where id = :id";
            if(update(strUpdate, params) > 0){
                result = true;
            }            
        }catch(Exception ex){
            LOGGER.warning("No se actualizo el prooveedor: " + ex.getMessage());
        }
        return result;
    }
    
}
