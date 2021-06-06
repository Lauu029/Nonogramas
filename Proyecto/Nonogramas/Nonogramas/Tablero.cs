using System;
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
        Lista[] filas;
        Lista[] columnas;
        //Números que determina el tamaño de la lista más larga en columnas y en filas
        public int longitudF = 0;
        public int longitudC = 0;
            
        //dimensión del puzle
        public int dim;

        //Pasa de posiciones del tablero a coordenadas de la matriz
        int i = 0;
        int j = 0;

        int[,] resultadosUsuario;

        //método constructor
        public Tablero(string file)
        {
            //Creo el lector de niveles
            StreamReader lectorNivel = new StreamReader(file);

            string leeLineas;//lector de las líneas

            string info = "";//info que va leyendo de filas y columnas

            
            //leo la información de la matriz de soluciones
            do
            {
                //Hago la separación por comas también en la matriz para que no den problemas con los números de dos dígitos
                leeLineas = lectorNivel.ReadLine().Replace(" ", ",").Trim();

                if (leeLineas != ";" && leeLineas != "")
                    info += leeLineas + ";";

            } while (leeLineas != ";" && !lectorNivel.EndOfStream);//lee hasta el final del archivo

            //divido el string
            string[] matrizNivel = info.Split(";");

            //asigno la dimensión por la cantidad de filas que haguardado
            dim = matrizNivel.Length -1;

            //Cierro el lector
            lectorNivel.Close();

            //establezco el tamaño de la matriz y de los arrays de info
            solucion = new int[dim, dim];
            filas = new Lista[dim];
            columnas = new Lista[dim];

            //relleno la matriz con la info de filas
            for (int i = 0; i < solucion.GetLength(0); i++)
            {
                //string auxiliar
                string aux = matrizNivel[i];

                //Segundo string auxiliar
                string[] aux2 = aux.Split(",");

                for (int j = 0; j < solucion.GetLength(1); j++)
                {
                    //voy asignando cada número en la matriz
                    solucion[i, j] = int.Parse(aux2[j]);
                }
            }
            

            //Variables auxiliares para almacenar los datos y las cantidades
            int cantidad=0;
            string aux3="";//guarda la info de cada fila
            string infoEntera="";//guarda la info completa
            string [] valoresFilas;//almacena lo guardado en un array
            char valor= ' ';
            bool flag = false;//flag necesario para el condicional

            //Saco la info de filas por la matriz
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    //Miientras haya 0 no entra, una vez encuentra el primer 0 entra siempre
                    if (solucion[i, j] != 0 || flag)
                    {
                        flag = true;
                        //Comprueba si el número es diferente al anterior, que no sea 0 y no está en la primera posición
                        if(j > 0 && solucion[i,j]!=solucion[i,j-1] && solucion[i,j]!=0)
                        {
                            //si ha almacenado alguna cantidad de algún número lo escribe
                            if (cantidad > 0)
                            {
                                if (cantidad < 10)
                                {
                                    aux3 += "0";
                                }

                                aux3 += cantidad.ToString() + valor.ToString() + " ";
                            }
                            //establece el valor de color según el número asignado en la matriz
                            valor = DevuelveLetra(solucion[i,j]);
                            //inicia el contador de cantidad
                            cantidad = 1;
                            
                        }
                        //comprueba para la primera posición
                        else if (j == 0)
                        {
                            valor = DevuelveLetra(solucion[i, j]);
                            cantidad = 1;
                        }
                        //aumenta el contador si el valor es igual al anterior y este no es 0
                        else if (solucion[i,j]!=0)
                        {
                            cantidad++;
                        }
                        //para la última posición de la fila guarda lo que haya leído
                        if (j == solucion.GetLength(1) - 1)
                        {
                            if (cantidad < 10)
                            {
                                aux3 += "0";
                            }

                            aux3 += cantidad.ToString() + valor.ToString() + " ";
                        }
                        
                    }
                    
                }
                //Borra el último espacio almacenado
                aux3.TrimEnd(' ');
                infoEntera += aux3 + ";";
                //resetea las variables
                aux3 = "";
                cantidad = 0;
                flag = false;
            }
            //divide las filas leídas en un array
            valoresFilas = infoEntera.Split(";");

            //relleno la matriz con la info de columnas
            for (int i = 0; i < valoresFilas.Length - 1; i++)
            {
                //string auxiliar
                string aux = valoresFilas[i];

                //array para guardar cada número de cada fila de arrayNivel
                string[] aux2 = aux.Split(" ");

                Lista lst = new Lista();

                //variable para contar la longitud de las listas
                int lng = 0;

                for (int j = 0; j < aux2.Length-1; j++)
                {
                    //los dos primeros caracteres representan la cantidad
                    string temp = aux2[j].Substring(0, 2);

                    int temp2 = int.Parse(temp);

                    //El último caracter establece el color
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

                //Mete la lista en la posición indicada del array
                filas[i] = lst;
            }

            //reseteo la variable de info para almacenar lo de las columnas
            infoEntera = "";
            
            //Saco la info de columnas por la matriz
            for (int j = 0; j < dim; j++)
            {
                for (int i = 0; i < dim; i++)
                {
                    if (solucion[i, j] != 0 || flag)
                    {
                        flag = true;
                        if (i > 0 && solucion[i, j] != solucion[i-1, j ] && solucion[i, j] != 0)//Quitar j>0
                        {
                            if (cantidad > 0)
                            {
                                if (cantidad < 10)
                                {
                                    aux3 += "0";
                                }

                                aux3 += cantidad.ToString() + valor.ToString() + " ";
                            }
                            valor = DevuelveLetra(solucion[i, j]);
                            cantidad = 1;

                        }
                        else if (i == 0)
                        {
                            valor = DevuelveLetra(solucion[i, j]);
                            cantidad = 1;
                        }
                        else if (solucion[i, j] != 0)
                        {
                            cantidad++;
                        }
                        if (i == solucion.GetLength(1) - 1)
                        {
                            if (cantidad < 10)
                            {
                                aux3 += "0";
                            }

                            aux3 += cantidad.ToString() + valor.ToString() + " ";
                        }

                    }

                }

                aux3.TrimEnd(' ');
                infoEntera += aux3 + ";";
                aux3 = "";
                cantidad = 0;
                flag = false;
            }
            valoresFilas = infoEntera.Split(";");

            //relleno la matriz con la info de columnas
            for (int i = 0; i < valoresFilas.Length - 1; i++)
            {
                //string auxiliar
                string aux = valoresFilas[i];

                //array para guardar cada número de cada fila de arrayNivel
                string[] aux2 = aux.Split(" ");

                Lista lst = new Lista();

                //variable para contar la longitud de las listas
                int lng = 0;

                for (int j = 0; j < aux2.Length - 1; j++)
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
                    Lista.Pares par = new Lista.Pares
                    {
                        color = color,
                        valor = temp2
                    };

                    lst.InsertaPar(par);

                    lng++;
                }
                if (lng > longitudC) longitudC = lng;

                columnas[i] = lst;
            }

            //dimensiono la matriz de resultados
            resultadosUsuario = new int[dim, dim];

            //la lleno de 0 porque inicialmente está vacía
            for (int i = 0; i < resultadosUsuario.GetLength(0); i++)
            {
                for (int j = 0; j < resultadosUsuario.GetLength(1); j++) resultadosUsuario[i, j] = 0;
            }
        }
        //método que devuelve una letra en función del número dado
        static char DevuelveLetra(int num)
        {
            //asigna un valor dependiendo del número que haya en la matriz
            char letra;
            switch (num)
            {
                case 2: letra = 'a'; break;
                case 3: letra = 'b'; break;
                case 4: letra = 'c'; break;
                case 5: letra = 'd'; break;
                case 6: letra = 'e'; break;
                case 7: letra = 'f'; break;
                case 8: letra = 'g'; break;
                case 9: letra = 'h'; break;
                case 10: letra = 'i'; break;
                case 11: letra = 'j'; break;
                case 12: letra = 'k'; break;
                case 13: letra = 'l'; break;
                default: letra = 'n'; break;
            }
            return letra;
        }

        //Método que dibuja el tablero
        //Le paso comprobaciones para que vaya escribiendo cuantas le quedan al jugador cada vez que comprueba el tablero
        public void Dibujo(int comprobaciones)
        {
            Console.Clear();
            //Ajusto el color del fondo y las letras
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            //Primero dibujo todo el tablero vacío            
            for (int i = 0; i < longitudC; i++)
            {
                //Escribo tres por cada fila, escribo tantos como la más larga para que quepa la info completa
                for (int l = 0; l < longitudF; l++)
                {
                    Console.Write("   ");
                }
                //Escribo el separador de filas a columnas
                Console.Write("||");

                //Recorro cada posición del array y escribo espacios en todos los huecos donde irán luego los datos
                for (int j = 0; j < dim; j++)
                    Console.Write("   |");

                Console.WriteLine();

                //Debajo de cada fila escribo la línea de separación, excepto en la última que es diferente
                if (i != longitudC - 1)
                {
                    //Escribo tres espacios por cada número de filas(según la más larga)
                    for (int l = 0; l < longitudF; l++)
                    {
                        Console.Write("   ");
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
            for (int l = 0; l < longitudF; l++)
            {
                Console.Write("===");
            }

            //Las dos barras de separación
            Console.Write("||");

            //Tres = para debajo de cada número
            for (int m = 0; m < dim; m++)
            {
                //Si es la última fila hace el salto y dibuja el límite de tablero
                if (m == dim - 1)
                    Console.WriteLine("===|");

                else
                    Console.Write("====");
            }

            //Dibujo del resto del tablero
            for (int p = 0; p < solucion.GetLength(0); p++)
            {
               
                //Escribo dos espacios y la barra separadora por cada número de info de filas
                for (int l = 0; l < longitudF; l++)
                {
                    Console.Write("|  ");
                }

                //Separación con el tablero
                Console.Write("||");

                //Escribo el interior de la matriz de resultados según lo que haya en esta
                for (int r = 0; r < resultadosUsuario.GetLength(1); r++)
                {
                    //Si hay un 0 escribe espacios y la separación
                    if (resultadosUsuario[p, r] == 0)
                        Console.Write("   |");

                    //si no hay una x(14) escribe los espacios coloreados según el color que se pida
                    else if (resultadosUsuario[p, r] != 14)
                    {
                        switch (resultadosUsuario[p, r])
                        {
                            case 2: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
                            case 3: Console.BackgroundColor = ConsoleColor.Blue; break;
                            case 4: Console.BackgroundColor = ConsoleColor.Red; break;
                            case 5: Console.BackgroundColor = ConsoleColor.Green; break;
                            case 6: Console.BackgroundColor = ConsoleColor.Gray; break;
                            case 7: Console.BackgroundColor = ConsoleColor.Cyan; break;
                            case 8: Console.BackgroundColor = ConsoleColor.DarkBlue; break;
                            case 9: Console.BackgroundColor = ConsoleColor.DarkCyan; break;
                            case 10: Console.BackgroundColor = ConsoleColor.DarkGreen; break;
                            case 11: Console.BackgroundColor = ConsoleColor.DarkGray; break;
                            case 12: Console.BackgroundColor = ConsoleColor.DarkMagenta; break;
                            case 13: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                            default: Console.BackgroundColor = ConsoleColor.Black; break;
                        }

                        Console.Write("   ");

                        //resetea el color de fondo
                        Console.BackgroundColor = ConsoleColor.White;

                        //Separación de casillas
                        Console.Write("|");
                    }
                    else
                    {
                        //si hay una x la escribe
                        Console.Write(" X |");
                    }


                }
                Console.WriteLine();

                //Si no es la última fila escribe la separación entre filas simple
                if (p != solucion.GetLength(0) - 1)
                {
                    //Escribo tres - por cada número de filas para las indicaciones de las filas 
                    for (int l = 0; l < longitudF; l++)
                    {
                        Console.Write("---");
                    }
                    
                    //Separación con el tablero
                    Console.Write("||");

                    //Líneas de separación dentro del tablero
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
                    //En la última fila cierro el tablero, primero escribo el cierre de la info                    
                    for (int l = 0; l < longitudF; l++)
                    {
                        Console.Write("---");
                    }
                    
                    //Las dos barras de separación
                    Console.Write("||");

                    //Cierre del tablero
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

            //Una vez dibujado el tablero, meto la info de filas y columnas

            //Creo dos variables para guardar y cambiar la posición del cursor

            //se coloca en la primera posición de la izquierda de la info de columnas
            int x = 3 * longitudF + 3;
            int y = longitudC + (longitudC - 2);

            //coloco el cursor
            Console.SetCursorPosition(x, y);

            //Recorre el array que contiene la info de las columnas
            for (int i = 0; i < dim; i++)
            {
                int l = y;//variable auxiliar para mover el cursor

                //Recorre cada par de la lista correspondiente
                for (int j = columnas[i].LongLista() - 1; j >= 0; j--)
                {
                    //Creo un par auxiliar para recoger la info según la posición de la lista
                    Lista.Pares par = columnas[i].nEsimo(j);

                    //Cambio el color según la información del par
                    Console.ForegroundColor = par.color;

                    //Escribo el valor del par
                    Console.Write(par.valor);
                    //Posiciono el cursor dos espacios hacia arriba(siguiente casilla)
                    if (l >= 2)
                        l -= 2;

                    Console.SetCursorPosition(x, l);

                }
                //Muevo el cursor a la siguiente casilla a la derecha
                x += 4;

                //Vuelvo a la altura más baja de la info de columnas para repetir el proceso
                Console.SetCursorPosition(x, y);
            }

            //Coloco el cursor en la primera casilla de info de filas (arriba a la derecha)
            x = 3 * longitudF - 1;
            y += 2;
            
            Console.SetCursorPosition(x, y);//Coloco el cursor

            for (int i = 0; i < dim; i++)
            {
                int l = x;//variable auxiliar

                for (int j = filas[i].LongLista() - 1; j >= 0; j--)
                {
                    //Variable auxiliar
                    Lista.Pares par = filas[i].nEsimo(j);

                    //Si el numero es de dos cifras muevo uno más el cursor para que la info quede centrada en la casilla
                    if (par.valor > 9)
                    {
                        Console.SetCursorPosition(l - 1, y);
                    }

                    //Pone el color correspondiente y el valor
                    Console.ForegroundColor = par.color;
                    Console.Write(par.valor);

                    //Mueve el cursor tres a la izquierda para poner el siguiente valor
                    if (l >= 3)
                        l -= 3;
                    Console.SetCursorPosition(l, y);

                }
                //Mueve el cursor a la primera casilla de la fila de debajo para colocar la siguiente info
                y += 2;
                Console.SetCursorPosition(x, y);
            }

            //Coloco el cursor debajo del tablero para añadir la info extra
            Console.SetCursorPosition(0,2* longitudC + 2*dim +1);

            //Reseteo el color de las letras
            Console.ForegroundColor = ConsoleColor.Black;
            
            //Escribo la información de qué tecla debe pulsar el jugador para meter cada color
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
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write(" I ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
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
            Console.WriteLine();
            
            //Indica cuantas comprobaciones le quedan al jugador
            Console.WriteLine("Pulsa 'p' para comprobar si tienes errores; te quedan " + comprobaciones + " intentos para comprobar");

            Console.WriteLine();

            //Variable para indicar la fila del error en caso de que hubiera
            int filaError;

            //Si ha encontrado algún fallo lo escribe de color rojo indicando en que fila está
            if (!Compara(out filaError))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error en la fila " + filaError);
                Console.ForegroundColor = ConsoleColor.Black;
            }
            //Si no, indica que no hay errores
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ningún errror de momento :)");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine("Pulsa 'q' para salir del juego");
        }
       
        //Método que comprueba si la matriz que ha introducido el usuario es correcta
        public bool Compara(out int i)
        {
            //Variables para recorrer el bucle
            i = 0;
            int j = 0;

            //Variable de control del bucle
            bool iguales = true;

            //Recorre la matriz de soluciones para encontrar si hay alguna diferencia entre esa y la del usuario
            while (i < solucion.GetLength(0) && iguales)
            {
                while (j < solucion.GetLength(1) && iguales)
                {
                    //Compara lo que ha introducido el usuario con la matriz de soluciones
                    //Exceptua si hay espacios vacíos o x
                    if (resultadosUsuario[i, j] != solucion[i, j] && resultadosUsuario[i, j] != 0 && resultadosUsuario[i,j]!=14)
                        iguales = false;

                    //Si hay x donde tendría que haber un valor, también da error
                    else if (solucion[i, j] != 0 && resultadosUsuario[i, j] == 14)
                        iguales = false;

                    else 
                        j++;
                }
                i++;
                j = 0;
            }
            //devuelve true si no hay ningún error
            return iguales;
        }

        //Método que mueve el cursor por el tablero según la input de usuario
        //Recoge la info de la input de teclado y por referencia la posición x e y para saber donde estaba y guardar la nueva posición
        public void Mueve(string c, ref int posX, ref int posY)
        {
            //varía la X y la Y en función de la input y solo si no está en los límites correspondientes
            //va cambiando el valor de I y J para saber en que posición de la matriz está (ya que el cursor empieza en (0,0))
            if (c == "u" && posY > 2 * longitudC)
            {
                posY -= 2;
                if (i > 0) i--;
            }
            else if (c == "d" && posY < 2 * longitudC + (2 * dim) - 3)
            {
                posY += 2;
                if (i < dim - 1) i++;
            }
            else if (c == "l" && posX > 3 * longitudF + 3)
            {
                posX -= 4;
                if (j > 0) j--;
            }
            else if (c == "r" && posX < 3* longitudF + (4 * dim)-2)
            {
                posX += 4;
                if (j < dim - 1) j++;
            }
            //Muevo el cursor donde se le ha indicado
            Console.SetCursorPosition(posX, posY);
        }

        //Método que mete el valor en la matriz de resultados y lo dibuja en panalla
        //Recoge la info de la posición del cursor y de la input de teclado
        public void MeteValor(int posX, int posY, string c)
        {
            
            ConsoleColor colorMatriz;//variable auxiliar de color
            int introducido;//variable auxiliar de info de la input

            //Exceptua si la input es para moverse, comprobar y salir
            //Exceptúa también string vacío y la X
            if (c != "u" && c != "d" && c != "l" && c != "r" && c != "" && c != "x" && c != " " && c != "q" && c!="p")
            {
                /*Convierte el valor introducido en un int
                 (aunque el usuario mete una letra, el programa lo convierte en el número que corresponde)*/
                introducido = int.Parse(c);

                //Mete en la matriz del usuario la info que ha metido
                resultadosUsuario[i, j] = introducido;

                //asigna el color según el código que tengo preparado
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
                //Coloca el cursor 1 a la izquierda para dibujar los tres espacios de la casilla
                Console.SetCursorPosition(posX - 1, posY);

                Console.BackgroundColor = colorMatriz;//Pone el color correspondiente

                Console.Write("   ");

                Console.BackgroundColor = ConsoleColor.White;//Resetea el color de fondo
            }
            //Si la input es una x
            else if (c == "x")
            {
                /*Identifica las x con el 14 para diferenciarlo de las casillas vacías,
                 pero tienen el mismo valor a la hora de comprobar las matrices*/
                resultadosUsuario[i, j] = 14;

                //Escribe la cruz en pantalla
                Console.Write("X");
            }
            //Si el usuario pulsa enter, se borra lo que había en la casilla
            else if (c == " ")
            {
                //Pone a 0 la casilla en la info de resultados
                resultadosUsuario[i, j] = 0;

                //dibuja espacios en blanco para "borrar" lo que había en pantalla
                Console.SetCursorPosition(posX - 1, posY);
                Console.Write("   ");
            }

        }

        //Método que compueba si el puzle está completo y bien hecho
        public bool Completo()
        {
            //Variable de control del bucle
            bool completo = true;

            //variables para recorrer el bucle
            int i = 0;
            int j;

            while (i < solucion.GetLength(0) && completo)
            {
                j = 0;//reseteo j

                while (j < solucion.GetLength(1) && completo)
                {
                    //Comparo si la solucion del jugador es igual que la solucion del puzle
                    if (solucion[i, j] == resultadosUsuario[i, j])
                    {
                        j++;
                    }
                    //si no es igual comprueba que lo que haya no sea una x, si es asi lo cuenta como igual
                    else if (resultadosUsuario[i, j] == 14 && solucion[i, j] == 0)
                    {
                        j++;
                    }
                    //Si no se da ninguno de esos casos, completo es false
                    else
                    {
                        completo = false;
                    }
                }
                i++;
            }
            //devuelve completo
            return completo;
        }
    }
}
