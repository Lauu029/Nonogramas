﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Nonogramas
{
    class Tablero
    {
        //Matriz solución
        int[,] solucion;

        //Arrays de filas columnas con la información
        Lista [] filas;
        Lista [] columnas;
        //Números que determina el tamaño de la lista más larga en columnas y en filas
        public int longitudF=0;
        public int longitudC=0;

        //dimensión del puzle
        public int dim;

        //Pasa de posiciones del tablero a coordenadas de la matriz
        int i = 0;
        int j = 0;

        int[,] resultados;
        
        //método constructor
        public Tablero(string file)
        {
            //Creo el lector de niveles
            StreamReader lectorNivel = new StreamReader(file);

            //establezco la dimensión del tablero
            dim = int.Parse(lectorNivel.ReadLine());
            //Establezco la 
            //lector de las líneas
            string leeLineas="";
            //String inicial donde guardo la info que va leyendo de filas y columnas
            string info = "";
            
            
            while (leeLineas != ";") 
            {
                leeLineas = lectorNivel.ReadLine().Replace(" ", ",").Trim();
                if (leeLineas != ";"&&leeLineas!="")
                    info += leeLineas + ";";
            }

            //divido el string
            string [] arrayNivel = info.Split(";");

            //establezco el tamaño del array de info de filas
            filas = new Lista [dim];

            //relleno la matriz con la info de filas
            for (int i = 0; i < arrayNivel.Length-1; i++)
            {
                //string auxiliar
                string aux = arrayNivel[i];

                //array para guardar cada número de cada fila de arrayNivel
                string [] aux2 = aux.Split(",");

                Lista lst = new Lista();
                //variable para contar la longitud de las listas
                int lng = 0;
                for (int j = 0; j < aux2.Length; j++)
                {
                    
                    //divido el string para separar la información del color de la del número
                    string temp = aux2[j].Substring(0, 2);
                    int temp2 = int.Parse(temp);
                    temp = aux2[j].Substring(2, 1);
                    ConsoleColor color;
                    //asigna el color según el código que tengo preparado+
                    switch (temp)
                    {
                        case "a": color = ConsoleColor.DarkYellow; break;
                        case "b": color = ConsoleColor.Blue; break;
                        case "c": color = ConsoleColor.Red; break;
                        case "d": color = ConsoleColor.Green; break;
                        case "e": color = ConsoleColor.Gray; break;
                        case "f": color = ConsoleColor.Cyan; break;
                        case "g": color = ConsoleColor.DarkBlue; break;
                        case "h": color = ConsoleColor.DarkCyan; break;
                        case "i": color = ConsoleColor.DarkGreen; break;
                        case "j": color = ConsoleColor.DarkGray; break;
                        case "k": color = ConsoleColor.DarkMagenta; break;
                        case "l": color = ConsoleColor.DarkRed; break;
                        default: color = ConsoleColor.Black; break;
                    }
                    //variable para meter los pares en la lista
                    Lista.Pares par = new Lista.Pares
                    {
                        color = color,
                        valor = temp2
                    };
                    lst.InsertaPar(par);
                    lng++;
                }
                if (lng > longitudF) longitudF = lng;
                filas[i] = lst;
            }
            //Reseteo la variable info
            info = "";
            do
            {
                leeLineas = lectorNivel.ReadLine().Replace(" ", ",");
                if (leeLineas != ";" && leeLineas != "")
                    info += leeLineas + ";";
                

            } while (leeLineas != ";");

             //divido el string
             arrayNivel = info.Split(";");

            //establezco el tamaño del array de info de columnas
            columnas = new Lista[dim];

            //relleno la matriz con la info de columnas
            for (int i = 0; i < arrayNivel.Length-1; i++)
            {
                //string auxiliar
                string aux = arrayNivel[i];

                //array para guardar cada número de cada fila de arrayNivel
                string[] aux2 = aux.Split(",");

                Lista lst = new Lista();
                //variable para contar la longitud de las listas
                int lng = 0;
                for (int j = 0; j < aux2.Length; j++)
                {
                    string temp = aux2[j].Substring(0, 2);
                    int temp2 = int.Parse(temp);
                    temp = aux2[j].Substring(2, 1);
                    ConsoleColor color;
                    //asigna el color según el código que tengo preparado+
                    switch (temp)
                    {
                        case "a": color = ConsoleColor.DarkYellow; break;
                        case "b": color = ConsoleColor.Blue; break;
                        case "c": color = ConsoleColor.Red; break;
                        case "d": color = ConsoleColor.Green; break;
                        case "e": color = ConsoleColor.Gray; break;
                        case "f": color = ConsoleColor.Cyan; break;
                        case "g": color = ConsoleColor.DarkBlue; break;
                        case "h": color = ConsoleColor.DarkCyan; break;
                        case "i": color = ConsoleColor.DarkGreen; break;
                        case "j": color = ConsoleColor.DarkGray; break;
                        case "k": color = ConsoleColor.DarkMagenta; break;
                        case "l": color = ConsoleColor.DarkRed; break;
                        default: color = ConsoleColor.Black; break;
                    }
                    //variable para meter los pares en la lista
                    Lista.Pares par=new Lista.Pares();
                    par.color = color;
                    par.valor = temp2;
                    lst.InsertaPar(par);
                    lng++;
                }
                if (lng > longitudC) longitudC = lng;
                columnas[i] = lst;
            }

            //Reseteo la variable info
            info = "";
            do
            {
                //Hago la separación por comas también en la matriz para que no den problemas en los puzles a color
                leeLineas = lectorNivel.ReadLine().Replace(" ", ",").Trim();
                if (leeLineas != ";" && leeLineas != "")
                    info += leeLineas + ";";

            } while (leeLineas != ";"&& !lectorNivel.EndOfStream);

            //divido el string
            string [] matrizNivel = info.Split(";");

            //establezco el tamaño del array de info de filas
            solucion = new int[dim, dim];

            //relleno la matriz con la info de filas
            for (int i = 0; i < solucion.GetLength(0); i++)
            {
                //string auxiliar
                string aux = matrizNivel[i];
                //Segungo string auxiliar
                string[] aux2 = aux.Split(",");
                for (int j = 0; j < solucion.GetLength(1); j++)
                {
                    //voy asignando cada número
                    solucion[i, j] = int.Parse(aux2[j]);
                }
            }
            lectorNivel.Close();
            //dimensiono la matriz de resultados
            resultados = new int[dim, dim];
            //la lleno de 0 para representar espacios vacíos
            for (int i = 0; i < resultados.GetLength(0); i++)
            {
                for (int j = 0; j < resultados.GetLength(1); j++) resultados[i, j]=0;
            }
        }
        public void Dibujo()
        {
            //Ajusto el color del fondo y las letras
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            
            //Primero dibujo todo el tablero vacío            
            for (int i = 0; i < longitudC; i++)
            {
                //Escribo tres espacios por cada número de filas
                for (int l = 0; l < filas.Length; l++)
                {
                    Console.Write(" ");
                }
                //Escribo el separador de filas a columnas
                Console.Write("||");
                //Recorro cada posición del array y escribo espacios en todos los huecos donde irán luego los datos
                for (int j = 0; j < dim; j++)
                    Console.Write("   |");
                    
                Console.WriteLine();

                //Debajo de cada fila escribo la línea de separación, excepto en la última
                if (i != longitudC - 1)
                {
                    //Escribo tres espacios por cada número de filas
                    for (int l = 0; l < filas.Length; l++)
                    {
                        Console.Write(" ");
                    }
                    //Escribo el separador correspondiente
                    Console.Write("||");
                    for (int k = 0; k < dim; k++)
                    {
                        //Para la última línea escribo ---| y salto de línea y para el resto ---+
                        if (k == dim - 1)
                            Console.WriteLine("---|");
                        else
                            Console.Write("---+");
                    }
                }

            }
            //Al final de las filas dibujo la separación del tablero
            //Primero la separación para el dibujo de la info de las filas
            
            for (int l = 0; l < filas.Length; l++)
            {
                Console.Write("=");
            }
            //Las dos barras de separación
            Console.Write("||");
            //Tres - para debajo de cada número
            for (int m = 0; m < dim; m++)
            {
                //Si es la última fila hace el salto
                if (m == dim - 1)
                    Console.WriteLine("===|");
                else
                    Console.Write("====");
            }
            
            //Dibujo del resto del tablero
            for (int p = 0; p < resultados.GetLength(0); p++)
            {
                //Primero escribo la info de las filas y el inicio de la fila dependiendo si dim es par o impar 
                if (dim % 2 == 0)
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.Write(" ");
                }
                for (int q = 0; q < longitudF; q++)
                    Console.Write("  |");

                Console.Write("|");
                //Escribo el interior de la matriz de resultados
                for (int r = 0; r < resultados.GetLength(1); r++)
                {
                    
                        Console.Write("   |");
                }
                Console.WriteLine();
                //Si no es la última fila lo escribe normal
                if (p != resultados.GetLength(0) - 1)
                {
                    //Escribo tres - por cada número de filas para las indicaciones de las filas 
                    for (int l = 0; l < longitudF; l++)
                    {
                        Console.Write("---");
                    }
                    //si dim es par dibuja una barra más
                    if (dim % 2 == 0)
                    {
                        Console.Write("-");
                    }
                    Console.Write("||");
                    //Debajo de cada fila escribo la línea de separación
                    for (int k = 0; k < dim; k++)
                    {
                        //Para la última línea escribo ---| y salto de línea y para el resto ---+
                        if (k == dim - 1)
                            Console.WriteLine("---|");
                        else
                            Console.Write("---+");
                    }
                }
                else
                {
                    //En la última fila cierro el tablero
                    //Primero la separación para el dibujo de la info de las filas
                    for (int l = 0; l < longitudF; l++)
                    {
                        Console.Write("---");
                    }
                    //si dim es par dibuja una barra más
                    if (dim % 2 == 0)
                    {
                        Console.Write("-");
                    }
                    //Las dos barras de separación
                    Console.Write("||");
                    //Tres - para debajo de cada número
                    for (int m = 0; m < dim; m++)
                    {
                        //Si es la última fila hace el salto
                        if (m == dim - 1)
                            Console.WriteLine("---|");
                        else
                            Console.Write("----");
                    }
                }


            }
            //Creo dos variables para la posición del cursor
            int x = dim + 3;
            int y = longitudC + (longitudC - 2);
            //Una vez tengo el tablero vacío relleno la información de filas y columnas con las listas
            Console.SetCursorPosition(x, y);
            
            for (int i = 0; i < dim; i++)
            {
                int l = y;
                for (int j = columnas[i].LongLista()-1; j >= 0; j--)
                {
                    Lista.Pares par = columnas[i].nEsimo(j);
                    Console.ForegroundColor = par.color;
                    Console.Write(par.valor);
                    if(l>=2)
                    l -= 2;
                    Console.SetCursorPosition(x, l);
                    
                }
                x += 4;
                Console.SetCursorPosition(x, y);
            }
            x = dim-1;
            y +=2;
            //Una vez tengo el tablero vacío relleno la información de filas y columnas con las listas
            Console.SetCursorPosition(x, y);

            for (int i = 0; i < dim; i++)
            {
                int l = x;
                for (int j = filas[i].LongLista()-1; j >=0; j--)
                {
                    
                    Lista.Pares par = filas[i].nEsimo(j);
                    if (par.valor > 9)
                    {
                        Console.SetCursorPosition(l - 1, y);
                    }
                    Console.ForegroundColor = par.color;
                    Console.Write(par.valor);
                    if (l >= 3)
                        l -= 3;
                    Console.SetCursorPosition(l, y);

                }
                y += 2;
                Console.SetCursorPosition(x, y);
            }

            Console.SetCursorPosition(0, 2 * dim+10 );
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" A ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(" B ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" C ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(" D ");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(" E ");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write(" F ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(" G ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write(" H ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(" I ");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write(" J ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" K ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write(" L ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ESPACIO ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;              
            
        }
        public bool Compara(out int i)
        {
            i=0;
            int j = 0;
            bool iguales = true;
            while (i < solucion.GetLength(0) && iguales )
            {
                while (j < solucion.GetLength(1) && iguales)
                {
                    if (solucion[i, j] != resultados[i, j] && resultados[i, j] != 0)
                        iguales = false;
                    else
                        j++;
                }
                i++;
                j = 0;
            }
            return iguales;
            
            
        }
        public void Mueve(string c, ref int posX, ref int posY)
        {

            if (c == "u" && posY > 2 * longitudC)
            {
                posY -=2;
                if (i > 0) i--;
            }
            else if (c == "d" && posY < 2 * longitudC + (2 * dim)-3)
            {
                posY +=2;
                if (i < dim-1) i++;
            }
            else if(c=="l" && posX > dim + 3)
            {
                posX -=4;
                if (j > 0) j--;
            }
            else if(c=="r" &&posX < dim + (4 * dim) -1)
            {
                posX +=4;
                if (j < dim-1) j++;
            }
            Console.SetCursorPosition(posX, posY);
        }
        public void MeteValor(int posX, int posY, string c)
        {
            ConsoleColor colorMatriz;
            int introducido;
            if(c!="u" && c != "d" && c != "l" && c != "r" && c!="" && c!="x" && c!=" ")
            {
                introducido = int.Parse(c);
                resultados[i, j] = introducido;
                
                //asigna el color según el código que tengo preparado+
                switch (c)
                {
                    case "2": colorMatriz = ConsoleColor.DarkYellow; break;
                    case "3": colorMatriz = ConsoleColor.Blue; break;
                    case "4": colorMatriz = ConsoleColor.Red; break;
                    case "5": colorMatriz = ConsoleColor.Green; break;
                    case "6": colorMatriz = ConsoleColor.Gray; break;
                    case "7": colorMatriz = ConsoleColor.Cyan; break;
                    case "8": colorMatriz = ConsoleColor.DarkBlue; break;
                    case "9": colorMatriz = ConsoleColor.DarkCyan; break;
                    case "10": colorMatriz = ConsoleColor.DarkGreen; break;
                    case "11": colorMatriz = ConsoleColor.DarkGray; break;
                    case "12": colorMatriz = ConsoleColor.DarkMagenta; break;
                    case "13": colorMatriz = ConsoleColor.DarkRed; break;
                    default: colorMatriz = ConsoleColor.Black; break;
                }
                Console.SetCursorPosition(posX - 1, posY);
                Console.BackgroundColor = colorMatriz;
                Console.Write("   ");
                Console.BackgroundColor = ConsoleColor.White;
            }
            else if (c == "x")
            {
                resultados[i, j] = 0;
                Console.SetCursorPosition(posX - 1, posY);
                Console.Write(" X ");
            }
            else if(c==" ")
            {
                resultados[i, j] = 0;
                Console.SetCursorPosition(posX - 1, posY);
                Console.Write("   ");
            }

        }
    }
}