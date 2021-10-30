using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practica2E2Alcoholemia.Domains;
using System.IO;
using System.Text.Json;

namespace Practica2E2Alcoholemia.Infrastructure
{
    public class BebidasRepository
    {
        
        List<InBebidas> _bebidas;

        public BebidasRepository()
        {
            var fileName = "dummy.data.bebidas.json";
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                _bebidas = JsonSerializer.Deserialize<IEnumerable<InBebidas>>(json).ToList();
            }
        }

        public IEnumerable<InBebidas> GetAll()
        {
            var query = _bebidas.Select(bebidongas => bebidongas);

            return query;
        }

        public double CalculosAlcolemia(string bebida, int vasos, int peso)
        {
            var Resultado = _bebidas.FirstOrDefault(bebidongas => bebidongas.Nombre == bebida.ToUpper());
            double Respuesta;

            if(Resultado == null)
            {
                Respuesta = -1;
            }
            else
            {
                double AlcoholSangre = 0.15; //Utiliza un factor de 15% debido a que es la cantidad documentada que pasa de manera directa a la sangre.
                double MlEtanol = 0.789; //Para calcular la masa de etanol en sangre consideramos que la densidad del etanol es igual a 0.789 g/ml.
                double AlcoholPeso = 0.08; //Para calcular el volumen de sangre se considera una constante del 8% del peso corporal.
                double AlcoholTotal = Resultado.CantidadEnMl * vasos;
                double PorCadaTipo = Resultado.GradosAlcohol * AlcoholTotal;
                double PorSangre = AlcoholSangre * PorCadaTipo;
                double MlEtanolTotal = MlEtanol * PorSangre;
                double AlcPesoTotal = AlcoholPeso * peso;
                Respuesta = MlEtanolTotal / AlcPesoTotal;                
            }

            return Respuesta;
        }








    }
}
