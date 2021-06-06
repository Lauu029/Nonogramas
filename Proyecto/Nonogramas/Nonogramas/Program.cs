using System;

namespace Nonogramas
{
    class Program
    {
        static void Main(string[] args)
        {
            string nivel;//Nivel que escoge el usuario

            string mov="";//Input de usuario

            int posX, posY;//Posición del cursor

            int filaError;//indica la fila del error si hay

            while(mov != "q")
            {
                Menu(out nivel);
                nivel += ".txt";
                Tablero tab = new Tablero(nivel);//crea el puzle leyendo del archivo lo que se le pide

                //Número inicial de comprobaciones del usuario; se resetea para cada puzle depende del tamaño del mismo
                int comprobaciones= 4;

                //Dibujo inicial del tablero
                tab.Dibujo(comprobaciones);

                //Inicializo las coordenadas en la primera casilla del tablero que corresponde a la primera posición de la matriz
                posX = 3* tab.longitudF + 3;
                posY = 2 * tab.longitudC;

                //Coloca el cursor en la primera casilla del tablero
                Console.SetCursorPosition(posX, posY);

                //En cada vuelta del bucle comprueba si el puzle está completo y bien y si el jugador ha pulsado la tecla para salir
                while (!tab.Completo() && mov!="q")
                {
                    LeeInput(out mov);//Lee la input del usuario
                    tab.Mueve(mov, ref posX, ref posY);//Mueve el cursor si el usuario lo pide
                    tab.MeteValor(posX, posY, mov);//Mete el valor que el usuario ponga
                    //El usuario solo tiene un número limitado de opciones para comprobar si tiene errores
                    if (mov == "p" && comprobaciones > 0)
                    {
                        comprobaciones--;//resta uno a las comprobaciones que quedan
                        tab.Compara(out filaError);//Comparo las matrices
                        tab.Dibujo(comprobaciones);//Vuelvo a dibujar el tablero
                    }
                }
                //pequeño retardo para que el usuario vea lo que ha hecho
                System.Threading.Thread.Sleep(1000);
                LeeInput(out mov);
            }

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
                    case "H": c = "9"; break;
                    case "I": c = "10"; break;
                    case "J": c = "11"; break;
                    case "K": c = "12"; break;
                    case "L": c = "13"; break;
                    case "X": c = "x"; break;
                    case "Spacebar": c = "1"; break;
                    case "Enter": c = " "; break;
                    case "P": c = "p"; break;//comprueba tablero
                    case "Q": c = "q"; break;//sale del puzle
                    default: break;
                }
            }
            
        }
        //Menú de juego
        static void Menu(out string nivel)
        {
            //Resetea el color y limpia el tablero
            Console.ResetColor();
            Console.Clear();
            string aux;//para saber si el archivo es propio del usuario o no
            nivel = "";
            Console.Write("¿Quieres cargar un archivo propio o uno por defecto? [P/D]: ");
            aux = Console.ReadLine().ToUpper();
            //Si es un archivo del usuario simplemente pide el nombre del archivo
            if (aux == "P")
            {
                Console.Write("Escribe el nombre de tu puzle: ");
                nivel = Console.ReadLine();
            }
            //si es un archivo por defecto pregunta el tipo de archivo que se quiere y genera el nombre correspondiente según las respuestas del usuario
            else
            {
                Console.WriteLine("Elige un puzle");
                InputMenu(out aux, ref nivel, "B", "C", "D", "¿Blanco y negro o a color? [B/C]: ");
                InputMenu(out aux, ref nivel, "10", "15", "20", "Dimensión del puzle [10/15/20]: ");
                InputMenu(out aux, ref nivel, "1", "2", "c", "Elige un nivel[1/2]: ");
            }            
        }
        //Método que le la input del usuario y lo añade al string de nombre del texto
        static void InputMenu(out string aux, ref string nivel, string lim1, string lim2, string lim3 , string texto)
        {
            
            do
            {
                //lee lo que escriba el usuario y lo repite mientras no sea uno de las input esperadas
                Console.Write(texto);
                aux = Console.ReadLine().ToUpper();

            } while (aux != lim1 && aux != lim2 && aux !=lim3);
            //añade la entrada correspondiente al string de nombre del nivel
            nivel += aux;
        }


    }
}
