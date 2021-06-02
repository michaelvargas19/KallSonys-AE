
const express = require('express')
const session = require('express-session')
const bodyParser = require('body-parser');
const cors = require('cors');
const flash = require('connect-flash');

const mercadopago = require ('mercadopago');

mercadopago.configure({
    access_token: 'APP_USR-8537781120920919-060209-4e119817536a2678227c9c2d81a4c3db-769119802'
});

const app = express()

app.use(flash());
app.use(session({
    secret: 'mi sesion secretita',
    // resave: false,
    // saveUninitialized: true,
    // cookie: { secure: true }
}))
app.use(function(req, res, next){
    res.locals.flashData= req.flash('flashData');
    next();
});
app.set('views', '../web');
app.use(express.static('../web'));
app.set('view engine', 'ejs');
const port = 3000

let products = [{
    "sku" : "string1",
    "id" : "1",
    "price" : "10000",
    "name" : "Blusa",
    "description" : "Blusa color blanco manga larga"
},
{
    "sku" : "string2",
    "id" : "2",
    "price" : "20000",
    "name" : "Short",
    "description" : "Short color azul"
},
{
    "sku" : "string3",
    "id" : "3",
    "price" : "30000",
    "name" : "Falda",
    "description" : "Falda color negro corta"
}]

let books = [{
    "isbn": "9781593275846",
    "title": "Eloquent JavaScript, Second Edition",
    "author": "Marijn Haverbeke",
    "publish_date": "2014-12-14",
    "publisher": "No Starch Press",
    "numOfPages": 472,
},
{
    "isbn": "9781449331818",
    "title": "Learning JavaScript Design Patterns",
    "author": "Addy Osmani",
    "publish_date": "2012-07-01",
    "publisher": "O'Reilly Media",
    "numOfPages": 254,
},
{
    "isbn": "9781449365035",
    "title": "Speaking JavaScript",
    "author": "Axel Rauschmayer",
    "publish_date": "2014-02-01",
    "publisher": "O'Reilly Media",
    "numOfPages": 460,
}];

app.use(cors());

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.get('/', function (req, res) {
    console.log('raiz')
});

app.post('/book', (req, res) => {
    const book = req.body;

    // output the book to the console for debugging
    console.log(book);
    books.push(book);

    res.send('Book is added to the database');
});

app.get('/book', (req, res) => {
    res.json(books);
});

app.get('/register', (req, res) => {
    res.render('register', { title: 'Hey', user: req.session.nombres, userId: req.session.userId});
});

app.get('/account', (req, res) => {
    res.render('account', { title: 'Hey', user: req.session.nombres, userId: req.session.userId});
});

app.post('/login', (req, res) => {

    const username = req.body.username;
    const password = req.body.password;

    var jsonObj = '{"contrasena": "'+password+'","idAplicacion": "Manager","usuario": "'+username+'"}'
    consumeAPI_POST('/api/Sesiones', jsonObj, function(resConsumo){
        // console.log(resConsumo);
        var userId = resConsumo.data.datosUsuario.idUsuario;
        var userName = resConsumo.data.datosUsuario.nombres;
        req.session.userId = userId
        req.session.nombres = userName
        res.redirect('/products')
    })
});

app.get('/signout', (req, res) => {
    console.log('Cerrando session...')
    req.session.destroy()
    res.redirect('/products')
});




