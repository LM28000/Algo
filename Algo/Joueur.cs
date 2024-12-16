namespace ClasseJoueur
{
    public class Joueur
    {
        /// <summary>
        /// Nom du joueur.
        /// </summary>
        public string name = "ia";

        /// <summary>
        /// Score du joueur.
        /// </summary>
        public int score;

        /// <summary>
        /// Liste des mots trouvés par le joueur.
        /// </summary>
        public string[] ListeDeMot = new string[365];

        /// <summary>
        /// Occurrences des mots trouvés par le joueur.
        /// </summary>
        public int[] OccurenceMot = new int[365];

        /// <summary>
        /// Compteur de mots trouvés.
        /// </summary>
        public int compteur = 0;

        /// <summary>
        /// Indique si le joueur est une IA.
        /// </summary>
        public bool ia = false;

        /// <summary>
        /// Indice du joueur.
        /// </summary>
        public int indice;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Joueur"/> avec un nom spécifié.
        /// </summary>
        /// <param name="name">Nom du joueur.</param>
        public Joueur(string name)
        {
            this.name = name;
            this.score = 0;
        }

        /// <summary>
        /// Vérifie si un mot est contenu dans la liste des mots trouvés par le joueur.
        /// </summary>
        /// <param name="mot">Le mot à vérifier.</param>
        /// <returns>True si le mot est trouvé, sinon False.</returns>
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

        /// <summary>
        /// Ajoute un mot à la liste des mots trouvés par le joueur.
        /// </summary>
        /// <param name="mot">Le mot à ajouter.</param>
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

        /// <summary>
        /// Ajoute un point au score du joueur.
        /// </summary>
        /// <param name="mot">Le mot trouvé (non utilisé dans cette méthode).</param>
        public void Score_Add(string mot)
        {
            this.score += 1;
        }

        /// <summary>
        /// Retourne une chaîne de caractères représentant l'état du joueur.
        /// </summary>
        /// <returns>Une chaîne de caractères décrivant le joueur.</returns>
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

        /// <summary>
        /// Affiche les mots trouvés par le joueur.
        /// </summary>
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
