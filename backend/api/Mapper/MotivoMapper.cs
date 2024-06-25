using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Motivo;
using api.Models;

namespace api.Mapper
{
    public static class MotivoMapper
    {
        public static MotivoDto ToMotivoDto(this Motivo motivoModel)
        {
            return new MotivoDto
            {
                Id = motivoModel.Id,
                Str_motivo = motivoModel.Str_motivo,
                Bool_perdida = motivoModel.Bool_perdida,
                Bool_borrado = motivoModel.Bool_borrado,
                MotivosPorTipoDeMovimiento = motivoModel.MotivosPorTipoDeMovimiento
                    .Where(m => m.Bool_borrado != true)
                    .Select(m => m.ToMotivoPorTipoDeMovimientoDto()).ToList()
            };
        }
    }
}