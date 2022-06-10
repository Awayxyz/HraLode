
using System.Net.Sockets;

namespace ConsoleApp1;

public class LodeCore
{
    private readonly string[] _smery = new[] { "nahoru", "dolu", "doprava", "doleva" };

    private Random _rand = new Random();

    public const int VelikostHracihoPole = 7;
    
    public const string ZnakLode = "\u2587";
    public const string ZnakZasahu = "\u2573";
    public const string ZnakStrely = "\u20E3";  

    // vrati pole do puvodni podoby
    public void VynulujPole(string[,] hracPole)
    {
        for (int i = 0; i < hracPole.GetLength(0); i++)
        {
            for (int j = 0; j < hracPole.GetLength(1); j++)
            {
                hracPole[i, j] = " ";
            }
        }
    }

    

    // vychozi nastaveni pole hrace na zacatku hry
    public void PostavPoleHrace(string[,] hracPole)
    {
        VykresliPoleHrace(hracPole);
        Console.WriteLine("Lodě pokládejte ve stylu \"A1 nahoru/dolu/doprava/doleva\"");
        
        // bitevni lod 1x4
        InicializacePolozeniLodeNaPole("Bitevní loď - 4 políčka", 4, hracPole);
        
        // kriznik 1x3
        InicializacePolozeniLodeNaPole("Křižník - 3 políčka", 3, hracPole);
        
        // torpodoborec 1x2
        InicializacePolozeniLodeNaPole("Torpédoborec - 2 políčka", 2, hracPole);
        
        // torpodoborec 1x2
        InicializacePolozeniLodeNaPole("Torpédoborec - 2 políčka", 2, hracPole);
        
        // ponorka 1x1
        InicializacePolozeniLodeNaPole("Ponorka - 1 políčko", 1, hracPole);
        
        // ponorka 1x1
        InicializacePolozeniLodeNaPole("Ponorka - 1 políčko", 1, hracPole);
    }

    public void VykresliPoleHrace(string[,] hracPole)
    {
        int asciiA = 65;
        int cislaNalevo = 0;
        for (int i = -1; i < hracPole.GetLength(0); i++)
        {
            for (int j = -1; j < hracPole.GetLength(1); j++)
            {
                if (i == -1 && j == -1)
                {
                    Console.Write("   ");
                }
                else if (i == -1 && j > -1)
                {
                    Console.Write($" {(char)(asciiA+j)} ");
                }
                else if (i > -1 && j == -1)
                {
                    Console.Write($" {cislaNalevo} ");
                }
                else
                {
                    Console.Write($"[{hracPole[i, j]}]");
                }
            }
            
            Console.WriteLine();
            cislaNalevo++;
        }

        Console.WriteLine();
    }

    public void InicializacePolozeniLodeNaPole(string uvodniText, int delkaLodi, string[,] hracPole)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(uvodniText);
                string vstupLod = Console.ReadLine()!;
                string[] coordsLod = vstupLod.Split(" ");
                if (!_smery.Contains(coordsLod[1]) || coordsLod.Length != 2)
                {
                    throw new Exception();
                }

