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
    // Il faut une classe Personnage qui implémente IProtagoniste de
    // manière telle que :
    //
    // * Teinte retourne la couleur verte
    // * Nom et Symbole soient fixés à la construction
    // * Pos soit modifiable mais de manière privée
    // * AccepteContrôleManuel retourne true
    // * Déplacer modifie Pos avec la valeur reçue en paramètre
    // 
    // Personnage doit exposer un constructeur paramétrique acceptant
    // en paramètre un nom (string), un symbole (char) de même qu'une
    // position (Point) dans l'ordre, et initialise les propriétés
    // correspondantes
    // 
    // Personnage doit spécialiser ToString de manière telle que, pour
    // le code de test suivant :
    //
    // var p = new Personnage("Hugues", new Point(3,7), 'H');
    // Console.Write(p);
    // 
    // ... soit affiché ceci :
    //
    // Personnage Hugues à la position 3,7 qui a pour symbole H
    //

    public class Personnage : IProtagoniste
    {
        public ConsoleColor Teinte => ConsoleColor.Green;

        public string Nom { get; init; }

        public Point Pos { get; private set; }

        public char Symbole { get; init; }

        public bool AccepteContrôleManuel => true;

        public void Déplacer(Point p) => Pos = p;
        public Personnage(string nom, char symbole, Point pos)
        {
            Nom = nom;
            Symbole = symbole;
            Pos = pos;
        }

        public override string ToString()
            => $"Personnage {Nom} à la position {Pos} qui a pour symbole {Symbole}";
    }

}
