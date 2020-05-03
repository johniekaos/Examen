var tblIndice = 0;
var Pedidos = {
    ObtieneProductoDetalles: function (sku) {
        $.ajax({
            url: location.origin + "/Productos/Get",
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: { sku: sku },
            cache: false,
            success: function (data) {

                if (!data) {
                    console.log("ObtieneProductoDetalles - Error en resultado");
                    return;
                }

                $("#PRICE").val(data.PRICE.toFixed(2));
                if (!data.EXISTENCIA > 0) {
                    $("#txtCosto").val("No hay producto disponible");
                    $("#btnAgregar").attr("disabled", "disabled");
                }
                else {
                    $("#btnAgregar").removeAttr("disabled");
                }
                $('#AMOUT').val('');
                $('#AMOUT').focus();

            },
            error: function (e) {
                console.log("Ocurrio un error al consultar el producto: " + e.error);

            }
        });
    },
    GetValidaCantidad: function (sku, cantidad) {
        $.ajax({
            url: location.origin + "/Productos/Get",
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: { sku: sku },
            cache: false,
            success: function (data) {


                if (!data) {
                    console.log("GetValidaCantidad - Error en resultado");
                    return;
                }

                if (cantidad > data.EXISTENCIA) {
                    alert("No hay producto disponible");
                    $("#txtCosto").val("No hay producto disponible");
                    $("#btnAgregar").attr("disabled", "disabled");
                }
                else {
                    var multiplica = data.PRICE * cantidad;
                    $("#txtCosto").val(multiplica.toFixed(2));
                    $("#btnAgregar").removeAttr("disabled");
                }
            },
            error: function (e) {
                console.log("Ocurrio un error al consultar el producto: " + e.error);

            }
        });

    },
    Agregar: function () {
        var registro = $('<tr class="trDetalles r_' + tblIndice + '" producto="' + $('#PRODUCTO_W_SKU').val() + '-' + $('#AMOUT').val() + '">' +
            '<td>' + $('#PRODUCTO_W_SKU').val() +
            '</td>' +
            '<td>  ' + $('#PRODUCTO_W_SKU :selected').text() +
            '</td> ' +
            '<td>  ' + $('#PRICE').val() +
            '</td> ' +
            '<td>  ' + $('#AMOUT').val() +
            '</td> ' +
            '<td class="dataTotal">  ' + ($('#PRICE').val() * $('#AMOUT').val()).toFixed(2) +
            '</td> ' +
            '<td>  ' +
            '<button indice="' + tblIndice + '" class="btn btn-danger btnEliminar">Eliminar</button>' +
            '</td>' +
            '<tr>')

        $('#tblPedidos').append(registro);
        tblIndice += 1;
        Pedidos.LimpiarCampos();
        Pedidos.OpcionGuardar();
        Pedidos.CalcularTotal();
    },
    LimpiarCampos: function () {
        $("#PRODUCTO_W_SKU").val("");
        $('#AMOUT').val('');
        $('#PRICE').val('');
        $('#txtCosto').val('');
    },
    OpcionGuardar: function () {
        if ($('#tblPedidos .btnEliminar').length > 0)
            $('#btnGuardar').show();
        else
            $('#btnGuardar').hide();

    },
    CalcularTotal: function () {
        var acumulado = 0.0;
        $('#tblPedidos').find('.dataTotal').each(function () {
            acumulado += parseFloat($(this).text());
        });

        $('#lblAcumulado').text(acumulado.toFixed(2));
    },
    Guardar: function () {
        var detalles = {
            Registro: []
        };

        $('#tblPedidos').find('.trDetalles').each(function () {
            var Registro = {};
            var par = $(this).attr("producto");
            var datos = par.split("-");
            Registro.Sku = datos[0];
            Registro.Cantidad = datos[1];
            detalles.Registro.push(Registro);
        });

        console.log("se agregaran " + detalles.Registro.length + " elementos");

        if (detalles.Registro.length > 0) {
            $.ajax({
                url: location.origin + "/Pedidos/PostDetails",
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ detalles: detalles }),
                cache: false,
                success: function (data) {

                    if (data) {
                        if (data == true) {
                            alert("Se almacenó correctamente");
                            location.href = location.origin + "/Pedidos"
                        }
                        if (data == false) {
                            alert("Ocurrio un error al almacenar pedido");
                            location.href = location.origin + "/Pedidos/Create"
                        }
                    }
                },
                error: function (e) {
                    console.log("Ocurrio un error al almacenar detalle: " + JSON.stringify(e));
                }
            });
        }
    }
};


$().ready(function () {
    $('#btnGuardar').hide();

    $('#PRODUCTO_W_SKU').on('change', function () {
        if (this.value === '')
            return;

        Pedidos.ObtieneProductoDetalles(this.value);
    });

    $('#AMOUT').on('change', function () {
        if ($('#PRODUCTO_W_SKU').val() === '')
            return;
        if ($('#PRODUCTO_W_SKU').val() < 0) {
            $('#PRODUCTO_W_SKU').val(0);
        }
        Pedidos.GetValidaCantidad($('#PRODUCTO_W_SKU').val(), this.value);
    });

    $('#btnAgregar').on('click', function () {
        if ($('#PRODUCTO_W_SKU').val() == '') {
            alert("Debe seleccionar un producto.");
            $('#PRODUCTO_W_SKU').focus();
            return false;
        }

        if ($('#AMOUT').val() === '' || $.trim($('#AMOUT').val()) < 1) {
            alert("Debe definir la cantidad de producto.");
            $('#AMOUT').focus();
            return false;
        }

        Pedidos.Agregar();

    });

    $('#tblPedidos').on('click', '.btnEliminar', function () {

        var indice = $(this).attr('indice');
        $('#tblPedidos').find(".r_" + indice).remove();

        Pedidos.OpcionGuardar();
        Pedidos.CalcularTotal();
    });

    $('#btnGuardar').on('click', function () {
        Pedidos.Guardar();
    });

});