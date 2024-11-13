﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Contexts;
using Proyecto_API_MHW.DataClass;

namespace Proyecto_API_MHW.Controllers
{
    [Route("monstro")]
    [ApiController]
    public class MonstroGrandeController : ControllerBase
    {
        private readonly MhwApiContext MhwApi;

        public MonstroGrandeController(MhwApiContext context)
        {
            this.MhwApi = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DtomonstroGrande>>> GetMonstro(int id)
        {
            List<DtomonstroGrande> response = await MhwApi.MonstroGrandes

                .AsSplitQuery()
                .Include(m => m.IdCategoriaNavigation)
                .Include(m => m.IdBiomas)
                .Include(m => m.IdElementos)
                .Include(m => m.IdImagenNavigation)
                .Include(m => m.IdRangos)
                .Include(m => m.Items)
                .Include(m => m.MgDebilidades)
                .ThenInclude(e => e.IdElementoNavigation)
                .Select(monstro =>
                    new DtomonstroGrande()
                    {
                        id = monstro.IdMonstrog,
                        name = monstro.Nombre ?? string.Empty,
                        health = monstro.Vida ?? int.MinValue,
                        monsterClass = monstro.IdCategoriaNavigation.Tipo,
                        image = new DtoImagen()
                        {
                            imageUrl = monstro.IdImagenNavigation.ImageUrl,
                            iconUrl = monstro.IdImagenNavigation.IconUrl
                        },
                        location = monstro.IdBiomas.Select(b => b.NombreBioma).ToList(),
                        range = monstro.IdRangos.Select(r => r.Rango1).ToList(),
                        elements = monstro.IdElementos.Select(e => new DtoElemento()
                        {
                            id_elemento = e.IdElemento,
                            elemento = e.Elemento1
                        }).ToList(),
                        weekness = monstro.MgDebilidades.Select(d => new DtoDebilidad()
                        {
                            id_elemento = d.IdElemento,
                            elemento = d.IdElementoNavigation.Elemento1,
                            eficacia = (double)d.Eficacia,
                        }).ToList(),
                        items = monstro.Items.Select(i => new DtoItem()
                        {
                            id = i.IdItem,
                            name = i.NombreItem,
                            description = i.DescripcionItem
                        }).ToList(),

                    }
                )
                .AsNoTracking()
                .ToListAsync();
            return Ok(response.Find(x => x.id == id));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DtoMonstroPreview>>> GetPreview()
        {
            return Ok(await MhwApi.MonstroGrandes
                .AsSplitQuery()
                .Include(m => m.IdImagenNavigation)
                .Select(monstro => new DtoMonstroPreview
                {
                    idMonstro = monstro.IdMonstrog,
                    name = monstro.Nombre,
                    iconImage = monstro.IdImagenNavigation.IconUrl,
                    url = $"https://localhost:7101/monstro/get/{monstro.IdMonstrog}" 
                })
                .AsNoTracking()
                .ToListAsync()
                );
        }
    }
}
