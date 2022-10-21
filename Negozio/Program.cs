using System;
using System.Text;

String[] prodotti = { "pane", "salame", "mortazza", "maionese", "insalata" };

Decimal[] prezzi = { 4.50m, 24m, 15m, 4m, 1.50m };  //prezzo/Kg

Decimal[] giacenze = { 40, 12, 10, 10, 15 };

String? prodotto;
Decimal quantità;
String um,risposta;
String input;
int pos;
Decimal totaleProdotto;
Decimal totaleScontrino=0;

Console.OutputEncoding = System.Text.Encoding.UTF8;     //abilito i caratteri speciali

do
{
    Console.Clear();        //cancello lo schermo

    Console.WriteLine("PRODOTTO".PadRight(30) + "PREZZO".PadRight(10) + "GIACENZA".PadRight(20)+"Totale scontrino: "+ String.Format("{0:c2}", totaleScontrino));
    for (int i = 0; i < prodotti.Length; i++)
    {
        if (giacenze[i] > 0)
            Console.ForegroundColor = ConsoleColor.White;
        else
            Console.ForegroundColor = ConsoleColor.Red;     //se il prodotto è esaurito lo mostro in rosso
        Console.Write(prodotti[i].PadRight(30));
        Console.Write(prezzi[i].ToString().PadRight(10));
        Console.WriteLine(giacenze[i]);
    }

    Console.Write("\nChe prodotto vuoi vendere? ");
    prodotto = Console.ReadLine();
    
    pos = -1;
    for (int i = 0; i < prodotti.Length; i++)
        if (prodotti[i] == prodotto.ToLower())        //converto in minuscolo
        {
            pos = i;
            break;
        }

    if (pos >= 0)  //trovato!
    {
        Console.WriteLine("Prezzo/kg : " + String.Format("{0:c2}", prezzi[pos]));
        Console.Write("Inserisci la quantità: ");
        //  quantità = Convert.ToDecimal(Console.ReadLine());
        input = Console.ReadLine();
        if (!Decimal.TryParse(input,out quantità))      //provo a convertire la stringa in decimal
            Console.WriteLine("Quantità non valida!");
        else        //la quantità è stata convertita correttamente
        {
            Console.Write("Inserisci l'unità di misura (g/hg/kg): ");
            um = Console.ReadLine();
            switch (um)      // converto in kg
            {
                case "g":
                    quantità = quantità / 1000;
                    break;
                case "hg":
                    quantità = quantità / 10;
                    break;
                case "kg":
                    break;
                default:
                    Console.WriteLine("Unità di misura non valida!");
                    break;
            }
            if (um == "g" || um == "hg" || um == "kg")
            {
                if (giacenze[pos] < quantità)
                    Console.WriteLine("Quantità non disponibile!");
                else
                {
                    giacenze[pos] -= quantità;
                    totaleProdotto = prezzi[pos] * quantità;
                    totaleScontrino += totaleProdotto;
                    Console.WriteLine("Il prezzo del prodotto " + prodotti[pos] + " è: " + String.Format("{0:c2}", totaleProdotto));
                }
            }
        }
        
      

    }
    else
        Console.WriteLine("Prodotto non trovato!");

    Console.Write("Vuoi acquistare un altro prodotto (sì/no)? : ");
    risposta = Console.ReadLine();

} while (risposta.ToLower() == "sì");

Console.WriteLine("Totale scontrino: " + String.Format("{0:c2}", totaleScontrino));