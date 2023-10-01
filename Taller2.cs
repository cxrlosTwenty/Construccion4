//Primer Punto
using System;
using System.Collections.Generic;

public interface IHabilidad
{
    int Atacar();
    float Defensa();
}

public abstract class Pokemon : IHabilidad
{
    private string nombre;
    private string tipo;
    protected int[] ataques = new int[3];
    protected int defensa;

    public Pokemon(string nombre, string tipo, int[] ataques, int defensa)
    {
        this.nombre = nombre;
        this.tipo = tipo;
        this.ataques = ataques;
        this.defensa = defensa;
    }

    public abstract int Atacar();

    public abstract float Defensa();

    public void MostrarDatos()
    {
        Console.WriteLine("Nombre: " + nombre);
        Console.WriteLine("Tipo: " + tipo);
        Console.WriteLine("Ataques: ");
        foreach (int ataque in ataques)
        {
            Console.WriteLine("- " + ataque);
        }
        Console.WriteLine("Defensa: " + defensa);
        Console.WriteLine();
    }
}

public class Pokemon1 : Pokemon
{
    private Random random;

    public Pokemon1(string nombre, string tipo, int[] ataques, int defensa) : base(nombre, tipo, ataques, defensa)
    {
        random = new Random();
    }

    public override int Atacar()
    {
        int ataqueIndex = random.Next(0, 3);
        int puntaje = ataques[ataqueIndex];
        int multiplicador = random.Next(0, 2); // Puede ser 0 o 1

        return puntaje * multiplicador;
    }

    public override float Defensa()
    {
        return defensa * 1.0f;
    }
}

public class Pokemon2 : Pokemon
{
    private Random random;

    public Pokemon2(string nombre, string tipo, int[] ataques, int defensa) : base(nombre, tipo, ataques, defensa)
    {
        random = new Random();
    }

    public override int Atacar()
    {
        int ataqueIndex = random.Next(0, 3);
        int puntaje = ataques[ataqueIndex];
        int multiplicador = random.Next(0, 2); // Puede ser 0 o 1

        return puntaje * multiplicador;
    }

