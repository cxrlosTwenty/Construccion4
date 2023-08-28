
using System.Security.Cryptography;

namespace FraccionCompletaCalculadora
{
    struct FraccionCompleta
    {
        public int Numerador;
        public int Denominador;
    }

    class FraccionCompletaCalculadora
    {
        public static FraccionCompleta Add(FraccionCompleta a, FraccionCompleta b)
        {
            FraccionCompleta resultado;
            resultado.Numerador = a.Numerador * b.Denominador + b.Numerador * a.Denominador;
            resultado.Denominador = a.Denominador * b.Denominador;
            simplifica(ref resultado);
            return resultado;
        }

        public static FraccionCompleta Subtract(FraccionCompleta a, FraccionCompleta b)
        {
            FraccionCompleta resultado;
            resultado.Numerador = a.Numerador * b.Denominador - b.Numerador * a.Denominador;
            resultado.Denominador = a.Denominador * b.Denominador;
            simplifica(ref resultado);
            return resultado;
        }

        public static FraccionCompleta Multiply(FraccionCompleta a, FraccionCompleta b)
        {
            FraccionCompleta resultado;
            resultado.Numerador = a.Numerador * b.Numerador;
            resultado.Denominador = a.Denominador * b.Denominador;
            simplifica(ref resultado);
            return resultado;
        }

        public static FraccionCompleta Divide(FraccionCompleta a, FraccionCompleta b)
        {
            if (b.Numerador == 0)
            {
                throw new DivideByZeroException("no se puede dividir por cero");
            }

            FraccionCompleta resultado;
            resultado.Numerador = a.Numerador * b.Denominador;
            resultado.Denominador = a.Denominador * b.Numerador;
            simplifica(ref resultado);
            return resultado;
        }

        private static int MaxComDiv(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static void simplifica(ref FraccionCompleta fraccion)
        {
            int valorSimple = MaxComDiv(fraccion.Numerador, fraccion.Denominador);
            fraccion.Numerador /= valorSimple;
            fraccion.Denominador /= valorSimple;
        }
    }

    class Program
    {
        static FraccionCompleta InputFraccion(string datoIngresado)
        {
            Console.Write(datoIngresado);
            FraccionCompleta fraccion;
            fraccion.Numerador = int.Parse(Console.ReadLine());
            Console.Write("Ingrese denominador: ");
            fraccion.Denominador = int.Parse(Console.ReadLine());

            return fraccion;
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("FraccionCompleta Calculadora");
                Console.WriteLine("-------------------");

                FraccionCompleta primeraFraccion = InputFraccion("Ingrese el primer numerador: ");
                FraccionCompleta segundaFraccion = InputFraccion("Ingrese el segundo numerador: ");

                Console.Write("Ingrese el signo aritmetico (+, -, *, /): ");
                char operacion = Console.ReadLine()[0];

                FraccionCompleta resultado;

                switch (operacion)
                {
                    case '+':
                        resultado = FraccionCompletaCalculadora.Add(primeraFraccion, segundaFraccion);
                        break;
                    case '-':
                        resultado = FraccionCompletaCalculadora.Subtract(primeraFraccion, segundaFraccion);
                        break;
                    case '*':
                        resultado = FraccionCompletaCalculadora.Multiply(primeraFraccion, segundaFraccion);
                        break;
                    case '/':
                        resultado = FraccionCompletaCalculadora.Divide(primeraFraccion, segundaFraccion);
                        break;
                    default:
                        Console.WriteLine("Invalid operator.");
                        return;
                }

                Console.WriteLine($"resultado: {resultado.Numerador}/{resultado.Denominador}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Por favor ingrese números enteros válidos.");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error. No se puede dividir por cero: (0)");
            }
        }
    }
}

//*******************************************************************************************
//2do PUNTO TABLAS DE MULTIPLICAR OCULTAS

