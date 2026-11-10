using System.ComponentModel.Design;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;

Console.WriteLine("=================================================");
Console.WriteLine("       Bienvenido al gestor de Partidos          ");
Console.WriteLine("=================================================");
menu();
Console.WriteLine("Que deseas hacer");
int decision = int.Parse(Console.ReadLine());
while (decision != 6)
{
    switch (decision)
    {
        case 1:
            Buscar_estadisticas_equipo();
        break;
        case 2:
            Buscar_jornada();
        break;
        case 3:
            mayoresMVP();
        break;
        case 4:
            Clasificacion();
        break;
        case 5:
            Guardar_archivos_con_nombres_equipos();
        break;
        default:
            Console.WriteLine("Opcion no valida");
            break;

    }

    menu();
    Console.WriteLine("Que deseas hacer");
    decision = int.Parse(Console.ReadLine());
}

void menu()
{
    Console.WriteLine("[1] Visualizar los datos de Partido de un equipo");
    Console.WriteLine("[2] Visualizar los datos de Partido de una Jornada");
    Console.WriteLine("[3] Visualizar los tres mejores jugadores de la Temporada");
    Console.WriteLine("[4] Visualizar la clasificacion");
    Console.WriteLine("[5] Crear un archivo por cada equipo");
    Console.WriteLine("[6] Salir de la app");
}

void Buscar_estadisticas_equipo()
{
    try
    {
        Console.WriteLine("¿De qué equipo necesitas saber las estadísticas?");
        string equipo = Console.ReadLine();

        Buscar_estadisticas_equipo_nombre(equipo);
    }
    catch (Exception error)
    {
        Console.WriteLine("A ocurrido un erro :" + error.Message);
    }
}

