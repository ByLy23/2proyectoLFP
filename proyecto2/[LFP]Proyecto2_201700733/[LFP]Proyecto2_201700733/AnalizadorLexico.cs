using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto2_201700733
{
    class AnalizadorLexico
    {
        LinkedList<Error> lErrores;
        LinkedList<Token> listaTokens;
        int columna=1;
        int fila=1;
        int estado=0;
        char c;
        string lexema="";
        public LinkedList<Token> escanear(String entrada)
        {
            lErrores = new LinkedList<Error>();
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
                            estado = 0;
                            agregaToken(Token.Tipo.LLAVE_ABIERTA, c+"", fila, columna);
                            limpiar();
                        }
                        else if (c=='[')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.CORCHETE_ABIERTO, c + "", fila, columna);
                            limpiar();
                        }
                        else if (c == '(')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.PARENTESIS_ABIERTO, c + "", fila, columna);
                            limpiar();

                        }
                        else if (c == ')')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.PARENTESIS_CERRADO, c + "", fila, columna);
                            limpiar();

                        }
                        else if (c == ']')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.CORCHETE_CERRADO, c + "", fila, columna);
                            limpiar();

                        }
                        else if (c == '}')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.LLAVE_CERRADA, c + "", fila, columna);
                            limpiar();
                        }
                        else if (c == '-')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.SIGNO_RESTA, c + "", fila, columna);
                            limpiar();
                        }
                        else if (c == '+')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.SIGNO_SUMA, c + "", fila, columna);
                            limpiar();
                        }
                        else if (c == '*')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.SIGNO_MULTI, c + "", fila, columna);
                            limpiar();
                        }
                        else if (c == ',')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.COMA, c + "", fila, columna);
                            limpiar();

                        }
                        else if (c == '.')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.PUNTO, c + "", fila, columna);
                            limpiar();

                        }
                        else if (c == ':')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.DOS_PUNTOS, c + "", fila, columna);
                            limpiar();

                        }
                        else if (c == ';')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.PUNTO_COMA, c + "", fila, columna);
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
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b' || c==' ')
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
                                agregarError("Simbolo Desconocido", c+"", fila, columna);
                                estado = 0;
                                limpiar();
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
                        else if (c=='[')
                        {
                            cambiar(c, 15);
                            
                        }
                        else
                        {
                            estado = 0;
                            verificaLexema(lexema);
                            limpiar();
                            i -= 1;
                        }
                        break;
                    case 2:
                        if (c=='/')
                        {
                            cambiar(c, 14);
                        }
                        else if (c == '*')
                        {
                            cambiar(c, 10);
                        }
                        else
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.SIGNO_DIVI, lexema, fila, columna);
                            i -= 1;
                            limpiar();
                        }
                        break;
                    case 3:
                        if (c=='=')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.IGUALADOR, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.SIGNO_IGUAL, lexema, fila, columna);
                            limpiar();
                            i -= 1;
                        }
                        break;
                    case 4:
                        if (c=='=')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.SIGNO_MAYOR_IGUAL, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.SIGNO_MAYOR, lexema, fila, columna);
                            limpiar();
                            i -= 1;
                        }
                        break;
                    case 5:
                        if (c =='=')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.SIGNO_MENOR_IGUAL, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.SIGNO_MENOR, lexema, fila, columna);
                            limpiar();
                            i -= 1;
                        }
                        break;
                    case 6:
                        if (c=='=')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.SIGNO_DIFERENTE_A, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            estado = 0;
                            agregarError("Se esperaba el signo: =", lexema, fila, columna);
                            limpiar();
                            i -= 1;
                        }
                        break;
                    case 7:
                        if (c=='\'')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.COM_SIMPLE_CHAR, lexema, fila, columna);
                            limpiar();
                        }
                        else if (c == '\n')
                        {
                            cambiar(c, 7);
                            fila++;
                            columna = 1;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b' || c == ' ')
                        {
                            cambiar(c, 7);
                        }
                        else if (Char.IsLetterOrDigit(c) || Char.IsSymbol(c) || Char.IsSeparator(c) || Char.IsPunctuation(c))
                        {
                            cambiar(c, 7);
                        }
                        break;
                    case 8:
                        if (Char.IsNumber(c))
                        {
                            cambiar(c, 8);
                        }
                        else if (c=='.')
                        {
                            cambiar(c, 12);
                        }
                        else
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.NUM_ENTERO, lexema, fila, columna);
                            limpiar();
                            i -= 1;
                        }
                        break;
                    case 9:
                        if (c == '"' || c == '”' || c == '“')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.CADENA, lexema, fila, columna);
                            limpiar();
                        }
                        else if (c == '\n')
                        {
                            cambiar(c, 9);
                            fila++;
                            columna = 1;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b' || c == ' ')
                        {
                            cambiar(c, 9);
                        }
                        else if (Char.IsLetterOrDigit(c) || Char.IsSymbol(c) || Char.IsSeparator(c) || Char.IsPunctuation(c))
                        {
                            cambiar(c, 9);
                        }
                        break;
                    case 10:
                        if (c =='*')
                        {
                            cambiar(c, 11);
                        }
                        else if (c == '\n')
                        {
                            cambiar(c, 10);
                            fila++;
                            columna = 1;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b' || c==' ')
                        {
                            cambiar(c, 10);
                        }
                        else if (Char.IsLetterOrDigit(c) || Char.IsSymbol(c) || Char.IsSeparator(c) || Char.IsPunctuation(c))
                        {
                            cambiar(c, 10);
                        }
                        break;
                    case 11:
                        if (c=='/')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.COM_BLOQUE, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            estado = 0;
                            agregarError("Se esperaba: /", c + "", fila, columna);
                            i -= 1;
                        }
                        break;
                    case 12:
                        if (Char.IsNumber(c))
                        {
                            cambiar(c, 13);
                        }
                        else
                        {
                            agregarError("Se esperaba al menos un numero", c + "", fila, columna);
                        }
                        break;
                    case 13:
                        if (Char.IsNumber(c))
                        {
                            cambiar(c, 13);
                        }
                        else if (c=='f')
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.NUM_FLOTANTE, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            cambiar(c, 0);
                            agregaToken(Token.Tipo.NUM_DECIMAL, lexema, fila, columna);
                            limpiar();
                            i -= 1;
                        }
                        break;
                    case 14:
                        if (c=='\n')
                        {
                            estado = 0;
                            agregaToken(Token.Tipo.COMENTARIO_SIMPLE, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            cambiar(c, 14);
                        }
                        break;
                    case 15:
                        if (c==']')
                        {
                            cambiar(c, 15);
                            agregaToken(Token.Tipo.ARREGLO, lexema, fila, columna);
                            limpiar();
                        }
                        else
                        {
                            estado = 0;
                            limpiar();
                            i--;
                        }
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
        private void verificaLexema(string lexema)
        {
            switch (lexema)
            {
                case "Console":
                    agregaToken(Token.Tipo.CONSOLA, lexema, fila, columna);
                    break;
                case "class":
                    agregaToken(Token.Tipo.RES_CLASE, lexema, fila, columna);
                    break;
                case "static":
                    agregaToken(Token.Tipo.RES_STATIC, lexema, fila, columna);
                    break;
                case "void":
                    agregaToken(Token.Tipo.RES_VOID, lexema, fila, columna);
                    break;
                case "String":
                    agregaToken(Token.Tipo.RES_STRING, lexema, fila, columna);
                    break;
                case "string":
                    agregaToken(Token.Tipo.RES_STRING, lexema, fila, columna);
                    break;
                case "int":
                    agregaToken(Token.Tipo.RES_INT, lexema, fila, columna);
                    break;
                case "bool":
                    agregaToken(Token.Tipo.RES_BOOL, lexema, fila, columna);
                    break;
                case "char":
                    agregaToken(Token.Tipo.RES_CHAR, lexema, fila, columna);
                    break;
                case "float":
                    agregaToken(Token.Tipo.RES_FLOAT, lexema, fila, columna);
                    break;
                case "false":
                    agregaToken(Token.Tipo.RES_FALSE, lexema, fila, columna);
                    break;
                case "true":
                    agregaToken(Token.Tipo.RES_TRUE, lexema, fila, columna);
                    break;
                case "new":
                    agregaToken(Token.Tipo.RES_NUEVO, lexema, fila, columna);
                    break;
                case "if":
                    agregaToken(Token.Tipo.RES_IF, lexema, fila, columna);
                    break;
                case "else":
                    agregaToken(Token.Tipo.RES_ELSE, lexema, fila, columna);
                    break;
                case "switch":
                    agregaToken(Token.Tipo.RES_SWITCH, lexema, fila, columna);
                    break;
                case "case":
                    agregaToken(Token.Tipo.RES_CASE, lexema, fila, columna);
                    break;
                case "break":
                    agregaToken(Token.Tipo.RES_PAUSA, lexema, fila, columna);
                    break;
                case "for":
                    agregaToken(Token.Tipo.RES_FOR, lexema, fila, columna);
                    break;
                case "while":
                    agregaToken(Token.Tipo.RES_WHILE, lexema, fila, columna);
                    break;
                case "WriteLine":
                    agregaToken(Token.Tipo.ESCRITURA, lexema, fila, columna);
                    break;
                case "graficarVector":
                    agregaToken(Token.Tipo.DIBUJA_VECTOR, lexema, fila, columna);
                    break;
                case "default":
                    agregaToken(Token.Tipo.RES_DEFAULT, lexema, fila, columna);
                    break;
                default:
                    agregaToken(Token.Tipo.IDENTIFICADOR, lexema, fila, columna);
                    break;
            }
        }
        public LinkedList<Error> lerr()
        {
            return lErrores;
        }
        public void imprimirErrores(LinkedList<Error> lerr)
        {
            foreach (var item in lerr)
            {
                Console.WriteLine(item.TipoError+" "+item.NombreError+" "+item.Fila+" "+item.Columna);
            }
        }
        private void limpiar()
        {
            lexema = "";
        }
        private void agregaToken(Token.Tipo tipo, String nombre,int fila, int columna)
        {
            listaTokens.AddLast(new Token(tipo, nombre,fila,columna));
        }
        private void agregarError(String tipo, String nombre, int fila, int columna)
        {
            lErrores.AddLast(new Error(tipo, nombre, fila, columna));
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
