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

namespace _LFP_Proyecto2_201700733
{
    public partial class Form1 : Form
    {
        private String testoguardado = "";
        LinkedList<Token> ltokens = new LinkedList<Token>();
        public static bool errorLexicoSintactico = false;
        public static LinkedList<Error> lerror = new LinkedList<Error>();
        LinkedList<directorio> directorios= new LinkedList<directorio>();

        public Form1()
        {
            InitializeComponent();
        }
        private void GuararComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leerFichero(cajita);
        }
        public void leerFichero(RichTextBox caja)
        {
            String texto;
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Seleccionar Fichero";
            //TIPO DE FICHERO A SELECCIONAR
            file.Filter = "c sharp(*.cs)|*.cs";
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

        private void LimpiarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cajita.Clear();
            resultadocajita.Clear();
        }

        private void GenerarTraduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorLexicoSintactico = false;
            AnalizadorLexico lex = new AnalizadorLexico();
            Analizador_Sintactico parser = new Analizador_Sintactico();
            lerror.Clear();
            ltokens.Clear();
            ltokens = lex.escanear(cajita.Text);
            ltokens.AddLast(new Token(Token.Tipo.ULTIMO, "ultimo", -1, -1));
            lerror = lex.lerr();
            lex.imprimir(ltokens);
            lex.imprimirErrores(lerror);
            parser.parsear(ltokens);
            Console.WriteLine("Fin xd");
            if (errorLexicoSintactico)
            {

            }
            else
            {
                parser.mostrarTraduccion(resultadocajita);
                guardarVariables();
                directorios.AddLast(new directorio(testoguardado+".cs"));
                directorios.AddLast(new directorio(testoguardado + ".py"));
                iniciarPython();
            }
         
        }        
        private void iniciarPython()
        {
            try
            {
                System.Diagnostics.Process.Start(procesoPy);
            }catch(Exception ex)
            {

            }
        }
        string procesoPy;
        private void guardarVariables()
        {
            SaveFileDialog guardado = new SaveFileDialog();
            if (guardado.ShowDialog()==DialogResult.OK)
            {
                StreamWriter nuevo = File.CreateText(guardado.FileName + ".cs");
                nuevo.Write(cajita.Text);
                testoguardado = guardado.FileName;
                StreamWriter nuevo1 = File.CreateText(guardado.FileName + ".py");
                procesoPy = guardado.FileName + ".py";
                nuevo1.Write(resultadocajita.Text);
                nuevo1.Close();
                nuevo.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Byron Antonio Orellana Alburez \n 201700733 \n Version 3.0");
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            if (guardar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter nuevo = File.CreateText(guardar.FileName);
                testoguardado = guardar.FileName;
                nuevo.Write(cajita.Text);
                nuevo.Close();
            }
        }

        private void ErroresToolStripMenuItem_Click(object sender, EventArgs e)
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
            try
            {
                for (int i = 0; i < lerror.Count(); i++)
                {
                    t = "";
                    t = "<tr>" +
                        "<td><strong>" + (i + 1).ToString() +
                    "</strong></td>" +
                    "<td>" + lerror.ElementAt(i).Fila +
                    "</td>" +
                    "<td>" + lerror.ElementAt(i).Columna +
                    "</td>" +
                    "<td>" + lerror.ElementAt(i).NombreError +
                    "</td>" +
                     "<td>" + lerror.ElementAt(i).TipoError +
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
            catch(Exception ex)
            {
                MessageBox.Show("No hay errores");
            }
        }

        private void TokensToolStripMenuItem_Click(object sender, EventArgs e)
        {

            String pagina;
            pagina = "<html>" +
            "<body bgcolor= #559FD7>" +
            "<h1 align='center'><U>TABLA DE TOKENS</U></h1></br>" +
            "<table cellpadding='10' border = '2' align='center'>" +
            "<tr>" +
            "<td bgcolor= #567EB9><strong>#" + "</strong></td>" +
            "<td bgcolor= #2B64B8><strong>TipoToken" + "</strong></td>" +
            "<td bgcolor= #2B64B8><strong>Lexema" + "</strong></td>" +
            "<td bgcolor= #5592EE><strong>Fila" + "</strong></td>" +
            "<td bgcolor= #5592EE><strong>Columna" + "</strong></td>" +
            "</tr>";
            String cadena = "";
            String t;
            try
            {
                for (int i = 0; i < ltokens.Count(); i++)
                {
                    t = "";
                    t = "<tr>" +
                        "<td><strong>" + (i + 1).ToString() +
                    "</strong></td>" +
                    "<td>" + ltokens.ElementAt(i).GetTipo().ToString() +
                    "</td>" +
                    "<td>" + ltokens.ElementAt(i).getNombre() +
                    "</td>" +
                    "<td>" + ltokens.ElementAt(i).getFila() +
                    "</td>" +
                    "<td>" + ltokens.ElementAt(i).getColumna() +
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
            }catch(Exception ex)
            {
                MessageBox.Show("No hay Tokens");
            }
        }

        private void LimpiarDocumentosRecientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in directorios)
            {
                string dir = item.getDireccion();
                Console.WriteLine(dir);
                if (System.IO.File.Exists(@dir))
                {
                    // Use a try block to catch IOExceptions, to
                    // handle the case of the file already being
                    // opened by another process.
                    try
                    {
                        System.IO.File.Delete(dir);
                    }
                    catch (System.IO.IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }
            }
            
        }
    }
}