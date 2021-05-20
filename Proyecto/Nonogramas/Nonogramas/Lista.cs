using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogramas
{
    class Lista
    {
        public class Pares
        {
            public int valor;

            public ConsoleColor color;

            public Pares sig;
        }

        Pares pri;

        public Lista()
        {
            pri = null;
        }
        //Método que inserta al final de la lista el par que se le pasa
        public void InsertaPar(Pares par)
        {
            //2 casos
            //lista vacia
            if (pri == null)
            {
                pri = new Pares();  //creamos nodo en pri
                pri.valor = par.valor;
                pri.color = par.color;
                pri.sig = null;
            }
            else //lista no vacia
            {
                Pares aux = pri;  //recorremos la lista hasta el ultimo nodo

                while (aux.sig != null)
                {
                    aux = aux.sig;
                }
                //aux apunta al ultimo nodo
                aux.sig = new Pares();  //creamos el nuevo a continuacion
                aux = aux.sig;  //avanzamos aux al nuevo nodo
                aux.valor = par.valor; //ponemos info
                aux.color = par.color;
                aux.sig = null; //siguiente a null
            }
        }

        //Recorre los n elementos de la lista y devuelve el elemento n
        //Nunca va a devolver null
        public Pares nEsimo(int n)
        {
            Pares parAux = pri;
            //Recorre hasta n
            for (int i = 0; i < n; i++)
            {
                parAux = parAux.sig;
            }

            return parAux;
        }

        //Calcula el tamaño de la lista
        public int LongLista()
        {
            Pares par=pri;
            int i = 0;
            while (par != null)
            {
                par = par.sig;
                i++;
            }
                
            return i;
        }

        
    }
}
