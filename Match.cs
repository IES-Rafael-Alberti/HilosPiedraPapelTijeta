namespace HilosPiedraPapelTijeta;

public class Match
{
    private static readonly string[] op = { "Piedra", "Papel", "Tijera" };
    private static string jugadaJugador1;
    private static string jugadaJugador2;
    private static readonly Random random = new Random();
    private static readonly object lockObject = new object();
    private static int puntajeJugador1 = 0;
    private static int puntajeJugador2 = 0;

    public static void Game()
    {
        int ronda = 1;
        while (puntajeJugador1 < 2 && puntajeJugador2 < 2)
        {
            Console.WriteLine($"\nRonda {ronda}:");

            Thread hilo1 = new Thread(Jugador1);
            Thread hilo2 = new Thread(Jugador2);

            hilo1.Start();
            hilo2.Start();

            hilo1.Join();
            hilo2.Join();

            DeterminarGanadorRonda();

            ronda++;
        }

        DeterminarGanadorFinal();
    }

    static void Jugador1()
    {
        lock (lockObject)
        {
            jugadaJugador1 = op[random.Next(op.Length)];
            Console.WriteLine($"Jugador 1 juega: {jugadaJugador1}");
        }
    }

    static void Jugador2()
    {
        lock (lockObject)
        {
            jugadaJugador2 = op[random.Next(op.Length)];
            Console.WriteLine($"Jugador 2 juega: {jugadaJugador2}");
        }
    }

    static void DeterminarGanadorRonda()
    {
        if (jugadaJugador1 == jugadaJugador2)
        {
            Console.WriteLine("Empate en esta ronda");
        }
        else if (
            (jugadaJugador1 == "Piedra" && jugadaJugador2 == "Tijera") ||
            (jugadaJugador1 == "Papel" && jugadaJugador2 == "Piedra") ||
            (jugadaJugador1 == "Tijera" && jugadaJugador2 == "Papel"))
        {
            Console.WriteLine("Jugador 1 gana esta ronda");
            puntajeJugador1++;
        }
        else
        {
            Console.WriteLine("Jugador 2 gana esta ronda");
            puntajeJugador2++;
        }
    }

    static void DeterminarGanadorFinal()
    {
        Console.WriteLine("\nResultado Final:");
        Console.WriteLine($"Puntaje Jugador 1: {puntajeJugador1}");
        Console.WriteLine($"Puntaje Jugador 2: {puntajeJugador2}");

        if (puntajeJugador1 > puntajeJugador2)
        {
            Console.WriteLine("Jugador 1 es el ganador al mejor de tres");
        }
        else if (puntajeJugador2 > puntajeJugador1)
        {
            Console.WriteLine("Jugador 2 es el ganador al mejor de tres");
        }
    }
}