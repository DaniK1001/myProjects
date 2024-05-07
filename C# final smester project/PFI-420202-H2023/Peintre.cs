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
    // Il faut une interface ISourceTeintes. Cette interface doit
    // exposer une seule méthode, TeinteDe, acceptant un char en
    // paramètre et retournant un ConsoleColor.
    //
    // (note : les classes qui implémenteront cette interface pourront
    //         retrouver la couleur à afficher pour un symbole donné;
    //         c'est entre autres le cas de la classe Partie)
    public interface ISourceTeintes
    {
        ConsoleColor TeinteDe(char c);
    }

    //
    // Il faut aussi compléter la classe Peintre ci-dessous de manière
    // à ce que :
    //
    // * elle expose une méthode MarquerInaccessible qui prendra en
    //   paramètre une string, et retournera true si cette string ne
    //   débute pas par le texte "* Personnage" (note : ceci a pour
    //   but de montrer clairement les personnages qu'il est possible
    //   de contrôler manuellement)
    //
    // * elle expose une méthode générique Peindre<T> acceptant en
    //   paramètre un T (l'élément à afficher) et un ConsoleColor (la
    //   couleur à utiliser pour l'affichage). Cette méthode devra
    //   s'assurer d'afficher l'élément avec la couleur demandée, et
    //   de remettre en place la couleur d'affichage originale par la
    //   suite (pour que les affichages subséquents ne soient pas
    //   affectés par le changement de couleur). Autrement dit, si la
    //   couleur d'affichage à la base est grise et qu'un appel à
    //   Peindre("allo", ConsoleColor.Green) est fait, alors "allo"
    //   devrait s'afficher en vert, mais les affichages qui suivront
    //   cet appel devraient se faire en gris
    //
    class Peintre // Note de l'architecte : je n'ai pas eu le temps de
                  // terminer cette classe; j'ai besoin de votre aide
    {
        public void PeindreMenu(string menu, Partie source)
        {
            var lignes = menu.Split
            (
               new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries
            );
            foreach (var ligne in lignes)
                Peindre($"{ligne}\n", MarquerInaccessible(ligne) ? ConsoleColor.Gray : ConsoleColor.White);
        }
        public void Peindre(string surface, ISourceTeintes source)
        {
            foreach (char c in surface)
                Peindre(c, Carte.EstMur(c) || Carte.EstVide(c) ?
                   Console.ForegroundColor : source.TeinteDe(c));
        }

        public void Peindre<T>(T element, ConsoleColor nouvelle)
        {
            Console.ForegroundColor = nouvelle;
            Console.Write(element);
            Console.ResetColor();
        }

        public bool MarquerInaccessible(string str)
        {
            const string MARQUEUR = "* Personnage";

            return str.IndexOf(MARQUEUR) != 0;
        }
    }
}
