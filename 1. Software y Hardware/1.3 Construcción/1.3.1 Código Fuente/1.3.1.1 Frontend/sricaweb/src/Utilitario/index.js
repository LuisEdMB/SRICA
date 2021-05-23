import * as Constante from '../Constante'

Date.prototype.addDays = function(days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}

export function ManejarTeclaEnter(e, callback) {
  const code = e.which || e.keyCode
  if (code === 13) callback()
}

export const QuitarMilisegundosAlTiempo = (time) => {
    return Math.floor(time/1000)
}

export const RemoverParametroDeURL = () => {
    var actualURL = window.location.href
    var url = actualURL.substring(actualURL.lastIndexOf('/') + 1)
    var dominio =  url.split('?')[0]
    window.history.pushState({}, document.title, "/" + dominio );
}

export const RemoverDuplicadosArrayObjeto = (array, propiedadClave) => {
  return [...new Map(array.map(item => [item[propiedadClave], item])).values()]
}

export const SinAsignacionATexto = (texto) => {
    if (texto === '---')
        return 'Sin Asignación'
    return texto
}

export const EstadoATexto = (estado) => {
    if (estado) return 'Activo'
    return 'Inactivo'
}

export const BooleanATexto = (boolean) => {
    if (boolean) return 'Sí'
    return 'No'
}

export const SeleccionIrisATexto = (ambos, izquierda, derecha) => {
    if (ambos)
        return 'Ambos'
    else if (izquierda)
        return 'Izquierda'
    else if (derecha)
        return 'Derecha'
    else
        return 'Ninguno'
}

export const TextoASeleccionIris = (dosIrisSeleccionadoRespaldo, texto) => {
    if (dosIrisSeleccionadoRespaldo)
        switch (texto) {
            case 'Ambos':
                return ObtenerObjetoSeleccionIris(true, false, false, false, false)
            case 'Izquierda':
                return ObtenerObjetoSeleccionIris(true, false, false, true, false)
            case 'Derecha':
                return ObtenerObjetoSeleccionIris(true, false, false, false, true)
            default:
                return ''
        }
    else
        switch (texto) {
            case 'Ambos':
                return ObtenerObjetoSeleccionIris(true, false, false, false, false)
            case 'Izquierda':
                return ObtenerObjetoSeleccionIris(false, true, false, false, false)
            case 'Derecha':
                return ObtenerObjetoSeleccionIris(false, false, true, false, false)
            default:
                return ''
        }
}

const ObtenerObjetoSeleccionIris = (dosIris, izquierda, derecha, izquierdaTemporal, derechaTemporal) => {
    return {
        Ambos: {
            Seleccion: 'IndicadorDosIris',
            Valor: dosIris
        },
        Izquierda: {
            Seleccion: 'IndicadorIrisIzquierda',
            Valor: izquierda
        },
        Derecha: {
            Seleccion: 'IndicadorIrisDerecha',
            Valor: derecha
        },
        IzquierdaTemporal: {
            Seleccion: 'IndicadorIrisIzquierdaTemporal',
            Valor: izquierdaTemporal
        },
        DerechaTemporal: {
            Seleccion: 'IndicadorIrisDerechaTemporal',
            Valor: derechaTemporal
        }
    }
}

export const ObtenerListadoArrayDeArray = (array, propiedad) => {
    var resultado = []
    array.map((item) => {
        item[propiedad].map((subItem) => {
            resultado.push(subItem)
        })
    })
    return resultado
}

export const EncontrarEnArray = (array, propiedad, encontrar) => {
    if (array === undefined) return false
    var resultado = array.filter((item) => item[propiedad] === encontrar)
    if (resultado === undefined) return false
    return resultado[0]
}

export const EncontrarPropiedadEnArray = (array, propiedad, encontrar, propiedadObtener) => {
    if (array === undefined) return false
    var resultado = array.filter((item) => item[propiedad] === encontrar)
    if (resultado === undefined) return false
    return resultado[0][propiedadObtener]
}

export const ArchivoABase64 = (archivo, callback) => {
    let reader = new FileReader();
    reader.readAsDataURL(archivo);
    reader.onload = function () {
        callback(reader.result)
    };
}

export const ConvertirTextoAObjetoJSON = (texto) => {
    var resultado = {}
    try{
        resultado = JSON.parse(texto)
    }
    catch{
        return {}
    }
    return resultado
}

export const ObtenerSoloFechaDeDate = (fecha, separador) => {
  var dia = fecha.getDate()
  var mes = fecha.getMonth() + 1
  var anio = fecha.getFullYear()
  return ('0' + mes).slice(-2) + separador + ('0' + dia).slice(-2) + separador + anio
}

export const ConvertirADateDesdeString = (fecha, separador) => {
  var fechaSplit = fecha.split(separador)
  return new Date(fechaSplit[1] + separador + fechaSplit[0] + separador + fechaSplit[2])
}

