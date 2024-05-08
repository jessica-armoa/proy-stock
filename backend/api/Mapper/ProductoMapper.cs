using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Ferreteria;
using api.Dtos.Producto;
using api.Models;

namespace api.Mapper
{
    public static class ProductoMapper
    {
        public static ProductoDto ToProductoDto(this Producto productoModel)
        {
            return new ProductoDto
            {
                id_producto = productoModel.Id,
                str_ruta_imagen = productoModel.Str_ruta_imagen,
                str_nombre = productoModel.Str_nombre,
                str_descripcion = productoModel.Str_descripcion,
                int_cantidad_actual = productoModel.Int_cantidad_actual,
                int_cantidad_minima = productoModel.Int_cantidad_minima,
                dec_costo_PPP = productoModel.Dec_costo_PPP,
                int_iva = productoModel.Int_iva,
                dec_precio_mayorista = productoModel.Dec_precio_mayorista,
                dec_precio_minorista = productoModel.Dec_precio_minorista,
                detallesDeMovimientos = productoModel.Detalles_De_Movimientos.Select(d => d.ToDetalleDeMovimientoDto()).ToList(),
                fk_deposito = productoModel.Fk_deposito,
                fk_proveedor = productoModel.Fk_proveedor,
                fk_marca = productoModel.Fk_marca
            };
        }

        public static Producto ToProductoFromCreate(this CreateProductoDto productoDto, int depositoId, int proveedorId, int marcaId)
        {
            return new Producto
            {
                Str_ruta_imagen = productoDto.str_ruta_imagen,
                Str_nombre = productoDto.str_nombre,
                Str_descripcion = productoDto.str_descripcion,
                Int_cantidad_actual = productoDto.int_cantidad_actual,
                Int_cantidad_minima = productoDto.int_cantidad_minima,
                Dec_costo_PPP = productoDto.dec_costo_PPP,
                Int_iva = productoDto.int_iva,
                Dec_precio_mayorista = productoDto.dec_precio_mayorista,
                Dec_precio_minorista = productoDto.dec_precio_minorista,
                Fk_deposito = depositoId,
                Fk_proveedor = proveedorId,
                Fk_marca = marcaId
            };
        }
        
        public static Producto ToProductoFromUpdate(this UpdateProductoRequestDto productoDto)
        {
            return new Producto
            {
                Str_ruta_imagen = productoDto.str_ruta_imagen,
                Str_nombre = productoDto.str_nombre,
                Str_descripcion = productoDto.str_descripcion,
                Int_cantidad_actual = productoDto.int_cantidad_actual,
                Int_cantidad_minima = productoDto.int_cantidad_minima,
                Dec_costo_PPP = productoDto.dec_costo_PPP,
                Int_iva = productoDto.int_iva,
                Dec_precio_mayorista = productoDto.dec_precio_mayorista,
                Dec_precio_minorista = productoDto.dec_precio_minorista,
            };
        }
    }
}