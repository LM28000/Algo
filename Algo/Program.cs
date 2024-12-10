namespace Algo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Plateau plateau = new Plateau(4);
            plateau.toString();
            string mot = Console.ReadLine();

            bool estPresent = plateau.Test_Plateau(mot);
            Console.WriteLine($"Le mot '{mot}' est {(estPresent ? "présent" : "absent")} sur le plateau.");
        
        }
    }
}