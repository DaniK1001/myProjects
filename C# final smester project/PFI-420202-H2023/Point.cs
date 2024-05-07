#nullable disable

///
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
    // Il faut une classe Point telle que :
    //
    // * elle expose des propriétés X et Y toutes deux de type int,
    //   et qui ne peuvent pas être modifiées après la construction
    // * elle expose un constructeur par défaut modélisant un Point
    //   à l'origine
    // * elle expose un constructeur paramétrique acceptant en
    //   paramètre des valeurs pour x et y, et initialise les
    //   propriétés en conséquence
    // * elle spécialise ToString de manière à ce que le code
    //   suivant :
    //
    // Console.Write(new Point(3,7));
    //
    //   ... affiche 3,7
    //
    // * elle implémente IEquatable<Point>
    // * elle implémente les opérateurs == et != prenant en paramètre
    //   deux instances de Point
    //
    // (note : deux Point seront équivalents s'ils ont la même valeur
    //         pour leurs X respectifs et s'il en va de même pour
    //         leurs Y)
    //

    public class Point : IEquatable<Point>
    {

        public int X { get; init; }
        public int Y { get; init; }

        public Point() : this(0, 0) {}

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point other)
            => !(other is null) && other.X == X && other.Y == Y;

        public override bool Equals(object obj)
        {
            if(obj == null && !(obj is Point)) 
                return false;

            return Equals((Point)obj);
        }

        public override int GetHashCode() => base.GetHashCode();

        //Légèrement, très légèrement, insipiré de Vector2 de Unity
        public static Point Haut = new Point(0, -1);
        public static Point Bas = new Point(0, 1);
        public static Point Gauche = new Point(-1, 0);
        public static Point Droite = new Point(1, 0);
        public static Point Zero = new Point(0, 0);

        public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

        public static bool operator ==(Point a, Point b)
        {
            if(!(a is null))
                return a.Equals(b);

            return b is null;
        }
        public static bool operator !=(Point a, Point b) => !(a == b);

        public override string ToString()
            => $"{X},{Y}";
    }
}
