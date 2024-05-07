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
    // Il faut une classe Trésor telle que :
    //
    // * elle expose une propriété Teinte de type ConsoleColor qui
    //   est non-modifiable et retourne la couleur jaune
    // * elle expose une propriété Nom de type string (le nom du
    //   trésor) qui peut être consultée publiquement mais ne peut
    //   être modifiée qu'à la construction
    // * elle expose une propriété Symbole de type char (le symbole
    //   affichable du trésor) qui peut être consultée publiquement
    //   mais ne peut être modifiée qu'à la construction
    // * elle expose une propriété Pos de type Point (la position
    //   du trésor), qui soit modifiable mais de manière privée
    // 
    // Trésor doit exposer un constructeur paramétrique acceptant
    // en paramètre un nom (string), un symbole (char) de même
    // qu'une position (Point) dans l'ordre, et initialise les
    // propriétés correspondantes
    // 
    // Trésor doit spécialiser ToString de manière telle que, pour
    // le code de test suivant :
    //
    // var p = new Trésor("Cennes", new Point(3,7), '$');
    // Console.Write(p);
    // 
    // ... soit affiché ceci :
    //
    // Trésor Cennes à la position 3,7 qui a pour symbole $
    //

    public class Trésor : IAffichable
    {
        public ConsoleColor Teinte => ConsoleColor.Yellow;

        public string Nom { get; init; }

        //Comme pour Sortie, je sais pas pourquoi vous voulez private set
        public Point Pos { get; private set; }

        public char Symbole { get; init; }

        public Trésor(string nom, char symbole, Point pos)
        {
            Nom = nom;
            Pos = pos;
            Symbole = symbole;
        }

        public override string ToString()
            => $"Trésor {Nom} à la position {Pos} qui a pour symbole {Symbole}";
    }
}
