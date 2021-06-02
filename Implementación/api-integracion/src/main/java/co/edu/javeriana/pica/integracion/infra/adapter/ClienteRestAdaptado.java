package co.edu.javeriana.pica.integracion.infra.adapter;

import co.edu.javeriana.pica.integracion.core.modelo.query.CapacidadServicio;
import co.edu.javeriana.pica.integracion.infra.servreg.Product;
import com.fasterxml.jackson.databind.ObjectMapper;

import javax.enterprise.context.ApplicationScoped;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.Invocation;
import javax.ws.rs.client.WebTarget;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Logger;

@ApplicationScoped
public class ClienteRestAdaptado {

    private static Logger LOGGER = Logger.getLogger(ClienteRestAdaptado.class.getName());

    public Product invocarServicio(CapacidadServicio capacidad, String request){
        Product result= null;
        Client client = ClientBuilder.newBuilder().newClient();
        WebTarget target = client.target(capacidad.getRuta());
        try{
            Map<String, Object> params = new ObjectMapper().readValue(request, HashMap.class);
            for (Map.Entry<String, Object> entry : params.entrySet()) {
                //webClient.query(entry.getKey(), entry.getValue());
                target = target.path(capacidad.getPath()).queryParam(entry.getKey(), entry.getValue());
            }
        }catch(Exception ex){
            LOGGER.info("Error convirtiendo parametros: "  + ex.getMessage());
        }

        //target = target.path(capacidad.getPath()).queryParam("idProveedor", idProveedor).queryParam("nombreCapacidad", capacidad);

        LOGGER.info("Target: " + target.getUri());
        Invocation.Builder builder = target.request();
        //Response response = builder.get();


        result = builder.get(Product.class);
        LOGGER.info("Response 2: " + result.toString());


        return result;
    }

    public List<Product> invocarServicio(CapacidadServicio capacidad){
        List<Product> result= new ArrayList<Product>();
        Client client = ClientBuilder.newBuilder().newClient();
        WebTarget target = client.target(capacidad.getRuta());
        try{
            target = target.path(capacidad.getPath());
        }catch(Exception ex){
            LOGGER.info("Error convirtiendo parametros: "  + ex.getMessage());
        }

        //target = target.path(capacidad.getPath()).queryParam("idProveedor", idProveedor).queryParam("nombreCapacidad", capacidad);

        LOGGER.info("Target: " + target.getUri());
        Invocation.Builder builder = target.request();
        //Response response = builder.get();


        result = builder.get(result.getClass());
        LOGGER.info("Response 2: " + result.toString());


        return result;
    }


}
