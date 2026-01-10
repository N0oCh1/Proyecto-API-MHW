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
        // URL para obtener una prevista de los mosntros 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DtoMonstroPreview>>> GetPreview()
        {
            return Ok(await MhwApi.MonstroGrandes
                .Include(e => e.IdElementos)
                .AsSplitQuery()
                .Select(monstro => new DtoMonstroPreview
                {
                    idMonstro = monstro.IdMonstrog,
                    nombre = monstro.Nombre,
                    elementos = monstro.IdElementos.Select(e => new DtoElemento()
                    {
                        elemento = e.Elemento1
                    }).ToList(),
                    imagen = new DtoImagen()
                    {
                        id_imagen = monstro.IdImagenNavigation.IdImagen,
                        imageUrl = monstro.IdImagenNavigation.ImageUrl,
                        iconUrl = monstro.IdImagenNavigation.IconUrl
                    },
                    detalle = $"https://localhost:7101/monstro/{monstro.IdMonstrog}"
                })
                .AsNoTracking()
                .OrderBy(e => e.idMonstro)
                .ToListAsync()
                );
        }
        // URL del detalle de los cada monstros

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DtomonstroGrande>> GetMonstro(int id)
        {
            var dto = await MhwApi.MonstroGrandes
                .Where(m => m.IdMonstrog == id)        // <-- filtrar en BD
                .AsSplitQuery()
                .Include(m => m.IdCategoriaNavigation)
                .Include(m => m.IdBiomas)
                .Include(m => m.IdElementos)
                .Include(m => m.IdImagenNavigation)
                .Include(m => m.IdRangos)
                .Include(m => m.Items)
                .Include(m => m.MgDebilidades)
                    .ThenInclude(d => d.IdElementoNavigation)
                .Select(monstro => new DtomonstroGrande
                {
                    idMonstro = monstro.IdMonstrog,
                    nombre = monstro.Nombre,
                    descripcion = monstro.Descripcion,
                    // Si Vida es int? en el modelo EF, elige un fallback razonable
                    vida = monstro.Vida ?? 0,

                    // Navegación de categoría (puede ser null si no obligatoria)
                    tipo = new DtoCategoria
                    {
                        id_categoria = monstro.IdCategoria,
                        categoria = monstro.IdCategoriaNavigation != null
                                       ? monstro.IdCategoriaNavigation.Tipo
                                       : string.Empty
                    },

                    // Navegación de imagen: protege null
                    imagen = monstro.IdImagenNavigation == null
                             ? new DtoImagen { id_imagen = 0, imageUrl = string.Empty, iconUrl = string.Empty }
                             : new DtoImagen
                             {
                                 id_imagen = monstro.IdImagenNavigation.IdImagen,
                                 imageUrl = monstro.IdImagenNavigation.ImageUrl ?? string.Empty,
                                 iconUrl = monstro.IdImagenNavigation.IconUrl ?? string.Empty
                             },

                    // Colecciones: EF puede materializar null si la navegación es opcional; usa ToList() tras Select
                    biomas = monstro.IdBiomas != null
                             ? monstro.IdBiomas.Select(b => new DtoBioma
                             {
                                 id_bioma = b.IdBioma,
                                 bioma = b.NombreBioma
                             }).ToList()
                             : new List<DtoBioma>(),

                    rangos = monstro.IdRangos != null
                             ? monstro.IdRangos.Select(r => new DtoRango
                             {
                                 id_rango = r.IdRango,
                                 rango = r.Rango1
                             }).ToList()
                             : new List<DtoRango>(),

                    elementos = monstro.IdElementos != null
                                ? monstro.IdElementos.Select(e => new DtoElemento
                                {
                                    id_elemento = e.IdElemento,
                                    elemento = e.Elemento1
                                }).ToList()
                                : new List<DtoElemento>(),

                    debilidad = monstro.MgDebilidades != null
                                ? monstro.MgDebilidades.Select(d => new DtoDebilidad
                                {
                                    id_elemento = d.IdElemento,
                                    elemento = d.IdElementoNavigation != null
                                                    ? d.IdElementoNavigation.Elemento1
                                                    : string.Empty,
                                    eficacia = d.Eficacia.HasValue ? (double)d.Eficacia.Value : 0.0
                                }).ToList()
                                : new List<DtoDebilidad>(),

                    items = monstro.Items != null
                            ? monstro.Items.Select(i => new DtoItem
                            {
                                id = i.IdItem,
                                name = i.NombreItem,
                                description = i.DescripcionItem
                            }).ToList()
                            : new List<DtoItem>()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();                 // <-- devuelve null si no existe

            if (dto is null)
                return NotFound("Monstro no encontrado");

            return Ok(dto);
        }

            [HttpGet("biomas")]
        public async Task<ActionResult<List<DtoBioma>>> getBiomas()
        {
            return await MhwApi.Biomas.Select(x => new DtoBioma()
            {
                id_bioma = x.IdBioma,
                bioma = x.NombreBioma
            }).ToListAsync();
        }
        // URL para incresar nuevos monstros

        [HttpPost]
        public async Task<ActionResult> CrearMonstro(DtomonstroGrande data)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // ver guía de ModelState [2](https://stackoverflow.com/questions/65661027/in-ef-core-5-how-can-i-insert-an-entity-with-a-many-to-many-relation-by-setting)

            // 1) Verificar existencia por nombre (más eficiente que ForEachAsync)
            var existente = await MhwApi.MonstroGrandes
                .FirstOrDefaultAsync(m => m.Nombre == data.nombre);

            if (existente is not null)
                return StatusCode(409, $"El monstro ya existe id: {existente.IdMonstrog}");

            // 2) Validar FK requerida (evita int.MinValue)
            if (data.tipo?.id_categoria is null)
                return BadRequest("id_categoria es requerido.");

            // 3) Crear imagen (validar que 'imagen' viene)
            if (data.imagen is null)
                return BadRequest("imagen es requerida.");

            var nuevaImagen = new ImagenMonstro
            {
                ImageUrl = data.imagen.imageUrl,
                IconUrl = data.imagen.iconUrl
            };
            MhwApi.ImagenMonstros.Add(nuevaImagen);
            await MhwApi.SaveChangesAsync();

            // 4) Crear monstruo principal
            var nuevoMonstro = new MonstroGrande
            {
                Nombre = data.nombre,
                Vida = data.vida,
                Descripcion = data.descripcion,
                IdCategoria = data.tipo.id_categoria.Value,
                IdImagen = nuevaImagen.IdImagen
            };
            MhwApi.MonstroGrandes.Add(nuevoMonstro);
            await MhwApi.SaveChangesAsync();

            // 5) Items (uno-a-muchos: se crean siempre, no necesitan Find)
            data.items?.ForEach(item =>
            {
                nuevoMonstro.Items.Add(new Item
                {
                    IdMonstro = nuevoMonstro.IdMonstrog,
                    NombreItem = item.name,
                    DescripcionItem = item.description
                });
            });

            // 6) Rangos: validar cada id antes de agregar (FindAsync puede devolver null) [1](https://praeclarum.org/2018/12/17/nullable-reference-types.html)
            foreach (var r in data.rangos ?? Enumerable.Empty<DtoRango>())
            {
                var rango = await MhwApi.Rangos.FindAsync(r.id_rango);
                if (rango is null)
                    return NotFound($"Rango id {r.id_rango} no existe.");
                nuevoMonstro.IdRangos.Add(rango);
            }

            // 7) Debilidades: validar elemento existente y crear relación
            foreach (var w in data.debilidad ?? Enumerable.Empty<DtoDebilidad>())
            {
                var elemento = await MhwApi.Elementos.FindAsync(w.id_elemento);
                if (elemento is null)
                    return NotFound($"Elemento id {w.id_elemento} no existe.");

                nuevoMonstro.MgDebilidades.Add(new MgDebilidade
                {
                    IdElementoNavigation = elemento,
                    IdMonstroNavigation = nuevoMonstro,
                    Eficacia = w.eficacia
                });
            }

            // 8) Elementos: validar y agregar
            foreach (var e in data.elementos ?? Enumerable.Empty<DtoElemento>())
            {
                var el = await MhwApi.Elementos.FindAsync(e.id_elemento);
                if (el is null)
                    return NotFound($"Elemento id {e.id_elemento} no existe.");

                nuevoMonstro.IdElementos.Add(el);
            }

            // 9) Biomas: **este es el caso del warning original**; validar y agregar
            foreach (var b in data.biomas ?? Enumerable.Empty<DtoBioma>())
            {
                var bioma = await MhwApi.Biomas.FindAsync(b.id_bioma);
                if (bioma is null)
                    return NotFound($"Bioma id {b.id_bioma} no existe."); // FindAsync -> null si no existe [1](https://praeclarum.org/2018/12/17/nullable-reference-types.html)

                nuevoMonstro.IdBiomas.Add(bioma); // ya no hay CS8604
            }

            await MhwApi.SaveChangesAsync();

            // devuelvo el id creado (útil para frontend)
            return Ok(new { id = nuevoMonstro.IdMonstrog });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<MonstroGrande>> PutMonstro(int id, [FromBody] DtomonstroGrande data)
        {
            try
            {
                // 1) Validar nombre duplicado (excluyendo el mismo id)
                var conflicto = await MhwApi.MonstroGrandes
                    .FirstOrDefaultAsync(m => m.Nombre == data.nombre && m.IdMonstrog != id);
                if (conflicto is not null)
                    return Conflict($"El monstro ya existe con id: {conflicto.IdMonstrog}");

                // 2) Cargar el monstruo a actualizar (SIN AsNoTracking para poder persistir cambios)
                var monstroElegido = await MhwApi.MonstroGrandes
                    .Include(m => m.IdCategoriaNavigation)
                    .Include(m => m.IdBiomas)
                    .Include(m => m.IdElementos)
                    .Include(m => m.IdImagenNavigation)
                    .Include(m => m.IdRangos)
                    .Include(m => m.Items)
                    .Include(m => m.MgDebilidades).ThenInclude(d => d.IdElementoNavigation)
                    .FirstOrDefaultAsync(e => e.IdMonstrog == id);

                if (monstroElegido is null)
                    return NotFound($"Monstro id {id} no existe.");

                // 3) Validaciones/normalizaciones mínimas del DTO para evitar null-derefs
                if (data.tipo?.id_categoria is null)
                    return BadRequest("id_categoria es requerido.");

                var imgUrl = data.imagen?.imageUrl ?? string.Empty;
                var iconUrl = data.imagen?.iconUrl ?? string.Empty;

                // 4) Actualizar propiedades escalares
                monstroElegido.Nombre = data.nombre ?? monstroElegido.Nombre;
                monstroElegido.Vida = data.vida;
                monstroElegido.Descripcion = data.descripcion ?? monstroElegido.Descripcion;
                monstroElegido.IdCategoria = data.tipo.id_categoria.Value;

                // 5) Navegación de imagen: puede estar null
                if (monstroElegido.IdImagenNavigation is null)
                {
                    monstroElegido.IdImagenNavigation = new ImagenMonstro
                    {
                        ImageUrl = imgUrl,
                        IconUrl = iconUrl
                    };
                }
                else
                {
                    monstroElegido.IdImagenNavigation.ImageUrl = imgUrl;
                    monstroElegido.IdImagenNavigation.IconUrl = iconUrl;
                }

                // 6) Actualizar colecciones (protegiendo DTOs null y usando FindAsync)
                monstroElegido.IdBiomas.Clear();
                foreach (var b in (data.biomas ?? new List<DtoBioma>()))
                {
                    var bioma = await MhwApi.Biomas.FindAsync(b.id_bioma); // null si no existe
                    if (bioma is null) return NotFound($"Bioma id {b.id_bioma} no existe.");
                    monstroElegido.IdBiomas.Add(bioma);
                }

                monstroElegido.IdRangos.Clear();
                foreach (var r in (data.rangos ?? new List<DtoRango>()))
                {
                    var rango = await MhwApi.Rangos.FindAsync(r.id_rango);
                    if (rango is null) return NotFound($"Rango id {r.id_rango} no existe.");
                    monstroElegido.IdRangos.Add(rango);
                }

                monstroElegido.IdElementos.Clear();
                foreach (var e in (data.elementos ?? new List<DtoElemento>()))
                {
                    var elemento = await MhwApi.Elementos.FindAsync(e.id_elemento);
                    if (elemento is null) return NotFound($"Elemento id {e.id_elemento} no existe.");
                    monstroElegido.IdElementos.Add(elemento);
                }

                monstroElegido.MgDebilidades.Clear();
                foreach (var w in (data.debilidad ?? new List<DtoDebilidad>()))
                {
                    var elemento = await MhwApi.Elementos.FindAsync(w.id_elemento);
                    if (elemento is null) return NotFound($"Elemento id {w.id_elemento} no existe.");

                    monstroElegido.MgDebilidades.Add(new MgDebilidade
                    {
                        IdElementoNavigation = elemento,
                        IdMonstroNavigation = monstroElegido,
                        Eficacia = w.eficacia
                    });
                }

                monstroElegido.Items.Clear();
                foreach (var i in (data.items ?? new List<DtoItem>()))
                {
                    monstroElegido.Items.Add(new Item
                    {
                        NombreItem = i.name ?? string.Empty,
                        DescripcionItem = i.description ?? string.Empty
                        // El FK a Monstro se fija por la navegación al agregar
                    });
                }

                await MhwApi.SaveChangesAsync();
                return Ok(monstroElegido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    

        [HttpDelete("{id}")]
        public async Task<ActionResult<MonstroGrande>> EliminarMonstro(int id)
        {
            MonstroGrande monstroElegido = new MonstroGrande()
            {
                IdMonstrog = id
            };
            MhwApi.MonstroGrandes.Remove(monstroElegido);
            await MhwApi.SaveChangesAsync();
            return Ok($"Monstro eliminado id: {id}");
        }


    }
}
