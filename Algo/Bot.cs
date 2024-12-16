using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Dico;

namespace BOTIA
{
    internal class Program
    {
         private Dictionnaire dictionnaire = new Dictionnaire();
        private Plateau plateau = new Plateau(4);
        
        private string Find() 
        {
            dictionnaire.contenu = dictionnaire.LireDictionnaire("MotsPossiblesFR.txt");
            int dictionnaire_nb = dictionnaire.contenu.Count;
            for (int i = 0; i <= dictionnaire_nb; i++) 
            {
                if (plateau.Test_Plateau(dictionnaire.contenu[i]) == true)
                {

                }
            }
        }

        static void Main(string[] args)
        {
        }
    }
}
