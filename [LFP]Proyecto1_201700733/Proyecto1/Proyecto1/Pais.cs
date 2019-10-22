using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    public class Pais
    {
      private string nombre;
        private string bandera;
        private string poblacion;
        private string porcentaje;
        private string continente;
        private string grafica;
        private string saturacionGrafica;
        public Pais(string nombre, string bandera, string poblacion, string porcentaje,string continente,string grafica)
        {
            this.nombre = nombre;
            this.continente = continente;
            this.bandera = bandera;
            this.poblacion = poblacion;
            this.porcentaje = porcentaje;
            this.Grafica = grafica;
        }
  
        public string Nombre { get => nombre; set => nombre = value; }
        public string Contiente { get => continente; set => continente = value; }
        public string Bandera { get => bandera; set => bandera = value; }
        public string Poblacion { get => poblacion; set => poblacion = value; }
        public string Porcentaje { get => porcentaje; set => porcentaje = value; }
        public string Grafica { get => grafica; set => grafica = value; }
        public string SaturacionGrafica { get => saturacionGrafica; set => saturacionGrafica = value; }
    }

}
