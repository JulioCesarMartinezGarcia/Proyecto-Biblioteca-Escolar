//Console.WriteLine("Hello, World!");
using Proyecto_Biblioteca_escolar;

class Program
{
    // Algoritmo de ordenamiento por inserción manual (Día 4)
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

        // 2. Verificación de que ningún título queda con celdas vacías y se cargaron los 8
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
    }

}