const express = require("express");
const app = express();
const mercadopago = require("mercadopago");

// SUBSTITUA PELO SEU TOKEN DE ACESSO DISPONÍVEL EM: https://developers.mercadopago.com/panel/credentials
mercadopago.configurations.setAccessToken("TEST-6182417685751336-012713-6f6746e5e8fbbc152d77d9fbb2280a44-707281849");

app.use(express.urlencoded({ extended: false }));
app.use(express.json());
app.use(express.static("../../ client"));

app.get("/", função(req, res){res.status(200).sendFile("pagamento.aspx")});

app.post("/ process_payment", (req, res) => {

    var payment_data = {
        transaction_amount: Number(req.body.transactionAmount),
        token: req.body.token,
        descrição: req.body.description,
        parcelas: Número(req.body.installments),
        payment_method_id: req.body.paymentMethodId,
        issuer_id: req.body.issuer,
        pagador: {
            email: req.body.email,
            identificação: {
                tipo: req.body.docType,
                número: req.body.docNumber
            }
        }
    };

    mercadopago.payment.save(payment_data)
        .então(função(resposta) {
            res.status(response.status).json({
                status: response.body.status,
                status_detail: response.body.status_detail,
                id: response.body.id
            });
        })
        .catch(função(erro) {
            res.status(response.status).send(erro);
        });
});

app.listen(8080, () => {
    console.log("O servidor agora está rodando na porta 8080");
});