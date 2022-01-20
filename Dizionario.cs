using System;
using System.IO;
using System.Text;

namespace Dizionario
{
    internal class Program
    {
        static void Main(string[] argomenti)
        {
            // Nessun parametro specificato
            if (argomenti.Length == 0) return;

            Console.OutputEncoding = Encoding.Unicode;

            switch (argomenti[0])
            {
                // Comando "Inserisci"
                case "inserisci":
                    if (argomenti.Length == 4 || argomenti.Length == 5)
                    {
                        // Apro dizionario
                        StreamWriter scrittore = new StreamWriter("Dizionario.txt", true, Encoding.Unicode);

                        string vocabolo = argomenti[1] + "|" + argomenti[2] + "|" + argomenti[3];
                        if (argomenti.Length == 5) vocabolo = vocabolo + "|" + argomenti[4];

                        // Scrivo il vocabolo
                        scrittore.WriteLine(vocabolo);

                        // Chiudo dizionario
                        scrittore.Close(); scrittore = null;
                    }
                    else Console.WriteLine("Numero argomenti incorretto");

                    break;
                // Comando "Rimuovi"
                case "rimuovi":
                    if (argomenti.Length > 0)
                    {
                        // Apro i file
                        StreamWriter scrittore = new StreamWriter("._temp", false, Encoding.Unicode);
                        StreamReader lettore = new StreamReader("Dizionario.txt", true);

                        string vocabolo;

                        // Finche il file non termina
                        while (lettore.EndOfStream == false)
                        {
                            // Leggo un vocabolo
                            vocabolo = lettore.ReadLine();

                            // Se il vocabolo è diverso lo ricopio
                            bool scrivere = true;
                            for (int i = 1; i < argomenti.Length; i++)
                                if (vocabolo.Contains(argomenti[i]) == true) scrivere = false;
                            if (scrivere == true) scrittore.WriteLine(vocabolo);
                        }

                        // Chiudo i file
                        scrittore.Close(); scrittore = null;
                        lettore.Close(); lettore = null;

                        // Elimino file vecchio e rinomino il nuovo
                        File.Delete("Dizionario.txt");
                        File.Move("._temp", "Dizionario.txt");
                    }
                    else Console.WriteLine("Numero argomenti incorretto");

                    break;
                // Comando "Cerca"
                case "cerca":
                    if (argomenti.Length > 0)
                    {
                        // Apro il dizionario
                        StreamReader lettore = new StreamReader("Dizionario.txt", true);

                        string vocabolo;

                        // Finche il file non termina
                        while (lettore.EndOfStream == false)
                        {
                            // Leggo un vocabolo
                            vocabolo = lettore.ReadLine();

                            // Se il vocabolo è diverso lo ricopio
                            bool trovato = false;
                            for (int i = 1; i < argomenti.Length; i++)
                                if (vocabolo.Contains(argomenti[i]) == true) trovato = true;
                            if (trovato == true)
                            {
                                string[] parti = vocabolo.Split('|', 4);
                                vocabolo = parti[0] + " (" + parti[1] + ") - " + parti[2];
                                if (parti.Length > 3) vocabolo = vocabolo + " - " + parti[3];
                                
                                Console.WriteLine(vocabolo);
                            }
                        }

                        // Chiudo il dizionario
                        lettore.Close(); lettore = null;
                    }
                    else Console.WriteLine("Numero argomenti incorretto");

                    break;
                // Comando "importa"
                case "importa":
                    if (argomenti.Length > 1)
                    {
                        string vocabolo;

                        // Per ogni file
                        for (int file = 1; file < argomenti.Length; file++)
                        {
                            if (argomenti[file].Contains(".tsv") == true)
                            {
                                // Apro i file
                                StreamWriter scrittore = new StreamWriter("Dizionario.txt", true, Encoding.Unicode);
                                StreamReader lettore = new StreamReader(argomenti[file], true);

                                // Finche il file non termina
                                while (lettore.EndOfStream == false)
                                {
                                    // Leggo un vocabolo
                                    vocabolo = lettore.ReadLine();

                                    string[] parti = vocabolo.Split('\t', 4);
                                    vocabolo = parti[0] + "|" + parti[1] + "|" + parti[2];
                                    if (parti.Length > 3) vocabolo = vocabolo + "|" + parti[3];
                                    scrittore.WriteLine(vocabolo);
                                }

                                // Chiudo i file
                                scrittore.Close(); scrittore = null;
                                lettore.Close(); lettore = null;
                            }
                            else Console.WriteLine("Estensione non supportata");
                        }
                    }
                    else Console.WriteLine("Numero argomenti incorretto");

                    break;
                // Comando "modifica"
                case "modifica":
                    if (argomenti.Length == 5 || argomenti.Length == 6)
                    {
                        // Apro i file
                        StreamWriter scrittore = new StreamWriter("._temp", true, Encoding.Unicode);
                        StreamReader lettore = new StreamReader("Dizionario.txt", true);

                        string vocabolo;

                        // Finche il file non termina
                        while (lettore.EndOfStream == false)
                        {
                            // Leggo un vocabolo
                            vocabolo = lettore.ReadLine();

                            // Se il vocabolo è diverso lo ricopio
                            bool scrivere = false;
                            for (int i = 1; i < argomenti.Length; i++)
                                if (vocabolo.Contains(argomenti[i]) == true) scrivere = true;
                            if (scrivere == true)
                            {
                                vocabolo = argomenti[2] + "|" + argomenti[3] + "|" + argomenti[4];
                                if (argomenti.Length == 6) vocabolo = vocabolo + "|" + argomenti[5];
                            }
                            scrittore.WriteLine(vocabolo);
                        }

                        // Chiudo i file
                        scrittore.Close(); scrittore = null;
                        lettore.Close(); lettore = null;

                        // Elimino file vecchio e rinomino il nuovo
                        File.Delete("Dizionario.txt");
                        File.Move("._temp", "Dizionario.txt");
                    }
                    else Console.WriteLine("Numero argomenti incorretto");

                    break;
                // Comando "salva"
                case "salva":
                    if (argomenti.Length == 2 && argomenti[1].Contains(".tsv") == true)
                    {
                        // Apro i file
                        StreamWriter scrittore = new StreamWriter(argomenti[1], true, Encoding.Unicode);
                        StreamReader lettore = new StreamReader("Dizionario.txt", true);

                        string vocabolo;

                        // Finche il file non termina
                        while (lettore.EndOfStream == false)
                        {
                            // Leggo un vocabolo
                            vocabolo = lettore.ReadLine();
                            vocabolo = vocabolo.Replace('|', '\t');
                            scrittore.WriteLine(vocabolo);
                        }

                        // Chiudo i file
                        scrittore.Close(); scrittore = null;
                        lettore.Close(); lettore = null;
                    }
                    else Console.WriteLine("Numero argomenti o argomento incorretto");

                    break;
                // Comando non supportato
                default:
                    Console.WriteLine("Comando non supportato");

                    return;
            }
        }
    }
}