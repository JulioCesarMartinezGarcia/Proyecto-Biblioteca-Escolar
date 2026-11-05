//Console.WriteLine("Hello, World!");
using Proyecto_Biblioteca_escolar;

class Program
{
    // Algoritmo de ordenamiento por inserción manual 
    static void OrdenarCatalogoPorInsercion(List<Titulo> catalogo)
    {
        int n = catalogo.Count;
        for (int i = 1; i < n; i++)
        {
            Titulo actual = catalogo[i];
            int j = i - 1;

            // Ordena de mayor a menor por préstamos, y si hay empates, alfabéticamente de A a Z
            while (j >= 0 && (
                catalogo[j].PrestamosHistoricos < actual.PrestamosHistoricos ||
                (catalogo[j].PrestamosHistoricos == actual.PrestamosHistoricos && string.Compare(catalogo[j].Nombre, actual.Nombre, StringComparison.Ordinal) > 0)
            ))
            {
                catalogo[j + 1] = catalogo[j];
                j--;
            }

            catalogo[j + 1] = actual;
        }
    }
    // Búsqueda secuencial por título (con manejo de caso no encontrado)
    public static Titulo BuscarPorTituloSecuencial(List<Titulo> catalogo, string nombreBuscado)
    {
        foreach (var titulo in catalogo)
        {
            if (titulo.Nombre.Equals(nombreBuscado, StringComparison.OrdinalIgnoreCase))
            {
                return titulo; // Retorna el título si lo encuentra
            }
        }
        return null; // Manejo del caso no encontrado
    }

