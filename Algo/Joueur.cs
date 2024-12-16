namespace ClasseJoueur
{
    internal class Joueur
    {
        // Nom du joueur, par défaut "ia"
        public string name = "ia";

        // Score du joueur
        public int score;

        // Liste des mots trouvés par le joueur
        public string[] ListeDeMot = new string[365];

        // Occurrences de chaque mot trouvé
        public int[] OccurenceMot = new int[365];

        // Compteur de mots trouvés
        public int compteur = 0;

        // Indique si le joueur est une IA
        public bool ia = false;

        // Indice utilisé pour des opérations internes
        public int indice;

        // Constructeur qui initialise le nom du joueur et son score
        public Joueur(string name)
        {
            this.name = name;
            this.score = 0;
        }

        // Méthode pour vérifier si un mot est déjà dans la liste
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

        // Méthode pour ajouter un mot à la liste
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

        // Méthode pour ajouter un score basé sur un mot
        public void Score_Add(string mot)
        {
            // Ajouter le score du mot au score du joueur
            this.score += 1; // A changer!!!
        }

        // Méthode pour obtenir une représentation en chaîne de caractères de l'objet
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

        // Méthode pour afficher les mots trouvés par le joueur
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
