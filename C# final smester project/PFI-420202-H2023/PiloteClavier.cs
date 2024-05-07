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
    // Il faut une classe PiloteClavier exposant une méthode Lire.
    // Cette méthode :
    //
    // * prendra en paramètre un Mode (le mode courant du jeu)
    // * retournera un uplet fait d'une Direction et d'un Mode
    //
    // L'exécution de cette méthode ira comme suit :
    //
    // * lire une touche au clavier (note : Console.ReadKey serait
    //   sans doute utile ici)
    // * selon la touche lue, retourner une paire (Direction,Mode)
    //   correspondant au contenu de la table suivante :
    //
    // +--------------------+---------------------------------+
    // |     Touche lue     |        Valeur retournée         |
    // +--------------------+---------------------------------+
    // | Flèche droite      | (Direction.Droite, modeCourant) |
    // | Lettre D           |                                 |
    // +--------------------+---------------------------------+
    // | Flèche haut        | (Direction.Haut, modeCourant)   |
    // | Lettre W           |                                 |
    // +--------------------+---------------------------------+
    // | Flèche gauche      | (Direction.Gauche, modeCourant) |
    // | Lettre A           |                                 |
    // +--------------------+---------------------------------+
    // | Flèche bas         | (Direction.Bas, modeCourant)    |
    // | Lettre S           |                                 |
    // +--------------------+---------------------------------+
    // | Barre d'espacement | (Direction.Aucune, Mode.Pause)  |
    // +--------------------+---------------------------------+
    // | Échap              | (Direction.Aucune, Mode.Fini)   |
    // +--------------------+---------------------------------+
    // | Toute autre touche | (Direction.Aucune, modeCourant) |
    // +--------------------+---------------------------------+
    //
    // (note : il y a plusieurs manières de faire ceci)
    //

    public class PiloteClavier
    {
        public (Direction, Mode) Lire(Mode curr)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    return (Direction.Droite, curr);

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    return (Direction.Gauche, curr);

                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    return (Direction.Haut, curr);

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    return (Direction.Bas, curr);

                case ConsoleKey.Spacebar:
                    return (Direction.Aucune, Mode.Pause);

                case ConsoleKey.Escape:
                    return (Direction.Aucune, Mode.Fini);

                default:
                    return (Direction.Aucune, curr);
            }
        }
    }
}
