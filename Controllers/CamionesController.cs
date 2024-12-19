using System.Net;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transportes_API.Services;

namespace Transportes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamionesController : ControllerBase
    {
        private readonly ICamiones _service;
        private readonly TransportContext _context;

        public CamionesController(ICamiones service, TransportContext context)
        {
            _service = service;
            _context = context;
        }

        //GET 
        [HttpGet]
        [Route("getCamiones")]

        public List<Camiones_DTO> getCamiones()
        {
            List<Camiones_DTO> lista=_service.GetCamiones();
            return lista;
        }

        //GETbyID
        [HttpGet]
        [Route("getCamion/{id}")]

        public Camiones_DTO getCamion(int id)
        {
            Camiones_DTO camion = _service.GetCamion(id);
            return camion;
        }

        //POST(Insertar)
        [HttpGet]
        [Route("insertCamion")]

        public IActionResult insertCamion([FromBody] Camiones_DTO camion)
        {
            string respuesta =_service.InsertCamion(camion);

            return Ok(new { respuesta });
        }

        //PUT(Actualizar)

        [HttpPut]
        [Route("updateCamion")]

        public IActionResult updateCamion([FromBody] Camiones_DTO camion)
        {
            string respuesta = _service.UpdateCamion(camion);

            return Ok(new { respuesta });
        }

        //DELETE

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult deleteCamion(int id)
        {
            string respuesta = _service.DeleteCamion(id);

            return Ok(new { respuesta });
        }
    }
}
