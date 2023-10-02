//Punto 1
using System;

public interface IPokemon
{
    int Atacar();
    double Defensa();
}

public abstract class PokemonBase : IPokemon
{
    private string nombre;
    private string tipo;
    protected int[] ataques = new int[3];
    protected int defensa;

    public PokemonBase(string nombre, string tipo, int[] ataques, int defensa)
    {
        this.nombre = nombre;
        this.tipo = tipo;
        this.ataques = ataques;
        this.defensa = defensa;
    }

    public abstract int Atacar();

    public abstract double Defensa();

    public string Nombre
    {
        get { return nombre; }
    }

    public string Tipo
    {
        get { return tipo; }
    }
}

public class Pokemon1 : PokemonBase
{
    public Pokemon1(string nombre, string tipo, int[] ataques, int defensa)
        : base(nombre, tipo, ataques, defensa)
    {
    }

    public override int Atacar()
    {
        Random random = new Random();
        int indiceAtaque = random.Next(0, 3);
        return ataques[indiceAtaque] * (random.Next(0, 2));
    }

    public override double Defensa()
    {
        return defensa * 1.0;
    }
}

public class Pokemon2 : PokemonBase
{
    public Pokemon2(string nombre, string tipo, int[] ataques, int defensa)
        : base(nombre, tipo, ataques, defensa)
    {
    }

    public override int Atacar()
    {
        Random random = new Random();
        int indiceAtaque = random.Next(0, 3);
        return ataques[indiceAtaque] * (random.Next(0, 2));
    }

    public override double Defensa()
    {
        return defensa * 0.5;
    }
}

class Program
{
    static void Main()
    {
        int[] ataquesPokemon1 = { 30, 20, 40 };
        int defensaPokemon1 = 20;
        Pokemon1 pokemon1 = new Pokemon1("Pokemon1", "Tipo1", ataquesPokemon1, defensaPokemon1);

        int[] ataquesPokemon2 = { 25, 35, 10 };
        int defensaPokemon2 = 30;
        Pokemon2 pokemon2 = new Pokemon2("Pokemon2", "Tipo2", ataquesPokemon2, defensaPokemon2);

        int puntosPokemon1 = 0;
        int puntosPokemon2 = 0;

        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"Turno {i}:");

            int ataque1 = pokemon1.Atacar();
            double defensa1 = pokemon1.Defensa();

            int ataque2 = pokemon2.Atacar();
            double defensa2 = pokemon2.Defensa();

            Console.WriteLine($"{pokemon1.Nombre} ataca con {ataque1} puntos y defiende con {defensa1} puntos.");
            Console.WriteLine($"{pokemon2.Nombre} ataca con {ataque2} puntos y defiende con {defensa2} puntos.");

            if (ataque1 > ataque2 * defensa2)
            {
                puntosPokemon1++;
                Console.WriteLine($"{pokemon1.Nombre} gana este turno.");
            }
            else if (ataque2 > ataque1 * defensa1)
            {
                puntosPokemon2++;
                Console.WriteLine($"{pokemon2.Nombre} gana este turno.");
            }
            else
            {
                Console.WriteLine("Empate en este turno.");
            }

            Console.WriteLine();
        }

        if (puntosPokemon1 > puntosPokemon2)
        {
            Console.WriteLine($"{pokemon1.Nombre} gana la batalla.");
        }
        else if (puntosPokemon2 > puntosPokemon1)
        {
            Console.WriteLine($"{pokemon2.Nombre} gana la batalla.");
        }
        else
        {
            Console.WriteLine("La batalla termina en empate.");
        }
    }
}

//*********************
//Punto 2

public interface IDatosJugador
{
    string Nombre { get; }
    string Posicion { get; }
    int Rendimiento { get; }
}

public class Jugador : IDatosJugador
{
    private string nombre;
    private string posicion;
    private int rendimiento;

    public Jugador(string nombre, string posicion, int rendimiento)
    {
        this.nombre = nombre;
        this.posicion = posicion;
        this.rendimiento = rendimiento;
    }

    public string Nombre
    {
        get { return nombre; }
    }

    public string Posicion
    {
        get { return posicion; }
    }

    public int Rendimiento
    {
        get { return rendimiento; }
    }
}

public class PartidoBaloncesto
{
    private List<IDatosJugador> jugadoresRegistrados = new List<IDatosJugador>();
    public List<IDatosJugador> equipo1 = new List<IDatosJugador>();
    public List<IDatosJugador> equipo2 = new List<IDatosJugador>();
    private Random random = new Random();

    public void AgregarJugador(IDatosJugador jugador)
    {
        jugadoresRegistrados.Add(jugador);
    }

    public void SeleccionarEquipos()
    {
        jugadoresRegistrados = jugadoresRegistrados.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i < jugadoresRegistrados.Count; i++)
        {
            if (i % 2 == 0)
            {
                equipo1.Add(jugadoresRegistrados[i]);
            }
            else
            {
                equipo2.Add(jugadoresRegistrados[i]);
            }
        }
    }

    public string DeterminarGanador()
    {
        int puntajeEquipo1 = equipo1.Sum(jugador => jugador.Rendimiento);
        int puntajeEquipo2 = equipo2.Sum(jugador => jugador.Rendimiento);

        if (puntajeEquipo1 > puntajeEquipo2)
        {
            return "Equipo 1 gana";
        }
        else if (puntajeEquipo2 > puntajeEquipo1)
        {
            return "Equipo 2 gana";
        }
        else
        {
            return "Empate";
        }
    }
}

class Jugadores
{
    static void Main()
    {
        PartidoBaloncesto partido = new PartidoBaloncesto();

        partido.AgregarJugador(new Jugador("Jose", "Alvares", 7));
        partido.AgregarJugador(new Jugador("Cristian", "Hernandez", 8));
        partido.AgregarJugador(new Jugador("Alex", "Pabon", 6));
        partido.AgregarJugador(new Jugador("Julio", "Torres", 9));
        partido.AgregarJugador(new Jugador("Sergio", "Casas", 5));
        partido.AgregarJugador(new Jugador("Julian", "Suarez", 7));

        partido.SeleccionarEquipos();

        Console.WriteLine("Equipo 1:");
        foreach (var jugador in partido.equipo1)
        {
            Console.WriteLine($"{jugador.Nombre} - Posición: {jugador.Posicion} - Rendimiento: {jugador.Rendimiento}");
        }

        Console.WriteLine("\nEquipo 2:");
        foreach (var jugador in partido.equipo2)
        {
            Console.WriteLine($"{jugador.Nombre} - Posición: {jugador.Posicion} - Rendimiento: {jugador.Rendimiento}");
        }

        string resultado = partido.DeterminarGanador();
        Console.WriteLine($"\nResultado: {resultado}");
    }
}