app.post('/newUser', (req, res) => {
    const https = require("https");

    const username = req.body.username;
    const name = req.body.name;
    const lastname = req.body.lastname;
    const email = req.body.email;
    const password = req.body.password;

    var jsonObj = '{"usuario": "'+username+'","data": {"usuario": "'+username+'","email": "'+email+'","nombres": "'+name+'","apellidos": "'+lastname+'","contrasena": "'+password+'","identificacion": "","telefonoMovil": "","idTipoAuth": 1,"organizacion": "","cargo": "","description": "","esExterno": false}}'
    console.log(jsonObj)
    
    const data = jsonObj

    const options = {
        host: 'autenticacionapi.azurewebsites.net',
        path: '/api/Usuarios',
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        }
    }

    const requ = https.request(options, res1 => {
        console.log(`statusCode: ${res1.statusCode}`)
      
        res1.on('data', d => {
        //   process.stdout.write(d)
        //   console.log(JSON.parse(d))
          req.flash('flashData','Gracias por crear tu cuenta, ahora estas autentificado.');
          res.redirect('/products')
        })
    })
    requ.on('error', error => {
        console.error(error)
    })

    requ.write(data)
    requ.end()
});

app.get('/products', (req, res) => {
    // console.log('products: '+req.session.nombres)
    
    consumeAPI_GET('/api/Productos?skip=0&take=10', function(productosCatalogo){
        // console.log(productosCatalogo);
        let productos = productosCatalogo.data
        let groupedProducts = groupBy(productos, 'codigoCatalogo');
        let html = ''
        // console.log(groupedProducts);
        for(let i in groupedProducts){
            // console.log(groupedProducts[i][0].nombre)
            html += '<div class="col-md-12"><h1>'+i+'</h1></div>'
            for(let j in groupedProducts[i]){
                if(groupedProducts[i][j].valorUnitario==0){
                    groupedProducts[i][j].valorUnitario = 100
                }
                html += '<div class="col-md-4 product simpleCart_shelfItem text-center">'
                html += '   <img class="item_image" src="/assets/'+ groupedProducts[i][j].sku +".jpg"+ '" alt=" "/>'
                html += '   <div class="mask">'
                html += '       <a href="/products/'+groupedProducts[i][j].sku+'">Ver producto</a>'
                html += '   </div>'
                html += '   <div class="product_name item_name">'+groupedProducts[i][j].nombre+'</div>'
                html += '   <p class="item_description">'+groupedProducts[i][j].descripcion+'</p>'
                html += '   <p class="item_sku" style="display:none">'+groupedProducts[i][j].sku+'</p>'
                html += '   <p class="item_idu" style="display:none">'+groupedProducts[i][j].id+'</p>'
                html += '   <br/>'
                html += '   <p><a class="item_add" href="javascript:;"><i></i> <span class="item_price">'+groupedProducts[i][j].valorUnitario+'</span></a></p>'
                html += '</div>'
            }
            
        }
        // console.log(html);
        res.render('index', { user: req.session.nombres, userId: req.session.userId, htmlProductos: html });
    })
});

app.get('/products/:sku', (req, res) => {
    // res.json(products);
    // res.render('single', { title: 'Hey', product_id: req.params.id});
    // console.log(req.params.id);

    //var Request = require("request");

    /*Request.get({
        "headers": { "content-type": "application/json" },
        // "url": "http://httpbin.org/post",
        "url": "https://catalogosapi.azurewebsites.net/api/Productos/sku/"+req.params.sku
    }, (error, response, body) => {
        if(error) {
            return console.dir(error);
        }
        let producto = JSON.parse(body);
        let image = '/images/si.jpg';
        if( producto.data.multimedia.url != 'string' ){
            image = producto.data.multimedia.url;
        }
        // console.dir(res.data.nombre);
        if(producto.data.valorUnitario==0){
            producto.data.valorUnitario = 550
        }
        res.render('single', { 
            product_name: producto.data.nombre,
            product_description: producto.data.descripcion,
            product_price: producto.data.valorUnitario,
            product_brand: producto.data.marca,
            product_rating: producto.data.calificacion,
            product_image: image
        });
    });*/

    consumeAPI_GET('/api/Productos/sku/'+req.params.sku, function(producto){

        console.log("Producto "+producto.data);
        consumeAPI_GET('/api/Catalogos?skip=0&take=10', function(listaCatalogo){
            // --Catalogo
            var htmlCatalogos = ''
            for(i in listaCatalogo.data){
                htmlCatalogos += '<li><a href="/catalogs/'+listaCatalogo.data[i].codigoCatalogo+'">'+listaCatalogo.data[i].nombre+'</a></li>'
            }
            
            console.log("Unitario "+producto.data);

            var image = '/images/si.jpg';
            if(producto.data.valorUnitario==0){
                producto.data.valorUnitario = 560
            }
            var htmlCalificacion = ''
            if(producto.data.calificacion<=0){
                htmlCalificacion = 'Sin calificación'
            }else{
                for (let i = 0; i < producto.data.calificacion; i++) {
                    htmlCalificacion += '<i class="fa fa-star"></i>'
                }
            }
            console.log("Producto Data"+producto.data);
            res.render('single', { 

                product_name: producto.data.nombre,
                product_description: producto.data.descripcion,
                product_price: producto.data.valorUnitario,
                product_brand: producto.data.marca,
                product_rating: htmlCalificacion,
                product_image: image,
                product_sku: producto.data.sku,
                catalogs: htmlCatalogos
            });
        })        
    })
});

