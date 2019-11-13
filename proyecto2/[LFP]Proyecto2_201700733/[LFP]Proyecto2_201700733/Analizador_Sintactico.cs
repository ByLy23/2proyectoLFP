using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto2_201700733
{
    class Analizador_Sintactico
    {
        int tokenActual = 0;
        Token controlToken;
        string guardado;
        LinkedList<Token> listaToken;
        public void parsear(LinkedList<Token> ltoks)
        {
            this.listaToken = ltoks;
            tokenActual = 0;
            controlToken = listaToken.ElementAt(tokenActual);
            INICIO();
        }
        private void INICIO()
        {
            clase();
            emparejar(Token.Tipo.LLAVE_ABIERTA);
            cuerpo();
            emparejar(Token.Tipo.LLAVE_CERRADA);

        }
        private void cuerpo()
        {
            mestatico();
            emparejar(Token.Tipo.LLAVE_ABIERTA);
            operaciones();
            emparejar(Token.Tipo.LLAVE_CERRADA);
        }
        private void mestatico()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_STATIC)
            {
                emparejar(Token.Tipo.RES_STATIC);
                if (controlToken.GetTipo() == Token.Tipo.RES_VOID)
                {
                    emparejar(Token.Tipo.RES_VOID);
                    identificador();
                    if (controlToken.GetTipo() == Token.Tipo.PARENTESIS_ABIERTO)
                    {
                        emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                        emparejar(Token.Tipo.ARREGLO);
                        identificador();
                        emparejar(Token.Tipo.PARENTESIS_CERRADO);
                    }
                }
            }
        }
        private void operaciones()
        {
            if (controlToken.GetTipo() != Token.Tipo.LLAVE_CERRADA)
            {
                declaracionvariables();
                comentarios();
                escritura();
                graficavector();
                bloqueif();
                bloqueswitch();
                bloquefor();
                bloquewhile();
            }
           
        }
        private void masoperaciones()
        {
            operaciones();
        }
        private void declaracionvariables()
        {
            if (controlToken.GetTipo()==Token.Tipo.RES_STRING || controlToken.GetTipo() == Token.Tipo.RES_INT || controlToken.GetTipo() == Token.Tipo.RES_FLOAT || controlToken.GetTipo() == Token.Tipo.RES_BOOL || controlToken.GetTipo() == Token.Tipo.RES_CHAR || controlToken.GetTipo() == Token.Tipo.ARREGLO)
            {
                tipo();
                identificador();
                variable();
                if (controlToken.GetTipo()==Token.Tipo.PUNTO_COMA)
                {
                    emparejar(Token.Tipo.PUNTO_COMA);
                    masdeclaraciones();
                }
                masoperaciones();
            }
            else if (controlToken.GetTipo()==Token.Tipo.IDENTIFICADOR)
            {
                emparejar(Token.Tipo.IDENTIFICADOR);
                variable();
                if (controlToken.GetTipo() == Token.Tipo.PUNTO_COMA)
                {
                    emparejar(Token.Tipo.PUNTO_COMA);
                    masdeclaraciones();
                }
                masoperaciones();
            }

        }
        private void masdeclaraciones()
        {
            declaracionvariables();
        }
        private void variable()
        {
            if (controlToken.GetTipo() == Token.Tipo.SIGNO_IGUAL)
            { 
                emparejar(Token.Tipo.SIGNO_IGUAL);
                valor();
                masvariables();
            }
            else if (controlToken.GetTipo()==Token.Tipo.COMA)
            {
                masvariables();
            }

        }
        private void masvariables()
        {
            if (controlToken.GetTipo()==Token.Tipo.COMA)
            {
                emparejar(Token.Tipo.COMA);
                identificador();
                variable();
            }
           
        }
        private void tipo()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_STRING)
            {
                emparejar(Token.Tipo.RES_STRING);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_CHAR)
            {
                emparejar(Token.Tipo.RES_CHAR);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_INT)
            {
                emparejar(Token.Tipo.RES_INT);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_BOOL)
            {
                emparejar(Token.Tipo.RES_BOOL);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_FLOAT)
            {
                emparejar(Token.Tipo.RES_FLOAT);
            }
            else if (controlToken.GetTipo() == Token.Tipo.ARREGLO)
            {
                emparejar(Token.Tipo.ARREGLO);
            }
            else
            {
                //epsilon
            }
        }
        private void valor()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_NUEVO)
            {
                emparejar(Token.Tipo.RES_NUEVO);
                tipo();
            }
            else if (controlToken.GetTipo()==Token.Tipo.LLAVE_ABIERTA)
            {
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                arreglonumeros();
                emparejar(Token.Tipo.LLAVE_CERRADA);
                emparejar(Token.Tipo.PUNTO_COMA);
            }
            else
            {
                expresion();
            }
        }
        private void arreglonumeros()
        {
            if(controlToken.GetTipo() == Token.Tipo.COM_SIMPLE_CHAR || controlToken.GetTipo() == Token.Tipo.RES_FALSE || controlToken.GetTipo() == Token.Tipo.RES_TRUE || controlToken.GetTipo() == Token.Tipo.NUM_DECIMAL || controlToken.GetTipo() == Token.Tipo.NUM_FLOTANTE || controlToken.GetTipo() == Token.Tipo.NUM_ENTERO || controlToken.GetTipo() == Token.Tipo.CADENA)
            {
                simbolos();
                massimbolos();
            }
        }
        private void massimbolos()
        {
            if (controlToken.GetTipo() == Token.Tipo.COMA)
            {
                emparejar(Token.Tipo.COMA);
                simbolos();
                massimbolos();
            }
        }
        private void expresion()
        {
            simbolos();
            masvalores();
        }
        private void simbolos()
        {
            if (controlToken.GetTipo() == Token.Tipo.CADENA)
            {
                emparejar(Token.Tipo.CADENA);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_ENTERO)
            {
                emparejar(Token.Tipo.NUM_ENTERO);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_FLOTANTE)
            {
                emparejar(Token.Tipo.NUM_FLOTANTE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_DECIMAL)
            {
                emparejar(Token.Tipo.NUM_DECIMAL);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_TRUE)
            {
                emparejar(Token.Tipo.RES_TRUE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_FALSE)
            {
                emparejar(Token.Tipo.RES_FALSE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.COM_SIMPLE_CHAR)
            {
                emparejar(Token.Tipo.COM_SIMPLE_CHAR);
            }

            else
            {
                identificador();
            }

        }
        private void masvalores()
        {
            caracter();
            if (controlToken.GetTipo() == Token.Tipo.SIGNO_SUMA)
            {
                emparejar(Token.Tipo.SIGNO_SUMA);
                masvalores();
            }
            else if (controlToken.GetTipo() == Token.Tipo.SIGNO_RESTA)
            {
                emparejar(Token.Tipo.SIGNO_RESTA);
                masvalores();
            }
        }
        private void caracter()
        {
            otrocaracter();
            if (controlToken.GetTipo() == Token.Tipo.SIGNO_MULTI)
            {
                emparejar(Token.Tipo.SIGNO_MULTI);
                caracter();
            }
            else if (controlToken.GetTipo() == Token.Tipo.SIGNO_DIVI)
            {
                emparejar(Token.Tipo.SIGNO_DIVI);
                caracter();
            }
        }
        private void otrocaracter()
        {
            if (controlToken.GetTipo() == Token.Tipo.NUM_FLOTANTE)
            {
                emparejar(Token.Tipo.NUM_FLOTANTE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_DECIMAL)
            {
                emparejar(Token.Tipo.NUM_DECIMAL);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_ENTERO)
            {
                emparejar(Token.Tipo.NUM_ENTERO);
            }
            else if (controlToken.GetTipo() == Token.Tipo.PARENTESIS_ABIERTO)
            {
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                masvalores();
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
            }
        }
        private void comentarios()
        {
            if (controlToken.GetTipo() == Token.Tipo.COMENTARIO_SIMPLE)
            {
                emparejar(Token.Tipo.COMENTARIO_SIMPLE);
                comentarios();
                masoperaciones();
            }
            else if (controlToken.GetTipo() == Token.Tipo.COM_BLOQUE)
            {
                emparejar(Token.Tipo.COM_BLOQUE);
                comentarios();
                masoperaciones();
            }
        }
        private void escritura()
        {
            if (controlToken.GetTipo() == Token.Tipo.CONSOLA)
            {
                emparejar(Token.Tipo.CONSOLA);
                emparejar(Token.Tipo.PUNTO);
                emparejar(Token.Tipo.ESCRITURA);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                muestraescritura();
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
                emparejar(Token.Tipo.PUNTO_COMA);
                masoperaciones();
            }
        }
        private void muestraescritura()
        {
            valor();
            masidentificadores();
        }
        private void masidentificadores()
        {
            if (controlToken.GetTipo() == Token.Tipo.SIGNO_SUMA)
            {
                emparejar(Token.Tipo.SIGNO_SUMA);
                valor();
                masidentificadores();
            }
        }
        private void graficavector()
        {
            if (controlToken.GetTipo() == Token.Tipo.DIBUJA_VECTOR)
            {
                emparejar(Token.Tipo.DIBUJA_VECTOR);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                identificador();
                emparejar(Token.Tipo.COMA);
                emparejar(Token.Tipo.CADENA);
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
                emparejar(Token.Tipo.PUNTO_COMA);
                masoperaciones();
            }
        }
        private void bloqueif()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_IF)
            {
                emparejar(Token.Tipo.RES_IF);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                condicion();
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
                emparejar(Token.Tipo.CORCHETE_ABIERTO);
                bloque();
                emparejar(Token.Tipo.CORCHETE_CERRADO);
                elses();
                masoperaciones();

            }
        }
        private void condicion()
        {
            identificador();
            operador();
            respuesta();
        }
        private void respuesta()
        {
            if (controlToken.GetTipo() == Token.Tipo.COM_SIMPLE_CHAR || controlToken.GetTipo() == Token.Tipo.RES_FALSE || controlToken.GetTipo() == Token.Tipo.RES_TRUE || controlToken.GetTipo() == Token.Tipo.NUM_DECIMAL || controlToken.GetTipo() == Token.Tipo.NUM_FLOTANTE || controlToken.GetTipo() == Token.Tipo.NUM_ENTERO || controlToken.GetTipo() == Token.Tipo.CADENA)
            {
                simbolos();
            }
            else
            {
                identificador();
            }
            
        }
        private void operador()
        {
            if (controlToken.GetTipo()==Token.Tipo.IGUALADOR)
            {
                emparejar(Token.Tipo.IGUALADOR);
            }
            else if (controlToken.GetTipo()==Token.Tipo.SIGNO_MAYOR)
            {
                emparejar(Token.Tipo.SIGNO_MAYOR);
            }
            else if (controlToken.GetTipo() == Token.Tipo.SIGNO_MENOR)
            {
                emparejar(Token.Tipo.SIGNO_MENOR);
            }
            else if (controlToken.GetTipo() == Token.Tipo.SIGNO_DIFERENTE_A)
            {
                emparejar(Token.Tipo.SIGNO_DIFERENTE_A);
            }
        }
        private void bloque()
        {
            instruccion();
        }
        private void instruccion()
        {
            if (controlToken.GetTipo() != Token.Tipo.LLAVE_CERRADA)
            {
                bloqueif();
                declaracionvariables();
                comentarios();
                escritura();
                graficavector();
                bloqueswitch();
                bloquefor();
                bloquewhile();
            }
           
        }
        private void elses()
        {
            if (controlToken.GetTipo()==Token.Tipo.LLAVE_ABIERTA)
            {
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                bloque();
                emparejar(Token.Tipo.LLAVE_CERRADA);
            }
        }
        private void bloqueswitch()
        {
            if (controlToken.GetTipo()==Token.Tipo.RES_SWITCH)
            {
                emparejar(Token.Tipo.RES_SWITCH);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                identificador();
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                casos();
                emparejar(Token.Tipo.LLAVE_CERRADA);
                masoperaciones();
            }
           
        }
        private void casos()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_CASE)
            {
                emparejar(Token.Tipo.RES_CASE);
                emparejar(Token.Tipo.NUM_ENTERO);
                emparejar(Token.Tipo.DOS_PUNTOS);
                bloque();
                emparejar(Token.Tipo.RES_PAUSA);
                emparejar(Token.Tipo.PUNTO_COMA);
                casos();
            }
            defaults();
        }
        private void defaults()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_DEFAULT)
            {
                emparejar(Token.Tipo.RES_DEFAULT);
                emparejar(Token.Tipo.DOS_PUNTOS);
                bloque();
                emparejar(Token.Tipo.RES_PAUSA);
                emparejar(Token.Tipo.PUNTO_COMA);
            }
        }
        private void bloquefor()
        {
            if (controlToken.GetTipo()==Token.Tipo.RES_FOR)
            {
                emparejar(Token.Tipo.RES_FOR);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                condiciones();
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                bloque();
                emparejar(Token.Tipo.LLAVE_CERRADA);
                masoperaciones();
            }
            
        }
        private void condiciones()
        {
            asignacion();
            emparejar(Token.Tipo.PUNTO_COMA);
            condicion();
            emparejar(Token.Tipo.PUNTO_COMA);
            incremento();
        }
        private void asignacion()
        {
            tipo();
            identificador();
            emparejar(Token.Tipo.SIGNO_IGUAL);
            simbolos();
        }
        private void incremento()
        {
            identificador();
            solucion();
        }
        private void solucion()
        {
            if (controlToken.GetTipo()==Token.Tipo.SIGNO_SUMA)
            {
                emparejar(Token.Tipo.SIGNO_SUMA);
                emparejar(Token.Tipo.SIGNO_SUMA);
            }
            else if (controlToken.GetTipo()==Token.Tipo.SIGNO_RESTA)
            {
                emparejar(Token.Tipo.SIGNO_RESTA);
                emparejar(Token.Tipo.SIGNO_RESTA);
            }
        }
        private void bloquewhile()
        {
            if (controlToken.GetTipo()==Token.Tipo.RES_WHILE)
            {
                emparejar(Token.Tipo.RES_WHILE);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                condicion();
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                bloque();
                emparejar(Token.Tipo.LLAVE_CERRADA);
                masoperaciones();
            }
        }
        private void clase()
        {
            if (controlToken.GetTipo()==Token.Tipo.RES_CLASE)
            {
                emparejar(Token.Tipo.RES_CLASE);
                identificador();
            }
            else { error(); }
        }
        
        private void identificador()
        {
            if (controlToken.GetTipo()==Token.Tipo.IDENTIFICADOR)
            {
                emparejar(Token.Tipo.IDENTIFICADOR);
            }
            else { error(); }
        }
        private void emparejar(Token.Tipo tipo)
        {
            Token.Tipo tips = controlToken.GetTipo();
        
            if (controlToken.GetTipo()!=tipo)
            {
                Console.WriteLine("Error, no es el simbolo que se esperaba"+tips);
            }
            if (controlToken.GetTipo()!=Token.Tipo.ULTIMO)
            {
                tokenActual++;
                controlToken = listaToken.ElementAt(tokenActual);
            }
            
        }
        private void error()
        {
            Console.WriteLine("Error, no es el simbolo que se esperaba");
        }
    }
}
