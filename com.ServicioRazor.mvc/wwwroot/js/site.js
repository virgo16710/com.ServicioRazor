// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function ()
{
    $('.VerRegion').click(function ()
    {
        const dataID = $(this).data('id');
        var response = $.ajax({
            url: '/GetRegion',
            type: 'GET',
            data: { id: dataID }
        });
        response.done(function (data) {
            const idRegion = $('#IdRegion');
            const NomRegion = $('#NomRegion'); 
            idRegion.text(data.idRegion);
            NomRegion.text(data.region)
            $('#DialogRegion').modal('show');
        });
        response.fail(function (data) {
            console.log(data);
        })
    });
    $('.EditComuna').click(function () {
        const dataRegion = $('#idRegionFormComuna').val();
        const dataComuna = $(this).data('id');
        var response = $.ajax({
            url: '/GetComuna',
            type: 'GET',
            contentType: 'application/json',
            data: { IdRegion: dataRegion, IdComuna:dataComuna }
        })
        response.done(function (data)
        {
            var obj = data;
            $('#TextIdComuna').val(obj.idComuna);
            $('#TextNomComuna').val(obj.comuna);
            $('#TextDensidad').val(obj.densidad);
            $('#TextSuperficie').val(obj.superficie);
            $('#TextPoblacion').val(obj.poblacion);
            $('#TextIdRegion').val(obj.idRegion);
            $('#DialogFormComuna').modal('show'); 
        });
        response.fail(function (data) {
            console.log(data);
        })
        
    });
    $('#AddComuna').click(function () {
        $('#DialogFormComuna').modal('show'); 
    })
});