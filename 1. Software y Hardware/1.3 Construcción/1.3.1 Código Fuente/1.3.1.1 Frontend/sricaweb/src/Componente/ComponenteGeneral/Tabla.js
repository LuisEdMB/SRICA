import React from 'react'

import { createMuiTheme, MuiThemeProvider } from '@material-ui/core'
import MUIDataTable from 'mui-datatables'

export const Tabla = (props) => {
    const opcionesTabla = {
        responsive: "scroll",
        disableToolbarSelect: true,
        rowsPerPageOptions: [10, 20, 30, 40, 50],
        print: false,
        downloadOptions: props.downloadOptions === undefined ? {} : props.downloadOptions,
        download: props.download === undefined ? false : props.download,
        onDownload: (buildHead, buildBody, columns, data) => props.onDownload === undefined
            ? {}
            : props.onDownload(buildHead, buildBody, columns, data),
        viewColumns: false,
        search: false,
        onRowsSelect: (rowsSelected, allRows) => props.onRowsSelect(rowsSelected, allRows),
        onTableChange: (action, tableState) => props.onTableChange === undefined 
            ? {} 
            : props.onTableChange(action, tableState),
        selectableRows: props.seleccionFila ? 'multiple' : 'none',
        rowsSelected: [],
        textLabels:{
            body: {
                noMatch: "No se encontraron registros.",
                toolTip: "Ordenar",
                columnHeaderTooltip: (column) => `Ordenar por ${column.label}`
            },
            pagination: {
                next: "Siguiente Página",
                previous: "Página Anterior",
                rowsPerPage: "Filas por página:",
                displayRows: "de",
            },
            toolbar: {
                search: "Buscar",
                downloadCsv: "Exportar Reporte",
                print: "Imprimir",
                viewColumns: "Ver Columnas",
                filterTable: "Filtros de la Tabla",
            },
            filter: {
                all: "Todos",
                title: "FILTROS",
                reset: "REINICIAR",
            },
            viewColumns: {
                title: "Mostrar Columnas",
                titleAria: "Mostrar/Ocultar Columnas de la Tabla",
            },
            selectedRows: {
                text: "fila(s) seleccionada(s)",
                delete: "Eliminar",
                deleteAria: "Eliminar Filas Seleccionadas",
            }
        }
    }

    const RemoverChipFilter = createMuiTheme({
        overrides: {
            MUIDataTableFilterList: {
              chip: {
                display: 'none'
              }
            },
            MuiGridListTile: {
                root: {
                    width: "100% !important"
                }
            }
        }
    })

    return(
        <MuiThemeProvider theme={ RemoverChipFilter }>
            <MUIDataTable
                title={ '' }
                data={ props.datos }
                columns={ props.columnas }
                options={ opcionesTabla }
            />
        </MuiThemeProvider>
    )
}