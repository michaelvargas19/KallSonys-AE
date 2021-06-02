package co.edu.javeriana.pica.proveedor.infra.kafka;

import co.edu.javeriana.pica.proveedor.core.modelo.Proveedor;
import io.quarkus.kafka.client.serialization.ObjectMapperDeserializer;

public class ProveedorDeserializer extends ObjectMapperDeserializer<DTOProveedor> {

    public ProveedorDeserializer(){
        super(DTOProveedor.class);
    }
}
