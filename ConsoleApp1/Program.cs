// Patrik Mašlaň, 2.B, x.6.2022, PVA, Program Lodě

using ConsoleApp1;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var core = new LodeCore();
var protihrac = new LodeNepritel();

Console.WriteLine("========== Lodě ==========");

const int velikostHracihoPole = 7;
string[,] hracPole = new string[velikostHracihoPole, velikostHracihoPole];
string[,] nepritelPole = new string[velikostHracihoPole, velikostHracihoPole];

core.VynulujPole(hracPole, nepritelPole);  

protihrac.VygenerujPoleNepritele(nepritelPole);

// test
core.VykresliPoleHrace(nepritelPole);
// test

// Pravidla hry
Console.WriteLine("Potopte nepřátelské lodě dříve, než on ty Vaše!\n");

core.PostavPoleHrace(hracPole);

//jadro.ZahajHru();














