using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto2_201700733
{
    class AnalizadorLexico
    {
        LinkedList<Token> listaTokens;
        int columna=1;
        int fila=1;
        int estado=0;
        char c;
        string lexema="";
        public LinkedList<Token> escanear(String entrada)
        {
            entrada = entrada + "#";
            listaTokens = new LinkedList<Token>();
            for (int i = 0; i < entrada.Length; i++)
            {
                c = entrada.ElementAt(i);
                switch (estado)
                {
                    case 0:
                        if (c=='{')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.LLAVE_ABIERTA, lexema, fila, columna);
                            limpiar();
                        }
                        else if (c=='[')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.CORCHETE_ABIERTO, lexema, fila, columna);
                            limpiar();
                        }
                        else if (c == '(')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.PARENTESIS_ABIERTO, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == ')')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.PARENTESIS_CERRADO, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == ']')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.CORCHETE_CERRADO, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == '}')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.LLAVE_CERRADA, lexema, fila, columna);
                            limpiar();
                        }
                        else if (c == '-')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.SIGNO_RESTA, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == '+')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.SIGNO_SUMA, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == '*')
                        { 
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.SIGNO_MULTI, lexema, fila, columna);
                            limpiar();
                        }
                        else if (c == ',')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.COMA, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == '.')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.PUNTO, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == ':')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.DOS_PUNTOS, lexema, fila, columna);
                            limpiar();

                        }
                        else if (c == ';')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.PUNTO_COMA, lexema, fila, columna);
                            limpiar();

                        }
                        else if (Char.IsLetter(c))
                        {
                            cambiar(c, 1);
                        }
                        else if (c=='/')
                        {
                            cambiar(c, 2);
                        }
                        else if (c=='=')
                        {
                            cambiar(c, 3);
                        }
                        else if (c == '>')
                        {
                            cambiar(c, 4);
                        }
                        else if (c == '<')
                        {
                            cambiar(c, 5);
                        }
                        else if (c == '!')
                        {
                            cambiar(c, 6);
                        }
                        else if (c == '\'')
                        {
                            cambiar(c, 7);
                        }
                        else if (Char.IsDigit(c))
                        {
                            cambiar(c, 8);
                        }
                        else if (c == '"' || c == '”' || c == '“')
                        {
                            cambiar(c, 9);
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b')
                        {
                            estado = 0;
                        }
                        else if (c == '\n')
                        {
                            estado = 0;
                            columna = 1;
                            fila++;
                        }
                        else
                        {
                            if (c=='#' && i== entrada.Length-1)
                            {
                                Console.WriteLine("Fin de Analisis lexico");
                            }
                            else
                            {
                                Console.WriteLine("Error lexico con: "+c);
                                estado = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Char.IsLetterOrDigit(c))
                        {
                            cambiar(c, 1);
                        }
                        else if (c=='_')
                        {
                            cambiar(c, 1);
                        }
                        else
                        {
                           // agregaToken(Token.Tipo.IDENTIFICADOR, lexema, fila, columna);
                            i -= 1;
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                    case 12:
                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    case 16:
                        break;
                    case 17:
                        break;
                    case 18:
                        break;
                    case 19:
                        break;
                }
                columna++;
            }
            return listaTokens;
        }
        private void cambiar(char c, int n)
        {
            estado = n;
            lexema += c;
        }
        private void limpiar()
        {
            lexema = "";
        }
        private void agregaToken(Token.Tipo tipo, String nombre,int fila, int columna)
        {
            listaTokens.AddLast(new Token(tipo, nombre,fila,columna));
        }
      public  void imprimir(LinkedList<Token> toks)
        {
            foreach (var item in toks)
            {
                Console.WriteLine(item.getNombre()+" "+item.GetTipo()+" "+item.getFila()+" "+item.getColumna());
            }
        }
    }
}
