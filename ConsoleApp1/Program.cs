// Patrik Mašlaň, 2.B, 11.6.2022, PVA, Lodě

using ConsoleApp1;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var core = new LodeCore();
var protihrac = new LodeProtihrac();

Console.WriteLine("========== Lodě ==========");

string[,] hracPole = new string[LodeCore.VelikostHracihoPole, LodeCore.VelikostHracihoPole];
string[,] nepritelPole = new string[LodeCore.VelikostHracihoPole, LodeCore.VelikostHracihoPole];
string[,] nepritelPoleZasahy = new string[LodeCore.VelikostHracihoPole, LodeCore.VelikostHracihoPole];

core.VynulujPole(hracPole);  
core.VynulujPole(nepritelPole);  
core.VynulujPole(nepritelPoleZasahy);  

protihrac.VygenerujPoleNepritele(nepritelPole);

// Pravidla hry
Console.WriteLine("Potopte nepřátelské lodě dříve než on ty Vaše!\n");

core.PostavPoleHrace(hracPole);

core.ZahajHru(hracPole, nepritelPole, nepritelPoleZasahy);