void Buscar_estadisticas_equipo_nombre(string equipo)
{
    try
    {
        List<string> archivo_contenido = new List<string>();

        string ruta = crear_archivo(equipo);
        string[] lineas = leerFichero();

        archivo_contenido.Add("======================================================");
        archivo_contenido.Add("                  " + equipo + "                        ");
        archivo_contenido.Add("======================================================");

        Console.WriteLine("======================================================");
        Console.WriteLine("                  " + equipo + "                        ");
        Console.WriteLine("======================================================");

        foreach (string line in lineas.Skip(1))
        {
            string[] registros = line.Split(',');

            if (registros[1].Equals(equipo) || registros[2].Equals(equipo))
            {
                archivo_contenido.Add(registros[0]);
                archivo_contenido.Add(registros[1] + " Contra " + registros[2]);
                archivo_contenido.Add("Resultado : " + registros[3]);
                archivo_contenido.Add("MVP del partido : " + registros[4]);
                archivo_contenido.Add("Mejor equipo del partido : " + registros[8]);
                archivo_contenido.Add("-----------------------------------------------------------------");

                Console.WriteLine(registros[0]);
                Console.WriteLine(registros[1] + " Contra " + registros[2]);
                Console.WriteLine("Resultado : " + registros[3]);
                Console.WriteLine("MVP del partido : " + registros[4]);
                Console.WriteLine("Mejor equipo del partido : " + registros[8]);
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }

        File.WriteAllLines(ruta, archivo_contenido);
    }
    catch (Exception error)
    {
        Console.WriteLine("Lo sentimos a ocurrido un error :" + error.Message);
    }
}
void Buscar_jornada()
{
    try
    {
        List<string> archivo_contenido_jornada = new List<string>();
        Console.WriteLine("De que jornada quieres ver los datos");
        string Jornada = Console.ReadLine();
        string ruta = crear_archivo(Jornada);
        Jornada = Jornada.ToLower();
        string[] lineas = leerFichero();
        archivo_contenido_jornada.Add("============================================================");
        archivo_contenido_jornada.Add("             Partidos Jugados en la " + Jornada + "            ");
        archivo_contenido_jornada.Add("============================================================");
        Console.WriteLine("============================================================");
        Console.WriteLine("             Partidos Jugados en la " + Jornada + "            ");
        Console.WriteLine("============================================================");
        foreach (string line in lineas)
        {
            String[] registros = line.Split(',');

            if (registros[0].ToLower().Equals(Jornada))
            {
                archivo_contenido_jornada.Add(registros[1] + " " + registros[3] + " " + registros[2]);
                archivo_contenido_jornada.Add("MVP del partido : " + registros[4]);
                archivo_contenido_jornada.Add("Mejor equipo del partido : " + registros[8]);
                archivo_contenido_jornada.Add("-----------------------------------------------------------------");

                Console.WriteLine(registros[1] + " " + registros[3] + " " + registros[2]);
                Console.WriteLine("MVP del partido : " + registros[4]);
                Console.WriteLine("Mejor equipo del partido : " + registros[8]);
                Console.WriteLine("-----------------------------------------------------------------");

            }


        }
        File.WriteAllLines(ruta, archivo_contenido_jornada);
    }
    catch (Exception error)
    {
        Console.WriteLine("Lo sentimos a ocurrido un error :" + error.Message);
    }
}

void mayoresMVP()
{
    try
    {

        // Variables para guardar los tres mejores jugadores.
        List<string> Mejores3Jugadores = new List<string>();
        String jugador1 = "";
        int veces1 = 0;
        String jugador2 = "";
        int veces2 = 0;
        String jugador3 = "";
        int veces3 = 0;

        // Diccionario para contar los MVP de cada jugador.
        Dictionary<string, int> JugadoresMVP = new Dictionary<string, int>();

        // Leemos el fichero.
        string[] lineas = leerFichero();

        foreach (String line in lineas.Skip(1))
        {
            String[] registro = line.Split(',');

            // Obtenemos el jugador MVP.
            String Jugador_mvp = registro[4];

            // Contamos cuántas veces aparece cada jugador.
            if (JugadoresMVP.ContainsKey(Jugador_mvp))
            {
                JugadoresMVP[Jugador_mvp]++;
            }
            else
            {
                JugadoresMVP[Jugador_mvp] = 1;
            }
        }

        // Buscamos los tres jugadores con más MVP.
        foreach (KeyValuePair<string, int> jugadores in JugadoresMVP)
        {
            if (jugadores.Value > veces1)
            {
                jugador3 = jugador2;
                veces3 = veces2;
                jugador2 = jugador1;
                veces2 = veces1;
                jugador1 = jugadores.Key;
                veces1 = jugadores.Value;
            }
            else if (jugadores.Value > veces2)
            {
                jugador3 = jugador2;
                veces3 = veces2;
                jugador2 = jugadores.Key;
                veces2 = jugadores.Value;
            }
            else if (jugadores.Value > veces3)
            {
                jugador3 = jugadores.Key;
                veces3 = jugadores.Value;
            }
        }

        // Creamos el archivo para guardar los resultados.
        string ruta = crear_archivo("Los_tres_mejores_jugadores");

        // Guardamos los resultados en la lista.
        Mejores3Jugadores.Add("===========================================");
        Mejores3Jugadores.Add("     Mejores 3 jugadores de la temporada   ");
        Mejores3Jugadores.Add("===========================================");
        Mejores3Jugadores.Add(" El primer jugador es " + jugador1 + " siendo MVP " + veces1 + " veces");
        Mejores3Jugadores.Add(" El segundo jugador es " + jugador2 + " siendo MVP " + veces2 + " veces");
        Mejores3Jugadores.Add(" El tercer jugador es " + jugador3 + " siendo MVP " + veces3 + " veces");

        // Mostramos los resultados por consola.
        Console.WriteLine("===========================================");
        Console.WriteLine("     Mejores 3 jugadores de la temporada   ");
        Console.WriteLine("===========================================");
        Console.WriteLine(" El primer jugador es " + jugador1 + " siendo MVP " + veces1 + " veces");
        Console.WriteLine(" El segundo jugador es " + jugador2 + " siendo MVP " + veces2 + " veces");
        Console.WriteLine(" El tercer jugador es " + jugador3 + " siendo MVP " + veces3 + " veces");

        // Escribimos los resultados en el archivo.
        File.WriteAllLines(ruta, Mejores3Jugadores);
    }
    catch (Exception error)
    {
        Console.WriteLine("Lo sentimos a ocurrido un error :" + error.Message);
    }
}
void Clasificacion()
{
    // Diccionarios para guardar los puntos y la diferencia de canastas de cada equipo.
    int contador = 10000;
    Dictionary<string,int> ranking = new Dictionary<string,int>();
    Dictionary<string,int> diferencia_canastas = new Dictionary<string, int>();

    // Leemos el fichero y saltamos la primera línea porque contiene los encabezados.
    string [] lineas = leerFichero();
    foreach(string line in lineas.Skip(1))
    {
        // Separamos los datos del partido y obtenemos las canastas de cada equipo.
        String [] registro = line.Split(',');
        String [] resultado = registro[3].Split('-');

        int equipo_local = int.Parse(resultado[0]);
        int equipo_visitante = int.Parse(resultado[1]);

        // REPARTO DE PUNTOS.
        // Si gana el local recibe 3 puntos; si gana el visitante también.
        if (equipo_local > equipo_visitante)
        {
            // Si el equipo existe sumamos 3 puntos, si no lo añadimos al diccionario.
            if (ranking.ContainsKey(registro[1]))
                ranking[registro[1]] = ranking[registro[1]] + 3;
            else
                ranking[registro[1]] = 3;
        }
        else if(equipo_local < equipo_visitante)
        {
            if (ranking.ContainsKey(registro[2]))
                ranking[registro[2]] = ranking[registro[2]] + 3;
            else
                ranking[registro[2]] = 3;
        }
        // Si empatan, cada equipo recibe 1 punto.
        else
        {
            if(ranking.ContainsKey(registro[1]))
                ranking[registro[1]]++;
            else
                ranking[registro[1]] = 1;

            if(ranking.ContainsKey(registro[2]))
                ranking[registro[2]]++;
            else
                ranking[registro[2]] = 1;
        }

        // Calculamos la diferencia de canastas: canastas anotadas menos recibidas.
        // Si el equipo ya existe acumulamos la diferencia del partido.
        if (diferencia_canastas.ContainsKey(registro[1]))
            diferencia_canastas[registro[1]] += equipo_local - equipo_visitante;
        else
            diferencia_canastas[registro[1]] = equipo_local - equipo_visitante;

        if (diferencia_canastas.ContainsKey(registro[2]))
            diferencia_canastas[registro[2]] += equipo_visitante - equipo_local;
        else
            diferencia_canastas[registro[2]] = equipo_visitante - equipo_local;
    }

    // Creamos una lista con los nombres de los equipos para poder ordenarlos.
    List<string> equipos = new List<string>(ranking.Keys);

    // MÉTODO DE LA BURBUJA.
    // i controla las pasadas y j compara los equipos consecutivos.
    for (int i = 0; i < equipos.Count - 1; i++)
    {
        // Restamos i para no volver a comparar los equipos ya ordenados.
        for (int j = 0; j < equipos.Count - 1 - i; j++)
        {
            // Guardamos los dos equipos que vamos a comparar.
            string equipoA = equipos[j];
            string equipoB = equipos[j + 1];

            // Ordenamos de mayor a menor puntuación.
            // Si empatan, colocamos primero al que tenga mejor diferencia de canastas.
            if (ranking[equipoB] > ranking[equipoA] ||
               (ranking[equipoB] == ranking[equipoA] && diferencia_canastas[equipoB] > diferencia_canastas[equipoA]))
            {
                // Intercambiamos sus posiciones utilizando una variable temporal.
                string temp = equipos[j];
                equipos[j] = equipos[j + 1];
                equipos[j + 1] = temp;
            }
        }
    }

    // MOSTRAMOS LA CLASIFICACIÓN ORDENADA.
    Console.WriteLine("=================================================");
    Console.WriteLine("          CLASIFICACIÓN DE LA LIGA               ");
    Console.WriteLine("=================================================");

    // Recorremos los equipos ordenados mostrando su posición, puntos y diferencia.
    int posicion = 1;
    foreach (string equipo in equipos)
    {
        Console.WriteLine(posicion + "º - " + equipo + ": " + ranking[equipo] + " puntos (Diferencia: " + diferencia_canastas[equipo] + ")");
        posicion++;
    }

    Console.WriteLine("=================================================");
}
void Guardar_archivos_con_nombres_equipos()
{
    try
    {

        string[] equipos = new string[18];
        string[] lineas = leerFichero();
        int cont = 0;
        foreach(String Line in lineas)
        {
            string [] registros = Line.Split(',');

            if(registros[0].Equals("Jornada 1"))
            {
                equipos[cont] = registros[1];
                cont++;
                equipos[cont] = registros[2];
                cont++;
            }
        }

        foreach(String equipo in equipos)
        {
            Buscar_estadisticas_equipo_nombre(equipo);
        }

        

    }
    catch (Exception error)
    {

    }

}
string[] leerFichero()
{
    string ruta = "../../../acb2526.csv";
    string[] lineas = File.ReadAllLines(ruta);
    return lineas;
}
String crear_archivo (String Nombre)
{
    string nombreLimpio = Nombre.Trim().Replace(" ", "_");
    string ruta = "./" + nombreLimpio + ".txt";
    FileStream archivo = File.Create(ruta);
    archivo.Close();
    return ruta;
}
