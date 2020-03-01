$(document).ready(function () {

    $('#TipoPessoa').change(function (e) {

        var btnCadastrar = $('#btnCadastrar');

        if (e.target.value === "") {
            btnCadastrar.prop('readonly', true);
        }
        else if (e.target.value === "0") {
            btnCadastrar.prop('readonly', false);
            $('#divCNPJ').hide();
            $('#divCPF').show();
        }
        else if (e.target.value === "1") {
            btnCadastrar.prop('readonly', false);
            $('#divCNPJ').show();
            $('#divCPF').hide();
        }
    });

});
