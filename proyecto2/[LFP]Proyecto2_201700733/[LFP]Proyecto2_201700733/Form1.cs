﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _LFP_Proyecto2_201700733
{
    public partial class Form1 : Form
    {
        private String testoguardado = "";
        LinkedList<Token> ltokens;
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
            consolacajita.Clear();
            resultadocajita.Clear();
        }

        private void GenerarTraduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalizadorLexico lex = new AnalizadorLexico();
            ltokens = lex.escanear(cajita.Text);
            lex.imprimir(ltokens);
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}