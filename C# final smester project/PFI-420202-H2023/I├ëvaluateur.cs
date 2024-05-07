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
   // Il faut :
   //
   // * une interface nommée IÉvaluateur, exposant une seule méthode
   //   nommée ÉvaluerSituation. Cette méthode doit accepter en
   //   paramètre un Mode et retourner un Mode
   //
   // (note : les classes qui implémenteront cette interface seront
   //         capables d'influencer le mode d'exécution du jeu en
   //         prenant une décision sur la base des événements; la
   //         classe Partie en est un exemple)
   //

    public interface IÉvaluateur
    {
        Mode ÉvaluerSituation(Mode m);
    }
}
