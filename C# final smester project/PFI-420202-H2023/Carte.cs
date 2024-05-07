/// Ce fichier doit être complété par l'étudiante ou
/// l'étudiant tel que le demande l'architecte
/// 
/// par Pierre-Étienne Brindle, 2023-05-02
/// -------------------------------------------------
namespace PFI_420202_H2023
{
    public class DimensionCarteInvalideException : Exception { }
    class Carte
    {
        //
        // Notes de l'architecte:
        //
        // Il faut :
        //
        // * Une méthode de classe EstMur acceptant en paramètre un
        //   char et retournant true seulement si ce char représente
        //   un mur (note : les symboles pouvant jouer le rôle de mur
        //   sont '+', '|' et '=')
        public static bool EstMur(char c)
        {
            const string MURS = "+|=";
            return Algos.EstDans(MURS, c);
        }
        //
        // * Une méthode de classe EstVide acceptant en paramètre un
        //   char et retournant true seulement si ce char représente
        //   un espace vide (note : un espace vide est un blanc)
        public static bool EstVide(char c) => AlgosTexte.EstBlanc(c);
        //
        // * Une propriété d'instance Hauteur représentant la hauteur
        //   de la carte. Cette valeur est fixée à la construction de
        //   la carte et doit être un entier strictement positif

        int hauteur;
        public int Hauteur
        {
            get => hauteur;
            init
            {
                if (!EstPositif(value))
                    throw new DimensionCarteInvalideException(); 

                hauteur = value;
            }
        }
        //
        // * Une propriété d'instance Largeur représentant la largeur
        //   de la carte. Cette valeur est fixée à la construction de
        //   la carte et doit être un entier strictement positif
        int largeur;
        public int Largeur
        {
            get => largeur;
            init
            {
                if (!EstPositif(value))
                    throw new DimensionCarteInvalideException();

                largeur = value;
            }
        }

        private static bool EstPositif(int x) => x > 0;
        //
        // * Un constructeur de Carte prenant en paramètre la hauteur
        //   et la largeur souhaitées, dans l'ordre
        public Carte(int hauteur, int largeur)
        {
            Hauteur = hauteur;
            Largeur = largeur;
        }
        //
        // * Une méthode d'instance EstLigneValide, acceptant en
        //   paramètre un entier (la ligne à valider) et retournant
        //   true seulement si cette ligne est dans [0,Hauteur)
        public bool EstLigneValide(int ligne) => Algos.EstEntreDemiOuvert(ligne, 0, hauteur);

        //
        // * Une méthode d'instance EstColonneValide, acceptant en
        //   paramètre un entier (la colonne à valider) et retournant
        //   true seulement si cette colonne est dans [0,Largeur)
        public bool EstColonneValide(int colonne) => Algos.EstEntreDemiOuvert(colonne, 0, largeur);
        //
        // * Une méthode d'instance EstValide acceptant en paramètre
        //   un Point et retournant true seulement si ce point est
        //   dans la carte, donc si le X est une colonne valide et le
        //   Y est une ligne valide
        public bool EstValide(Point p) => EstColonneValide(p.X) && EstLigneValide(p.Y);
        //
        // * Une méthode d'instance ProchainePosition acceptant en
        //   paramètre un Point et une Direction, et créant le Point
        //   voisin de celui reçu en paramètre dans la direction
        //   demandée. Ainsi le Point à droite du Point X=3,Y=7 est
        //   un Point X=4,Y=7. Si ce nouveau Point est valide au sens
        //   de la méthode EstValide, alors retournez-le, sinon
        //   retournez le Point reçu en paramètre tout simplement
        public Point ProchainePosition(Point p, Direction d)
        {
            Point nouveau = p + CalculerDifférenceDePosition(d);
            return EstValide(nouveau) ? nouveau : p;
        }

        private static Point CalculerDifférenceDePosition(Direction d)
        {
            switch (d)
            {
                case Direction.Haut:
                    return Point.Haut;

                case Direction.Bas:
                    return Point.Bas;

                case Direction.Droite:
                    return Point.Droite;

                case Direction.Gauche:
                    return Point.Gauche;

                default:
                    return Point.Zero;
            }
        }
    }
    static class OutilsCarte
    {
        //
        // Notes de l'architecte:
        //
        // Il faut :
        //
        // * Une méthode de classe GénérerLigneValide acceptant en
        //   paramètre une Carte et un Random, et retournant un
        //   numéro de ligne valide, donc une valeur située dans
        //   l'intervalle [0..carte.Hauteur)
        public static int GénérerLigneValide(Carte c, Random rng)
            => rng.Next(0, c.Hauteur);
        // 
        // * Une méthode de classe GénérerColonneValide acceptant en
        //   paramètre une Carte et un Random, et retournant un
        //   numéro de colonne valide, donc une valeur située dans
        //   l'intervalle [0..carte.Largeur)
        public static int GénérerColonneValide(Carte c, Random rng)
            => rng.Next(0, c.Largeur);
        //
        // * Une méthode de classe GénérerPositionValide acceptant en
        //   paramètre une Carte et un Random, et retournant un Point
        //   dont le X et le Y sont choisis de manière pseudoaléatoire
        public static Point GénérerPositionValide(Carte c, Random rng)
            => new Point(GénérerColonneValide(c, rng), GénérerLigneValide(c, rng));
    }
}
