/// Ce fichier doit être complété par l'étudiante ou
/// l'étudiant tel que le demande l'architecte
/// 
/// par Pierre-Étienne Brindle, 2023-05-02
/// -------------------------------------------------
namespace PFI_420202_H2023
{
   //
   // Notes de l'architecte:
   //
   // Il faut une classe Sortie telle que :
   //
   // * elle expose une propriété Teinte de type ConsoleColor qui
   //   est non-modifiable et retourne la couleur cyan
   // * elle expose une propriété Nom de type string (le nom de la
   //   sortie) qui peut être consultée publiquement mais ne peut
   //   être modifiée qu'à la construction
   // * elle expose une propriété Symbole de type char (le symbole
   //   affichable de la sortie) qui peut être consultée
   //   publiquement mais ne peut être modifiée qu'à la construction
   // * elle expose une propriété Pos de type Point (la position de
   //   la sortie), qui soit modifiable mais de manière privée
   // 
   // Sortie doit exposer un constructeur paramétrique acceptant
   // en paramètre un nom (string), un symbole (char) de même
   // qu'une position (Point) dans l'ordre, et initialise les
   // propriétés correspondantes
   // 
   // Sortie doit spécialiser ToString de manière telle que, pour
   // le code de test suivant :
   //
   // var p = new Sortie("Là", new Point(3,7), '@');
   // Console.Write(p);
   // 
   // ... soit affiché ceci :
   //
   // Sortie Là à la position 3,7 qui a pour symbole @
   //

    public class Sortie : IAffichable
    {
        public ConsoleColor Teinte => ConsoleColor.Cyan;

        public string Nom { get; init; }

        //Je me demande toujours pourquoi vous vouliez qu'elle soit modifiable de manière privée 
        public Point Pos { get; private set; }

        public char Symbole { get; init; }

        public Sortie(string nom, char symbole, Point pos)
        {
            Nom = nom;
            Symbole = symbole;
            Pos = pos;
        }

        public override string ToString()
            => $"Sortie {Nom} à la position {Pos} qui a pour symbole {Symbole}";
    }
}
