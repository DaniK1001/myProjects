using System.Text;

/// Ce fichier doit être complété par l'étudiante ou
/// l'étudiant tel que le demande l'architecte
/// 
/// par Pierre-Étienne Brindle, 2023-05-02
/// -------------------------------------------------
namespace PFI_420202_H2023
{

    static class AlgosTexte
    {
        //
        // Notes de l'architecte:
        //
        // Il faut :
        //
        // * une méthode de classe TrouverSi acceptant en paramètre
        //   une string, un indice de début et un prédicat applicable
        //   à un char, et retournant l'indice du premier char dans
        //   la string parcourue (à partir de l'indice de début) pour
        //   lequel le prédicat s'avère.


        // * si aucun char ne respectant le prédicat n'est trouvé,
        //   cette méthode doit retourner -1
        public static int TrouverSi(string str, int indexDébut, Func<char, bool> predicat)
        {
            if (!Algos.EstEntreDemiOuvert(indexDébut, 0, str.Length))
                return -1;

            int diffIndex = TrouverSi(str.Substring(indexDébut), predicat) ;

            return diffIndex == -1 ? diffIndex : indexDébut + diffIndex;
        }

        //
        // * une méthode de classe TrouverSi acceptant en paramètre
        //   une string, et un prédicat applicable à un char, et
        //   retournant l'indice du premier char dans la string
        //   parcourue pour lequel le prédicat s'avère.
        // * si aucun char ne respectant le prédicat n'est trouvé,
        //   cette méthode doit retourner -1
        public static int TrouverSi(string str, Func<char, bool> predicat)
        {
            try { return str.IndexOf(str.First(predicat)); }
            catch (InvalidOperationException) { return -1; }
        }
        //
        // * une méthode de classe EstBlanc acceptant en paramètre
        //   un char et retournant true seulement si ce char est un
        //   blanc (note : il existe une méthode de classe dans le
        //   type char qui peut être utile ici)
        public static bool EstBlanc(char c) => char.IsWhiteSpace(c);
        //
        // * une méthode de classe Combiner acceptant en paramètre
        //   un tableau de string et une paire d'indices [debut,fin)
        //   représentant un intervalle à demi ouvert, et retournant
        //   une string concaténant toutes les string du tableau
        //   dans l'intervalle indiqué, insérant un espace après
        //   chacune. Par exemple, ce qui suit doit s'avérer:
        //
        public static string Combiner(string[] strs, int indexDébut, int indexFin)
        {                                      
            string result = string.Join(" ", strs.Take(indexDébut..(indexFin + 1)));

            return string.IsNullOrEmpty(result) ? "" : result + " "; 
        }

        // var s = Combiner
        //         (
        //            new []{ "moi", "J'aime", "mon", "prof" }, 1, 4
        //         );
        // s == "J'aime mon prof ";
        //
    }
}
