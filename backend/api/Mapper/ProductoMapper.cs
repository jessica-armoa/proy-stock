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
                Id = productoModel.Id,
                Str_ruta_imagen = productoModel.Str_ruta_imagen,
                Str_nombre = productoModel.Str_nombre,
                Str_descripcion = productoModel.Str_descripcion,
                Int_cantidad_actual = productoModel.Int_cantidad_actual,
                Int_cantidad_minima = productoModel.Int_cantidad_minima,
                Dec_costo = productoModel.Dec_costo,
                Dec_costo_PPP = productoModel.Dec_costo_PPP,
                Int_iva = productoModel.Int_iva,
                Dec_precio_mayorista = productoModel.Dec_precio_mayorista,
                Dec_precio_minorista = productoModel.Dec_precio_minorista,
                DetallesDeMovimientos = productoModel.DetallesDeMovimientos.Select(d => d.ToDetalleDeMovimientoDto()).ToList(),
                DepositoId = productoModel.DepositoId,
                DepositoNombre = productoModel.Deposito?.Str_nombre,
                ProveedorId = productoModel.ProveedorId,
                ProveedorNombre = productoModel.Proveedor?.Str_nombre,
                MarcaId = productoModel.MarcaId,
                MarcaNombre = productoModel.Marca?.Str_nombre
            };
        }

        public static Producto ToProductoFromCreate(this CreateProductoRequestDto productoDto, int depositoId, int proveedorId, int marcaId)
        {
            return new Producto
            {
                Str_ruta_imagen = productoDto.Str_ruta_imagen,
                Str_nombre = productoDto.Str_nombre,
                Str_descripcion = productoDto.Str_descripcion,
                Int_cantidad_minima = productoDto.Int_cantidad_minima,
                Dec_costo = productoDto.Dec_costo,
                Dec_costo_PPP = productoDto.Dec_costo_PPP,
                Int_iva = productoDto.Int_iva,
                Dec_precio_mayorista = productoDto.Dec_precio_mayorista,
                Dec_precio_minorista = productoDto.Dec_precio_minorista,
                DepositoId = depositoId,
                ProveedorId = proveedorId,
                MarcaId = marcaId
            };
        }
        public static Producto ToProductoCantidadFromCreate(this CreateProductoCantidadDto productoDto, int? depositoId, int? proveedorId, int? marcaId)
        {
            return new Producto
            {
                Str_ruta_imagen = productoDto.Str_ruta_imagen,
                Str_nombre = productoDto.Str_nombre,
                Str_descripcion = productoDto.Str_descripcion,
                Int_cantidad_actual = productoDto.Int_cantidad_actual,
                Int_cantidad_minima = productoDto.Int_cantidad_minima,
                Dec_costo = productoDto.Dec_costo,
                Dec_costo_PPP = productoDto.Dec_costo_PPP,
                Int_iva = productoDto.Int_iva,
                Dec_precio_mayorista = productoDto.Dec_precio_mayorista,
                Dec_precio_minorista = productoDto.Dec_precio_minorista,
                DepositoId = depositoId,
                ProveedorId = proveedorId,
                MarcaId = marcaId
            };
        }
        
        public static Producto ToProductoFromUpdate(this UpdateProductoRequestDto productoDto)
        {
            return new Producto
            {
                Str_ruta_imagen = productoDto.Str_ruta_imagen,
                Str_nombre = productoDto.Str_nombre,
                Str_descripcion = productoDto.Str_descripcion,
                Int_cantidad_minima = productoDto.Int_cantidad_minima,
                Dec_costo = productoDto.Dec_costo,
                Dec_costo_PPP = productoDto.Dec_costo_PPP,
                Int_iva = productoDto.Int_iva,
                Dec_precio_mayorista = productoDto.Dec_precio_mayorista,
                Dec_precio_minorista = productoDto.Dec_precio_minorista
            };
        }
        public static Producto ToProductoCantidadFromUpdate(this UpdateProductoCantidadDto productoDto)
        {
            return new Producto
            {
                Str_ruta_imagen = productoDto.Str_ruta_imagen,
                Str_nombre = productoDto.Str_nombre,
                Str_descripcion = productoDto.Str_descripcion,
                Int_cantidad_actual = productoDto.Int_cantidad_actual,
                Int_cantidad_minima = productoDto.Int_cantidad_minima,
                Dec_costo = productoDto.Dec_costo,
                Dec_costo_PPP = productoDto.Dec_costo_PPP,
                Int_iva = productoDto.Int_iva,
                Dec_precio_mayorista = productoDto.Dec_precio_mayorista,
                Dec_precio_minorista = productoDto.Dec_precio_minorista        
            };
        }
    }
}