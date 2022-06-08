
namespace ConsoleApp1;

public class LodeJadro
{
    private readonly string[] _smery = new[] { "nahoru", "dolu", "doprava", "doleva" };

    private const string ZnakLode = "\u2587";
    private const string ZnakZasahu = "\u2573";
    private const string ZnakStrely = "\u25A2";
    
    // vrati obe pole do puvodni podoby
    public void VynulujPole(string[,] hracPole, string[,] nepritelPole)
    {
        for (int i = 0; i < hracPole.GetLength(0); i++)
        {
            for (int j = 0; j < hracPole.GetLength(1); j++)
            {
                hracPole[i, j] = " ";
                nepritelPole[i, j] = " ";
            }
        }
    }

    

    // vychozi nastaveni pole hrace na zacatku hry
    public void PostavPoleHrace(string[,] hracPole)
    {
        VykresliPoleHrace(hracPole);
        Console.WriteLine("Lode pokladejte ve stylu \"A1 nahoru/dolu/doprava/doleva\"");
        
        // bitevni lod 1x4
        InicializacePolozeniLodeNaPole("Bitevní loď - 4 políčka", 4, hracPole);
        
        // kriznik 1x3
        InicializacePolozeniLodeNaPole("Křižník - 3 políčka", 3, hracPole);
        
        // torpodoborec 1x2
        InicializacePolozeniLodeNaPole("Torpédoborec - 2 políčka", 2, hracPole);
        
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
                string vstupLod = Console.ReadLine();
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
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Zadali jste nesprávné souřadnice pro postavení lodi. Zkuste to znovu.\n");
                continue;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Na souřadnicích, kde jste loď položily, se loď překrývá s jinou lodí. Zkuste to znovu.");
                continue;
            }
            catch (Exception e)
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
        switch (Xhodnota)
        {
            case "A":
                sloupec = 0;
                break;
            case "B":
                sloupec = 1;
                break;
            case "C":
                sloupec = 2;
                break;
            case "D":
                sloupec = 3;
                break;
            case "E":
                sloupec = 4;
                break;
            case "F":
                sloupec = 5;
                break;
            default:
                throw new IndexOutOfRangeException();
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
                        catch (IndexOutOfRangeException e)
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
                    catch (IndexOutOfRangeException e)
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
                    catch (IndexOutOfRangeException e)
                    { }
                    // kontrola nad sebou
                    try
                    {
                        if(poleHrace[radek - i - 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException e)
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
                        if (poleHrace[radek - i + 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException e)
                    { }
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
                    catch (IndexOutOfRangeException e)
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
                    catch (IndexOutOfRangeException e)
                    { }
                    // kontrola nad sebou
                    if (i == 0)
                    {
                        try
                        {
                            if (poleHrace[radek - i - 1, sloupec] == ZnakLode) throw new ArgumentException();
                        }
                        catch (IndexOutOfRangeException e)
                        { }
                    }
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
                        if (poleHrace[radek - i + 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException e)
                    { }
                    // kontrola na prave strane
                    try
                    {
                        if (radek - i + 1 <= 5)
                        {
                            if(poleHrace[radek - i + 1, sloupec + 1] == ZnakLode) throw new ArgumentException();
                        }
                        if (i == 0)
                        {
                            if(poleHrace[radek - i, sloupec + 1] == ZnakLode) throw new ArgumentException();  
                        }
                        if (radek - i - 1 >= 0)
                        {
                            if(poleHrace[radek - i - 1, sloupec + 1] == ZnakLode) throw new ArgumentException();  
                        }
                    }
                    catch (IndexOutOfRangeException e)
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
                    catch (IndexOutOfRangeException e)
                    { }
                    // kontrola nad sebou
                    try
                    {
                        if (poleHrace[radek - i - 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException e)
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
                        if (poleHrace[radek - i + 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException e)
                    { }
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
                    catch (IndexOutOfRangeException e)
                    { }
                    // kontrola na leve strane
                    try
                    {
                        if (radek - i + 1 <= 5)
                        {
                            if(poleHrace[radek - i + 1, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        }
                        if (i == 0)
                        {
                            if(poleHrace[radek - i, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        }
                        if (radek - i - 1 >= 0)
                        {
                            if(poleHrace[radek - i - 1, sloupec - 1] == ZnakLode) throw new ArgumentException();
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    { }
                    // kontrola nad sebou
                    try
                    {
                        if (poleHrace[radek - i - 1, sloupec] == ZnakLode) throw new ArgumentException();
                    }
                    catch (IndexOutOfRangeException e)
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
}