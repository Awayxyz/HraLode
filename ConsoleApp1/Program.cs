// Patrik Mašlaň, 2.B, x.6.2022, PVA, Program Lodě

using System.Threading.Channels;
using ConsoleApp1;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var jadro = new LodeJadro();
var protihrac = new LodeNepritel();

Console.WriteLine("========== Lode ==========");

const int velikostHracihoPole = 6;
string[,] hracPole = new string[velikostHracihoPole, velikostHracihoPole];
string[,] nepritelPole = new string[velikostHracihoPole, velikostHracihoPole];

jadro.VynulujPole(hracPole, nepritelPole);  

protihrac.VygenerujPoleNepritele(nepritelPole);

// Pravidla hry
Console.WriteLine("Potopte nepratelske lode drive, nez on ty Vase!\n");

jadro.PostavPoleHrace(hracPole);