app.get('/products-list', (req, res) => {
    res.json(products);
});

app.get('/checkout', (req, res) => {
    // res.json(products);
    // res.render('checkout', { title: 'Hey', message: 'Hello there!'});
    console.log(req.session.userId)
    res.render('checkout', { user: req.session.nombres, userId: req.session.userId });
});

// -- CATALOGOS
app.get('/catalogs/:code', (req, res) => {
    let html = ''
    consumeAPI_GET('/api/Catalogos/codigo/'+req.params.code,function(infoCatalogo){

        consumeAPI_GET('/api/Productos/ranking/catalogo?codigo='+req.params.code+'&skip=0&take=10', function(productosCatalogo){
            // console.log(infoCatalogo.data)
            html += '<div class="col-md-12 text-center"><h1>'+infoCatalogo.data.nombre+'</h1></div>'
            for(i in productosCatalogo.data){
                html += '<div class="col-md-4 product simpleCart_shelfItem text-center">'
                html += '   <img class="item_image" src="/images/p1.jpg" alt="" />'
                html += '   <div class="mask">'
                html += '       <a href="/products/'+productosCatalogo.data[i].sku+'">Ver producto</a>'
                html += '   </div>'
                html += '   <div class="product_name item_name">'+productosCatalogo.data[i].nombre+'</div>'
                html += '   <p class="item_description">'+productosCatalogo.data[i].descripcion+'</p>'
                html += '   <p class="item_sku" style="display:none">'+productosCatalogo.data[i].sku+'</p>'
                html += '   <p class="item_idu" style="display:none">'+productosCatalogo.data[i].id+'</p>'
                html += '   <br/>'
                html += '   <p><a class="item_add" href="javascript:;"><i></i> <span class="item_price">'+productosCatalogo.data[i].valorUnitario+'</span></a></p>'
                html += '</div>'
            }

            // console.log(html)
            res.render('catalogs', { htmlCatalogos: html });
        })  
    })
});

// -- PAGOS

app.get('/pagar', (req, res) => {
    // res.json(products);
    res.render('pagos', { title: 'Hey', message: 'Hello there!'});
});

app.get('/pago', (req, res) => {
    // res.json(products);
    let precio_unitario = 50000
    let objPedido = '[{"id_proveedor": "PROV1","id_cliente": "2","id_producto": "60b35891d92364fe97876855","cantidad": 1,"precio_unitario": '+precio_unitario+'}]' 
    consumeAPI_POST_pedidos('/api/Pedidos', objPedido, function(pedido){
        console.log(pedido)

        let numero_pedido = pedido.id
        let fecha_pedido = pedido.fecha_pedido
        jsonPago = '{"numero_pedido": "'+numero_pedido+'","fecha_pago": "'+fecha_pedido+'","valor_pagado": '+precio_unitario+'}'
        consumeAPI_POST_pedidos('/api/Pago', jsonPago, function(pago){
            console.log(pago)
        })
    })

    res.render('pagogracias', { title: 'Hey', message: 'Hello there!'});
});