                PolozLodNaPole(coordsLod[0].Substring(0, 1),
                    coordsLod[0].Substring(1, 1),
                    delkaLodi,
                    coordsLod[1],
                    hracPole);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Zadali jste nesprávné souřadnice pro postavení lodi. Zkuste to znovu.\n");
                continue;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Na souřadnicích, kde jste loď položily, se loď překrývá s jinou lodí. Zkuste to znovu.");
                continue;
            }
            catch (Exception)
            {
                Console.WriteLine("Zadali jste vstup ve špatném tvaru. Zkuste to znovu.\n");
                continue;
            }
            break;
        }
        
        VykresliPoleHrace(hracPole);
    }
    
    public void PolozLodNaPole(string Xhodnota, string Yhodnota, int delkaLodi, string smer, string[,] poleHrace)
    {
        int radek = int.Parse(Yhodnota) - 1;
        int sloupec;
        if (!int.TryParse(Xhodnota, out sloupec))
        {
            sloupec = (int)Xhodnota.ToCharArray()[0] - 65;
            if (sloupec > VelikostHracihoPole - 1 || sloupec < 0) throw new IndexOutOfRangeException();
        }
        
        switch (smer)
        {
            case "nahoru":
                // kontrola, zda je lod v ramci pole
                for (int i = 0; i < delkaLodi; i++)
                {
                    string temp = poleHrace[radek-i, sloupec];
                }
                
                // kontrola, zda se vedle nevyskytuje lod
                for (int i = 0; i < delkaLodi; i++)
                {
                    // poleHrace[osaX-i, osaY] - uvodni pole
                    // kontrola pod sebou pri prvnim bloku
                    if (i == 0)
                    {
                        try
                        {
                            if (poleHrace[radek - i + 1, sloupec] == ZnakLode) throw new ArgumentException();
                        }
                        catch (IndexOutOfRangeException)
                        { }
                    }
                    // kontrola na prave strane
                    try
                    {
                        if (radek - i + 1 <= 5)
                        {
                            if(poleHrace[radek - i + 1, sloupec + 1] == ZnakLode) throw new ArgumentException();
                        }
                        if(poleHrace[radek - i, sloupec + 1] == ZnakLode) throw new ArgumentException();
                        if (radek - i - 1 >= 0)
                        {
                            if(poleHrace[radek - i - 1, sloupec + 1] == ZnakLode) throw new ArgumentException();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola na leve strane
                    try
                    {
                        if (radek - i + 1 <= 5)
                        {
                            if(poleHrace[radek - i + 1, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        }
                        if(poleHrace[radek - i, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        if (radek - i - 1 >= 0)
                        {
                            if(poleHrace[radek - i - 1, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola nad sebou
                    try
                    {
                        if(poleHrace[radek - i - 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola primo pod sebou
                    try
                    {
                        if(poleHrace[radek - i, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                }
                
                // polozeni lode
                for (int i = 0; i < delkaLodi; i++)
                {
                    poleHrace[radek-i, sloupec] = ZnakLode;
                }
                break;
            case "dolu":
                // kontrola, zda je lod v ramci pole
                for (int i = 0; i < delkaLodi; i++)
                {
                    string temp = poleHrace[radek+i, sloupec];
                }
                
                // kontrola, zda se vedle nevyskytuje lod
                for (int i = 0; i < delkaLodi; i++)
                {
                    // poleHrace[osaX-i, osaY] - uvodni pole
                    // kontrola pod sebou pri prvnim bloku
                    try
                    {
                        if (poleHrace[radek + i + 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola na prave strane
                    try
                    {
                        if (radek + i + 1 <= 5)
                        {
                            if(poleHrace[radek + i + 1, sloupec + 1] == ZnakLode) throw new ArgumentException();
                        }
                        if(poleHrace[radek + i, sloupec + 1] == ZnakLode) throw new ArgumentException();
                        if (radek + i - 1 >= 0)
                        {
                            if(poleHrace[radek + i - 1, sloupec + 1] == ZnakLode) throw new ArgumentException(); 
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola na leve strane
                    try
                    {
                        if (radek + i + 1 <= 5)
                        {
                            if(poleHrace[radek + i + 1, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        }
                        if(poleHrace[radek + i, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        if (radek + i - 1 >= 0)
                        {
                            if(poleHrace[radek + i - 1, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola nad sebou
                    if (i == 0)
                    {
                        try
                        {
                            if (poleHrace[radek + i - 1, sloupec] == ZnakLode) throw new ArgumentException();
                        }
                        catch (IndexOutOfRangeException)
                        { }
                    }
                    // kontrola primo pod sebou
                    try
                    {
                        if(poleHrace[radek + i, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                }
                
                // polozeni lode
                for (int i = 0; i < delkaLodi; i++)
                {
                    poleHrace[radek+i, sloupec] = ZnakLode;
                }
                break;
            case "doleva":
                // kontrola, zda je lod v ramci pole
                for (int i = 0; i < delkaLodi; i++)
                {
                    string temp = poleHrace[radek, sloupec-i];
                }
                
                // kontrola, zda se vedle nevyskytuje lod
                for (int i = 0; i < delkaLodi; i++)
                {
                    // poleHrace[osaX-i, osaY] - uvodni pole
                    // kontrola pod sebou pri prvnim bloku
                    try
                    {
                        if (poleHrace[radek + 1, sloupec - i] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola na prave strane
                    try
                    {
                        if (radek + 1 <= 5)
                        {
                            if(poleHrace[radek + 1, sloupec - i + 1] == ZnakLode) throw new ArgumentException();
                        }
                        if (i == 0)
                        {
                            if(poleHrace[radek, sloupec - i + 1] == ZnakLode) throw new ArgumentException();  
                        }
                        if (radek - 1 >= 0)
                        {
                            if(poleHrace[radek - 1, sloupec - i + 1] == ZnakLode) throw new ArgumentException();  
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola na leve strane
                    try
                    {
                        if (radek + 1 <= 5)
                        {
                            if(poleHrace[radek + 1, sloupec - i - 1] == ZnakLode) throw new ArgumentException();
                        }
                        if(poleHrace[radek, sloupec - i - 1] == ZnakLode) throw new ArgumentException();
                        if (radek - 1 >= 0)
                        {
                            if(poleHrace[radek - 1, sloupec - i - 1] == ZnakLode) throw new ArgumentException();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola nad sebou
                    try
                    {
                        if (poleHrace[radek - 1, sloupec - i] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola primo pod sebou
                    try
                    {
                        if(poleHrace[radek, sloupec - i] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                }
                
                // polozeni lode
                for (int i = 0; i < delkaLodi; i++)
                {
                    poleHrace[radek, sloupec-i] = ZnakLode;
                }
                break;
            case "doprava":
                // kontrola, zda je lod v ramci pole
                for (int i = 0; i < delkaLodi; i++)
                {
                    string temp = poleHrace[radek, sloupec+i];
                }
                
                // kontrola, zda se vedle nevyskytuje lod
                for (int i = 0; i < delkaLodi; i++)
                {
                    // poleHrace[osaX-i, osaY] - uvodni pole
                    // kontrola pod sebou pri prvnim bloku
                    try
                    {
                        if (poleHrace[radek + 1, sloupec + i] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola na prave strane
                    try
                    {
                        if (radek + 1 <= 5)
                        {
                            if(poleHrace[radek + 1, sloupec + i + 1] == ZnakLode) throw new ArgumentException();
                        }
                        if(poleHrace[radek, sloupec + i + 1] == ZnakLode) throw new ArgumentException();
                        if (radek - 1 >= 0)
                        {
                            if(poleHrace[radek - 1, sloupec + i + 1] == ZnakLode) throw new ArgumentException();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola na leve strane
                    try
                    {
                        if (radek + 1 <= 5)
                        {
                            if(poleHrace[radek + 1, sloupec + i - 1] == ZnakLode) throw new ArgumentException();
                        }
                        if (i == 0)
                        {
                            if(poleHrace[radek, sloupec + i - 1] == ZnakLode) throw new ArgumentException();
                        }
                        if (radek - 1 >= 0)
                        {
                            if(poleHrace[radek - 1, sloupec + i - 1] == ZnakLode) throw new ArgumentException();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola nad sebou
                    try
                    {
                        if (poleHrace[radek - 1, sloupec + i] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    // kontrola primo pod sebou
                    try
                    {
                        if(poleHrace[radek, sloupec + 1] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException)
                    { }
                }
                
                // polozeni lode
                for (int i = 0; i < delkaLodi; i++)
                {
                    poleHrace[radek, sloupec+i] = ZnakLode;
                }
                break;
        }
        
        
        
    }

    public void ZahajHru(string[,] hracPole, string[,] nepritelPole, string[,] nepritelPoleZasahy)
    {
        List<Tuple<int, int>> zasahleLode = new List<Tuple<int, int>>();
        List<Tuple<int, int>> zasahleIndexy = new List<Tuple<int, int>>();
        
        List<Tuple<int, int>> zasahleLodeProtihrace = new();
        List<Tuple<int, int>> zasahleIndexyProtihrace = new();
        
        // loop samotne hry
        while (true)
        {
            VykresliPoleHrace(hracPole);
            Console.WriteLine();
            Console.Write("Napiš souřadnice, na které chceš vystřelit: ");
            string strela = Console.ReadLine()!;
            char strelaSloupecPismeno = strela.Substring(0, 1).ToCharArray()[0];
            int strelaRadekCislo;
            if (strela.Length != 2 || !char.IsLetter(strelaSloupecPismeno) ||
                (int) strelaSloupecPismeno < 65 || (int) strelaSloupecPismeno > 65+7 ||
                !int.TryParse(strela.AsSpan(1, 1), out strelaRadekCislo) ||
                strelaRadekCislo < 1 || strelaRadekCislo > 7)
            {
                Console.WriteLine("Nesprávný input. Zkuste to znovu.");
                continue;
            }
            
            int radekStrely, sloupecStrely;
            radekStrely = strelaRadekCislo - 1;
            sloupecStrely = (int)strelaSloupecPismeno - 65;

            if (zasahleIndexy.Contains(new Tuple<int, int>(radekStrely, sloupecStrely)))
            {
                Console.WriteLine("Na tyto souřadnice jste již vystřelily. Zkuste to znova.");
                continue;
            }
            
            zasahleIndexy.Add(new Tuple<int, int>(radekStrely, sloupecStrely));
            
            if (nepritelPole[radekStrely, sloupecStrely].Equals(ZnakLode) && 
                !zasahleLode.Contains(new Tuple<int, int>(radekStrely, sloupecStrely)))
            {
                nepritelPoleZasahy[radekStrely, sloupecStrely] = ZnakZasahu;
                zasahleLode.Add(new Tuple<int, int>(radekStrely, sloupecStrely));
            }
            else
            {
                nepritelPoleZasahy[radekStrely, sloupecStrely] = ZnakStrely;
            }
            
            VykresliPoleHrace(nepritelPoleZasahy);
            
            // ukonci hru, pokud pocet zasahlych indexu je roven celkovemu poctu delek lodi
            if (zasahleLode.Count is 13 or > 13)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Gratuluji, vyhráli jste!");
                break;
            }

            // kolo protihrace

            int radekStrelyProtihrace, sloupecStrelyProtihrace;
            do
            {
                radekStrelyProtihrace = _rand.Next(0, VelikostHracihoPole);
                sloupecStrelyProtihrace = _rand.Next(0, VelikostHracihoPole);
            } while (zasahleIndexyProtihrace.Contains(new Tuple<int, int>(radekStrelyProtihrace, sloupecStrelyProtihrace)));

            zasahleIndexyProtihrace.Add(new Tuple<int, int>(radekStrelyProtihrace, sloupecStrelyProtihrace));

            if (hracPole[radekStrelyProtihrace, sloupecStrelyProtihrace].Equals(ZnakLode) &&
                !zasahleLodeProtihrace.Contains(new Tuple<int, int>(radekStrelyProtihrace, sloupecStrelyProtihrace)))
            {
                hracPole[radekStrelyProtihrace, sloupecStrelyProtihrace] = ZnakZasahu;
                zasahleLodeProtihrace.Add(new Tuple<int, int>(radekStrelyProtihrace, sloupecStrelyProtihrace));
            }
            else
            {
                hracPole[radekStrelyProtihrace, sloupecStrelyProtihrace] = ZnakStrely;
            }
            
            if (zasahleLodeProtihrace.Count is 13 or > 13)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Bohužel jste prohráli. Zkuste to znovu.");
                break;
            }
            
        }
    }
}