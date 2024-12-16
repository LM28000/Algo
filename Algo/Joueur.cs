namespace ClasseJoueur
{
    public class Joueur
    {
        public string name = "ia";
        public int score;
        public string[] ListeDeMot = new string[365];
        public int[] OccurenceMot = new int[365];
        public int compteur = 0;
        public bool ia = false;
        public int indice;

        public Joueur(string name)
        {
            this.name = name;
            this.score = 0;
        }

        public bool Contain(string mot)
        {
            for (int i = 0; i < ListeDeMot.Length; i++)
            {
                if (ListeDeMot[i] == mot)
                {
                    return true;
                }
            }
            return false;
        }

        public void Add_Mot(string mot)
        {
            for (int i = 0; i < ListeDeMot.Length; i++)
            {
                if (this.ListeDeMot[i] != mot)
                {
                    this.ListeDeMot[this.compteur] = mot;
                    this.OccurenceMot[this.compteur] = 1;
                }
                else
                {
                    this.OccurenceMot[i] = this.OccurenceMot[i] + 1;
                }
            }
            this.compteur++;
        }

        public void Score_Add(string mot)
        {
            this.score += 1;
        }

        public string toString()
        {
            string message = "";
            for (int i = 0; i < ListeDeMot.Length; i++)
            {
                if (this.OccurenceMot[i] != 0)
                {
                    message = message + this.ListeDeMot[i] + "; ";
                }
            }
            return "Le joueur " + this.name + " a un score de " + this.score + " et il a trouvé ces mots : " + message + ".";
        }

        public void AfficherMotsJoueurs()
        {
            for (int i = 0; i < ListeDeMot.Length; i++)
            {
                if (this.OccurenceMot[i] != 0)
                {
                    Console.WriteLine(this.ListeDeMot[i]);
                }
            }
        }
    }
}
