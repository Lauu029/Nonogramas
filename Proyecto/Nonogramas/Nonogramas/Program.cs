﻿using System;

namespace Nonogramas
{
    class Program
    {
        static void Main(string[] args)
        {
            //Nivel que escoge el usuario
            string nivel;

            //Variable que contendrá la input de usuario
            string mov="";

            //Variables para la posición del cursor
            int posX, posY;
           

            int filaError;//indica la fila del error si hay

            while(mov != "q")
            {
                Menu(out nivel);
                nivel += ".txt";
                Tablero tab = new Tablero(nivel);//crea el puzle leyendo del archivo lo que se le pide

                
                //Número inicial de comprobaciones del usuario; se resetea para cada puzle depende del tamaño del mismo
                int comprobaciones= tab.dim % 2;

                //Dibujo inicial del tablero
                tab.Dibujo(comprobaciones);

                //Inicializo las coordenadas en la primera casilla del tablero que corresponde a la primera posición de la matriz
                posX = 3* tab.longitudF + 3;
                posY = 2 * tab.longitudC;

                //Coloca el cursor en la primera casilla del tablero
                Console.SetCursorPosition(posX, posY);

                //En cada vuelta del bucle comprueba si el puzle está completo y bien y si el jugador ha pulsado la tecla para salir
                while (!tab.Completo()&&mov!="q")
                {
                    LeeInput(out mov);//Lee la input del usuario

                    tab.Mueve(mov, ref posX, ref posY);//Mueve el cursor si el usuario lo pide

                    tab.MeteValor(posX, posY, mov);//Mete el valor que el usuario ponga

                    //El usuario solo tiene un número limitado de opciones para comprobar si tiene errores
                    if (mov == "p" && comprobaciones > 0)
                    {
                        //resta uno a las comprobaciones que quedan
                        comprobaciones--;
                        //Comparo las matrices
                        tab.Compara(out filaError);
                        //Vuelvo a dibujar el tablero
                        tab.Dibujo(comprobaciones);
                    }
                }
                //pequeño retardo para que el usuario vea lo que ha hecho
                System.Threading.Thread.Sleep(1000);

                Console.SetCursorPosition(0, 4 * tab.dim);
                Console.Write("Pulsa 'enter' para hacer otro puzle o 'q' para salir :) ");
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
                    case "Q": c = "q"; break;//sale del juego
                    default: break;
                }
            }
            
        }
        //Menú de juego
        static void Menu(out string nivel)
        {
            Console.Clear();
            string aux = "";
            nivel = "";
            Console.Write("¿Quieres cargar un archivo propio o uno por defecto? [P/D]: ");
            aux = Console.ReadLine().ToUpper();
            if (aux == "P")
            {
                Console.Write("Escribe el nombre de tu puzle: ");
                nivel = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Elige un puzle");
                InputMenu(out aux, ref nivel, "B", "C", "D", "¿Blanco y negro o a color? [B/C]: ");
                InputMenu(out aux, ref nivel, "10", "15", "20", "Dimensión del puzle [10/15/20]: ");
                InputMenu(out aux, ref nivel, "1", "2", "c", "Elige un nivel[1/2]: ");
            }            
        }
        static void InputMenu(out string aux, ref string nivel, string lim1, string lim2, string lim3 , string texto)
        {
            
            do
            {
                Console.Write(texto);
                aux = Console.ReadLine().ToUpper();

            } while (aux != lim1 && aux != lim2 && aux !=lim3);
            nivel += aux;
        }


    }
}
