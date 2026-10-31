using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Biblioteca_escolar
{
    public class Titulo
    {
        public string Isbn { get; set; }
        public string Nombre { get; set; }
        public int PrestamosHistoricos { get; set; }

        // Lista para almacenar los ejemplares de este título de forma irregular (sin celdas vacías)
        public List<Ejemplar> Ejemplares { get; set; }

        public Titulo(string isbn, string nombre, int prestamosHistoricos)
        {
            Isbn = isbn;
            Nombre = nombre;
            PrestamosHistoricos = prestamosHistoricos;
            Ejemplares = new List<Ejemplar>();
        }
    }

    public class Ejemplar
    {
        public string CodigoInventario { get; set; }
        public bool Disponible { get; set; }

        public Ejemplar(string codigoInventario)
        {
            CodigoInventario = codigoInventario;
            Disponible = true; // Por defecto está disponible al crearlo
        }
    }
}
