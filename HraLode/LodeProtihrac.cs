namespace HraLode;

public class LodeProtihrac
{
    private LodeCore _core = new LodeCore();
    private Random _rand = new Random();

    public void VygenerujPoleNepritele(string[,] nepritelPole)
    {
        InicializujLodProtihrace(4, nepritelPole);
        InicializujLodProtihrace(3, nepritelPole);
        InicializujLodProtihrace(2, nepritelPole);
        InicializujLodProtihrace(2, nepritelPole);
        InicializujLodProtihrace(1, nepritelPole);
        InicializujLodProtihrace(1, nepritelPole);
    }

    public void InicializujLodProtihrace(int delkaLodi, string[,] nepritelPole)
    {
        while (true)
        {
            try
            {
                Tuple<int, int> souradnice = VygenerujSouradnice(nepritelPole);
                string smer = String.Empty;
                switch (_rand.Next(0, 4))
                {
                    case 0:
                        smer = "nahoru";
                        break;
                    case 1:
                        smer = "dolu";
                        break;
                    case 2:
                        smer = "doprava";
                        break;
                    case 3:
                        smer = "doleva";
                        break;
                }
                _core.PolozLodNaPole(souradnice.Item1.ToString(), souradnice.Item2.ToString(), delkaLodi, smer, nepritelPole);
            }
            catch (IndexOutOfRangeException)
            { continue; }
            catch (ArgumentException)
            { continue; }
            catch (Exception)
            { continue; }
            break;
        }
    }
    
    public Tuple<int, int> VygenerujSouradnice(string[,] nepritelPole)
    {
        List<Tuple<int, int>> volneIndexy = new List<Tuple<int, int>>(LodeCore.VelikostHracihoPole*LodeCore.VelikostHracihoPole);
        for (int i = 0; i < nepritelPole.GetLength(0); i++)
        {
            for (int j = 0; j < nepritelPole.GetLength(1); j++)
            {
                if(!nepritelPole[i,j].Equals(LodeCore.ZnakLode)) 
                    volneIndexy.Add(new Tuple<int, int>(i, j));
            }
        }
        int index = _rand.Next(0, volneIndexy.Count);
        return volneIndexy[index];
    }
}


