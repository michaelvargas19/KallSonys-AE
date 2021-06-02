package co.edu.javeriana.pica.integracion.core.servicio;

import co.edu.javeriana.pica.integracion.infra.adapter.ClienteRest;
import co.edu.javeriana.pica.integracion.infra.servreg.ClienteServiceRegistry;
import co.edu.javeriana.pica.integracion.infra.servreg.Product;
import co.edu.javeriana.pica.integracion.core.modelo.query.CapacidadServicio;
import org.apache.velocity.VelocityContext;
import org.apache.velocity.app.Velocity;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

@ApplicationScoped
public class ServicioQueryProductos {
    @Inject
    ClienteServiceRegistry clienteServiceRegistry;

    @Inject
    ClienteRest clienteRest;

    private static Logger LOGGER = Logger.getLogger(ServicioQueryProductos.class.getName());

    public String obtenerProductosProveedorPorTexto(Long idProveedor, String texto){
        //buscar configuracion del servicio
        LOGGER.info("Antes de buscar el servicio");
        CapacidadServicio serviceRegistry = clienteServiceRegistry.obtenerServicioProveedor(idProveedor, "BUSCAR");
        LOGGER.info("Despues de buscar el servicio: " + serviceRegistry.getNombreServicio() + " - " + serviceRegistry.getNombreCapacidad());
        //mapear el request
        String request = mapearRequestBuscarPorTexto("texto", texto,serviceRegistry.getPlantillaRequest());

        //hacer llamado
        ArrayList<Product> productos = new ArrayList<>();
        LOGGER.info("Antes de buscar productos y setear al Canonico");
        productos = (ArrayList<Product>) clienteRest.invocarServicio(serviceRegistry, request, productos.getClass());

        LOGGER.info("Despues de buscar productos: " + productos.size());

        String response = mapearResponseBuscarPorTexto(idProveedor, productos, serviceRegistry.getPlantillaResponse());
        return response;
    }

    private String mapearRequestBuscarPorTexto(String parametro, String valorParametro, String plantilla){
        VelocityContext context = new VelocityContext();
        context.put(parametro, valorParametro);

        StringWriter output = new StringWriter();
        LOGGER.info("Parametro: " + parametro + " - Plantilla: " + plantilla);
        // evaluate the template string and merge them together
        Velocity.evaluate(context, output, "log or null", plantilla);

        LOGGER.info("mapearRequestBuscarPorTexto: " + output);
        return output.toString();
    }

    private String mapearResponseBuscarPorTexto(Long idProveedor, List<Product> arregloProducto, String plantilla){
        VelocityContext context = new VelocityContext();
        context.put("arregloProducto", arregloProducto);

        StringWriter output = new StringWriter();
        LOGGER.info("Productos: " + arregloProducto.getClass() + " - Plantilla: " + plantilla);
        // evaluate the template string and merge them together
        Velocity.evaluate(context, output, "log or null", plantilla);

        context.put("idProveedor", idProveedor);
        StringWriter output2 = new StringWriter();
        Velocity.evaluate(context, output2, "log or null", output.toString());

        LOGGER.info("mapearResponseBuscarPorTexto: " + output2);
        return output2.toString();
    }


    public String buscarProducto(Long idProveedor, String codigo){
        String response = null;
        //buscar configuracion del servicio
        LOGGER.info("Antes de buscar producto en el servicio BUSCARPRODUCTO: " + codigo);
        CapacidadServicio serviceRegistry = clienteServiceRegistry.obtenerServicioProveedor(idProveedor, "BUSCARPRODUCTO");
        if(serviceRegistry != null){

            LOGGER.info("Despues de buscar producto en el servicio: " + serviceRegistry.getNombreServicio()+ " - " + serviceRegistry.getNombreCapacidad());
            //mapear el request
            String request = mapearRequestBuscarPorTexto("code" ,codigo, serviceRegistry.getPlantillaRequest());

            //hacer llamado
            Product producto = new Product();
            LOGGER.info("Antes de buscar productos y setear al Canonico");
            producto = (Product) clienteRest.invocarServicio(serviceRegistry, request, producto.getClass());

            LOGGER.info("Despues de buscar productos: " + producto.getName());

            response = mapearResponseObjetoProducto(idProveedor, producto, serviceRegistry.getPlantillaResponse());
        }else{
            response = "{\"status\": \"WARNING\", \"message\": \"No de encontro la configuracion del servicio a consumir del proveedor.\"}";
        }

        return response;
    }

    private String mapearResponseObjetoProducto(Long idProveedor, Product product, String plantilla){
        VelocityContext context = new VelocityContext();
        context.put("product", product);

        StringWriter output = new StringWriter();
        LOGGER.info("Producto: " + product.getName() + " - Plantilla: " + plantilla);
        // evaluate the template string and merge them together
        Velocity.evaluate(context, output, "log or null", plantilla);

        context.put("idProveedor", idProveedor);
        StringWriter output2 = new StringWriter();
        Velocity.evaluate(context, output2, "log or null", output.toString());

        LOGGER.info("mapearResponseObjetoProducto: " + output2);
        return output2.toString();
    }

}
