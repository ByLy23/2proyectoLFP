using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    /*
     * NombreToken
     * ID Token
     * Lexema
     * Fila
     * Columna
     */

    public class Token
    {
        private String nombreToken;
        private int IdToken;
        private String Lexema;
        private int fila;
        private int columna;

        public Token(string nombreToken, int idToken1, string lexema1, int fila, int columna)
        {
            NombreToken = nombreToken;
            IdToken1 = idToken1;
            Lexema1 = lexema1;
            Fila = fila;
            Columna = columna;
        }

        public string NombreToken { get => nombreToken; set => nombreToken = value; }
        public int IdToken1 { get => IdToken; set => IdToken = value; }
        public string Lexema1 { get => Lexema; set => Lexema = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }

    }
}
