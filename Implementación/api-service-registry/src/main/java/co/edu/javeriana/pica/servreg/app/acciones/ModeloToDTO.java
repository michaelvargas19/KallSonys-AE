package co.edu.javeriana.pica.servreg.app.acciones;

import java.util.List;
import java.util.logging.Logger;

import javax.json.*;

import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceCapability;
import co.edu.javeriana.pica.servreg.core.modelo.cmd.ServiceRegistry;

public class ModeloToDTO {

    private static Logger LOGGER = Logger.getLogger(ModeloToDTO.class.getName());

    public static JsonObject obtenerTodosToPayload(List<ServiceRegistry> model) {
        LOGGER.info("Inicio metodo obtenerTodosToPayload, registros: " + model.size());
        JsonObjectBuilder response = Json.createObjectBuilder();        
             
        if(model != null && model.size() > 0){
            JsonArrayBuilder arrayB = Json.createArrayBuilder();
            for (ServiceRegistry servicio : model) {            
                arrayB.add(modelServicioToJSON(servicio));
            }   
            response.add("status", "OK");
            response.add("payload", arrayB);
        }else{
            response.add("status", "WARNING");
            response.add("payload", "No hay registros configurados");            
        }
        LOGGER.info("Response: " + response.toString());
        return response.build();
    }

    public static JsonArray buscarPorIdToPayload(ServiceRegistry model) {
        JsonArrayBuilder response = Json.createArrayBuilder();
        if(model != null ){
            //response.add("status", "OK");
            //response.add("payload", modelServicioToJSON(model));
            response =  modelServicioToJSON(model);
        }else{
            //response.add("status", "WARNING");
            //response.add("payload", "no se encontro el servicio.");
        }
        
        return response.build();
    }

    private static JsonArrayBuilder modelServicioToJSON(ServiceRegistry model){
        JsonObjectBuilder objServicio = Json.createObjectBuilder();
        JsonArrayBuilder arrayCapabilility = Json.createArrayBuilder();
        if(model.getCapacidades() != null && model.getCapacidades().size() > 0){
            for (ServiceCapability capacidad : model.getCapacidades()) {
                objServicio.add("idServicio", model.getId());
                objServicio.add("nombreServicio", model.getNombre());
                objServicio.add("descripcionServicio", model.getDescripcion());
                objServicio.add("ruta", model.getRuta());
                objServicio.add("protocolo", model.getProtocolo());
                objServicio.add("idProveedor", model.getIdProveedor());
                objServicio.add("estadoServicio", model.getEstado());
                objServicio.add("idCapacidad", capacidad.getId());
                objServicio.add("nombreCapacidad", capacidad.getNombre());
                objServicio.add("descripcionCapacidad", capacidad.getDescripcion());
                objServicio.add("metodoHTTP", capacidad.getMetodoHTTP());
                objServicio.add("plantillaRequest", capacidad.getPlantillaRequest());
                objServicio.add("plantillaResponse", capacidad.getPlantillaResponse());
                objServicio.add("estadoCapacidad", capacidad.getEstado());
                objServicio.add("path", capacidad.getPath());
                arrayCapabilility.add(objServicio);
            }
        }
        return arrayCapabilility;
    }
    

    public static JsonObject respuestaPersistencia(boolean result) {
        JsonObjectBuilder response = Json.createObjectBuilder(); 
        if(result == true){
            response.add("status", "OK");
            response.add("payload", "Servicio almacenado satisfactoriamente.");
        }else{
            response.add("status", "WARNING");
            response.add("payload", "No se persistio el registro."); 
        }        
        return response.build();
    }

    public static JsonObject respuestaActualizacion(boolean result) {
        JsonObjectBuilder response = Json.createObjectBuilder(); 
        if(result == true){
            response.add("status", "OK");
            response.add("payload", "Servicio actualizado satisfactoriamente.");
        }else{
            response.add("status", "WARNING");
            response.add("payload", "No se actualizo el registro."); 
        }        
        return response.build();
    }

}
