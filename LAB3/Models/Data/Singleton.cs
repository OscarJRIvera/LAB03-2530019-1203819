using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArbolBinario;
using DoubleLinkedListLibrary1;


namespace LAB3.Models.Data
{
    public class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public AVL<LlaveArbol> Arbol_Farmacos;
        public DoubleLinkedList<Farmaco> List2;//mi lista aqui 
        public DoubleLinkedList<PedidosFarmacos> Pedidos;
        public String nombre;
        public int Nit = 0;
        public String Nombrecliente = "";
        public string dirrecion = "";
        public int id;
        public int eliminados = 0;

        public Random random;

        private Singleton()
        {
            random = new Random();
            Pedidos = new DoubleLinkedList<PedidosFarmacos>();
            Arbol_Farmacos = new AVL<LlaveArbol>(LlaveArbol.Compare_Llave_Arbol); //Se inicializa el arbol binario por nombre de farmaco
            List2 = new DoubleLinkedList<Farmaco>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}