using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MorzeOrai
{
  class Program
  {
    static Dictionary<string, string> abcmorze = new Dictionary<string, string>();
    static Dictionary<string, string> morzeabc = new Dictionary<string, string>();

    static List<Szoveg> idezetek = new List<Szoveg>();

    static void ABCbeolvas()
    {
      StreamReader be = new StreamReader("morzeabc.txt");
      be.ReadLine();

      while (!be.EndOfStream)
      {
        string[] a = be.ReadLine().Split('\t');
        abcmorze.Add(a[0], a[1]);
        morzeabc.Add(a[1], a[0]);
      }

      be.Close();
    }

    static void Harmadik()
    {
      Console.WriteLine($"3. feladat: A morze abc {abcmorze.Count} db karakter kódját tartalmazza.");
    }

    static void Negyedik()
    {
      Console.Write("4. feladat: Kérek egy karaktert: ");
      string betu = Console.ReadLine();

      if (abcmorze.ContainsKey(betu))
      {
        Console.WriteLine($"\t A {betu} karakter morzekódja: {abcmorze[betu]}");
      }
      else
      {
        Console.WriteLine("\tNem található a kódtárban ilyen karakter!");
      }


    }

    static void Otodik()
    {
      StreamReader be = new StreamReader("morze.txt");

      while (!be.EndOfStream)
      {
        string[] a = be.ReadLine().Split(';');

        string szerzo = a[0].Trim();
        string idezet = a[1].Trim();

        idezetek.Add(new Szoveg(
            Morze2Szoveg(szerzo),
            Morze2Szoveg(idezet)
          ));

      }

      be.Close();
    }

    static string Morze2Szoveg(string kodolt)
    {
      StringBuilder vissza = new StringBuilder();
      string[] szavak = kodolt.Replace("       ", ";").Split(';');

      foreach (var szo in szavak)
      {
        string[] betuk = szo.Trim().Replace("   ", ";").Split(';');

        foreach (var betu in betuk)
        {
          vissza.Append(morzeabc[betu]);
        }
        vissza.Append(" ");
      }

      return vissza.ToString().Trim();
    }

    static void Hetedik()
    {
      Console.WriteLine($"7. feladat: Az első idézet szerzője: {idezetek[0].Szerzo}");
    }

    static void Nyolcadik()
    {
      //int max = idezetek[0].Hossz;
      //int melyik = 0;

      //for (int i = 1; i < idezetek.Count; i++)
      //{
      //  if (idezetek[i].Hossz > max)
      //  {
      //    max = idezetek[i].Hossz;
      //    melyik = i;
      //  }
      //}
      int max = idezetek.Max(x => x.Hossz);

      var melyik = (from i in idezetek
                    where i.Hossz == max
                    select i).ToList();

      //Console.WriteLine($"8. feladat: A leghosszabb idézet szerzője és az idézet: {idezetek[melyik].Szerzo}: {idezetek[melyik].Idezet}");
      Console.WriteLine($"8. feladat: A leghosszabb idézet szerzője és az idézet: {melyik[0].Szerzo}: {melyik[0].Idezet}");
    }

    static void Kilencedik()
    {
      Console.WriteLine("9. feladat: ARISZTOTELÉSZ idézetei");

      #region linq select-es megoldás
      //var arisz = from i in idezetek
      //            where i.Szerzo == "ARISZTOTELÉSZ"
      //            select i;

      //foreach (var ar in arisz)
      //{
      //  Console.WriteLine($"\t- {ar.Idezet}");
      //}
      #endregion      
    }

    static void Main(string[] args)
    {
      ABCbeolvas();
      Harmadik();
      Negyedik();
      Otodik();
      Hetedik();
      Nyolcadik();
      Kilencedik();

      Console.ReadLine();
    }
  }
}
