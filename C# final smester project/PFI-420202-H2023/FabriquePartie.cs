using static PFI_420202_H2023.AlgosTexte;
#nullable disable

/// Ce fichier a été laissé par l'architecte qui affirme qu'il
/// est complet et fonctionnel. Vous ne devez pas le modifier de
/// quelque manière que ce soit.
/// 
/// par Patrice Roy et Philippe Simard, 2023
/// ------------------------------------------------------------

namespace PFI_420202_H2023
{
   class PartieMalforméeException : Exception { }
   class PersonnageMalforméException : Exception { }
   class MonstreMalforméException : Exception { }
   class ChaîneMalforméeException : Exception { }
   class TrésorMalforméException : Exception { }
   class SortieMalforméeException : Exception { }
   static class FabriquePartie
   {
      // précondition : pos < s.Length
      // note : ne supporte pas les guillemets imbriqués
      static string ExtraireJeton(string s, int pos)
      {
         int fin;
         if (s[pos] == '\"')
         {
            fin = TrouverSi(s, pos + 1, c => c == '\"');
            if (fin == -1)
               throw new ChaîneMalforméeException();
            ++fin; // pour conserver le '\"' fermant
         }
         else
         {
            fin = TrouverSi(s, pos + 1, EstBlanc);
            if (fin == -1)
               fin = s.Length;
         }
         return s.Substring(pos, fin - pos);
      }
      static string[] Segmenter(string s)
      {
         List<string> segments = new();

         int pos = TrouverSi(s, c => !EstBlanc(c));
         while (pos != -1)
         {
            var jeton = ExtraireJeton(s, pos);
            segments.Add(jeton);
            pos = TrouverSi(s, pos + jeton.Length, c => !EstBlanc(c));
         }
         return segments.ToArray();
      }
      static Personnage CréerPersonnage(string s, Partie partie, Random r)
      {
         // personnage symbole nom
         var segments = Segmenter(s);
         if (segments[0].Length != 1)
            throw new PersonnageMalforméException();
         return new (segments[1], segments[0][0], partie.TrouverPositionLibre(r));
      }
      static Monstre CréerMonstre(string s, Partie partie, Random r)
      {
         // personnage symbole nom
         var segments = Segmenter(s);
         if (segments[0].Length != 1)
            throw new MonstreMalforméException();
         return new (segments[1], segments[0][0], partie.TrouverPositionLibre(r));
      }
      static Trésor CréerTrésor(string s, Partie partie, Random r)
      {
         // trésor symbole nom
         var segments = Segmenter(s);
         if (segments[0] != "trésor") return null;
         segments = Segmenter(Combiner(segments, 1, segments.Length));
         if (segments[0].Length != 1)
            throw new TrésorMalforméException();
         return new (segments[1], segments[0][0], partie.TrouverPositionLibre(r));
      }
      static Sortie CréerSortie(string s, Partie partie, Random r)
      {
         // sortie symbole nom
         var segments = Segmenter(s);
         if (segments[0] != "sortie") return null;
         segments = Segmenter(Combiner(segments, 1, segments.Length));
         if (segments[0].Length != 1)
            throw new SortieMalforméeException();
         return new (segments[1], segments[0][0], partie.TrouverBordureLibre(r));
      }
      static IProtagoniste CréerProtagoniste(string s, Partie partie, Random r)
      {
         // format variable selon le préfixe
         var segments = Segmenter(s);
         return segments[0] switch
         {
            "personnage" =>
               CréerPersonnage(Combiner(segments, 1, segments.Length), partie, r),
            "monstre" =>
               CréerMonstre(Combiner(segments, 1, segments.Length), partie, r),
            _ => null
         };
      }
      public static Partie CréerPartie(string nomFichier)
      {
         Random dé = new();
         using (var sr = new StreamReader(nomFichier))
         {
            // partie nom hau lar
            var segments = Segmenter(sr.ReadLine());
            if (segments[0] != "partie")
               throw new PartieMalforméeException();
            var nom = segments[1];
            var hauteur = int.Parse(segments[2]);
            var largeur = int.Parse(segments[3]);
            var partie = new Partie(nom, hauteur, largeur);
            for (string ligne = sr.ReadLine(); ligne != null; ligne = sr.ReadLine())
            {
               var prota = CréerProtagoniste(ligne.Trim(), partie, dé);
               if (prota != null)
                  partie.Ajouter(prota);
               else
               {
                  // sortie ou trésor?
                  var trésor = CréerTrésor(ligne.Trim(), partie, dé);
                  if (trésor != null)
                     partie.Ajouter(trésor);
                  else
                  {
                     var sortie = CréerSortie(ligne.Trim(), partie, dé);
                     if (sortie != null)
                        partie.Ajouter(sortie);
                  }
               }
            }
            return partie;
         }
      }
   }
}
