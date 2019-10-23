﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto2_201700733
{
    class Error
    {
        private int fila;
        private int columna;
        private string nombreError;
        public Error(String nombreError, int fila, int columna)
        {
            this.Fila = fila;
            this.Columna = columna;
            this.NombreError = nombreError;
        }

        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }
        public string NombreError { get => nombreError; set => nombreError = value; }
    }
}