app.get('/mispedidos', (req, res) => {
    let html_mispedidos = ''

    consumeAPI_GET_pedidos('/api/Pedidos/ObtenerPedidoPorCliente/2', function(pedidoCliente){
        console.log(pedidoCliente)

        html_mispedidos += '<ul>'
        for(i in pedidoCliente){
            html_mispedidos += '<li>Fecha pedido:'+pedidoCliente[i].fecha_pedido+' - Número de orden: '+pedidoCliente[i].id_orden+'</li>'
        }
        html_mispedidos += '</ul>'

        res.render('mispedidos', { title: 'Hey', user: req.session.nombres, userId: req.session.userId, mispedidos: html_mispedidos});
    })
})



app.get('/test', (req, res) => {
    /*var Request = require("request");

    Request.post({
        "headers": { "content-type": "application/json" },
        // "url": "http://httpbin.org/post",
        "url": "https://autenticacionapi.azurewebsites.net/api/Sesiones",
        "body": JSON.stringify({
            "contrasena": "Scare2021",
            "idAplicacion": "Manager",
            "usuario": "Admin"
        })
    }, (error, response, body) => {
        if(error) {
            return console.dir(error);
        }
        console.dir(JSON.parse(body));
    });*/

    const https = require("https");
    
    const data = JSON.stringify({
        "contrasena": "Scare2021",
        "idAplicacion": "Manager",
        "usuario": "Admin"
    })

    const options = {
        host: 'autenticacionapi.azurewebsites.net',
        path: '/api/Sesiones',
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        }
    }

    const requ = https.request(options, res => {
        console.log(`statusCode: ${res.statusCode}`)
      
        res.on('data', d => {
        //   process.stdout.write(d)
        //   console.log(JSON.parse(d))
        })
    })
    requ.on('error', error => {
        console.error(error)
    })

    requ.write(data)
    requ.end()

})

app.get('/testpago', (req, res) => {
    res.render('pagar', { title: 'Hey', message: 'Hello there!'});
})
app.post('/pagopro', (req, res) => {

    

    // Crea un objeto de preferencia
    let preference = {
        items: [
            {
                title: 'Mi producto',
                unit_price: 100,
                quantity: 1,
            }
        ],
        back_urls: {
			"success": "http://localhost:3000/feedback",
			"failure": "http://localhost:3000/feedback",
			"pending": "http://localhost:3000/feedback"
		},
        auto_return: 'approved',
    };
    
    mercadopago.preferences.create(preference)
    .then(function(response){
    // Este valor reemplazará el string "<%= global.id %>" en tu HTML
        global.id = response.body.id;
        console.log(response.body)
        res.redirect(response.body.init_point)

        
    }).catch(function(error){
        console.log(error);
    });
})
app.get('/feedback', function(request, response) {
    response.json({
       Payment: request.query.payment_id,
       Status: request.query.status,
       MerchantOrder: request.query.merchant_order_id
   })
});

// -- FUNCIONES GENERALES

function groupBy(objectArray, property) {
    return objectArray.reduce((acc, obj) => {
       const key = obj[property];
       if (!acc[key]) {
          acc[key] = [];
       }
       // Add object to list for given key's value
       acc[key].push(obj);
       return acc;
    }, {});
}

