using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Proyecto
{
    public partial class Form1 : Form
    {
        public Boolean verificaGuardado = false;
        public static int numeroErrores = 0;
        private static String testoguardado = "";
        public static int numeroNumeros = 0;
        private static int estado = 0;
        public static int columna = 0;
        public int fila = 1;
        char c;
        private TreeNode nuevo;
        public static LinkedList<Error> erros = new LinkedList<Error>();
        public static LinkedList<Token> toks = new LinkedList<Token>();
        Boolean compruebaErrores = false;
        String lexema = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void WdjfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leerFichero(lienzo);
            verificaGuardado = true;
        }
        public void leerFichero(RichTextBox caja)
        {
            String texto;
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Seleccionar Fichero";
            //TIPO DE FICHERO A SELECCIONAR
            file.Filter = "La Yolanda(*.ly)|*.ly";
            /*************************************/
            if (file.ShowDialog() == DialogResult.OK)
            {
                testoguardado = file.FileName.ToString();
                texto = file.FileName;
                abrirFichero(texto, caja);
            }
        }
        public void abrirFichero(String text, RichTextBox caja)
        {

            int contador = 0;
            string linea = "";
            caja.Text = "";
            System.IO.StreamReader file = new System.IO.StreamReader(@text);
            while ((linea = file.ReadLine()) != null)
            {

                caja.Text += linea + "\r\n";

                contador++;
            }
            file.Close();
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verificaGuardado)
            {
                String temporal = lienzo.Text;
                using (StreamWriter sw = new StreamWriter(testoguardado))
                {
                    sw.Write(temporal);

                }
                MessageBox.Show("Guardado");

            }
            else
            {
                SaveFileDialog guardar = new SaveFileDialog();
                if (guardar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamWriter nuevo = File.CreateText(guardar.FileName);
                    testoguardado = guardar.FileName;
                    nuevo.Write(lienzo.Text);
                    nuevo.Close();
                }
                verificaGuardado = true;
            }
        }



      

          
        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in toks)
            {
                Console.WriteLine(item.NombreToken+" FILA: "+item.Fila+" COLUMNA: "+item.Columna+" TOKEN: "+item.Lexema1);
            }
            foreach (var item in erros)
            {
                Console.WriteLine(item.NombreError1+" "+item.Simbolo1+" FILA: "+item.Fila+" COLUMNA: "+item.Columna);
            }
        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Byron Antonio Orellana Alburez\n201700733");

        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void AnalizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fila = 1;
            columna = 0;
            toks.Clear();
            erros.Clear();
            analizarTexto(lienzo.Text);
            pintarTexto();
           
            if (compruebaErrores)
            {
                VerErrores();
            }
            else
            {
                mostrarNodo();
                VerTokens();
            }
           
        }
        private void analizarTexto(String rish)
        {
            for (int i = 0; i < rish.Length; i++)
            {
                c = rish[i];
                columna++;
                switch (estado)
                {
                    case 0:
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            lexema = lexema + c;
                        }
                        else if (Char.IsNumber(c))
                        {
                            estado = 2;
                            lexema = lexema + c;
                        }
                        else if (c == ':')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Dos Puntos",3, fila, columna);
                          
                            lexema = "";
                        }
                        else if (c == '{')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Llave Abierta", 1, fila, columna);
                            lexema = "";
                        }
                        else if (c == '}')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Llave Cerrada", 2, fila, columna);
                            lexema = "";
                        }
                        else if (c==';')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Punto y coma", 4, fila, columna);
                            lexema = "";
                        }
                        else if (c == '"' || c == '”')
                        {
                            estado = 3;
                            lexema = lexema + c;
                        }
                        else if (c == ' ')
                        {
                            estado = 0;
                        }
                        else if (c == '\n')
                        {
                            estado = 0;
                            columna = 0;
                            fila++;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b')
                        {
                            estado = 0;
                        }
                        else
                        {
                            //agregar EERRRRORRR
                            estado = 0;
                            compruebaErrores = true;
                            Console.WriteLine("Error: " + c + " Fila: " + fila + " Columna: " + columna);

                            agregarError(c+"", fila, columna);
                            lexema = "";

                            //error de caracter
                        }
                        break;
                    case 1:
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            lexema = lexema + c;
                            //Console.WriteLine(lexema);                          
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b')
                        {
                            estado = 0;
                             verificarToken(lexema,"tipo",0,fila,columna);
                            lexema = "";
                        }
                        else if (c == '\n')
                        {
                            estado = 0;
                            columna = 0;
                            fila++;
                            verificarToken(lexema, "tipo", 0, fila, columna);
                            // verificaToken(lexema.ToLower(), "tipo", fila, columna);
                            lexema = "";
                        }
                        else if (c == ' ')
                        {
                            //verificar lexema
                            Console.WriteLine(lexema);
                            estado = 0;
                            verificarToken(lexema, "tipo", 0, fila, columna);
                            // verificaToken(lexema.ToLower(), "tipo", fila, columna);
                            lexema = "";
                        }
                        else
                        {
                            estado = 0;
                            Console.WriteLine(lexema);
                            verificarToken(lexema, "tipo", 0, fila, columna);
                            lexema = "";
                            i--;
                            columna--;
                        }
                        //letras
                        break;
                    case 2://numeros
                        if (Char.IsNumber(c))
                        {
                            estado = 2;
                            lexema = lexema + c;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b')
                        {
                            agregarToken(lexema, "Numero", 6, fila, columna);
                            estado = 0;
                           
                            lexema = "";
                        }
                        else if (c == '\n')
                        {
                            agregarToken(lexema, "Numero", 6, fila, columna);
                            estado = 0;
                            columna = 0;
                            fila++;
                            
                            lexema = "";
                        }
                        else if (c == ' ')
                        {
                            agregarToken(lexema, "Numero", 6, fila, columna);
                            estado = 0;
                           
                            lexema = "";
                        }
                        else
                        {
                            agregarToken(lexema, "Numero", 6, fila, columna);
                            estado = 0;
                            lexema = "";

                            i--;
                            columna--;
                            numeroErrores++;
                        }
                        break;

                    case 3:
                        if (c == '”' || c == '"')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Cadena", 5, fila, columna);
                            lexema = "";

                        }
                        else if (c == '\n')
                        {
                            estado = 3;
                            columna = 0;
                            fila++;
                            lexema = lexema + c;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b')
                        {
                            estado = 3;
                            lexema = lexema + c;
                        }
                        else if (Char.IsLetterOrDigit(c) || Char.IsSymbol(c) || Char.IsSeparator(c) || Char.IsPunctuation(c))
                        {
                            estado = 3;
                            lexema = lexema + c;
                        }
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }
            }
        }
        private void agregarError(String Simbolo, int fila, int columna)
        {
            erros.AddLast(new Error(Simbolo, "Simbolo no identificado :v", fila, columna));
        }
        private void verificarToken(String lexema, String tipo, int id, int fila, int columna)
        {
            switch (lexema)
            {
                case "Planificador":
                    agregarToken(lexema, "Reservada Planificador", 7, fila, columna);
                    break;
                case "Año":
                    agregarToken(lexema, "Reservada Anio", 8, fila, columna);
                    break;
                case "Mes":
                    agregarToken(lexema, "Reservada Mes", 9, fila, columna);
                    break;
                case "Dia":
                    agregarToken(lexema, "Reservada Dia", 10, fila, columna);
                    break;
                case "Descripcion":
                    agregarToken(lexema, "Reservada Descripcion", 11, fila, columna);
                    break;
                case "Imagen":
                    agregarToken(lexema, "Reservada Imagen", 12, fila, columna);
                    break;
            }
        }
        private void pintarPalabra(String lexema, Color color, int pos)
        {
            if (this.lienzo.Text.Contains(lexema))
            {
                int index = -1;
                int selectStart = this.lienzo.SelectionStart;
                while ((index = this.lienzo.Text.IndexOf(lexema, (index + 1))) != -1)
                {
                    this.lienzo.Select((index + pos), lexema.Length);
                    this.lienzo.SelectionColor = color;
                }
            }
        }
        private void pintarTexto()
        {
            foreach (var item in toks)
            {
                if (item.IdToken1.Equals(5))
                {
                    pintarPalabra(item.Lexema1, Color.Orange, 0);

                }
                else if (item.IdToken1.Equals(7) || item.IdToken1.Equals(8) || item.IdToken1.Equals(9) || item.IdToken1.Equals(10) || item.IdToken1.Equals(11) || item.IdToken1.Equals(12))
                {
                    pintarPalabra(item.Lexema1, Color.Blue, 0);
                }
                else if (item.IdToken1.Equals(6))
                {
                    pintarPalabra(item.Lexema1, Color.Purple, 0);
                }
                else if (item.IdToken1.Equals(1) || item.IdToken1.Equals(2) || item.IdToken1.Equals(3) || item.IdToken1.Equals(4))
                {
                    pintarPalabra(item.Lexema1, Color.Black, 0);
                }
            }

        }
        private String planificador;
        private String comparacion_planificador;
        private String anios;
        private String esperanza="";
        private String com_anio;
        private String mes;
        private String com_mes;
        private String dia;
        private String com_dia;
        private void mostrarNodo()
        {
            foreach (var item in toks)
            {
               
            }
           /* for (int i = 0; i < toks.Count(); i++)
            {
                if (toks.ElementAt(i).IdToken1.Equals(5) && toks.ElementAt(i-2).IdToken1.Equals(7))
                { if (item.IdToken1.Equals(7))
                {
                    comparacion_planificador = item.Lexema1;
                    nuevo = new TreeNode(item.Lexema1);
                    arbolito.Nodes.Add(nuevo);
                    
                }
                  planificador = toks.ElementAt(i).Lexema1;
                 
                      for (int j = 0; j < toks.Count(); j++)
                        //PLANIFICADOR
                       {
                        if (toks.ElementAt(j).IdToken1.Equals(5) && toks.ElementAt(j - 2).IdToken1.Equals(7))
                        {
                            comparacion_planificador = toks.ElementAt(j).Lexema1;
                        }                       
                            if (toks.ElementAt(j).IdToken1.Equals(6)&& toks.ElementAt(j - 2).IdToken1.Equals(8) && comparacion_planificador.Equals(planificador))
                            {
                            anios = toks.ElementAt(j).Lexema1;
                                TreeNode anio = new TreeNode(toks.ElementAt(j).Lexema1);
                                nuevo.Nodes.Add(anio);
                            //ANIOS
                            for (int k = 0; k < toks.Count();   k++)
                            {
                                if (toks.ElementAt(k).IdToken1.Equals(5) && toks.ElementAt(k - 2).IdToken1.Equals(7))
                                {
                                    comparacion_planificador = toks.ElementAt(k).Lexema1;

                                }

                                if (toks.ElementAt(k).IdToken1.Equals(6) && toks.ElementAt(k - 2).IdToken1.Equals(8) && comparacion_planificador.Equals(planificador))
                                {
                                    com_anio = toks.ElementAt(k).Lexema1;
                                    
                                }
                                else
                                {
                                    esperanza = toks.ElementAt(k).Lexema1;
                                }
                                if (toks.ElementAt(k).IdToken1.Equals(6) && toks.ElementAt(k - 2).IdToken1.Equals(9))
                                {
                                    if ( comparacion_planificador.Equals(planificador))
                                    {
                                        if ( com_anio.Equals(anios)|| esperanza.Equals(anios))
                                        {
                                            mes = toks.ElementAt(k).Lexema1;
                                            TreeNode meses = new TreeNode(toks.ElementAt(k).Lexema1);
                                            anio.Nodes.Add(meses);
                                        }
                                        else
                                        {
                                           // esperanza = toks.ElementAt(k).Lexema1;
                                            break;
                                        }
                                   
                                    }
                                   
                                             
                                }
                               
                              //  if (toks.ElementAt(k).IdToken1.Equals(6) && toks.ElementAt(k-2).IdToken1.Equals(9))
                                //{

                                //}
/*
                                if (toks.ElementAt(k).IdToken1.Equals(6) && toks.ElementAt(k - 2).IdToken1.Equals(8) && comparacion_planificador.Equals(planificador))
                                {
                                    com_anio = toks.ElementAt(k).Lexema1;
                                }
                                    if (toks.ElementAt(k).IdToken1.Equals(6) && toks.ElementAt(k - 2).IdToken1.Equals(9) && comparacion_planificador.Equals(planificador) && com_anio.Equals(anios))
                                    {
                                        mes = toks.ElementAt(k).Lexema1;
                                        TreeNode meses = new TreeNode(toks.ElementAt(k).Lexema1);
                                        anio.Nodes.Add(meses);
                                    }
                                
                            }
                           }
                        
                       }
                }
            }
            foreach (var item in toks)
            {
                if (item.IdToken1.Equals(5))
                {
                    nuevo = new TreeNode(item.Lexema1);
                    arbolito.Nodes.Add(nuevo);
                }
            }*/
        }
       private void VerTokens()
        {
            String pagina;
            pagina = "<html>" +
            "<body bgcolor= #B4FFE1>" +
            "<h1 align='center'><U>TABLA DE TOKENS</U></h1></br>" +
            "<table cellpadding='20' border = '1' align='center'>" +
            "<tr>" +
            "<td bgcolor= #808000><strong>#" + "</strong></td>" +
            "<td bgcolor= #808000><strong>Lexema" + "</strong></td>" +
            "<td bgcolor= #808000><strong>idToken" + "</strong></td>" +
            "<td bgcolor= #808000><strong>Token" + "</strong></td>" +
            "</tr>";
            String cadena = "";
            String t;
            for (int i = 0; i < toks.Count(); i++)
            {
                t = "";
                t="<tr>"+
                    "<td><strong>"+ (i + 1).ToString()+
                "</strong></td>" + 
                "<td>" + toks.ElementAt(i).Lexema1 +
                "</td>" +
                "<td>" + toks.ElementAt(i).IdToken1 +
                "</td>" +
                "<td>" + toks.ElementAt(i).NombreToken +
                "</td>" +
                "</tr>";
                cadena = cadena + t;
            }
            pagina = pagina + cadena +
           "</table>" +
           "</body>" +
           "</html>";
            File.WriteAllText("Tokens.html", pagina);
            System.Diagnostics.Process.Start("Tokens.html");
            
        }
        private void VerErrores()
        {
            String pagina;
            pagina = "<html>" +
            "<body bgcolor= #B4FFE1>" +
            "<h1 align='center'><U>TABLA DE ERRORES</U></h1></br>" +
            "<table cellpadding='20' border = '1' align='center'>" +
            "<tr>" +
            "<td bgcolor= #808000><strong>#" + "</strong></td>" +
            "<td bgcolor= #808000><strong>Fila" + "</strong></td>" +
            "<td bgcolor= #808000><strong>Columna" + "</strong></td>" +
            "<td bgcolor= #808000><strong>Caracter" + "</strong></td>" +
            "<td bgcolor= #808000><strong>Descripcion" + "</strong></td>" +
            "</tr>";
            String cadena = "";
            String t;
            for (int i = 0; i < erros.Count(); i++)
            {
                t = "";
                t = "<tr>" +
                    "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +
                "<td>" + erros.ElementAt(i).Fila +
                "</td>" +
                "<td>" + erros.ElementAt(i).Columna +
                "</td>" +
                "<td>" + erros.ElementAt(i).Simbolo1 +
                "</td>" +
                 "<td>" +erros.ElementAt(i).NombreError1 +
                "</td>" +
                "</tr>";
                cadena = cadena + t;
            }
            pagina = pagina + cadena +
           "</table>" +
           "</body>" +
           "</html>";
            File.WriteAllText("Errores.html", pagina);
            System.Diagnostics.Process.Start("Errores.html");

        }
        private void agregarToken(String lexema, String tipo, int id, int fila, int columna)
        {
            toks.AddLast(new Token(tipo, id, lexema, fila, columna));
        }
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
