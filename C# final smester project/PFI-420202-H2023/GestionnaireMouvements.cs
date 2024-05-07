/// Ce fichier a été laissé par l'architecte qui affirme qu'il
/// est complet et fonctionnel. Vous ne devez pas le modifier de
/// quelque manière que ce soit.
/// 
/// par Patrice Roy et Philippe Simard, 2023
/// ------------------------------------------------------------
namespace PFI_420202_H2023
{
   class GestionnaireMouvements
   {
      List<(IProtagoniste p, Point avant, Point après)> Dépl { get; } = new();
      public void Ajouter((IProtagoniste p, Point avant, Point après) mouvement)
      {
         lock (Dépl)
            Dépl.Add(mouvement);
      }
      public (bool, Mode) Appliquer(IÉvaluateur évaluateur, Mode modeCourant)
      {
         lock (Dépl)
         {
            if (Dépl.Count == 0)
               return (false, évaluateur.ÉvaluerSituation(modeCourant));
            foreach (var prota in Dépl)
               prota.p.Déplacer(prota.après);
            Dépl.Clear();
            return (true, évaluateur.ÉvaluerSituation(modeCourant));
         }
      }
   }
}