    public override float Defensa()
    {
        return defensa * 0.5f;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Ingrese los datos del Pokémon 1:");

        Console.Write("Nombre: ");
        string nombre1 = Console.ReadLine();

        Console.Write("Tipo: ");
        string tipo1 = Console.ReadLine();

        int[] ataques1 = new int[3];
        for (int i = 0; i < 3; i++)
        {
            Console.Write("Ataque " + (i + 1) + ": ");
            ataques1[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.Write("Defensa: ");
        int defensa1 = Convert.ToInt32(Console.ReadLine());

        Pokemon1 pokemon1 = new Pokemon1(nombre1, tipo1, ataques1, defensa1);

        Console.WriteLine("Ingrese los datos del Pokémon 2:");

        Console.Write("Nombre: ");
        string nombre2 = Console.ReadLine();

        Console.Write("Tipo: ");
        string tipo2 = Console.ReadLine();

        int[] ataques2 = new int[3];
        for (int i = 0; i < 3; i++)
        {
            Console.Write("Ataque " + (i + 1) + ": ");
            ataques2[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.Write("Defensa: ");
        int defensa2 = Convert.ToInt32(Console.ReadLine());

        Pokemon2 pokemon2 = new Pokemon2(nombre2, tipo2, ataques2, defensa2);

        Console.WriteLine();

        Console.WriteLine("Pokémon 1:");
        pokemon1.MostrarDatos();

        Console.WriteLine("Pokémon 2:");
        pokemon2.MostrarDatos();

        Console.WriteLine("¡Comienza la batalla!");
        Console.WriteLine();

        int resultado1 = 0;
        int resultado2 = 0;

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("Turno " + (i + 1) + ":");

            int ataque1 = pokemon1.Atacar();
            float defensa2 = pokemon2.Defensa();

            Console.WriteLine(pokemon1.GetType().Name + " ataca con " + ataque1 + " puntos");
            Console.WriteLine(pokemon2.GetType().Name + " defiende con " + defensa2 + " puntos");

            resultado1 += ataque1 - (int)defensa2;

            int ataque2 = pokemon2.Atacar();
            float defensa1 = pokemon1.Defensa();

            Console.WriteLine(pokemon2.GetType().Name + " ataca con " + ataque2 + " puntos");
            Console.WriteLine(pokemon1.GetType().Name + " defiende con " + defensa1 + " puntos");

            resultado2 += ataque2 - (int)defensa1;

            Console.WriteLine();
        }

        Console.WriteLine("Resultado de la batalla:");

        Console.WriteLine($"{pokemon1.GetType().Name}: {resultado1} puntos");
        Console.WriteLine($"{pokemon2.GetType().Name}: {resultado2} puntos");

        if (resultado1 > resultado2)
        {
            Console.WriteLine($"{pokemon1.GetType().Name} gana la batalla!");
        }
        else if (resultado2 > resultado1)
        {
            Console.WriteLine($"{pokemon2.GetType().Name} gana la batalla!");
        }
        else
        {
            Console.WriteLine("La batalla termina en empate.");
        }

        Console.ReadLine();
    }
}


//Punto 2
public interface IJugador
{
    string Nombre { get; set; }
    string Posicion { get; set; }
    int Rendimiento { get; set; }
}

public class Jugador : IJugador
{
    private string _nombre;
    private string _posicion;
    private int _rendimiento;

    public Jugador(string nombre, string posicion, int rendimiento)
    {
        _nombre = nombre;
        _posicion = posicion;
        _rendimiento = rendimiento;
    }

    public string Nombre
    {
        get => _nombre;
        set => _nombre = value;
    }

    public string Posicion
    {
        get => _posicion;
        set => _posicion = value;
    }

    public int Rendimiento
    {
        get => _rendimiento;
        set => _rendimiento = value;
    }
}

public class Equipo
{
    private List<Jugador> _jugadores;

    public Equipo()
    {
        _jugadores = new List<Jugador>();
    }

    public void AgregarJugador(Jugador jugador)
    {
        _jugadores.Add(jugador);
    }

    public List<Jugador> Jugadores
    {
        get => _jugadores;
    }

    public int RendimientoTotal
    {
        get
        {
            int rendimientoTotal = 0;
            foreach (Jugador jugador in _jugadores)
            {
                rendimientoTotal += jugador.Rendimiento;
            }

            return rendimientoTotal;
        }
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {
        List<Jugador> jugadores = new List<Jugador>();

        jugadores.Add(new Jugador("Juan", "Alvarez", 7));
        jugadores.Add(new Jugador("Jose", "Velez", 9));
        jugadores.Add(new Jugador("Maria", "Berrio", 8));
        jugadores.Add(new Jugador("Luis", "Villa", 6));
        jugadores.Add(new Jugador("Ana", "Lujan", 5));

        Equipo equipo1 = new Equipo();
        Equipo equipo2 = new Equipo();

        for (int i = 0; i < 3; i++)
        {
            equipo1.AgregarJugador(jugadores[i]);
            equipo2.AgregarJugador(jugadores[i + 3]);
        }

        Console.WriteLine("Equipo 1:");
        foreach (Jugador jugador in equipo1.Jugadores)
        {
            Console.WriteLine("* {0} ({1})", jugador.Nombre, jugador.Rendimiento);
        }

        Console.WriteLine("Equipo 2:");
        foreach (Jugador jugador in equipo2.Jugadores)
        {
            Console.WriteLine("* {0} ({1})", jugador.Nombre, jugador.Rendimiento);
        }

        Equipo equipoGanador;
        if (equipo1.RendimientoTotal > equipo2.RendimientoTotal)
        {
            equipoGanador = equipo1;
        }
        else
        {
            equipoGanador = equipo2;
        }

        Console.WriteLine("El ganador del partido es el equipo {0} con un rendimiento total de {1}", equipoGanador.Nombre, equipoGanador.RendimientoTotal);
    }
}
