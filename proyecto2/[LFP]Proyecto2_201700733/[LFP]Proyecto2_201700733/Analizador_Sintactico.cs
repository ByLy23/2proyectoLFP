using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _LFP_Proyecto2_201700733
{
    class Analizador_Sintactico
    {
        int tokenActual = 0;
        Token controlToken;
        string guardado="";
        LinkedList<Token> listaToken;
        public void parsear(LinkedList<Token> ltoks)
        {
            this.listaToken = ltoks;
            tokenActual = 0;
            controlToken = listaToken.ElementAt(tokenActual);
            INICIO();
        }
        public void mostrarTraduccion(RichTextBox caja)
        {
            caja.Text = guardado;
            Console.WriteLine(guardado);
        }
        public void traducir(String lexema)
        {
            guardado = guardado + " "+lexema;
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
                traducir(controlToken.getNombre());
                identificador();
                variable();
                if (controlToken.GetTipo()==Token.Tipo.PUNTO_COMA)
                {
                    emparejar(Token.Tipo.PUNTO_COMA);
                    traducir("\n");
                    masdeclaraciones();
                }
                masoperaciones();
            }
            else if (controlToken.GetTipo()==Token.Tipo.IDENTIFICADOR)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.IDENTIFICADOR);
                variable();
                if (controlToken.GetTipo() == Token.Tipo.PUNTO_COMA)
                {
                    emparejar(Token.Tipo.PUNTO_COMA);
                    traducir("\n");
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
                traducir(controlToken.getNombre());
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
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.COMA);
                traducir(controlToken.getNombre());
                identificador();
                variable();
            }
           
        }
        private void tipo1()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_STRING)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_STRING);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_CHAR)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_CHAR);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_INT)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_INT);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_BOOL)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_BOOL);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_FLOAT)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_FLOAT);
            }
            else if (controlToken.GetTipo() == Token.Tipo.ARREGLO)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.ARREGLO);
            }
            else
            {
                //epsilon
            }
        }
        private void tipo()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_STRING)
            {
                //traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_STRING);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_CHAR)
            {
               // traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_CHAR);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_INT)
            {
              //  traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_INT);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_BOOL)
            {
              //  traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_BOOL);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_FLOAT)
            {
               // traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_FLOAT);
            }
            else if (controlToken.GetTipo() == Token.Tipo.ARREGLO)
            {
               // traducir(controlToken.getNombre());
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
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_NUEVO);
                tipo1();
            }
            else if (controlToken.GetTipo()==Token.Tipo.LLAVE_ABIERTA)
            {
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                traducir("[");
                arreglonumeros();
                emparejar(Token.Tipo.LLAVE_CERRADA);
                traducir("]");
                traducir("\n");
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
                traducir(",");
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
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.CADENA);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_ENTERO)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.NUM_ENTERO);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_FLOTANTE)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.NUM_FLOTANTE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_DECIMAL)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.NUM_DECIMAL);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_TRUE)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_TRUE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.RES_FALSE)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_FALSE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.COM_SIMPLE_CHAR)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.COM_SIMPLE_CHAR);
            }

            else
            {
                traducir(controlToken.getNombre());
                identificador();
            }

        }
        private void masvalores()
        {
            caracter();
            if (controlToken.GetTipo() == Token.Tipo.SIGNO_SUMA)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.SIGNO_SUMA);
                masvalores();
            }
            else if (controlToken.GetTipo() == Token.Tipo.SIGNO_RESTA)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.SIGNO_RESTA);
                masvalores();
            }
        }
        private void caracter()
        {
            otrocaracter();
            if (controlToken.GetTipo() == Token.Tipo.SIGNO_MULTI)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.SIGNO_MULTI);
                caracter();
            }
            else if (controlToken.GetTipo() == Token.Tipo.SIGNO_DIVI)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.SIGNO_DIVI);
                caracter();
            }
        }
        private void otrocaracter()
        {
            if (controlToken.GetTipo() == Token.Tipo.NUM_FLOTANTE)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.NUM_FLOTANTE);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_DECIMAL)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.NUM_DECIMAL);
            }
            else if (controlToken.GetTipo() == Token.Tipo.NUM_ENTERO)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.NUM_ENTERO);
            }
            else if (controlToken.GetTipo()==Token.Tipo.IDENTIFICADOR)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.IDENTIFICADOR);
            }
            else if (controlToken.GetTipo() == Token.Tipo.PARENTESIS_ABIERTO)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                masvalores();
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
            }
        }
        private void comentarios()
           
        {
            if (controlToken.GetTipo() == Token.Tipo.COMENTARIO_SIMPLE)
            {
                String str = controlToken.getNombre();
                
                String cambio = str.Replace('/', '#');
                String final = cambio.Remove(0, 1);
                traducir(final);
                emparejar(Token.Tipo.COMENTARIO_SIMPLE);
                traducir("\n");
                comentarios();
                masoperaciones();
            }
            else if (controlToken.GetTipo() == Token.Tipo.COM_BLOQUE)
            {
                String primero = controlToken.getNombre();
                String cambio = primero.Replace('/', '\'');
                string cambio1 = cambio.Replace('*', '\'');
                traducir("'");
                traducir(cambio1);
                traducir("'");
                emparejar(Token.Tipo.COM_BLOQUE);
                traducir("\n");
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
            traducir("print(");
            simbolos();
            concatenacionsimbolos();
            traducir(")");
            traducir("\n");
        }
        private void concatenacionsimbolos()
        {
            if (controlToken.GetTipo()==Token.Tipo.SIGNO_SUMA)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.SIGNO_SUMA);
                simbolos();
                concatenacionsimbolos();
            }
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
                traducir("Aca deberia venir una grafica bien prrona pero no me da tiempo a hacerla :'v xd");
                traducir("\n");
                masoperaciones();
            }
        }
        private void bloqueif()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_IF)
            {
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.RES_IF);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                condicion();
                traducir("\n");
                emparejar(Token.Tipo.PARENTESIS_CERRADO);
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                bloque();
                emparejar(Token.Tipo.LLAVE_CERRADA);
                elses();
                masoperaciones();
            }
        }
        private void condicion()
        {
            traducir(controlToken.getNombre());
            identificador();
            traducir(controlToken.getNombre());
            operador();
            respuesta();
            traducir(":");
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
            else if (controlToken.GetTipo()==Token.Tipo.SIGNO_MAYOR_IGUAL)
            {
                emparejar(Token.Tipo.SIGNO_MAYOR_IGUAL);
            }
            else if (controlToken.GetTipo()==Token.Tipo.SIGNO_MENOR_IGUAL)
            {
                emparejar(Token.Tipo.SIGNO_MENOR_IGUAL);
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
            if (controlToken.GetTipo()==Token.Tipo.RES_ELSE)
            {
                traducir(controlToken.getNombre());
                traducir(":");
                traducir("\n");
                emparejar(Token.Tipo.RES_ELSE);
                emparejar(Token.Tipo.LLAVE_ABIERTA);
                bloque();
                emparejar(Token.Tipo.LLAVE_CERRADA);
            }
        }
        String identificadorSwitch;
        private void bloqueswitch()
        {
            if (controlToken.GetTipo()==Token.Tipo.RES_SWITCH)
            {
                traducir("if");
                emparejar(Token.Tipo.RES_SWITCH);
                emparejar(Token.Tipo.PARENTESIS_ABIERTO);
                traducir(controlToken.getNombre());
                identificadorSwitch = controlToken.getNombre();
                traducir("==");
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
                traducir(controlToken.getNombre());
                emparejar(Token.Tipo.NUM_ENTERO);
                traducir(controlToken.getNombre());
                traducir("\n");
                emparejar(Token.Tipo.DOS_PUNTOS);
                bloque();
                emparejar(Token.Tipo.RES_PAUSA);
                emparejar(Token.Tipo.PUNTO_COMA);
                mascasos();
            }
            defaults();
        }
        private void mascasos()
        {
            if (controlToken.GetTipo()==Token.Tipo.RES_CASE)
            {
                traducir("elif " + identificadorSwitch + " == ");
                casos();
            }    
        }
        private void defaults()
        {
            if (controlToken.GetTipo() == Token.Tipo.RES_DEFAULT)
            {
                emparejar(Token.Tipo.RES_DEFAULT);
                traducir("else");
                traducir(controlToken.getNombre());
                traducir("\n");
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
