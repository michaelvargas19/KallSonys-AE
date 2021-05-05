package co.edu.javeriana.pica.proveedores.infra.repository;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import javax.enterprise.context.ApplicationScoped;
import io.quarkus.hibernate.orm.panache.PanacheRepository;

@ApplicationScoped
public class ProveedorRepository implements PanacheRepository<ProveedorEntity>{
    

    public ProveedorEntity encontrarPorId(Long id){
        return findById(id);
    }

    public List<ProveedorEntity> encontrarTodos(){
        return listAll();
    }

    public ProveedorEntity encontrarPorTipoDocumento(String tipoDocumento, String codigo){
        Map<String, Object> params = new HashMap<>();
        params.put("tipoDocumento", tipoDocumento);
        params.put("codigo", codigo);
        return find("tipoDocumento = :tipoDocumento and codigo = :codigo", params).firstResult();
    }

    public void insertarProveedor(ProveedorEntity entity){
        persist(entity);
    }

    public int actualizarProveedor(ProveedorEntity entity){
        Map<String, Object> params = new HashMap<>();
        params.put("razonSocial", entity.getRazonSocial());
        params.put("correo", entity.getCorreo());
        params.put("celular", entity.getCelular());
        params.put("id", entity.getId());
        return update("razonSocial = :razonSocial, correo = :correo, celular = :celular where id = :id", params);
    }
    
}
