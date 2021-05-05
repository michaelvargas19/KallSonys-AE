package co.edu.javeriana.pica.proveedores.app.acciones;

import javax.json.Json;
import javax.json.JsonArrayBuilder;
import javax.json.JsonObject;
import javax.json.JsonObjectBuilder;
import java.util.List;
import co.edu.javeriana.pica.proveedores.core.modelo.Proveedor;

public class ModelToDTO {

    public static JsonObject obtenerTodosToPayload(List<Proveedor> model) {
        JsonObjectBuilder response = Json.createObjectBuilder();        
        JsonArrayBuilder arrayB = Json.createArrayBuilder();
        for (Proveedor proveedor : model) {            
            arrayB.add(modelProveedorToJSON(proveedor));
        }
        response.add("status", "OK");
        response.add("payload", arrayB);
        return response.build();
    }

    public static JsonObject buscarPorIdToPayload(Proveedor model) {
        JsonObjectBuilder response = Json.createObjectBuilder(); 
        response.add("status", "OK");
        response.add("payload", modelProveedorToJSON(model));
        return response.build();
    }

    private static JsonObjectBuilder modelProveedorToJSON(Proveedor model){
        JsonObjectBuilder objProveedor = Json.createObjectBuilder();        
        JsonObjectBuilder objectBuilder = Json.createObjectBuilder();
        objectBuilder.add("id", model.getId());
        objectBuilder.add("codigo", model.getCodigo());
        objectBuilder.add("tipoDocumento", model.getTipoDocumento());
        objectBuilder.add("razonSocial", model.getRazonSocial());
        objectBuilder.add("correo", model.getCorreo());
        objectBuilder.add("celular", model.getCelular());
        objProveedor.add("proveedor", objectBuilder);
        return objProveedor;
    }

    public static JsonObject respuestaAdicionarProveedor() {
        JsonObjectBuilder response = Json.createObjectBuilder(); 
        response.add("status", "OK");
        response.add("payload", "Proveedor almacenado satisfactoriamente");
        return response.build();
    }
    
}
