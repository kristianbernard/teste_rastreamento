function calculogeralcarrinho(control) {

    var qtdinf = 0 
    qtdinf = 0 // $("carregaproduto").find("input[id*='cboquantidade']").length;s
    var valortotal = 0;
    while (true) {
        try {
            var valorlinha = parseFloat(document.getElementById("cboquantidade_" + (qtdinf)).value.toString().replace(".", "").replace(",", ".")).toFixed(2) * parseFloat(document.getElementById("rptvalorunit_" + (qtdinf)).value.toString().replace("R$", "").replace(".", "").replace(",", ".")).toFixed(2);
            console.log(valorlinha);
            valortotal += valorlinha;
            qtdinf += 1;
        } catch {
            break;
        }



    }

    document.getElementById("lbltotal").value = valortotal.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

     

}

function resizeInput(control) {
    this.style.width = this.value.length + "ch";
}

function calculogeral(control) {




    var qtd = $("#GradeArqs tr").length + 1;


    for (var i = 2; i < qtd; i++) {


        try {

            var tr = $("#GradeArqs tr:nth-child(" + i + ")");

            //   var numero1 = $(tr).find("input[id*='txtMM45_compra']").val();
            // var numero2 = $(tr).find("input[id*='txtM45_compra']").val();




            var txtvalor = $(tr).find("input[id*='txtvalor']").val();
            var txtvldesconto = $(tr).find("input[id*='txtvldesconto']").val();

            if (txtvalor == null || txtvalor == '') {
                // alert("linha " + i + " Origem Vazia:" + txtorigin)
                continue;

            }

            if (txtvldesconto == null || txtvldesconto == '') {

                txtvldesconto = 0;

            }

            var txtvl = $(tr).find("input[id*='txtvltotal']").val();
            form1.txtvl = txtvalor - txtvldesconto;




        } catch (e) {

            //alert(": " + e.mensagem)
        }


        //if (document.getElementById("txtvalor").value == "") {
        //    document.getElementById("txtvalor").value = "0"
        //}
        //if (document.getElementById("txtvldesconto").value == "") {
        //    document.getElementById("txtvldesconto").value = "0"
        //}
        ////var cbotipoequipamento = form1.cbotipoequipamento.value;


        //document.getElementById("txtvltotal").value = document.getElementById("txtvalor").value - document.getElementById("txtvldesconto").value



    }
}
function dizoi(control) {

    alert("Oi")

}

