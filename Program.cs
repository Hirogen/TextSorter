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
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Downloads\Wien Raster\RasterFiles\";
            string ergebnisDir = @"\Ergebnis\";
            string ergebnisFile = "ergebnis.txt";
            StringBuilder ergebnisString;
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
                ergebnisString = new StringBuilder();
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
                    //ignoring first 6 Lines, .asc files have meta data in the first 6 lines, that are not relevant
                    for (int y = 6; y < anzahlZeilen; y++)
                    {
                        String[] split = allLines[y].Split(' ');
                        zwischenSpeicher = split.ToList();
                        zwischenSpeicherDouble = zwischenSpeicher.Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToList();
                        zwischenSpeicherDouble.Sort();
                        Console.Write("#");
                        endSpeicherDouble.AddRange(zwischenSpeicherDouble);
                    }

                    endSpeicherDouble.Sort();
                    ergebnisString.Append("File: " + files[i] + " Minimum: " + endSpeicherDouble.First() + " Maximum: " + endSpeicherDouble.Last()+"\n");
                    Console.WriteLine("\n\nEnde Datei_" + (i+1)+" von "+anzahlDirs+"\n\n");
                }
                endErgebnis.WriteLine(ergebnisString);
                Console.WriteLine("ENDE");
                Console.ReadKey();
                endErgebnis.Close();
		            
           }
           catch (Exception e)
           {
                Console.Write(e.ToString());
                Console.ReadKey();
           }
        }
    }
}