namespace TablasDeMultiplicar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el rango inicial: ");
            int inicio = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el rango final: ");
            int fin = int.Parse(Console.ReadLine());

            Random random = new Random();

            for (int i = inicio; i <= fin; i++)
            {
                int numeroOculto = random.Next(1, 11);

                Console.WriteLine($"Tabla del {i}:");
                for (int j = 1; j <= 10; j++)
                {
                    if (j == numeroOculto)
                    {
                        Console.WriteLine($"{i} x ? = {i * j}");
                    }
                    else
                    {
                        Console.WriteLine($"{i} x {j} = {i * j}");
                    }
                }

                Console.WriteLine("Ingrese su respuesta:");
                int respuesta = int.Parse(Console.ReadLine());

                if (respuesta == numeroOculto)
                {
                    Console.WriteLine("Correcto\n");
                }
                else
                {
                    Console.WriteLine($"Respuesta incorrecta. El valor correcto era: {numeroOculto}\n");
                }
            }
        }
    }
}

//*******************************************************************************************
//3er PUNTO NUMERO ESPECIAL

namespace NumeroEspecial
{
    class Program
    {
        static bool EsNumeroEspecial(int numero)
        {
            if (numero < 0)
            {
                return false;
            }

            if (numero % 5 == 0 && numero % 2 != 0 && numero % 3 != 0)
            {
                int sumaDigitos = 0;
                int num = numero;
                while (num > 0)
                {
                    sumaDigitos += num % 10;
                    num /= 10;
                }
                if (sumaDigitos > 10)
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {

            Console.Write("Ingresa un número: ");
            int numero = int.Parse(Console.ReadLine());

            if (EsNumeroEspecial(numero))
            {
                Console.WriteLine("El numero es especial");
            }
            else
            {
                Console.WriteLine("el numero ingresado no es especial");
            }
        }
    }
}

//*******************************************************************************************
//4to PUNTO ADIVINAR PALABRA

namespace JuegoAdivinarPalabras
{
    class Program
    {
        static void Main(string[] args)
        {
            string fraseOriginal = "El gato juega en el jardín";
            string[] palabrasOcultas = new string[] { "gato", "en", "el" };

            string fraseOculta = OcultarPalabras(fraseOriginal, palabrasOcultas);
            Console.WriteLine("Adivina las palabras ocultas en la siguiente frase:");
            Console.WriteLine(fraseOculta);

            int intentosRestantes = 10;
            int palabrasAdivinadas = 0;

            while (intentosRestantes > 0 && palabrasAdivinadas < palabrasOcultas.Length)
            {
                Console.Write("Ingresa una palabra: ");
                string palabraIngresada = Console.ReadLine().ToLower();

                if (Array.Exists(palabrasOcultas, palabra => palabra == palabraIngresada))
                {
                    if (Array.IndexOf(palabrasOcultas, palabraIngresada) != -1)
                    {
                        Console.WriteLine("¡Palabra correcta!");
                        palabrasAdivinadas++;
                        palabrasOcultas[Array.IndexOf(palabrasOcultas, palabraIngresada)] = ""; // Marcar como adivinada
                    }
                    else
                    {
                        Console.WriteLine("Palabra correcta");
                    }
                }
                else
                {
                    Console.WriteLine("Palabra incorrecta.");
                    intentosRestantes--;
                }

                Console.WriteLine($"Intentos restantes: {intentosRestantes}");
            }

            if (palabrasAdivinadas == palabrasOcultas.Length)
            {
                Console.WriteLine("Felicitaciones");
                Console.WriteLine($"La frase completa es: {fraseOriginal}");
            }
            else
            {
                Console.WriteLine("Se acabaron los intentos, intenta de nuevo");
            }
        }

        static string OcultarPalabras(string frase, string[] palabras)
        {
            foreach (var palabra in palabras)
            {
                frase = frase.Replace(palabra, new string('_', palabra.Length));
            }
            return frase;
        }
    }
}
