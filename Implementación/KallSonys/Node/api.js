var http = require('http')

	

function getAPI(options){
	var req = http.request(options, function(res){
				var body = ''

				res.on('data', function(chunk){
					body += chunk
				})

				res.on('end', function(){
					var price = JSON.parse(body)
					console.log(price)

					var gida = function(req, res){
						console.log('corriendo')
						res.end(body)
					}
					var servidor = http.createServer(gida);
					servidor.listen(8080)

				})

				res.on("error", function (error) {
					console.error(error);
				})
			})

	// var postData = "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"data\"\r\n\r\neyJpdiI6ImZsR0wwbVdUeG1TNWticDJpSmdUK3c9PSIsInZhbHVlIjoia21nSTFmWWV0eWdNNW13TzlTR3FwZz09IiwibWFjIjoiNjVjOWE2OGMzYmUyYzRjNzg0YWI3YjcxZjU3MGM4ZDhmYTQ0NDUxYjVkZDIxZTQ4NDM2YjBlMDlkMTI5OTk1NyJ9\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--";
	var postData = ''
	// console.log(req)
	req.write(postData);
	req.end();
}

var options = {
	'host': 'ar_pcrebe.test',
	'port': 801,
	// 'path': '/api/getssid'
	// 'method': 'POST',
	'path': '/api/encrypter/decrypt?text=eyJpdiI6Ik9YM0Rva08zbk5PZk85SXZFN3pwQ3c9PSIsInZhbHVlIjoiUFVjWWJQT2YzcHMrZGVIaG5MeStlU0gvd0J3cmN1ZSt4ZStJakRhejYvY1ZqVEFTYXJQRjU0OGRwQytQS3pnVzE2SzFxa0c3dEc0Q0xxKzZtSERFTFVjUWtBWVZFdGF1SU5KSzdFRHhmRTA9IiwibWFjIjoiMmE0ZmIwODQxNWI2YzZjNWY4NDU0YTMxYzE1ZDM4NmJhMzM4MTllMGQwM2RlYWFiNDYyODRlZTI0YzEwNjIwMiJ9"',
	'method': 'GET',
	'headers': {
		'content-type': 'multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW'
	},
}

getAPI(options);



// var http = require('http');
// // var fs = require('fs');

// var options = {
//   'method': 'POST',
//   'hostname': 'ar_pcrebe.test',
//   'port': 801,
//   'path': '/api/getssid',
//   'headers': {
//   	'content-type': 'multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW'
//   },
//   'maxRedirects': 20
// };

// var req = http.request(options, function (res) {
//   var chunks = [];

//   res.on("data", function (chunk) {
//     chunks.push(chunk);
//   });

//   res.on("end", function (chunk) {
//     var body = Buffer.concat(chunks);
//     console.log(body.toString());
//   });

//   res.on("error", function (error) {
//     console.error(error);
//   });
// });

// var postData = "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"data\"\r\n\r\neyJpdiI6ImZsR0wwbVdUeG1TNWticDJpSmdUK3c9PSIsInZhbHVlIjoia21nSTFmWWV0eWdNNW13TzlTR3FwZz09IiwibWFjIjoiNjVjOWE2OGMzYmUyYzRjNzg0YWI3YjcxZjU3MGM4ZDhmYTQ0NDUxYjVkZDIxZTQ4NDM2YjBlMDlkMTI5OTk1NyJ9\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--";

// // req.setHeader('content-type', 'multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW');

// req.write(postData);

// req.end();