function consumeAPI_POST(urlPath, jsonObj, callback){
    const https = require("https");
    const data = jsonObj

    const options = {
        host: 'autenticacionapi.azurewebsites.net',
        path: urlPath,
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        }
    }

    const requ = https.request(options, res1 => {
        // console.log(`statusCode: ${res1.statusCode}`)
        let dataR = '';
        res1.on('data', d => {
        //   process.stdout.write(d)
        //   console.log(JSON.parse(d))
        //   return JSON.parse(d)
            // callback(JSON.parse(d))
            dataR += d;

        })
        res1.on('end', () => {
            const body = JSON.parse(dataR)
            // console.log(body);
            callback(body)
        });
    })
    requ.on('error', error => {
        console.error(error)
    })

    requ.write(data)
    requ.end()
}

function consumeAPI_GET(urlPath, callback){
    const https = require('https')
    const options = {
        hostname: 'catalogosapi.azurewebsites.net',
        path: urlPath,
        method: 'GET'
    }

    const req = https.request(options, res => {
        let data = '';
        console.log(`statusCode: ${res.statusCode}`)

        res.on('data', d => {
            // callback(JSON.parse(d))
            data += d;
        })

        res.on('end', () => {
            const body = JSON.parse(data)
            callback(body)
            // console.log(body);
        });
    })

    req.on('error', error => {
        console.error(error)
    })

    req.end()
}

function consumeAPI_POST_pedidos(urlPath, jsonObj, callback){
    const https = require("https");
    const data = jsonObj

    const options = {
        host: 'ordenesapi.azurewebsites.net',
        path: urlPath,
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        }
    }

    const requ = https.request(options, res1 => {
        // console.log(`statusCode: ${res1.statusCode}`)
        let dataR = '';
        res1.on('data', d => {
        //   process.stdout.write(d)
        //   console.log(JSON.parse(d))
        //   return JSON.parse(d)
            // callback(JSON.parse(d))
            dataR += d;

        })
        res1.on('end', () => {
            const body = JSON.parse(dataR)
            // console.log(body);
            callback(body)
        });
    })
    requ.on('error', error => {
        console.error(error)
    })

    requ.write(data)
    requ.end()
}
function consumeAPI_GET_pedidos(urlPath, callback){
    const https = require('https')
    const options = {
        hostname: 'ordenesapi.azurewebsites.net',
        path: urlPath,
        method: 'GET'
    }

    const req = https.request(options, res => {
        let data = '';
        console.log(`statusCode: ${res.statusCode}`)

        res.on('data', d => {
            // callback(JSON.parse(d))
            data += d;
        })

        res.on('end', () => {
            const body = JSON.parse(data)
            callback(body)
            // console.log(body);
        });
    })

    req.on('error', error => {
        console.error(error)
    })

    req.end()
}






app.get('/book/:isbn', (req, res) => {
    // reading isbn from the URL
    const isbn = req.params.isbn;

    // searching books for the isbn
    for (let book of books) {
        if (book.isbn === isbn) {
            res.json(book);
            return;
        }
    }

    // sending 404 when not found something is a good practice
    res.status(404).send('Book not found');
});

app.delete('/book/:isbn', (req, res) => {
    // reading isbn from the URL
    const isbn = req.params.isbn;

    // remove item from the books array
    books = books.filter(i => {
        if (i.isbn !== isbn) {
            return true;
        }

        return false;
    });

    // sending 404 when not found something is a good practice
    res.send('Book is deleted');
});

app.post('/book/:isbn', (req, res) => {
    // reading isbn from the URL
    const isbn = req.params.isbn;
    const newBook = req.body;

    // remove item from the books array
    for (let i = 0; i < books.length; i++) {
        let book = books[i]

        if (book.isbn === isbn) {
            books[i] = newBook;
        }
    }

    // sending 404 when not found something is a good practice
    res.send('Book is edited');
});



app.put('/book/:isbn', (req, res) => {
    // reading isbn from the URL
    console.log('PUT');
    const usuario = req.params.usuario;
    const password = req.body;

    
    // sending 404 when not found something is a good practice
    res.send('Book is edited');
});

app.listen(port, () => console.log(`Hola inicia proceso escuchando por el puerto ${port}!`));

