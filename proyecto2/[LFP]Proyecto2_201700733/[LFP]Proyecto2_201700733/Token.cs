using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto2_201700733
{
    class Token
    {
        public enum Tipo{
            RES_CLASE,
            RES_PRIVATE,
            RES_PUBLIC,
            RES_STATIC,
            RES_VOID,
            RES_STRING,
            RES_INT,
            RES_BOOL,
            RES_CHAR,
            RES_FLOAT,
            RES_FALSE,
            RES_TRUE,
            RES_NUEVO,
            RES_IF,
            RES_ELSE,
            RES_SWITCH,
            RES_CASE,
            RES_PAUSA,
            RES_FOR,
            RES_WHILE,
            RES_ARREGLO,
            SIGNO_IGUAL,
            SIGNO_SUMA,
            SIGNO_RESTA,
            SIGNO_MULTI,
            SIGNO_DIVI,
            SIGNO_MAYOR_IGUAL,
            SIGNO_MENOR_IGUAL,
            SIGNO_DIFERENTE_A,
            SIGNO_MENOR,
            SIGNO_MAYOR,
            IGUALADOR,
            COMENTARIO_SIMPLE,
            COM_BLOQUE_INICIO,
            COM_BLOQUE_FIN,
            ESCRITURA,
            CONSOLA,
            DIBUJA_VECTOR,
            LLAVE_ABIERTA,
            LLAVE_CERRADA,
            COM_SIMPLE_CHAR,
            NUM_FLOTANTE,
            NUM_DECIMAL,
            IDENTIFICADOR,
            RES_MENU,
            CORCHETE_ABIERTO,
            CORCHETE_CERRADO,
            PARENTESIS_ABIERTO,
            PARENTESIS_CERRADO,
            PUNTO_COMA,
            COMA,
            PUNTO,
            NUM_ENTERO,
            ULTIMO
        }
        private String nombre;
        private Token.Tipo tipo;
        private int fila;
        private int columna;
        public Token()
        {
            nombre = "";
        }
        public Token(Token.Tipo toks, String nombre)
        {
            this.tipo = toks;
            this.nombre = nombre;
        }
        public string getNombre()
        {
            return nombre;
        }
        public Token.Tipo GetTipo()
        {
            return tipo;
        }
        public void setTipo(Token.Tipo tipo)
        {
            this.tipo = tipo;
        }
        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }
        public int getFila()
        {
            return fila;
        }
        public int getColumna()
        {
            return columna;
        }
        public void setFila(int fila)
        {
            this.fila = fila;
        }
        public void setColumna(int columna)
        {
            this.columna = columna;
        }
    }
}