export const FiltrarArrayPorPropiedadSegunRangoFecha = (array, propiedad, fechaInicio, fechaFin) => {
  var fechaInicioFormateado = new Date(ObtenerSoloFechaDeDate(new Date(fechaInicio), '/'))
  var fechaFinFormateado = new Date(ObtenerSoloFechaDeDate(new Date(fechaFin), '/')).addDays(1)
  return array.filter((item) => {
    var fecha = ConvertirADateDesdeString(item[propiedad], '/')
    return fechaInicioFormateado <= fecha && fecha <= fechaFinFormateado
  })
}

export const FiltrarArrayPorPropiedadSegunOtroArray = (arrayPrincipal, propiedad, array) => {
  return arrayPrincipal.filter((itemPrincipal) => 
    array.some((item) => itemPrincipal[propiedad] === item))
}

export const FiltrarArrayPorPropiedad = (array, propiedad, valor) => {
  return array.filter((item) => item[propiedad] === valor)
}

export const OrdenarArraySegunPropiedad = (array, propiedad, ascendente) => {
  if (ascendente)
    return array.sort((a,b) => {
      if (a[propiedad] > b[propiedad]) return 1
      if (b[propiedad] > a[propiedad]) return -1
    })
  else
    return array.sort((a,b) => {
      if (a[propiedad] > b[propiedad]) return -1
      if (b[propiedad] > a[propiedad]) return 1
    })
}

export const TomarRegistrosDeArraySegunLimite = (array, limite) => {
  return array.slice(0, limite)
}

export const DefinirFechaSegunCantidadMes = (fecha, meses) => {
  var nuevaFecha = new Date(fecha)
  return new Date(nuevaFecha.setMonth(nuevaFecha.getMonth() + meses))
}

export const AgregarPrefijoBase64 = (base64) => {
  return 'data:image/jpeg;base64,' + base64
}

export const ObtenerFechaYHora = () => {
  var fechaHora = new Date()
  var dia = fechaHora.getDate()
  var mes = fechaHora.getMonth() + 1
  var anio = fechaHora.getFullYear()
  var hora = fechaHora.getHours()
  var minuto = fechaHora.getMinutes()
  var segundo = fechaHora.getSeconds()

  if (hora.toString().length < 2) hora = '0' + hora;
  if (minuto.toString().length < 2) minuto = '0' + minuto;
  if (segundo.toString().length < 2) segundo = '0' + segundo;

  return ('0' + dia).slice(-2) + '/' + ('0' + mes).slice(-2) + '/' + anio + ' ' + 
    hora + ':' + minuto + ':' + segundo
}

export const GenerarColoresSegunCantidad = (cantidad) => {
  var resultado = []
  for (var i = 0; i < cantidad; i++ ) {
    var letras = 'BCDEF'.split('')
    var color = '#'
    for (var j = 0; j < 6; j++ ) {
      color += letras[Math.floor(Math.random() * letras.length)];
    }
    resultado.push(color)
  }
  return resultado;
}

export const GenerarPlantillaReporte = (columnas, filas, opciones) => {
    var plantilla = '<html><head><style>' + estiloPlantillaReporte + 
        '</style></head><body>'
    plantilla += '<h2 class="titulo">' + opciones.titulo + '</h2>'
    plantilla += '<h5 class="titulo">"Sistema de Reconocimiento de Iris para Control de Accesos"</h5>'
    plantilla += '<h4 class="titulo">Fecha: ' + ObtenerFechaYHora() + '</h4>'
    plantilla += '<h4 class="titulo">Usuario: ' + opciones.usuario.Usuario + ' (' + 
        opciones.usuario.Nombre + ')' + '</h4><br/><table class="tabla"><thead><tr>'

    columnas.map((columna) => {
        plantilla += '<th>' + columna.label + '</th>'
    })
    plantilla += '</tr></thead><tbody>'

    filas.map((fila) => {
        plantilla += '<tr>'
        fila.data.map((dato, i) => {
            if (i === opciones.columnaTieneObjetoHtml){
              if (opciones.esColumnaConImagen)
                plantilla += dato === '' 
                    ? '<td></td>' 
                    : ('<td><img class="imagen" src="data:' + Constante.MIME_TYPE_IMAGEN + 
                        ';base64,' + dato.props.src  + '"/></td>')
              else
                plantilla += '<td>' + dato.props.dangerouslySetInnerHTML.__html + '</td>'
            }
            else
                plantilla += '<td>' + dato + '</td>'
        })
        plantilla += '</tr>'
    })

    plantilla += '<tbody></table></body></html>'
    return plantilla
}

