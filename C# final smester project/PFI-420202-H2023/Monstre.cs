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
    // Il faut une classe Monstre qui implémente IProtagoniste de
    // manière telle que :
    //
    // * Teinte retourne la couleur rouge
    // * Nom et Symbole soient fixés à la construction
    // * Pos soit modifiable mais de manière privée
    // * AccepteContrôleManuel retourne false
    // * Déplacer modifie Pos avec la valeur reçue en paramètre
    // 
    // Monstre doit exposer un constructeur paramétrique acceptant
    // en paramètre un nom (string), un symbole (char) de même
    // qu'une position (Point) dans l'ordre, et initialise les
    // propriétés correspondantes
    // 
    // Monstre doit spécialiser ToString de manière telle que, pour
    // le code de test suivant :
    //
    // var m = new Monstre("Urg", new Point(3,7), 'U');
    // Console.Write(m);
    // 
    // ... soit affiché ceci :
    //
    // Monstre Urg à la position 3,7 qui a pour symbole U
    //

    public class Monstre : IProtagoniste
    {
        public ConsoleColor Teinte => ConsoleColor.Red;

        public string Nom { get; init; }

        public Point Pos { get; private set; }

        public char Symbole { get; init; }

        public bool AccepteContrôleManuel => false;

        public void Déplacer(Point p) => Pos = p;

        public Monstre(string nom, char symbole, Point pos)
        {
            Nom = nom;
            Symbole = symbole;
            Pos = pos;
        }

        public override string ToString()
            => $"Monstre {Nom} à la position {Pos} qui a pour symbole {Symbole}";
    }
}
