using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace apprentissageSUP
{
    class Neurone
    {
        private int numero;
        public List<Neurone> entrees;
        public List<Neurone> sorties;
        private int numerocouche;
        private double sortie;
        //Color couleur;
        // pptés pour le calcul de la rétropropagation du gradient
        private double somme=0;
        private double delta;

        public Neurone(int num, int numcouche)
        {
            numero = num;
            numerocouche = numcouche;
            sortie = 0;
            entrees = new List<Neurone>();
            sorties = new List<Neurone>();
        }

        public int GetNumero ()  { return numero; }

        public double GetSortie()
        {
            return sortie;
        }

        public int GetNumeroCouche()
        {
            return numerocouche;
        }

        public double GetSomme() { return somme; }

        public double g(double s)
        {  
            // Fonction sigmoïde classique
           //  if (sorties.Count==0)
          //      return s;
          //  else   //*) g := 1 / (1+exp(-s));
            return 1 / (1 + Math.Exp(-s));
        }

        public double gprime(double s)
        {
            // Dérivée de la sigmoïde, on en a bseoin dans backprop
            return Math.Exp(-s) / Math.Pow(1+Math.Exp(-s),2);
        }

        public void ResetSortie()
        {
            sortie = 0;
        }
        //*******************************************************************
        public void CalculeSortie(double[,] tabpoids)
        {
            int i;
            double w;
            Neurone neuronepred;
            // A remplir
            somme = 0;
            for (i = 0; i < entrees.Count; i++)
            {
                neuronepred = entrees[i];
                w = tabpoids[neuronepred.numero, numero];
                somme = somme + w * neuronepred.GetSortie();
            }
            sortie= g(somme);
        }
        public void Setdelta(double d)
        {
            delta = d;
        }
        public double Getdelta() { return delta; }

        public void ImposeSortie(double s) { sortie = s; }
    }
}
