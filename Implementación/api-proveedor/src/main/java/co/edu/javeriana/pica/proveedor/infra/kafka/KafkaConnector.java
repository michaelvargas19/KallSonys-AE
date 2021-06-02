package co.edu.javeriana.pica.proveedor.infra.kafka;

import co.edu.javeriana.pica.proveedor.core.modelo.Proveedor;
import org.eclipse.microprofile.reactive.messaging.Channel;
import org.eclipse.microprofile.reactive.messaging.Emitter;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

@ApplicationScoped
public class KafkaConnector {

    @Inject @Channel("proveedor-notificado")
    Emitter<DTOProveedor> emitter;

    public void notificar(DTOProveedor proveedor){

        emitter.send(proveedor);
    }

}
