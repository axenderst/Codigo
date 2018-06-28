$(document).ready(function () {
    $("#btnBuscar").click(function () {
        FiltrarMapas();
    });

    ArmaGridMapa();
});


function ArmaGridMapa(Datos) {
    $("#gvMapas").kendoGrid({
        dataSource: {
            data: Datos,
            schema: {
                model: {
                    fields: {
                        IdMapa: "IdMapa",
                        Sufijo: "Sufijo",
                        Hoja: "Hoja",
                        FilaEncabezado: "FilaEncabezado",
                        FechaCreacion: "FechaCreacion",
                        Fecha_UltimaModif: "Fecha_UltimaModif"
                    }
                }
            },
            pageSize: 10
        },
        selectable: "row",
        scrollable: false,
        sortable: false,
        filterable: true,
        pageable: {
            refresh: false,
            pageSizes: true,
            input: true,
            numeric: false
        },
        columns: [
            {
                field: "IdMapa",
                title: "IdMapa",
                width: "10%",
                hidden: true
            },
            {
                field: "Sufijo",
                title: "Sufijo",
                width: "25%"
            },
            {
                field: "Hoja",
                title: "Hoja",
                width: "10%"
            },
            {
                field: "FilaEncabezado",
                title: "FilaEncabezado",
                width: "10%"
            },
            {
                field: "FechaCreacion",
                title: "FechaCreacion",
                width: "15%"
            },
            {
                field: "Fecha_UltimaModif",
                title: "Fecha_UltimaModif",
                width: "15%"
            }
        ]
    });
}