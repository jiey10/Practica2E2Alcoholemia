using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practica2E2Alcoholemia.Infrastructure;
using Practica2E2Alcoholemia.Domains;

/*
Universidad Tecnologica Metropolitana
Materia: Aplicaciones Web Orientadas a Servicios
Maestro: Joel Ivan Chuc Uc 
Practica 3: Alcoholemia
Alumno: Jesus Ivan Estrella Yah
4to Cuatrimestre 4B
2do Parcial
Fecha: 30 - Octubre - 2021
*/

namespace Practica2E2Alcoholemia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BebidasController : ControllerBase
    {


        [HttpGet]
        [Route("Tomar_Datos")]
        public IActionResult GetAll()
        {
            var repository = new BebidasRepository();
            var bebidongas = repository.GetAll();
            return Ok(bebidongas);
        }

        [HttpGet]
        [Route("Calcular_Alcoholemia")]
        public IActionResult CalculosAlcolemia(string bebida, int vasos, int peso)
        {
            var Respuesta = "";
            var repository = new BebidasRepository();
            var bebidongas = repository.CalculosAlcolemia(bebida, vasos, peso);
            if(bebidongas == -1)
            {
                Respuesta = "La bebida ingresada: " + bebida + ", no existe. Intente con: Cerveza, Vino, Vermu, Licor, Brandy o Combinado";
            }
            else if(bebidongas > 0.8)
            {
                Respuesta = ("La cantidad del alcohol en la sangres es de: " + bebidongas.ToString(("##,##0.00")) + ", solicitar un conductor designado");
            }
            else
            {
                Respuesta = ("Conduzca con cuidado, buen viaje!!!");
            }
            return Ok(Respuesta);
        }


    }
}