    // Búsqueda binaria propia por ISBN sobre una copia ordenada por ISBN
    public static Titulo BuscarPorIsbnBinaria(List<Titulo> catalogo, string isbnBuscado)
    {
        // Creamos una copia del catálogo para ordenarla por ISBN sin afectar el orden principal por préstamos
        List<Titulo> copiaOrdenadaIsbn = new List<Titulo>(catalogo);

        // Ordenamiento por inserción manual auxiliar basado en el ISBN
        int n = copiaOrdenadaIsbn.Count;
        for (int i = 1; i < n; i++)
        {
            Titulo actual = copiaOrdenadaIsbn[i];
            int j = i - 1;
            while (j >= 0 && string.Compare(copiaOrdenadaIsbn[j].Isbn, actual.Isbn, StringComparison.Ordinal) > 0)
            {
                copiaOrdenadaIsbn[j + 1] = copiaOrdenadaIsbn[j];
                j--;
            }
            copiaOrdenadaIsbn[j + 1] = actual;
        }

        // Algoritmo de búsqueda binaria propia
        int inicio = 0;
        int fin = copiaOrdenadaIsbn.Count - 1;

        while (inicio <= fin)
        {
            int medio = inicio + (fin - inicio) / 2;
            int comparacion = string.Compare(copiaOrdenadaIsbn[medio].Isbn, isbnBuscado, StringComparison.Ordinal);

            if (comparacion == 0)
            {
                return copiaOrdenadaIsbn[medio]; // Libro encontrado
            }
            else if (comparacion < 0)
            {
                inicio = medio + 1; // Buscar en la mitad derecha
            }
            else
            {
                fin = medio - 1;    // Buscar en la mitad izquierda
            }
        }

        return null; // Manejo del caso no encontrado
    }
    public static Titulo BuscarPorIsbnBinariaConConteo(List<Titulo> catalogoOrdenadoIsbn, string isbnBuscado, out int comparaciones)
    {
        comparaciones = 0;
        int inicio = 0;
        int fin = catalogoOrdenadoIsbn.Count - 1;

        while (inicio <= fin)
        {
            comparaciones++; // Cada ciclo del while implica una comparación de rangos/valores
            int medio = inicio + (fin - inicio) / 2;
            int comparacion = string.Compare(catalogoOrdenadoIsbn[medio].Isbn, isbnBuscado, StringComparison.Ordinal);

            if (comparacion == 0)
            {
                return catalogoOrdenadoIsbn[medio]; // Encontrado
            }
            else if (comparacion < 0)
            {
                inicio = medio + 1;
            }
            else
            {
                fin = medio - 1;
            }
        }

        return null; // No encontrado
    }
    static void Main(string[] args)
    {
        // Catálogo principal usando List<Título>
        List<Titulo> catalogo = new List<Titulo>();

        // 1. Carga de los 8 títulos con cantidades irregulares de ejemplares
        Titulo t1 = new Titulo("978-0131103627", "C++ Programming", 12);
        t1.Ejemplares.Add(new Ejemplar("CPP-01"));
        t1.Ejemplares.Add(new Ejemplar("CPP-02"));
        catalogo.Add(t1);

        Titulo t2 = new Titulo("978-0321125217", "Domain-Driven Design", 8);
        t2.Ejemplares.Add(new Ejemplar("DDD-01"));
        t2.Ejemplares.Add(new Ejemplar("DDD-02"));
        t2.Ejemplares.Add(new Ejemplar("DDD-03"));
        catalogo.Add(t2);

        Titulo t3 = new Titulo("978-0201633610", "Design Patterns", 25);
        t3.Ejemplares.Add(new Ejemplar("DP-01"));
        catalogo.Add(t3); // Solo 1 ejemplar

        Titulo t4 = new Titulo("978-0132350884", "Clean Code", 30);
        t4.Ejemplares.Add(new Ejemplar("CC-01"));
        t4.Ejemplares.Add(new Ejemplar("CC-02"));
        t4.Ejemplares.Add(new Ejemplar("CC-03"));
        catalogo.Add(t4);

        Titulo t5 = new Titulo("978-0596007126", "Head First Design Patterns", 15);
        t5.Ejemplares.Add(new Ejemplar("HF-01"));
        t5.Ejemplares.Add(new Ejemplar("HF-02"));
        catalogo.Add(t5);

        Titulo t6 = new Titulo("978-0134494166", "Clean Architecture", 18);
        t6.Ejemplares.Add(new Ejemplar("CA-01"));
        catalogo.Add(t6);

        Titulo t7 = new Titulo("978-0321356680", "Effective Java", 20);
        t7.Ejemplares.Add(new Ejemplar("EJ-01"));
        t7.Ejemplares.Add(new Ejemplar("EJ-02"));
        catalogo.Add(t7);

        Titulo t8 = new Titulo("978-0137081073", "The Clean Coder", 10);
        t8.Ejemplares.Add(new Ejemplar("TCC-01"));
        t8.Ejemplares.Add(new Ejemplar("TCC-02"));
        catalogo.Add(t8);

        // Verificación de que ningún título queda con celdas vacías y se cargaron los 8
        Console.WriteLine($"Total de títulos cargados: {catalogo.Count}\n");
        Console.WriteLine("Verificación de estructura y datos:");

        foreach (var titulo in catalogo)
        {
            Console.WriteLine($"- Título: {titulo.Nombre} | ISBN: {titulo.Isbn} | Ejemplares registrados: {titulo.Ejemplares.Count} | Prestamos: {titulo.PrestamosHistoricos} ");
        }
        Console.WriteLine("=== ESTADO INICIAL ===");
        Console.WriteLine($"Título: {t1.Nombre} | Disponibles: {t1.ObtenerEjemplaresDisponibles()} | Históricos: {t1.PrestamosHistoricos}");

        Console.WriteLine("\n--- EJECUTANDO PRÉSTAMOS SIMULADOS ---");

        // Préstamo 1
        bool resultado1 = t1.RegistrarPrestamo();
        Console.WriteLine($"Préstamo 1 realizado: {resultado1} | Disponibles restantes: {t1.ObtenerEjemplaresDisponibles()} | Históricos: {t1.PrestamosHistoricos}");

        // Préstamo 2
        bool resultado2 = t1.RegistrarPrestamo();
        Console.WriteLine($"Préstamo 2 realizado: {resultado2} | Disponibles restantes: {t1.ObtenerEjemplaresDisponibles()} | Históricos: {t1.PrestamosHistoricos}");

        // Préstamo 3 (Intentando prestar cuando ya no hay copias disponibles)
        bool resultado3 = t1.RegistrarPrestamo();
        Console.WriteLine($"Préstamo 3 realizado (¿Había copias?): {resultado3} | Disponibles restantes: {t1.ObtenerEjemplaresDisponibles()} | Históricos: {t1.PrestamosHistoricos}");

        Console.WriteLine("\n=== CATÁLOGO ANTES DEL ORDENAMIENTO ===");
        foreach (var t in catalogo)
        {
            Console.WriteLine($"- {t.Nombre} | Préstamos: {t.PrestamosHistoricos}");
        }

        OrdenarCatalogoPorInsercion(catalogo);

        Console.WriteLine("\n=== CATÁLOGO ORDENADO POR PRÉSTAMOS (Y ALFABÉTICO EN EMPATES) ===");
        foreach (var t in catalogo)
        {
            Console.WriteLine($"- {t.Nombre} | Préstamos: {t.PrestamosHistoricos}");
        }
        // Pruebas de Búsqueda Secuencial por Título
        Console.WriteLine("\n=== PRUEBA DE BÚSQUEDA SECUENCIAL POR TÍTULO ===");
        string tituloBusqueda = "Clean Code";
        Titulo resultadoBusquedaTitulo = BuscarPorTituloSecuencial(catalogo, tituloBusqueda);

        if (resultadoBusquedaTitulo != null)
        {
            Console.WriteLine($"¡Encontrado! Título: {resultadoBusquedaTitulo.Nombre}, ISBN: {resultadoBusquedaTitulo.Isbn}, Disponibles: {resultadoBusquedaTitulo.ObtenerEjemplaresDisponibles()}");
        }
        else
        {
            Console.WriteLine($"El título '{tituloBusqueda}' no fue encontrado en el catálogo.");
        }

        // Pruebas de Búsqueda Binaria por ISBN
        Console.WriteLine("\n=== PRUEBA DE BÚSQUEDA BINARIA POR ISBN ===");
        string isbnBusqueda = "978-0132350884"; // Clean Code
        Titulo resultadoBusquedaIsbn = BuscarPorIsbnBinaria(catalogo, isbnBusqueda);

        if (resultadoBusquedaIsbn != null)
        {
            Console.WriteLine($"¡Encontrado por ISBN! Título: {resultadoBusquedaIsbn.Nombre} | Ejemplares restantes: {resultadoBusquedaIsbn.ObtenerEjemplaresDisponibles()}");
        }
        else
        {
            Console.WriteLine($"El ISBN '{isbnBusqueda}' no existe en el sistema.");
        }
        // 5. Integración de búsquedas con el catálogo ordenado y Prueba de Escritorio (Día 6)
        Console.WriteLine("\n=== INTEGRACIÓN Y PRUEBA DE ESCRITORIO COMPLETA ===");

        // Simulación de búsqueda interactiva / integrada sobre el catálogo ya ordenado
        string isbnPrueba = "978-0132350884"; // Clean Code
        string tituloPrueba = "Design Patterns";

        Console.WriteLine($"[Consulta 1] Buscando por ISBN: {isbnPrueba}");
        Titulo resIsbn = BuscarPorIsbnBinaria(catalogo, isbnPrueba);
        if (resIsbn != null)
        {
            Console.WriteLine($"-> Encontrado: '{resIsbn.Nombre}' | Préstamos históricos: {resIsbn.PrestamosHistoricos} | Ejemplares disponibles: {resIsbn.ObtenerEjemplaresDisponibles()}");
        }
        else
        {
            Console.WriteLine("-> Libro no encontrado por ISBN.");
        }

        Console.WriteLine($"\n[Consulta 2] Buscando por Título (Secuencial): {tituloPrueba}");
        Titulo resTitulo = BuscarPorTituloSecuencial(catalogo, tituloPrueba);
        if (resTitulo != null)
        {
            Console.WriteLine($"-> Encontrado: '{resTitulo.Nombre}' | ISBN: {resTitulo.Isbn} | Préstamos históricos: {resTitulo.PrestamosHistoricos}");
        }
        else
        {
            Console.WriteLine("-> Título no encontrado.");
        }
        // 6. Mediciones y comparaciones para el Día 7
        Console.WriteLine("\n=== MEDICIÓN Y COMPARACIÓN DE BÚSQUEDA BINARIA ===");

        // Creamos la copia ordenada por ISBN para las pruebas
        List<Titulo> copiaIsbn = new List<Titulo>(catalogo);
        // (Asegúrate de tener tu lógica de ordenamiento por ISBN aquí o usar la lista ya ordenada)
        copiaIsbn.Sort((a, b) => string.Compare(a.Isbn, b.Isbn, StringComparison.Ordinal));

        // Definimos 5 ISBNs de consulta (algunos existentes, otros de prueba)
        string[] isbnsPrueba = {
            "978-0132350884", // Clean Code
            "978-0201633610", // Design Patterns
            "978-0321125217", // Domain-Driven Design
            "978-0131103627", // C++ Programming
            "978-9999999999"  // No existente (para probar caso no encontrado)
        };

        Console.WriteLine($"{"ISBN Consultado",-20} | {"Comparaciones Propias",-22} | {"Resultado Propio"}");
        Console.WriteLine(new string('-', 65));

        foreach (string isbn in isbnsPrueba)
        {
            int compsPropias = 0;
            Titulo resultado = BuscarPorIsbnBinariaConConteo(copiaIsbn, isbn, out compsPropias);
            string nombreLibro = resultado != null ? resultado.Nombre : "No encontrado";

            Console.WriteLine($"{isbn,-20} | {compsPropias,-22} | {nombreLibro}");
        }
    }

}