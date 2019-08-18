using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{

    public class Error
    {
        private String Simbolo;
        private String NombreError;
        private int fila;
        private int columna;

        public Error(string simbolo1, string nombreError1, int fila, int columna)
        {
            Simbolo1 = simbolo1;
            NombreError1 = nombreError1;
            Fila = fila;
            Columna = columna;
        }

        public string Simbolo1 { get => Simbolo; set => Simbolo = value; }
        public string NombreError1 { get => NombreError; set => NombreError = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }
    }
}
