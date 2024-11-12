using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Contexts;
using Proyecto_API_MHW.DataClass;
using Proyecto_API_MHW.Models;
using Proyecto_API_MHW.Service;
using System.Security.Cryptography.X509Certificates;

namespace Proyecto_API_MHW.Controllers
{
    [ApiController]
    [Route("api/mg")]
    public class MonstroGrandeController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        public MonstroGrandeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        //[HttpGet("getTest")]
        //public async Task<ActionResult<List<MonstroGrandes>>> getmgtest()
        //{
        //    return await applicationDbContext.MonstroGrandes.Include(e=> e.categorias).ToListAsync();   
        //}
        [HttpGet("get")]
        public async Task<ActionResult<List<DtamonstroGrande>>> GetMonstroGrandes()
        {
            try
            {
                List<MonstroGrande> mg = await applicationDbContext.MonstroGrandes.ToListAsync();
                List<Categoria> categoria = await applicationDbContext.Categorias.ToListAsync();
                List<Bioma> bioma = await applicationDbContext.Biomas.ToListAsync();
                List<MgBioma> mgBioma = await applicationDbContext.MgBiomas.ToListAsync();
                List<Item> items = await applicationDbContext.items.ToListAsync();
                List<Elemento> elementos = await applicationDbContext.elementos.ToListAsync();
                List<MgElemento> mgElementos = await applicationDbContext.mgElementos.ToListAsync();
                List<MgDebilidad> mgDebilidads = await applicationDbContext.mgDebilidades.ToListAsync();
                List<Rango> rangos = await applicationDbContext.rangos.ToListAsync();
                List<MgRango> mgRangos = await applicationDbContext.mgRangos.ToListAsync();


                GetDataMG Getdata = new GetDataMG(
                    mg,
                    categoria,
                    bioma,
                    mgBioma,
                    rangos,
                    mgRangos,
                    elementos,
                    mgElementos,
                    mgDebilidads,
                    items
                    );
                List<DtamonstroGrande> response = Getdata.GetMonstro();
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ups, Ocurrio un erro");
            }
        }
       

        [HttpPost("post")]
        public async Task<ActionResult<MonstroGrande>> postMG(MonstroGrande data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                applicationDbContext.MonstroGrandes.Add(data);
                await applicationDbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(postMG), new { id = data.id_monstrog }, data);
            }catch (DbUpdateException)
            {
                return StatusCode(500, "Error for create Object");
            }
        }
    }
}
