namespace HilosPiedraPapelTijeta;

public class Tourney
{
    private static readonly string[] opciones = { "Piedra", "Papel", "Tijera" };
    private static readonly Random random = new Random();
    private static readonly object lockObject = new object();

    public static void Bracket()
    {
        Console.WriteLine("Inicio del Torneo de Piedra, Papel o Tijera\n");
        string[] jugadores = new string[16];

        for (int i = 0; i < jugadores.Length; i++)
        {
            jugadores[i] = $"Jugador {i + 1}";
        }

        while (jugadores.Length > 1)
        {
            Console.WriteLine($"\nRonda con {jugadores.Length} jugadores:");
            jugadores = JugarRonda(jugadores);
        }

        Console.WriteLine($"\nEl ganador del torneo es: {jugadores[0]}");
    }

    static string[] JugarRonda(string[] jugadores)
    {
        int numPartidas = jugadores.Length / 2;
        string[] ganadores = new string[numPartidas];

        Thread[] hilos = new Thread[numPartidas];

        for (int i = 0; i < numPartidas; i++)
        {
            int index = i; // Para evitar problemas con la variable capturada
            hilos[i] = new Thread(() =>
            {
                string ganador = JugarPartida(jugadores[index * 2], jugadores[index * 2 + 1]);
                lock (lockObject)
                {
                    ganadores[index] = ganador;
                }
            });
            hilos[i].Start();
        }

        foreach (var hilo in hilos)
        {
            hilo.Join();
        }

        return ganadores;
    }

    static string JugarPartida(string jugador1, string jugador2)
    {
        string jugadaJugador1 = opciones[random.Next(opciones.Length)];
        string jugadaJugador2 = opciones[random.Next(opciones.Length)];

        Console.WriteLine($"{jugador1} juega: {jugadaJugador1}");
        Console.WriteLine($"{jugador2} juega: {jugadaJugador2}");

        if (jugadaJugador1 == jugadaJugador2)
        {
            Console.WriteLine($"Empate entre {jugador1} y {jugador2}, se repite la partida");
            return JugarPartida(jugador1, jugador2);
        }
        else if (
            (jugadaJugador1 == "Piedra" && jugadaJugador2 == "Tijera") ||
            (jugadaJugador1 == "Papel" && jugadaJugador2 == "Piedra") ||
            (jugadaJugador1 == "Tijera" && jugadaJugador2 == "Papel"))
        {
            Console.WriteLine($"{jugador1} gana contra {jugador2}");
            return jugador1;
        }
        else
        {
            Console.WriteLine($"{jugador2} gana contra {jugador1}");
            return jugador2;
        }
    }
}