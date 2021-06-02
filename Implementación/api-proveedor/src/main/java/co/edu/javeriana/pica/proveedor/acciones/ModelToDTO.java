package co.edu.javeriana.pica.proveedor.acciones;

import co.edu.javeriana.pica.proveedor.core.modelo.Proveedor;

import javax.json.Json;
import javax.json.JsonArrayBuilder;
import javax.json.JsonObject;
import javax.json.JsonObjectBuilder;
import java.util.List;

public class ModelToDTO {

    public static JsonObject obtenerTodosToPayload(List<Proveedor> model) {
        JsonObjectBuilder response = Json.createObjectBuilder();   
        if(model != null && model.size() > 0){
            JsonArrayBuilder arrayB = Json.createArrayBuilder();
            for (Proveedor proveedor : model) {            
                arrayB.add(modelProveedorToJSON(proveedor));
            }
            response.add("status", "OK");
            response.add("payload", arrayB);
        }else{
            response.add("status", "WARNING");
            response.add("payload", "No encontro proveedores");
        }     
        
        return response.build();
    }

    public static JsonObject buscarPorIdToPayload(Proveedor model) {
        JsonObjectBuilder response = Json.createObjectBuilder(); 
        if(model != null){
            response.add("status", "OK");
            response.add("payload", modelProveedorToJSON(model));
        }else{
            response.add("status", "WARNING");
            response.add("payload", "No encontro el proveedor");
        }
        return response.build();
    }

    private static JsonObjectBuilder modelProveedorToJSON(Proveedor model){
        JsonObjectBuilder objectBuilder = Json.createObjectBuilder();
        objectBuilder.add("id", model.getId());
        objectBuilder.add("codigo", model.getCodigo());
        objectBuilder.add("tipoDocumento", model.getTipoDocumento());
        objectBuilder.add("razonSocial", model.getRazonSocial());
        objectBuilder.add("correo", model.getCorreo());
        objectBuilder.add("celular", model.getCelular());
        return objectBuilder;
    }

    public static JsonObject respuestaAdicionarProveedor(boolean result) {
        JsonObjectBuilder response = Json.createObjectBuilder(); 
        if(result == true){
            response.add("status", "OK");
            response.add("payload", "Proveedor almacenado satisfactoriamente");
        }else{
            response.add("status", "WARNING");
            response.add("payload", "No guardo el proveedor");
        }
        return response.build();
    }

    public static JsonObject respuestaActualizarProveedor(boolean result) {
        JsonObjectBuilder response = Json.createObjectBuilder(); 
        if(result == true){
            response.add("status", "OK");
            response.add("payload", "Proveedor actualizado satisfactoriamente");
        }else{
            response.add("status", "WARNING");
            response.add("payload", "No actualizo el proveedor");
        }
        return response.build();
    }
    
}
