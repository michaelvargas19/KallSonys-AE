
/**const express = require('express')
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express()
const port = 3000

app.use(cors());

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.post('/process_payment',function(req,res)
{
  const process_payment_header = req.headers;
  const process_payment = req.body;

  // output the book to the console for debugging
  console.log(process_payment_header);
  books.push(process_payment);

  res.send('Book is added to the database');

});

app.listen(port, () => console.log(`Hola inicia proceso escuchando por el puerto ${port}!`));
**/

const data = JSON.stringify({
  todo: 'Buy the milk'
})

const options = {
  hostname: 'localhost',
  port: 3000,
  path: '/process_payment',
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Content-Length': data.length
  }
}

const req = https.request(options, res => {
  console.log(`statusCode: ${res.statusCode}`)

  res.on('data', d => {
    process.stdout.write(d)
  })
})

req.on('error', error => {
  console.error(error)
})

req.write(data)
req.end()