using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Contexts;
using Proyecto_API_MHW.DataClass;
using Proyecto_API_MHW.Models;

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
                .Include(m => m.ImagenMonstros)
                .Include(m => m.IdRangos)
                .Include(m => m.Items)
                .Include(m => m.MgDebilidades)
                .ThenInclude(e => e.IdElementoNavigation)
                .Select(monstro =>
                    new DtomonstroGrande()
                    {
                        id = monstro.IdMonstrog,
                        name = monstro.Nombre,
                        health = monstro.Vida,
                        monsterClass = new DtoCategoria() 
                        {
                            id_categoria = monstro.IdCategoria,
                            categoria = monstro.IdCategoriaNavigation.Tipo
                        },
                        image = monstro.ImagenMonstros.Select(i=>new DtoImagen()
                        {
                            imageUrl = i.ImageUrl,
                            iconUrl = i.IconUrl
                        }).ToList(),
                        location = monstro.IdBiomas.Select(b => new DtoBioma()
                        {
                            id_bioma = b.IdBioma,
                            bioma = b.NombreBioma
                        }).ToList(),
                        range = monstro.IdRangos.Select(r => new DtoRango()
                        {
                            id_rango = r.IdRango,
                            rango = r.Rango1
                        }).ToList(),
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
                .Include(m => m.ImagenMonstros)
                .Select(monstro => new DtoMonstroPreview
                {
                    idMonstro = monstro.IdMonstrog,
                    name = monstro.Nombre,
                    image = monstro.ImagenMonstros.Select(i => new DtoImagen()
                    {
                        imageUrl = i.ImageUrl,
                        iconUrl = i.IconUrl
                    }).ToList(),
                    url = $"https://localhost:7101/monstro/get/{monstro.IdMonstrog}" 
                })
                .AsNoTracking()
                .ToListAsync()
                );
        }

        [HttpPost]
        public async Task<ActionResult<MonstroGrande>> CrearMonstro (DtomonstroGrande data) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            MhwApi.MonstroGrandes.Add(new MonstroGrande()
            {
                Nombre = data.name,
                Vida = data.health,
                IdCategoria = data.monsterClass.id_categoria,
                ImagenMonstros = data.image.Select(i => new ImagenMonstro() 
                {
                    IconUrl = i.iconUrl,
                    ImageUrl = i.imageUrl
                }).ToList(),
                IdBiomas = data.location.Select(l=>new Bioma()
                {
                    IdBioma = l.id_bioma,
                    NombreBioma = l.bioma
                }).ToList(),
            });
            await MhwApi.SaveChangesAsync();
            MonstroGrande mg = MhwApi.MonstroGrandes.OrderBy(id => id.IdMonstrog).LastOrDefault();
            int mgPk = mg.IdMonstrog;
            
            Console.WriteLine(mgPk.ToString() );
            return CreatedAtAction(nameof(CrearMonstro), new { id = data.id}, data);
        }
    }
}
