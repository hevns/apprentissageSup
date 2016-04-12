using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;	

//using System.ComponentModel;
using System.Windows.Forms;

namespace apprentissageSUP
{
    class Reseau
    {
        Random rnd = new Random();
        //int nbcouches;
        List<Neurone> listeneurones;     // Liste des neurones du réseau
        List<Neurone>[] tabcouches;  // tableau des couches de neurones
        double[,] tabpoids;      // Matrice d'adjacence des poids synaptiques

        // Constructeur
        // On initialise un réseau de neurones à partir du nombre d'entrées, du nb de couches
        // et du nb de neurones par couche
        public Reseau(int nbentrees, int nbcouches, int neuronesparcouche)
        {
            int i, j, k, cpt;
            Neurone neurone;

            // Nombre de neurones
            // nbneurones= nbentrees+(nbcouches-2)*neuronesparcouche+1;
            // Initialisation des listes avant de poursuivre
            listeneurones = new List<Neurone>();
            tabcouches = new List<Neurone>[nbcouches];
            for (i = 0; i < nbcouches; i++)
            {
                tabcouches[i] = new List<Neurone>();
            }
            cpt = -1;
            for (i = 0; i < nbentrees; i++)
            {
                cpt++;
                neurone = new Neurone(cpt, 0);
                listeneurones.Add(neurone);
                tabcouches[0].Add(neurone);
            }
            // On fait les couches cachées :
            for (i = 1; i < nbcouches - 1; i++)
                for (j = 0; j < neuronesparcouche; j++)
                {
                    cpt++;
                    neurone = new Neurone(cpt, i);
                    listeneurones.Add(neurone);
                    tabcouches[i].Add(neurone);
                    // Connexion avec neurones couche précédente
                    for (k = 0; k < tabcouches[i - 1].Count; k++)
                    {
                        tabcouches[i - 1][k].sorties.Add(neurone);
                        neurone.entrees.Add(tabcouches[i - 1][k]);
                    }
                }
            // On fait le neurone de sortie
            cpt++;
            neurone = new Neurone(cpt, nbcouches - 1);
            listeneurones.Add(neurone);
            tabcouches[nbcouches - 1].Add(neurone);
            // Connexion avec neurones couche précédente
            for (k = 0; k < tabcouches[nbcouches - 2].Count; k++)
            {
                tabcouches[nbcouches - 2][k].sorties.Add(neurone);
                neurone.entrees.Add(tabcouches[nbcouches - 2][k]);
            }

            // Initialisation de la matrice des poids synap. : 0= pas de synapse
            int nbneurones = listeneurones.Count;
            tabpoids = new double[nbneurones, nbneurones];
            Random rnd = new Random();
            for (i = 0; i < nbneurones; i++)
                for (j = 0; j < nbneurones; j++)
                    tabpoids[i, j] = rnd.NextDouble()*2-1;

        }

        /*****************************************************************/
        public void ReInitializationActivation()
        {
            Neurone n;
            int i;

            for (i = 0; i < this.listeneurones.Count; i++)
            {
                n = listeneurones[i];
                n.ResetSortie();
            }
        }

        //********************************************************************
        // Calcul de la sortie de tous les neurones d'une couche donnée
        // Méthode appelée par backprop
        public void ProcessLayerK(int k)
        {
            int i;
            Neurone neurone;

            for (i = 0; i < tabcouches[k].Count; i++)
            {
                neurone = tabcouches[k][i];
                // Calcul de la somme des entrées pondérées par les poids
                // synaptiques + sigmoïde.
                neurone.CalculeSortie(tabpoids);
            }
        }
        //************************************************************/
        /*  Affichage des infos d'1 neurone */
        public void AfficheInfoNeurone(int couche, int num, ListBox lbox)
        {
            Neurone neurone, neurone2;
            int i;

            neurone = tabcouches[couche][num];
            lbox.Items.Add("Neurone " + Convert.ToString(neurone.GetNumero()));
            lbox.Items.Add("Somme :" + Convert.ToString(neurone.GetSomme()));
            lbox.Items.Add("Sortie :" + Convert.ToString(neurone.GetSortie()));
            lbox.Items.Add(Convert.ToString(neurone.entrees.Count) + " entrées :");
            for (i = 0; i < neurone.entrees.Count; i++)
            {
                neurone2 = neurone.entrees[i];
                lbox.Items.Add("Num.:" + Convert.ToString(neurone2.GetNumero())
                    + " Poids:" + Convert.ToString(tabpoids[neurone2.GetNumero(), neurone.GetNumero()]));
            }
            lbox.Items.Add(Convert.ToString(neurone.sorties.Count) + "sorties :");
            for (i = 0; i < neurone.sorties.Count; i++)
            {
                neurone2 = neurone.sorties[i];
                lbox.Items.Add("Num.:" + Convert.ToString(neurone2.GetNumero())
                    + " Poids:" + Convert.ToString(tabpoids[neurone.GetNumero(), neurone2.GetNumero()]));
            }

        }