const estiloPlantillaReporte = `
.titulo {
    text-align: center;
}
.tabla  {
    border: 1px solid black;
    margin: 0 auto;
}
th, td  {
  border: 1px solid black;
}
.imagen {
  height: 100px;
  width: 200px;
}
.jsondiffpatch-delta {
    font-family: 'Bitstream Vera Sans Mono', 'DejaVu Sans Mono', Monaco, Courier, monospace;
    font-size: 12px;
    margin: 0;
    padding: 0 0 0 12px;
    display: inline-block;
  }
  .jsondiffpatch-delta pre {
    font-family: 'Bitstream Vera Sans Mono', 'DejaVu Sans Mono', Monaco, Courier, monospace;
    font-size: 12px;
    margin: 0;
    padding: 0;
    display: inline-block;
  }
  ul.jsondiffpatch-delta {
    list-style-type: none;
    padding: 0 0 0 20px;
    margin: 0;
  }
  .jsondiffpatch-delta ul {
    list-style-type: none;
    padding: 0 0 0 20px;
    margin: 0;
  }
  .jsondiffpatch-added .jsondiffpatch-property-name,
  .jsondiffpatch-added .jsondiffpatch-value pre,
  .jsondiffpatch-modified .jsondiffpatch-right-value pre,
  .jsondiffpatch-textdiff-added {
    background: #bbffbb;
  }
  .jsondiffpatch-deleted .jsondiffpatch-property-name,
  .jsondiffpatch-deleted pre,
  .jsondiffpatch-modified .jsondiffpatch-left-value pre,
  .jsondiffpatch-textdiff-deleted {
    background: #ffbbbb;
    text-decoration: line-through;
  }
  .jsondiffpatch-unchanged,
  .jsondiffpatch-movedestination {
    color: gray;
  }
  .jsondiffpatch-unchanged,
  .jsondiffpatch-movedestination > .jsondiffpatch-value {
    transition: all 0.5s;
    -webkit-transition: all 0.5s;
    overflow-y: hidden;
  }
  .jsondiffpatch-unchanged-showing .jsondiffpatch-unchanged,
  .jsondiffpatch-unchanged-showing .jsondiffpatch-movedestination > .jsondiffpatch-value {
    max-height: 100px;
  }
  .jsondiffpatch-unchanged-hidden .jsondiffpatch-unchanged,
  .jsondiffpatch-unchanged-hidden .jsondiffpatch-movedestination > .jsondiffpatch-value {
    max-height: 0;
  }
  .jsondiffpatch-unchanged-hiding .jsondiffpatch-movedestination > .jsondiffpatch-value,
  .jsondiffpatch-unchanged-hidden .jsondiffpatch-movedestination > .jsondiffpatch-value {
    display: block;
  }
  .jsondiffpatch-unchanged-visible .jsondiffpatch-unchanged,
  .jsondiffpatch-unchanged-visible .jsondiffpatch-movedestination > .jsondiffpatch-value {
    max-height: 100px;
  }
  .jsondiffpatch-unchanged-hiding .jsondiffpatch-unchanged,
  .jsondiffpatch-unchanged-hiding .jsondiffpatch-movedestination > .jsondiffpatch-value {
    max-height: 0;
  }
  .jsondiffpatch-unchanged-showing .jsondiffpatch-arrow,
  .jsondiffpatch-unchanged-hiding .jsondiffpatch-arrow {
    display: none;
  }
  .jsondiffpatch-value {
    display: inline-block;
  }
  .jsondiffpatch-property-name {
    display: inline-block;
    padding-right: 5px;
    vertical-align: top;
  }
  .jsondiffpatch-property-name:after {
    content: ': ';
  }
  .jsondiffpatch-child-node-type-array > .jsondiffpatch-property-name:after {
    content: ': [';
  }
  .jsondiffpatch-child-node-type-array:after {
    content: '],';
  }
  div.jsondiffpatch-child-node-type-array:before {
    content: '[';
  }
  div.jsondiffpatch-child-node-type-array:after {
    content: ']';
  }
  .jsondiffpatch-child-node-type-object > .jsondiffpatch-property-name:after {
    content: ': {';
  }
  .jsondiffpatch-child-node-type-object:after {
    content: '},';
  }
  div.jsondiffpatch-child-node-type-object:before {
    content: '{';
  }
  div.jsondiffpatch-child-node-type-object:after {
    content: '}';
  }
  .jsondiffpatch-value pre:after {
    content: ',';
  }
  li:last-child > .jsondiffpatch-value pre:after,
  .jsondiffpatch-modified > .jsondiffpatch-left-value pre:after {
    content: '';
  }
  .jsondiffpatch-modified .jsondiffpatch-value {
    display: inline-block;
  }
  .jsondiffpatch-modified .jsondiffpatch-right-value {
    margin-left: 5px;
  }
  .jsondiffpatch-moved .jsondiffpatch-value {
    display: none;
  }
  .jsondiffpatch-moved .jsondiffpatch-moved-destination {
    display: inline-block;
    background: #ffffbb;
    color: #888;
  }
  .jsondiffpatch-moved .jsondiffpatch-moved-destination:before {
    content: ' => ';
  }
  ul.jsondiffpatch-textdiff {
    padding: 0;
  }
  .jsondiffpatch-textdiff-location {
    color: #bbb;
    display: inline-block;
    min-width: 60px;
  }
  .jsondiffpatch-textdiff-line {
    display: inline-block;
  }
  .jsondiffpatch-textdiff-line-number:after {
    content: ',';
  }
  .jsondiffpatch-error {
    background: red;
    color: white;
    font-weight: bold;
  }
  `