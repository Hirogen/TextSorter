using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Downloads\Wien Raster\RasterFiles\Mah\";
            string ergebnisDir = @"\Ergebnis\";
            string ergebnisFile = "ergebnis.txt" , ergebnisString;
            String[] allLines, files;
            StreamWriter endErgebnis;
            List<string> listString, zwischenSpeicher;
            List<double> zwischenSpeicherDouble, endSpeicherDouble;
            try
            {
                if (!Directory.Exists(dirPath+ergebnisDir))
                {
                    Directory.CreateDirectory(dirPath+ergebnisDir);
                }
                if (!File.Exists(dirPath+ergebnisDir+ergebnisFile))
                {
                    File.Create(dirPath + ergebnisDir + ergebnisFile).Close();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            try
            {
                endErgebnis = new StreamWriter(dirPath + ergebnisDir + ergebnisFile);
                files = Directory.GetFiles(dirPath);
                int anzahlDirs = files.Length;
                for (int i = 0; i < anzahlDirs; i++)
                {
                    allLines = File.ReadAllLines(files[i]);

                    listString = new List<string>();
                    zwischenSpeicherDouble = new List<double>();
                    endSpeicherDouble = new List<double>();
                    zwischenSpeicher = new List<string>();
                    int anzahlZeilen = allLines.Length;
                    for (int y = 0; y < anzahlZeilen; y++)
                    {
                        String[] split = allLines[y].Split(' ');
                        zwischenSpeicher = split.ToList();
                        zwischenSpeicherDouble = zwischenSpeicher.Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToList();
                        zwischenSpeicherDouble.Sort();
                        Console.Write("#");
                        endSpeicherDouble.AddRange(zwischenSpeicherDouble);
                    }

                    endSpeicherDouble.Sort();
                    ergebnisString = "File: " + files[i] + " Minimum: " + endSpeicherDouble.First() + " Maximum: " + endSpeicherDouble.Last();
                    endErgebnis.WriteLine(ergebnisString);
                    Console.WriteLine("\n\nEnde Datei_" + (i+1)+" von "+anzahlDirs+"\n\n");
                }

                Console.WriteLine("ENDE");
                Console.ReadKey();
		            
           }
           catch (Exception e)
           {
                Console.Write(e.ToString());
                Console.ReadKey();
           }
        }
    }
}
