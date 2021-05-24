using System;

namespace Nonogramas
{
    class Program
    {
        static void Main(string[] args)
        {
            //Número inicial de comprobaciones del usuario
            int comprobaciones = 4;
            //Crea un tablero según el nivel que se le pida
            Tablero tab = new Tablero("BN102.txt");
            //Dibujo inicial del tablero
            tab.Dibujo(comprobaciones);
            //Variable que contendrá la input de usuario
            string mov;
            //Variables para la posición del cursor
            int posX, posY;
            //Inicializo las coordenadas en la primera casilla del tablero que corresponde a la primera posición de la matriz
            posX = tab.dim + 3;
            posY = 2 * tab.longitudC;
            //Variable que indicará en que fila está el error si lo hay
            int filaError;
            Console.SetCursorPosition(posX, posY);
            //En cada vuelta del bucle comprueba si el puzle está completo y bien
            while (!tab.Completo())
            {
                LeeInput(out mov);//Lee la input del usuario
                tab.Mueve(mov, ref posX, ref posY);//Mueve el cursor si el usuario lo pide
                tab.MeteValor(posX, posY, mov);//Mete el valor que el usuario ponga
                //El usuario solo tiene 4 opciones para comprobar si tiene errores
                if (mov == "q" && comprobaciones > 0)
                {
                    //resta uno a las comprobaciones que quedan
                    comprobaciones--;
                    //Comparo las matrices
                    tab.Compara(out filaError);
                    //Vuelvo a dibujar el tablero
                    tab.Dibujo(comprobaciones);
                }
            }
            Console.SetCursorPosition(0, 4 * tab.dim);

        }
        //Método que lee la input del usuario
        static void LeeInput(out string c)
        {
            c = "";
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key.ToString())
                {
                    //Teclas de movimiento
                    case "LeftArrow": c = "l"; break;
                    case "UpArrow": c = "u"; break;
                    case "RightArrow": c = "r"; break;
                    case "DownArrow": c = "d"; break;
                    //teclas de entrada de color
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
                    case "X": c = "x"; break;
                    case "Spacebar": c = "1"; break;
                    case "Enter": c = " "; break;
                    case "Q": c = "q"; break;//comprueba tablero
                    default: break;
                }
            }
        }
        //Menú de juego
        static void Menu()
        {
            Console.WriteLine("Elige un puzle");
            Console.Write("¿Blanco y negro o a color? [B/N]");
        }


    }
}
