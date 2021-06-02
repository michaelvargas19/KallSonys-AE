const express = require("express");
const app = express();
const mercadopago = require("mercadopago");
const cors = require('cors');

app.use(cors());

//REPLACE WITH YOUR ACCESS TOKEN AVAILABLE IN: https://www.mercadopago.com/developers/panel
mercadopago.configurations.setAccessToken("TEST-5637132045326929-051902-5d5383a0dafc71873a3f1650ec52a657-264000159");

app.use(express.urlencoded({ extended: false }));
app.use(express.json());
// app.use(express.static("../../client"));

app.get("/", function (req, res) {
  res.status(200).sendFile("index.html");
}); 

app.post("/process_payment", (req, res) => {

    console.log(req.body)

  var payment_data = {
    transaction_amount: Number(req.body.transactionAmount),
    token: req.body.token,
    description: req.body.description,
    installments: Number(req.body.installments),
    payment_method_id: req.body.paymentMethodId,
    issuer_id: req.body.issuerId,
    payer: {
      email: req.body.payer.email,
      identification: {
        type: req.body.payer.identification.docType,
        number: req.body.payer.identification.docNumber
      }
    }
  };

  console.log(res.status)
  mercadopago.payment.save(payment_data)
    .then(function(response) {
        response.status = 200
      res.status(response.status).json({
        status: response.body.status,
        message: response.body.status_detail,
        id: response.body.id
      });
    })
    .catch(function(error) {
        console.log('ERRRROR')
      res.status(error.status).send(error);
    });
});

app.listen(8080, () => {
  console.log("The server is now running on Port 8080");
});
