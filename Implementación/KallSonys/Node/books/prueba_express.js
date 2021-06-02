**var request = require("request");

request("http://www.google.com", function(error,response, body)
{
    console.log("hola");
});


const express=require('express');
const app=express();
app.post('/',function(req,res)
{
        res.send('Aqui toy! En post');
});


app.get('/',(function(req,res)
{
    res.send("Aqui estoy en get");
}));

var server=app.listen(3000,function() {});
**/

//--------------- Ejercicio 2: Creación de métodos 
const express = require("express");
const bodyParser = require('body-parser');
const app = express();
const port = 8081

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
let usuario = {
    usuario:'ECM9018F',
    password: 'Colombia2021**'
};


let respuesta = {
 error: false,
 codigo: 200,

};

//Obtener
app.put('/WsPortalUsuariosRest-web/ws/WsPortalUsuariosRest/autentica/', function(req, res) {
    const usuario = req.usuario.usuario;
    const password = req.usuario.password;

    console.log("En PUT");
    
    respuesta = {
     error: true,
     codigo: 200,
     mensaje: res.json
    };
    res.send(respuesta);
});

/*app.get('/usuario', function (req, res) {
    respuesta = {
     error: false,
     codigo: 200,
     mensaje: ''
    };

    if(usuario.nombre == '' || usuario.apellido == '') {
     respuesta = {
      error: true,
      codigo: 501,
      mensaje: 'El usuario no ha sido creado'
     };
    } else {
     respuesta = {
      error: false,
      codigo: 200,
      mensaje: 'respuesta del usuario',
      respuesta: usuario
     };
    }
    res.send(respuesta);
});


//crear

/*{
    "nombre": "hola",
    "apellido": "granola"
}*/
/*
app.post('/usuario', function (req, res) {
    if(!req.body.nombre || !req.body.apellido) {
     respuesta = {
      error: true,
      codigo: 502,
      mensaje: 'El campo nombre y apellido son requeridos'
     };
    } else {
     if(usuario.nombre !== '' || usuario.apellido !== '') {
      respuesta = {
       error: true,
       codigo: 503,
       mensaje: 'El usuario ya fue creado previamente'
      };
     } else {
      usuario = {
       nombre: req.body.nombre,
       apellido: req.body.apellido
      };
      respuesta = {
       error: false,
       codigo: 200,
       mensaje: 'Usuario creado',
       respuesta: usuario
      };
     }
    }
    
    res.send(respuesta);
   });

   // Actualizar (PUT)
   /*
   {
        "nombre": "hi",
        "apellido": "hi"
    }
    */
   /*
   app.put('/usuario', function (req, res) {
    if(!req.body.nombre || !req.body.apellido) {
     respuesta = {
      error: true,
      codigo: 502,
      mensaje: 'El campo nombre y apellido son requeridos'
     };
    } else {
     if(usuario.nombre === '' || usuario.apellido === '') {
      respuesta = {
       error: true,
       codigo: 501,
       mensaje: 'El usuario no ha sido creado'
      };
     } else {
      usuario = {
       nombre: req.body.nombre,
       apellido: req.body.apellido
      };
      respuesta = {
       error: false,
       codigo: 200,
       mensaje: 'Usuario actualizado',
       respuesta: usuario
      };
     }
    }
    
    res.send(respuesta);
   });

*/
//------------- Creación de rutas: permite agrupar la misma URL que responderá a distintos métodos ya que los vimos por separado
/*
const express = require("express");
const bodyParser = require('body-parser');
const app = express();

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
let usuario = {
 nombre:'',
 apellido: ''
};


app.route('/usuario')
 .get(function (req, res) {
  respuesta = {
   error: false,
   codigo: 200,
   mensaje: ''
  };
  if(usuario.nombre === '' || usuario.apellido === '') {
   respuesta = {
    error: true,
    codigo: 501,
    mensaje: 'El usuario no ha sido creado'
   };
  } else {
   respuesta = {
    error: false,
    codigo: 200,
    mensaje: 'respuesta del usuario',
    respuesta: usuario
   };
  }
  res.send(respuesta);
 })
 .post(function (req, res) {
  if(!req.body.nombre || !req.body.apellido) {
   respuesta = {
    error: true,
    codigo: 502,
    mensaje: 'El campo nombre y apellido son requeridos'
   };
  } else {
   if(usuario.nombre !== '' || usuario.apellido !== '') {
    respuesta = {
     error: true,
     codigo: 503,
     mensaje: 'El usuario ya fue creado previamente'
    };
   } else {
    usuario = {
     nombre: req.body.nombre,
     apellido: req.body.apellido
    };
    respuesta = {
     error: false,
     codigo: 200,
     mensaje: 'Usuario creado',
     respuesta: usuario
    };
   }
  }
  
  res.send(respuesta);
 })
 .put(function (req, res) {
  if(!req.body.nombre || !req.body.apellido) {
   respuesta = {
    error: true,
    codigo: 502,
    mensaje: 'El campo nombre y apellido son requeridos'
   };
  } else {
   if(usuario.nombre === '' || usuario.apellido === '') {
    respuesta = {
     error: true,
     codigo: 501,
     mensaje: 'El usuario no ha sido creado'
    };
   } else {
    usuario = {
     nombre: req.body.nombre,
     apellido: req.body.apellido
    };
    respuesta = {
     error: false,
     codigo: 200,
     mensaje: 'Usuario actualizado',
     respuesta: usuario
    };
   }
  }
  
  res.send(respuesta);
 })
 .delete(function (req, res) {
  if(usuario.nombre === '' || usuario.apellido === '') {
   respuesta = {
    error: true,
    codigo: 501,
    mensaje: 'El usuario no ha sido creado'
   };
  } else {
   respuesta = {
    error: false,
    codigo: 200,
    mensaje: 'Usuario eliminado'
   };
   usuario = { 
    nombre: '', 
    apellido: '' 
   };
  }
  res.send(respuesta);
 });

**/
   app.listen(3000, () => {
    console.log("El servidor está inicializado en el puerto 3000");
   });
