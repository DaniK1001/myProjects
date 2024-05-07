#nullable disable
/// Ce fichier doit être complété par l'étudiante ou
/// l'étudiant tel que le demande l'architecte
/// 
/// par Pierre-Étienne Brinde, 2023-05-02
/// -------------------------------------------------
namespace PFI_420202_H2023
{
    static class Algos
    {
        //
        // Notes de l'architecte:
        //
        // Il faut :
        //
        // * une méthode de classe TrouverSi acceptant en paramètre
        //   un IEnumerable<T> et un prédicat applicable à un T, et
        //   retournant une référence sur le premier T dans la
        //   séquence parcourue pour lequel le prédicat s'avère.
        // * si aucun T ne respectant le prédicat n'est trouvé, cette
        //   méthode doit retourner la valeur par défaut d'un T
        public static T TrouverSi<T>(this IEnumerable<T> list, Func<T, bool> predicat)
        => list.FirstOrDefault(predicat);

        //
        // * une méthode de classe Collecter acceptant en paramètre
        //   un IEnumerable<T> et un prédicat applicable à un T, et
        //   retournant une List<T> (potentiellement vide) contenant
        //   des références sur tous les T respectant le prédicat
        //   dans la séquence parcourue
        public static List<T> Collecter<T>(IEnumerable<T> list, Func<T, bool> predicat)
        => list.Where(predicat).ToList();

        //
        // * une méthode de classe EstDans acceptant en paramètre un
        //   IEnumerable<T> et un élément de type T, et retournant
        //   true seulement si l'élément est dans l'énumérable
        public static bool EstDans<T>(IEnumerable<T> list, T element)
            => list.Contains(element);
        //
        // * une méthode de classe EstEntreInclusif acceptant en
        //   paramètre trois T (une valeur, une borne min et une
        //   borne max), et retournant true seulement si la valeur
        //   est dans [min,max]. Note : exigez que T implémente le
        //   contrat IComparable<T>

        public static bool EstEntreInclusif<T>(T valeur, T min, T max) where T : IComparable<T>
        => valeur.CompareTo(min) >= 0 && valeur.CompareTo(max) <= 0;
        //
        // * une méthode de classe EstEntreDemiOuvert acceptant en
        //   paramètre trois T (une valeur, une borne min et une
        //   borne max), et retournant true seulement si la valeur
        //   est dans [min,max). Note : exigez que T implémente le
        //   contrat IComparable<T>
        //
        public static bool EstEntreDemiOuvert<T>(T valeur, T min, T max) where T : IComparable<T>
         => valeur.CompareTo(min) >= 0 && valeur.CompareTo(max) < 0;
        
    }
}
