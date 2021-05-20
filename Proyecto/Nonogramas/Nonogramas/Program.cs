using System;

namespace Nonogramas
{
    class Program
    {
        static void Main(string[] args)
        {
            Tablero tab = new Tablero("Nivel0.txt");
            tab.Dibujo();
            string mov="";
            int posX, posY;
            posX = tab.dim + 3;
            posY = 2 * tab.longitudC;
            Console.SetCursorPosition(posX, posY);
            while (true)
            {
                LeeInput(out mov);
                tab.Mueve(mov,ref posX,ref posY);
                tab.MeteValor(posX, posY, mov);
            }
            
        }
        static void LeeInput(out string c)
        {
            c = "";
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key.ToString())
                {
                    case "LeftArrow": c = "l"; break;
                    case "UpArrow": c = "u"; break;
                    case "RightArrow": c = "r"; break;
                    case "DownArrow": c = "d"; break;
                    case "A": c = "2"; break;
                    case "B": c = "3"; break;
                    case "C": c = "4"; break;
                    case "D": c = "5"; break;
                    case "E": c = "6"; break;
                    case "F": c = "7"; break;
                    case "G": c = "8"; break;
                    case "H": c = "10"; break;
                    case "I": c = "11"; break;
                    case "J": c = "12"; break;
                    case "K": c = "13"; break;
                    case "L": c = "14"; break;
                    case "X": c = "x";break;
                    case "Spacebar": c = "1"; break;
                    case "Enter": c = " "; break;
                    /*case "S": c = "s"; break;//guarda
                    case "Q": c = "q"; break;//sale*/
                    default: break;
                }
            }
        }
      
      
    }
}
