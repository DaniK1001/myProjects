/// Ce fichier doit être complété par l'étudiante ou
/// l'étudiant tel que le demande l'architecte
/// 
/// Pierre-Étienne Brindle, 2023-05-09
/// -------------------------------------------------
namespace PFI_420202_H2023
{
   //
   // Notes de l'architecte:
   //
   // Il faut :
   //
   // * une interface nommée IProtagoniste, que les divers types de
   //   protagonistes du jeu implémenteront. Le contrat décrit par
   //   cette interface sera d'exposer:
   //
   //   * une propriété Teinte dont le get retournera un ConsoleColor
   //     (la couleur utilisée pour afficher le protagoniste)
   //
   //   * une propriété Nom dont le get retournera une string (le nom
   //     du protagoniste)
   //
   //   * une propriété Pos dont le get retournera un Point (la
   //     position du protagoniste)
   //
   //   * une propriété Symbole dont le get retournera un char (le
   //     symbole utilisé pour afficher le protagoniste)
   //
   //   * une méthode Déplacer, acceptant en paramètre un Point (la
   //     destination du déplacement) et ne retournant rien
   //
   //   * une propriété booléenne AccepteContrôleManuel, qui
   //     permettra de savoir s'il est possible de contrôler un
   //     protagoniste avec les touches du clavier ou pas
   //
   // (note : les classes qui implémenteront cette interface seront
   //         des protagonistes du jeu, pour le moment des instances
   //         de Personnage et des instances de Monstre)
   //

    public interface IProtagoniste : IAffichable
    {
        bool AccepteContrôleManuel { get; }
        void Déplacer(Point p);
    }

    public interface IAffichable
    {
        ConsoleColor Teinte { get; }
        string Nom { get; }
        Point Pos { get; }
        char Symbole { get; }
    }
}
