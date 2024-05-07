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
    // Il faut compléter la classe Population ci-dessous de manière
    // telle que :
    //
    // * elle permette de "contenir" des références sur autant de
    //   IProtagoniste que nécessaire
    // * elle expose une propriété boolénne EstVide dont le get
    //   retournera true seulement si elle ne "contient" pas de
    //   IProtagoniste
    // * elle expose une méthode EstLibre acceptant en paramètre
    //   un Point et ne retournant true que si aucun des
    //   IProtagoniste qu'elle "contient" ne se trouve à cette
    //   position
    // * elle expose une méthode Ajouter acceptant un IProtagoniste
    //   en paramètre et l'ajoutant aux IProtagoniste qu'elle
    //   "contient"
    // * elle expose une méthode Supprimer acceptant un IProtagoniste
    //   en paramètre et le retirant des IProtagoniste qu'elle
    //   "contient"
    //
    // (note : j'ai commencé cette classe mais le temps m'a
    //         malheureusement manqué pour la compléter; je
    //         vous remercie pour votre aide!)
    //
    class Population : ISourceTeintes
    {
        public List<IProtagoniste> Collecter(Func<IProtagoniste, bool> pred) =>
           Algos.Collecter(Protagonistes, pred);
        public IProtagoniste TrouverSi(Func<IProtagoniste, bool> pred) =>
           Algos.TrouverSi(Protagonistes, pred);
        public ConsoleColor TeinteDe(char symbole)
        {
            var prota = TrouverSi(p => p.Symbole == symbole);
            return prota != null ? prota.Teinte : throw new SymboleAbsentException();
        }

        //La liste ne sera jamais null
        public Population() => Protagonistes = new List<IProtagoniste>();
        public List<IProtagoniste> Protagonistes { get; init; }
        public bool EstVide => Protagonistes.Count == 0;

        public bool EstLibre(Point p)
        => !Algos.EstDans(Protagonistes.Select((prota) => prota.Pos), p);

        public void Ajouter(IProtagoniste p)
        {
            //On ne peut pas mettre un protagoniste qui est déjà présent et
            //on ne peut pas ajouter un protagoniste au mêne endroit qu'un autre
            if (p != null && EstLibre(p.Pos) && !Algos.EstDans(Protagonistes, p))
                Protagonistes.Add(p);
        }

        public void Supprimer(IProtagoniste prota)
            => Protagonistes.Remove(prota);

    }
}
