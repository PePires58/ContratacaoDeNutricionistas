/*
 * Programador: Pedro Henrique Pires
 * Data: 03/06/2020
 * Implementação: Implementação Inicial da classe js
 */

$(document).ready(function () {
    $('#CEP').mask('99999-999');

    $('#CEP').change(function () {
        $.get({
            url: urlCadastrarCEP,
            data: { pCEP: $('#CEP').val() },
            success: function (data) {
                if (data.status === "OK") {
                    $('#Logradouro').val(data.data.logradouro);
                    $('#Bairro').val(data.data.bairro);
                    $('#Cidade').val(data.data.cidade);
                    $('#UF').val(data.data.uf);
                    $('#spanCEP').hide();
                }
                else
                    CEPOnError();
            },
            error: function () {
                CEPOnError();
            }
        });
    });

    function CEPOnError() {
        $('#Logradouro').val('');
        $('#Bairro').val('');
        $('#Cidade').val('');
        $('#UF').val('');
        $('#spanCEP').show();
    }

});