        /*****************************************************************/
        // Calcul nécessaire pour backprop
        private double sommedelta(Neurone neur)
        {
            int i;
            Neurone neuronesucc;
            double somme;

            somme = 0;
            for (i = 0; i < neur.sorties.Count; i++)
            {
                neuronesucc = neur.sorties[i];
                somme = somme + tabpoids[neur.GetNumero(), neuronesucc.GetNumero()]
                                * neuronesucc.Getdelta();
            }
            return somme;
        }

    /******************************************************************/
    public void backprop( List<List<double>> lvecteursentrees,
                          List<double> lsortiesdesirees, double alpha, int nbiterations)
    {
        int i,j,k;
        double z;
        Neurone neur, neursucc ;
        int nbcouches = tabcouches.Length;
        Random rnd2 = new Random();

        // NbIte est le nombre d'itérations, cad le nombre d'exemples qu vont servir à l'apprentissage
        for (int nbfois=0; nbfois <nbiterations ; nbfois++)
        {
            for (int numexemple = 0; numexemple < lvecteursentrees.Count; numexemple++)
            {
                // les entrées sont affectées aux sorties des neurones de la couche 0
                for (i = 0; i < lvecteursentrees[0].Count; i++)
                    tabcouches[0][i].ImposeSortie(lvecteursentrees[numexemple][i]);

                // On impose ensuite une constante sur le dernier neurone d'entrée
                neur = tabcouches[0][lvecteursentrees[0].Count];
                neur.ImposeSortie(1);

                // Sortie souhaitée :
                z = lsortiesdesirees[numexemple];

                // Calcul de la sortie de chaque neurone, couche par couche
                for (k = 1; k < nbcouches; k++)
                    ProcessLayerK(k);

                // neur = tabcouches[nbcouches - 1][0]; // sortie du réseau

                // Calcul du gradient et de la modification de chaque poids
                //pour chaque neurone de sortie i faire; ici 1 seul neurone de sortie
                for (i = 0; i < tabcouches[nbcouches - 1].Count; i++)
                {
                    // ici 1 seul neurone de sortie, i varie entre 0 et 0 !
                    neur = tabcouches[nbcouches - 1][i];
                    neur.Setdelta( //(z - neur.sortie);
                    neur.gprime(neur.GetSomme()) * (z - neur.GetSortie()));
                }

                // On redescend vers les couches les plus basses
                for (k = nbcouches - 2; k > -1; k--)
                {
                    // Pour chaque neurone de cette couche, on met à jour les poids
                    for (j = 0; j < tabcouches[k].Count; j++)
                    {
                        neur = tabcouches[k][j];
                        neur.Setdelta(neur.gprime(neur.GetSomme()) * sommedelta(neur));
                        // Mise à jour des poids entre j et les neurones i d'arrivée
                        for (i = 0; i < tabcouches[k + 1].Count; i++)
                        {
                            neursucc = tabcouches[k + 1][i];
                            tabpoids[neur.GetNumero(), neursucc.GetNumero()] =
                              tabpoids[neur.GetNumero(), neursucc.GetNumero()]
                               + alpha * neur.GetSortie() * neursucc.Getdelta();
                        }
                    }
                }
            }
        }
    }


        /**********************************************************************/

        /***********************************************************************/
      public  List<double> ResultatsEnSortie (List<List<double>> lvecteursentrees )
      {
          int i,k;
          Neurone neur;

          List<double> lres = new List<double>();
          // On teste 200 exemples de x pris entre -100 et +100
          // En fait, x2 sera compris entre -100 et 100, x sera utilisé pour l'affichage entre 0 et 200
          for (int numexemple = 0; numexemple < lvecteursentrees.Count; numexemple++)
          {
            // les entrées sont affectées aux sorties des neurones de la couche 0
            for (i = 0; i < lvecteursentrees[0].Count; i++)
                tabcouches[0][i].ImposeSortie(lvecteursentrees[numexemple][i]);

            // On impose ensuite une constante sur le dernier neurone d'entrée
            neur = tabcouches[0][lvecteursentrees[0].Count];
            neur.ImposeSortie(1);

            // Calcul de la sortie de chaque neurone
            for (k = 1; k < tabcouches.Length; k++)
                ProcessLayerK(k);
            // Le neurone de sortie est le 1er de la dernière couche
            neur = tabcouches[tabcouches.Length - 1][0];
           
            lres.Add( neur.GetSortie() );
          }
          return lres;
       }
       
    }
}