var http = require('http')

var gida = function(req, res){
	console.log('corriendo')
	res.end('Hola mundo')
}
var servidor = http.createServer(gida);
servidor.listen(8080)