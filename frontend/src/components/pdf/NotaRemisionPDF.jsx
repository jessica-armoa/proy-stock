import React from 'react';
import { Page, Text, View, Document, StyleSheet } from '@react-pdf/renderer';

const styles = StyleSheet.create({
  page: {
    flexDirection: 'row',
    backgroundColor: 'white'
  },
  section: {
    margin: 10,
    padding: 8,
    flexGrow: 1
  },
  mainBorder: {
    marginTop: 40,
    padding: 0,  
    marginBottom: 20,
    marginLeft: 80,
    marginRight: 40,
    border: 1.5,
    width:672, //tamaño maximo según documento y orientación. Restar margenes LETTER: [792.0],
    height: 532, //tamaño maximo según documento y orientación. Restar margenes LETTER: [612.0],    
  },
  header:{
    marginLeft:-1,
    marginTop:-1,
    marginRight: -1,
    marginBottom: 0,
    flexDirection: 'row',
  },
  header1: {
    marginRight: 0,
    padding: 8,
    border: 1,
    width:373, 
    height: 72, 
  },
  header2: {
    marginLeft:-1,
    padding: 8,
    border: 1,
    width:301, 
    height: 72, 
    textAlign: 'center',
  },  
  flexBorder:{
    marginLeft:-1,
    marginTop:-1,
    marginRight: -1,
    marginBottom: 0,    
    padding: 8,
    border: 1,
    flexDirection: 'row',
  },  
  normalBorder:{
    marginLeft:-1,
    marginTop:-1,
    marginRight: -1,
    marginBottom: 0,    
    padding: 8,
    border: 1,
  },
  normalFlexBorder0:{
    margin:0,    
    padding: 0,
    border: 0,    
    flexDirection: 'row',
  },
  normalBorderHeader1:{    
    marginLeft:-1, 
    marginTop:-1,
    marginRight: 0,
    marginBottom: 0,    
    padding: 4,
    paddingLeft: 2,
    paddingRight: 2,
    border: 1,  
    width:51,
  },
  normalBorderHeader2:{
    marginLeft:-1,   
    marginTop:-1,
    marginRight: -1,
    marginBottom: 0,    
    padding: 4,
    border: 1,   
    width:623,
  },
  normalBorderDetails1:{    
    marginLeft:-1, 
    marginTop:-1,
    marginRight: 0,
    marginBottom: 0,    
    padding: 4,
    border: 1,
    width:51,
    height: 180,  
  },
  normalBorderDetails2:{
    marginLeft:-1,   
    marginTop:-1,
    marginRight: -1,
    marginBottom: 0,    
    padding: 4,
    border: 1,
    height: 180,  
    width:623,
  },
  footer1: {
    marginRight: 0,
    padding: 8,
    border: 1,
    width:373, 
    height: 64.2, 
  },
  footer2: {
    marginLeft:-1,
    padding: 8,
    border: 1,
    width:301, 
    height: 64.2, 
    textAlign: 'center',
  }, 
});
//tamaños disponibles: https://github.com/diegomura/react-pdf/blob/master/packages/layout/src/page/getSize.js
function NotaRemisionPDF({
  data={
    "empresa":{
      "nombre": "REPUESTOS FERNANDEZ SRL",
      "direccion": "España 124 E/ Artigas - Asuncion",
      "telefono": "221546",
      "sucursal": "Yegros 645 y Cerrro Corá - Asunción",
      "actividad": "Venta de Autorepuestos",
    },
    "numero_timbrado": "215487930",
    "valido": "NOVIEMBRE DE 2007",
    "ruc": "800458998-2",
    "numero_nota": "002-003-0045327",
    "destinatario": {
      "nombre": "HELADERIA FRESQUITO SRL",
      "documento": "8002001594-3"
    },
    "punto":{
      "partida":"Yegros 645 y Cerro Corá - Asunción",
      "llegada":"Palma 584 y  14 de Mayo - Asunción",
    },
    "traslado":{
      "fecha_inicio":"30/12/2006",
      "fecha_fin":"30/12/2006",
      "vehiculo":"CHEVROLET HILUX",
      "rua":"ASX 675",
      "transportista":{
        "nombre":"REPUESTOS FERNANDEZ SRL",
        "ruc":"800458998-2",
      },
      "conductor":{
        "nombre":"Cosme Fulanito",
        "documento":"123456",
        "direccion":"Avda. Siempre Viva 742",
      },
      "motivo": "venta", //ver como manejar este tema
      "motivo_descripcion": "venta", 
      "comprobante_venta": "002-001-2547896", 
    },
    "productos":[
      {
        "cantidad": 2,
        "descripcion": "MOTOR REFRIGERADOR LION 3456 SERIE GFR456",
      },
      {
        "cantidad": 1,
        "descripcion": "TANQUE DE GAS REFRIGERADOR LION 3456 SERIE GFZ456",
      },
    ]
  },
  title="Titulo",
}) {
  return (
    <Document title={title} author="Grupo 5">
      <Page size="LETTER" orientation="landscape" style={styles.page}>
        <View style={styles.mainBorder}>        

          {/* CABECERAS */}
          <View style={styles.header}>
            <View style={styles.header1}>
              <Text style={{fontWeight:'ultrabold',fontSize:13, lineHeight:2 }}>{data.empresa.nombre}</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{data.empresa.direccion+" Telf.: "+data.empresa.telefono}</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{data.empresa.sucursal}</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{data.empresa.actividad}</Text>
            </View>
            <View style={styles.header2}>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{"TIMBRADO N° "+data.numero_timbrado}</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{"VALIDO HASTA "+data.valido}</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{"RUC "+data.ruc}</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{"NOTA DE REMISION"}</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>{data.empresa.actividad}</Text>
            </View>
          </View>

          {/* RAZON SOCIAL */}
          <View style={styles.flexBorder}>
            <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:523}}>{"NOMBRE O RAZÓN SOCIAL DEL DESTINATARIO: "}
              <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.destinatario.nombre}</Text>
            </Text>
            <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:151}}>{"DOCUMENTO: "}
              <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.destinatario.documento}</Text>
            </Text>
          </View>

          {/* DIRECCIONES */}
          <View style={styles.normalBorder}>
            <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2}}>{"DIRECCIÓN COMPLETA DEL PUNTO DE PARTIDA: "}
              <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.punto.partida}</Text>
            </Text>
            <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2}}>{"DIRECCIÓN COMPLETA DEL PUNTO DE LLEGADA: "}
              <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.punto.llegada}</Text>
            </Text>
          </View>

          {/* DATOS DEL TRASLADO */}
          <View style={styles.normalBorder}>

            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:373}}>{"FECHA DE INICIO DEL TRASLADO: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.fecha_inicio}</Text>
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:301}}>{"FECHA DE TÉRMINO DEL TRASLADO: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.fecha_fin}</Text>
              </Text>
            </View>

            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:373}}>{"MARCA VEHICULO DE TRANSPORTE: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.vehiculo}</Text>
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:301}}>{"NUMERO DE RUA: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.rua}</Text>
              </Text>
            </View>

            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:373}}>{"NOMBRE O RAZÓN SOCIAL TRANSPORTISTA: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.transportista.nombre}</Text>
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:301}}>{"RUC TRANSPORTISTA: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.transportista.ruc}</Text>
              </Text>
            </View>

            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:373}}>{"NOMBRE CONDUCTOR: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.conductor.nombre}</Text>
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:301}}>{"CEDULA IDENTIFICACIÓN CONDUCTOR: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.conductor.documento}</Text>
              </Text>
            </View>

            <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:301}}>{"DIRECCION DEL CONDUCTOR: "}
                <Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.conductor.direccion}</Text>
            </Text>

          </View>

          {/* MOTIVOS DEL TRASLADO */}
          <View style={styles.normalBorder}>
            
            <Text style={{fontWeight:'normal',fontSize:9, lineHeight:1.2,}}>{"MOTIVO DEL TRASLADO: "}
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2,}}>{"(marque una sola opción, deberá utilizar un documento por cada motivo de traslado)"}</Text>
            </Text>
            
            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"venta:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="venta" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"importación:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="importación" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"traslados locales empresa"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="traslados locales empresa" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"emisor móvil"}</Text>            
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="emisor móvil" ? "X" : "  "}</Text>{")"}
              </Text>            
            </View>

            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"exportación:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="exportación" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"consignación:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="consignación" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"transformación:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="transformación" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"exhibición:"}</Text>            
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="exhibición" ? "X" : "  "}</Text>{")"}
              </Text>            
            </View>

            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"compra:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="compra" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"devolución:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="devolución" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"reparación:"}</Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="reparación" ? "X" : "  "}</Text>{")"}
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:120}}>{"ferias:"}</Text>            
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:48.5}}>
                {"("}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="ferias" ? "X" : "  "}</Text>{")"}
              </Text>            
            </View>

            <View style={styles.normalFlexBorder0}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:337}}>
                {"otros: (indique motivos no previstos): "}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{data.traslado.motivo=="otros" ? data.traslado.motivo_descripcion : ""}</Text>
              </Text>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1.2, width:337}}>
                {"Comprobante de Venta: "}<Text style={{color:'blue',fontSize:8.5, lineHeight:1.2 }}>{"FACTURA "+data.traslado.comprobante_venta}</Text>
              </Text>            
            </View>
          
          </View>

          {/* CANTIDAD Y DETALLES */}
          <View style={styles.normalFlexBorder0}>
            <View style={styles.normalBorderHeader1}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1, textAlign: 'center'}}>{"CANTIDAD"}</Text>
            </View>
            <View style={styles.normalBorderHeader2}>
              <Text style={{fontWeight:'normal',fontSize:8.5, lineHeight:1, textAlign: 'center'}}>{"DESCRIPCIÓN DE LA MERCADERÍA"}</Text>
            </View>
          </View>
          <View style={styles.normalFlexBorder0}>
            <View style={styles.normalBorderDetails1}>
              {data.productos.map((producto)  => (
                <Text style={{fontWeight:'normal',color:'blue',fontSize:8.5, lineHeight:1, textAlign: 'right'}}>{producto.cantidad}</Text>
              ))}
            </View>
            <View style={styles.normalBorderDetails2}>
              {data.productos.map((producto)  => (
                <Text style={{fontWeight:'normal',color:'blue',fontSize:8.5, lineHeight:1}}>{producto.descripcion}</Text>
              ))}
            </View>
          </View>
          
          {/* PIE DE PAGINA */}
          <View style={styles.header}>
            <View style={styles.footer1}>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:2 }}>HABILITACION N° 12345</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>GRUPO5 IMPRESIONES S.A.</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>RUC 12345-1</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>N° 12 Grimmauld Place</Text>
            </View>
            <View style={styles.footer2}>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>ORIGINAL: DESTINATARIO</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>PRIMERA COPIA: REMITENTE</Text>
              <Text style={{fontWeight:'normal',fontSize:11, lineHeight:1.2 }}>SEGUNDA COPIA: SET</Text>
            </View>
          </View>

        </View>
      </Page>
    </Document>
  );
}

export default NotaRemisionPDF;