using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Contexts;
using Proyecto_API_MHW.DataClass;
using Proyecto_API_MHW.Models;
using System.Linq;

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
    // URL para obtener una prevista de los mosntros
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
                    nombre = monstro.Nombre,
                    imagen = monstro.ImagenMonstros.Select(i => new DtoImagen()
                    {
                        imageUrl = i.ImageUrl,
                        iconUrl = i.IconUrl
                    }).ToList(),
                    detalle = $"https://localhost:7101/monstro/{monstro.IdMonstrog}"
                })
                .AsNoTracking()
                .ToListAsync()
                );
        }
    // URL del detalle de los cada monstros
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
                        idMonstro = monstro.IdMonstrog,
                        nombre = monstro.Nombre,
                        vida = monstro.Vida,
                        tipo = new DtoCategoria() 
                        {
                            id_categoria = monstro.IdCategoria,
                            categoria = monstro.IdCategoriaNavigation.Tipo
                        },
                        imagen = monstro.ImagenMonstros.Select(i=>new DtoImagen()
                        {
                            imageUrl = i.ImageUrl,
                            iconUrl = i.IconUrl
                        }).ToList(),
                        biomas = monstro.IdBiomas.Select(b => new DtoBioma()
                        {
                            id_bioma = b.IdBioma,
                            bioma = b.NombreBioma
                        }).ToList(),
                        rangos = monstro.IdRangos.Select(r => new DtoRango()
                        {
                            id_rango = r.IdRango,
                            rango = r.Rango1
                        }).ToList(),
                        elementos = monstro.IdElementos.Select(e => new DtoElemento()
                        {
                            id_elemento = e.IdElemento,
                            elemento = e.Elemento1
                        }).ToList(),
                        debilidad = monstro.MgDebilidades.Select(d => new DtoDebilidad()
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
            return Ok(response.Find(x => x.idMonstro == id));
        }

     // URL para incresar nuevos monstros
        [HttpPost]
        public async Task<ActionResult<MonstroGrande>> CrearMonstro (DtomonstroGrande data) 
        {
        bool monstroIsExist = false;
        int idMonstroExistente = 0;
        // valido si ya existe un monstro con el mismo nombre
            await MhwApi.MonstroGrandes.ForEachAsync(monstro =>
            {
                if (monstro.Nombre == data.nombre)
                {
                    monstroIsExist = true;
                    idMonstroExistente = monstro.IdMonstrog;
                }
            });
        // validar si el modelo es valido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (monstroIsExist)
            {
                return StatusCode(409, $"El monstro ya existe id: {idMonstroExistente}");
            }

        // creo el nuevo mosntro para poder insertar en la base de datos 
            MonstroGrande nuevoMonstro = new MonstroGrande()
            {
                Nombre = data.nombre,
                Vida = data.vida,
                IdCategoria = data.tipo.id_categoria ?? int.MinValue,
                ImagenMonstros = data.imagen.Select(i => new ImagenMonstro()
                {
                    IconUrl = i.iconUrl,
                    ImageUrl = i.imageUrl
                }).ToList(),
            };
            MhwApi.MonstroGrandes.Add(nuevoMonstro);
        // guardo el nuevo mosntro para obtener la ID del mosntro creado
                
            await MhwApi.SaveChangesAsync();
            int mgID = nuevoMonstro.IdMonstrog;
        // inserto los items para el monstro
            data.items.ForEach(item =>
            {
                nuevoMonstro.Items.Add(new Item()
                {
                    IdMonstro = mgID,
                    NombreItem = item.name,
                    DescripcionItem = item.description
                });
            });
        // inserto el rango del mosntro
            data.rangos.ForEach(r =>
            {
                nuevoMonstro.IdRangos.Add(MhwApi.Rangos.Find(r.id_rango));
            });
        // inserto las debilidades del monstro
            data.debilidad.ForEach(w =>
            {
                nuevoMonstro.MgDebilidades.Add(new MgDebilidade()
                {
                    IdElementoNavigation = MhwApi.Elementos.Find(w.id_elemento),
                    IdMonstroNavigation = MhwApi.MonstroGrandes.Find(mgID),
                    Eficacia = w.eficacia
                });
            });
        // inserto los elementos del monstro
            data.elementos.ForEach(e =>
            {
                nuevoMonstro.IdElementos.Add(MhwApi.Elementos.Find(e.id_elemento));
            });
        // inserto el bioma al que pertenece el mosntro
            data.biomas.ForEach(b =>
            {
                nuevoMonstro.IdBiomas.Add(MhwApi.Biomas.Find(b.id_bioma));
            });
        // guardo los datos insertado para ese mosntro
            await MhwApi.SaveChangesAsync();
            return CreatedAtAction(nameof(CrearMonstro), new { id = nuevoMonstro.IdMonstrog }, nuevoMonstro);
        }
    }
}
