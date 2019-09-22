using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1
{
    public partial class Form1 : Form
    {
        public Boolean verificaGuardado = false;
        public static int numeroErrores = 0;
        private static String testoguardado = "";


        public static int numeroNumeros = 0;
        private static int estado = 0;
        string tokenControl = "";
        int idTokenControl = 0;
        public static int columna = 0;
        public int fila = 1;
        char c;
        public static LinkedList<Pais> paisGuardar = new LinkedList<Pais>();
        public static LinkedList<Error> erros = new LinkedList<Error>();
        public static LinkedList<Token> toks = new LinkedList<Token>();
        Boolean compruebaErrores = false;
        String lexema = "";
        string nombre = "";
        string bandera = "";
        string grafica = "";
        string poblacion = "";
        string continente = "";
        string porcentaje = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void LimpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lienz0.Clear();
            pinturillo.Image = null;
        }

        private void AnalizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fila = 1;
            columna = 0;
            toks.Clear();
            erros.Clear();
            compruebaErrores = false;
            analizarTexto(lienz0.Text);
            //pintarTexto();

            if (compruebaErrores)
            {
                VerErrores();
            }

            else
            {
                // mostrarNodo();
                mostrarGrafico();
                VerTokens();
                mostrarMejorOpcion();
            }

        }
        private void pintarPalabra(String lexema, Color color, int pos)
        {
            if (this.lienz0.Text.Contains(lexema))
            {
                int index = -1;
                int selectStart = this.lienz0.SelectionStart;
                while ((index = this.lienz0.Text.IndexOf(lexema, (index + 1))) != -1)
                {
                    this.lienz0.Select((index + pos), lexema.Length);
                    this.lienz0.SelectionColor = color;
                }
            }
        }
        private void agregarError(String Simbolo, int fila, int columna, String descripcion)
        {
            erros.AddLast(new Error(Simbolo, descripcion, fila, columna));
        }
        private void VerTokens()
        {
            String pagina;
            pagina = "<html>" +
            "<body bgcolor= #559FD7>" +
            "<h1 align='center'><U>TABLA DE TOKENS</U></h1></br>" +
            "<table cellpadding='10' border = '2' align='center'>" +
            "<tr>" +
            "<td bgcolor= #567EB9><strong>#" + "</strong></td>" +
            "<td bgcolor= #2B64B8><strong>Lexema" + "</strong></td>" +
            "<td bgcolor= #4376C4><strong>idToken" + "</strong></td>" +
            "<td bgcolor= #5592EE><strong>Token" + "</strong></td>" +
            "<td bgcolor= #5592EE><strong>Fila" + "</strong></td>" +
            "<td bgcolor= #5592EE><strong>Columna" + "</strong></td>" +
            "</tr>";
            String cadena = "";
            String t;
            for (int i = 0; i < toks.Count(); i++)
            {
                t = "";
                t = "<tr>" +
                    "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +
                "<td>" + toks.ElementAt(i).Lexema1 +
                "</td>" +
                "<td>" + toks.ElementAt(i).IdToken1 +
                "</td>" +
                "<td>" + toks.ElementAt(i).NombreToken +
                "</td>" +
                "<td>" + toks.ElementAt(i).Fila +
                "</td>" +
                "<td>" + toks.ElementAt(i).Columna +
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
            "<body bgcolor= #559FD7>" +
            "<h1 align='center'><U>TABLA DE ERRORES</U></h1></br>" +
            "<table cellpadding='10' border = '2' align='center'>" +
            "<tr>" +
            "<td bgcolor= #567EB9><strong>#" + "</strong></td>" +
            "<td bgcolor= #567EB9><strong>Fila" + "</strong></td>" +
            "<td bgcolor= #567EB9><strong>Columna" + "</strong></td>" +
            "<td bgcolor= #567EB9><strong>Caracter" + "</strong></td>" +
            "<td bgcolor= #567EB9><strong>Descripcion" + "</strong></td>" +
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
                 "<td>" + erros.ElementAt(i).NombreError1 +
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
        private void verificarToken(String lexema, String tipo, int id, int fila, int columna)
        {
            switch (lexema.ToLower())
            {
                case "grafica":
                    agregarToken(lexema, "Reservada Grafica", 1, fila, columna);
                    pintarPalabra(lexema, Color.Blue, 0);
                    break;
                case "nombre":
                    agregarToken(lexema, "Reservada Nombre", 2, fila, columna);
                    pintarPalabra(lexema, Color.Blue, 0);
                    break;
                case "bandera":
                    agregarToken(lexema, "Reservada Bandera", 3, fila, columna);
                    pintarPalabra(lexema, Color.Blue, 0);
                    break;
                case "saturacion":
                    agregarToken(lexema, "Reservada Saturacion", 4, fila, columna);
                    pintarPalabra(lexema, Color.Blue, 0);
                    break;
                case "pais":
                    agregarToken(lexema, "Reservada Pais", 5, fila, columna);
                    pintarPalabra(lexema, Color.Blue, 0);
                    break;
                case "continente":
                    agregarToken(lexema, "Reservada Continente", 6, fila, columna);
                    pintarPalabra(lexema, Color.Blue, 0);
                    break;
                case "poblacion":
                    agregarToken(lexema, "Reservada Poblacion", 7, fila, columna);
                    pintarPalabra(lexema, Color.Blue, 0);
                    break;
                default:
                    agregarError(lexema, fila, columna, "Palabra no identificada");
                    compruebaErrores = true;
                    lexema = "";
                    break;
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
                            agregarToken(lexema, "Dos Puntos", 8, fila, columna);
                            pintarPalabra(lexema, Color.Black, 0);

                            lexema = "";
                        }
                        else if (c == '{')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Llave Abierta", 9, fila, columna);
                            pintarPalabra(lexema, Color.Red, 0);
                            lexema = "";
                        }
                        else if (c == '}')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Llave Cerrada", 14, fila, columna);
                            pintarPalabra(lexema, Color.Red, 0);

                            lexema = "";
                        }
                        else if (c == ';')
                        {
                            estado = 0;
                            lexema = lexema + c;
                            agregarToken(lexema, "Punto y coma", 11, fila, columna);
                            pintarPalabra(lexema, Color.Orange, 0);
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
                            estado = 0;
                            compruebaErrores = true;
                            Console.WriteLine("Error: " + c + " Fila: " + fila + " Columna: " + columna);
                            agregarError(c + "", fila, columna, "Simbolo no identificado");
                            lexema = "";
                        }
                        break;
                    case 1:
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            lexema = lexema + c;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b')
                        {
                            estado = 0;
                            verificarToken(lexema, "tipo", 0, fila, columna);
                            lexema = "";
                        }
                        else if (c == '\n')
                        {
                            estado = 0;
                            columna = 0;
                            fila++;
                            verificarToken(lexema, "tipo", 0, fila, columna);
                            lexema = "";
                        }
                        else if (c == ' ')
                        {
                            Console.WriteLine(lexema);
                            estado = 0;
                            verificarToken(lexema, "tipo", 0, fila, columna);
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
                        break;
                    case 2:
                        if (Char.IsNumber(c))
                        {
                            estado = 2;
                            lexema = lexema + c;
                        }
                        else if (c == '\r' || c == '\t' || c == '\f' || c == '\b')
                        {
                            agregarToken(lexema, "Numero", 12, fila, columna);
                            pintarPalabra(lexema, Color.Green, 0);
                            estado = 0;

                            lexema = "";
                        }
                        else if (c == '\n')
                        {
                            agregarToken(lexema, "Numero", 12, fila, columna);
                            pintarPalabra(lexema, Color.Green, 0);
                            estado = 0;
                            columna = 0;
                            fila++;

                            lexema = "";
                        }
                        else if (c == ' ')
                        {
                            agregarToken(lexema, "Numero", 12, fila, columna);
                            pintarPalabra(lexema, Color.Green, 0);
                            estado = 0;

                            lexema = "";
                        }
                        else if (c == '%')
                        {
                            lexema = lexema + c;
                            agregarToken(lexema, "Porcentaje", 13, fila, columna);
                            pintarPalabra(lexema, Color.LightBlue, 0);
                            //pintarPalabra(c+"", Color.Black, 0);
                            estado = 0;
                            lexema = "";
                        }
                        else
                        {
                            agregarToken(lexema, "Numero", 12, fila, columna);
                            pintarPalabra(lexema, Color.Green, 0);
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
                            agregarToken(lexema, "Cadena", 10, fila, columna);
                            pintarPalabra(lexema, Color.Yellow, 0);
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
                }
            }
            // pinturillo.Image = Image.FromFile("C:/imagenes/2.jpg");
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void WdjfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leerFichero(lienz0);
            verificaGuardado = true;
        }
        public void leerFichero(RichTextBox caja)
        {
            String texto;
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Seleccionar Fichero";
            //TIPO DE FICHERO A SELECCIONAR
            file.Filter = "Organizacion Relacional Gustoso(*.ORG)|*.ORG";
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
                String temporal = lienz0.Text;
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
                    nuevo.Write(lienz0.Text);
                    nuevo.Close();
                }
                verificaGuardado = true;
            }
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("A:/PROGRAMACION/LENGUAJES/2do Semestre/Practica1/Proyecto/Proyecto/ManualUsuario.pdf");
        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Byron Antonio Orellana Alburez\n201700733");
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void GenerarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Document pdf = new Document();
                PdfWriter.GetInstance(pdf, new FileStream("ArchivoTesto.pdf", FileMode.Create));
                pdf.Open();
                Paragraph titulo = new Paragraph();
                titulo.Font = FontFactory.GetFont(FontFactory.TIMES, 18f, BaseColor.BLUE);
                titulo.Add("Paises con mayor saturacion");
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/imagenes/1.jpg");
                imagen.ScaleAbsolute(500, 500);
                pdf.Add(titulo);
                pdf.Add(imagen);
                pdf.Close();

                //System.Diagnostics.Process.Start("A:\\PROGRAMACION\\LENGUAJES\\2do Semestre\\[LFP]Proyecto1_201700733\\Proyecto1\\Proyecto1\\bin\\Debug\\ArchivoTesto.pdf");
            } catch (Exception ex)
            {
                MessageBox.Show("NO SE PUEDE ABRIR EL DOCUMENTO YA QUE LO TIENE ABIERTO EQUIS DE :V PERO IGUAL SI LO INVESTIGA \nLE GUA DAR EL NOMBRE DE LA EXCEPCION PA QUE SALGA DE ICNORANTE EQUIS DE \n" + ex);
            }
        }
        string enlaces;
        string cuerpo;
        string inicio;
        string declaracion_variables;
        private void mostrarGrafico()
        {
            for (int i = 0; i < toks.Count(); i++)
            {
                if (toks.ElementAt(i).IdToken1.Equals(1))
                {
                    tokenControl = toks.ElementAt(i).Lexema1.ToLower();
                    idTokenControl = toks.ElementAt(i).IdToken1;
                    grafica = toks.ElementAt(i).Lexema1;
                    Console.WriteLine(tokenControl);
                }

                else if (toks.ElementAt(i).IdToken1.Equals(6))
                {
                    tokenControl = toks.ElementAt(i).Lexema1.ToLower();
                    idTokenControl = toks.ElementAt(i).IdToken1;
                    Console.WriteLine(tokenControl);
                }
                else if (toks.ElementAt(i).IdToken1.Equals(5))
                {
                    tokenControl = toks.ElementAt(i).Lexema1.ToLower();
                    idTokenControl = toks.ElementAt(i).IdToken1;
                    Console.WriteLine(tokenControl);
                }
                if (idTokenControl.Equals(1) && toks.ElementAt(i).IdToken1.Equals(10) && toks.ElementAt(i - 2).IdToken1.Equals(2))
                {
                    Console.WriteLine("Nombre de Grafico" + toks.ElementAt(i).Lexema1);

                }
                else if (idTokenControl.Equals(6) && toks.ElementAt(i).IdToken1.Equals(10) && toks.ElementAt(i - 2).IdToken1.Equals(2))
                {
                    continente = toks.ElementAt(i).Lexema1;
                    Console.WriteLine("Nombre de Continente" + toks.ElementAt(i).Lexema1);
                }
                else if (idTokenControl.Equals(5) && toks.ElementAt(i).IdToken1.Equals(10) && toks.ElementAt(i - 2).IdToken1.Equals(2))
                {
                    nombre = toks.ElementAt(i).Lexema1;
                    Console.WriteLine("Nombre de Pais" + toks.ElementAt(i).Lexema1);
                }
                else if (idTokenControl.Equals(5) && toks.ElementAt(i).IdToken1.Equals(12) && toks.ElementAt(i - 2).IdToken1.Equals(7))
                {
                    poblacion = toks.ElementAt(i).Lexema1;
                    Console.WriteLine("Poblacion " + toks.ElementAt(i).Lexema1);
                }
                else if (idTokenControl.Equals(5) && toks.ElementAt(i).IdToken1.Equals(13) && toks.ElementAt(i - 2).IdToken1.Equals(4))
                {
                    porcentaje = toks.ElementAt(i).Lexema1;
                    Console.WriteLine("Saturacion" + toks.ElementAt(i).Lexema1);
                }
                else if (idTokenControl.Equals(5) && toks.ElementAt(i).IdToken1.Equals(10) && toks.ElementAt(i - 2).IdToken1.Equals(3))
                {
                    bandera = toks.ElementAt(i).Lexema1;
                    Console.WriteLine("Bandera" + toks.ElementAt(i).Lexema1);
                }
                if (!(bandera == "") && !(continente == "") && !(nombre == "") && !(poblacion == "") && !(porcentaje == "") && !(grafica == ""))
                {
                    paisGuardar.AddLast(new Pais(nombre, bandera, poblacion, porcentaje, continente, grafica));
                    //Console.WriteLine("el pais " + nombre + " Pertenece al continente de "+continente+" tiene " + poblacion + " habitantes por lo que el porcentaje de saturacion es de: " + porcentaje + " y su bandera es " + bandera);
                    bandera = "";
                    nombre = "";
                    poblacion = "";
                    porcentaje = "";
                    inicio = "digraph" + grafica + "{\n" + declaracion_variables + "\n" + enlaces+"}";
                    Console.WriteLine( inicio);
                }
            }
            generarGrafico();
        }
        private void generarGrafico()
        {
                    
        }
        private void mostrarMejorOpcion()
        {
            string contientes;
            string grafico;
            foreach (var item in paisGuardar)
            {
                grafico = item.Grafica;
                contientes = item.Contiente;
                Console.WriteLine(item.Bandera+" "+item.Contiente+" "+item.Nombre+" "+item.Porcentaje);
                if (contientes.Equals(item.Contiente))
                {
                    Console.WriteLine(item.Nombre + " Apunta al continente: " + contientes +" Que a la vez apunta al grafico "+ grafico);
                }

            }
        }
    }
}
