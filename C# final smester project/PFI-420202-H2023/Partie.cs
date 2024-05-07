using System.Text;
/// Ce fichier a été laissé par l'architecte qui affirme qu'il
/// est complet et fonctionnel. Vous ne devez pas le modifier de
/// quelque manière que ce soit.
/// 
/// par Patrice Roy et Philippe Simard, 2023
/// ------------------------------------------------------------
namespace PFI_420202_H2023
{
   class SymboleAbsentException : Exception { }
   class Partie : ISourceTeintes, IÉvaluateur
   {
      Dictionary<IProtagoniste, List<Trésor>> Possessions { get; } = new ();
      List<(IProtagoniste, Sortie)> Évadés { get; } = new ();
      List<(IProtagoniste, IProtagoniste)> Dévorés{ get; } = new ();
      void ÉvaluerTrésorerie()
      {
         // y a-t-il des gagnants d'un butin?
         for (int i = 0; i != Butin.Count;)
         {
            var trésor = Butin[i];
            var p = Populace.TrouverSi(p => p.Pos == trésor.Pos);
            if (p != null)
            {
               if (Possessions.ContainsKey(p))
                  Possessions[p].Add(trésor);
               else
                  Possessions.Add(p, new List<Trésor>() { trésor });
               Butin.RemoveAt(i);
            }
            else
               ++i;
         }
      }
      // retourne true s'il reste encore des personnages, faux sinon
      bool GérerFuites()
      {
         // y a-t-il une fuite?
         foreach (var sortie in Espoir)
         {
            var p = Populace.TrouverSi(p => p.Pos == sortie.Pos);
            if (p != null)
            {
               Populace.Supprimer(p);
               Évadés.Add((p, sortie));
               if (Populace.TrouverSi(p => p.AccepteContrôleManuel) == null)
                  return false;
            }
         }
         return true;
      }
      public Mode ÉvaluerSituation(Mode mode)
      {
         ÉvaluerTrésorerie();
         if (!GérerFuites())
            return Mode.Fini;
         // y a-t-il des morts?
         var monstres = Populace.Collecter(p => !p.AccepteContrôleManuel);
         var persos = Populace.Collecter(p => p.AccepteContrôleManuel);
         foreach (var m in monstres)
            for (int i = 0; i != persos.Count;)
               if (m.Pos == persos[i].Pos)
               {
                  Dévorés.Add((persos[i], m));
                  Populace.Supprimer(persos[i]);
                  persos.RemoveAt(i);
                  if (persos.Count == 0)
                     return Mode.Fini;
               }
               else
                  ++i;
         return mode;
      }

      Carte Lieu { get; init; }
      Population Populace { get; init; }
      List<Trésor> Butin { get; } = new ();
      List<Sortie> Espoir { get; } = new ();
      public string Nom { get; init; }

      public Point Déplacer(Point pos, Direction dir)
      {
         var nouvPos = Lieu.ProchainePosition(pos, dir);
         return Populace.EstLibre(nouvPos) ? nouvPos : pos;
      }
      public (IProtagoniste, Point, Point)
         Déplacer(IProtagoniste p, Direction dir) =>
            (p, p.Pos, p != null ? Déplacer(p.Pos, dir) : null);
      //
      // Les monstres ont un traitement particulier au sens où
      // ce sont des protagonistes qui peuvent en manger d'autres
      // (incluant d'autres monstres!)
      //
      public Point DéplacerMonstre(Point pos, Direction dir) =>
         Lieu.ProchainePosition(pos, dir);
      public (IProtagoniste, Point, Point)
         DéplacerMonstre(IProtagoniste p, Direction dir) =>
            (p, p.Pos, p != null ? DéplacerMonstre(p.Pos, dir) : null);

      public Point TrouverPositionLibre(Random r)
      {
         var pos = OutilsCarte.GénérerPositionValide(Lieu, r);
         while (!Populace.EstLibre(pos))
            pos = OutilsCarte.GénérerPositionValide(Lieu, r);
         return pos;
      }
      public Point TrouverBordureLibre(Random r)
      {
         var genPt = new Func<Random, Point>[]
         {
            (r) =>  // Droite
               new (Lieu.Largeur - 1, OutilsCarte.GénérerLigneValide(Lieu, r)),
            (r) => // Haut
               new (OutilsCarte.GénérerColonneValide(Lieu, r), 0),
            (r) => // Gauche
               new (0, OutilsCarte.GénérerLigneValide(Lieu, r)),
            (r) => // Bas
               new (OutilsCarte.GénérerColonneValide(Lieu, r), Lieu.Hauteur - 1)
         };
         const int NB_DIRECTIONS = 4;
         for(; ;)
         {
            Point pt = genPt[r.Next(0, NB_DIRECTIONS)](r);
            if (Populace.EstLibre(pt))
               return pt;
         }
      }
      public List<IProtagoniste> Collecter(Func<IProtagoniste, bool> pred) =>
         Populace.Collecter(pred);
      public IProtagoniste TrouverSi(Func<IProtagoniste, bool> pred) =>
         Populace.TrouverSi(pred);
      public Partie(string nom, int hauteur, int largeur)
      {
         Nom = nom;
         Lieu = new Carte(hauteur, largeur);
         Populace = new Population();
      }
      public void Ajouter(IProtagoniste p) => Populace.Ajouter(p);
      public void Ajouter(Trésor p)
      {
         Butin.Add(p);
      }
      public void Ajouter(Sortie p) => Espoir.Add(p);
      char TrouverSymbole(Point pt)
      {
         var p = Populace.TrouverSi(p => p.Pos == pt);
         if (p != null) return p.Symbole;
         var trésor = Algos.TrouverSi(Butin, p => p.Pos == pt);
         if (trésor != null) return trésor.Symbole;
         var sortie = Algos.TrouverSi(Espoir, p => p.Pos == pt);
         if (sortie != null) return sortie.Symbole;
         return default;
      }
      char TrouverSymbole(int x, int y) =>
         TrouverSymbole(new Point(x, y));
      public string Carte => CréerCarte();
      string CréerCarte()
      {
         int hauteur = Lieu.Hauteur;
         int largeur = Lieu.Largeur;
         string bordureHorizontale =
            $"+{new string('=', largeur)}+\n";
         StringBuilder sb = new();
         sb.Append(bordureHorizontale);
         for(int i = 0; i != hauteur; ++i)
         {
            sb.Append('|');
            for (int j = 0; j != largeur; ++j )
            {
               char symbole = TrouverSymbole(j, i);
               sb.Append(symbole == default ? ' ' : symbole);
            }
            sb.Append("|\n");
         }
         sb.Append(bordureHorizontale);
         return sb.ToString();
      }
      public string Présentation => CréerPrésentation();
      string CréerPrésentation()
      {
         StringBuilder sb = new();
         sb.Append($"{Nom} {Lieu.Hauteur} x {Lieu.Largeur} mettant en scène :\n");
         foreach (var p in Populace.Collecter(p => true))
            sb.Append($"* {p}\n");
         return sb.ToString();
      }
      public string Butins => CréerButins();
      string CréerButins()
      {
         StringBuilder sb = new();
         foreach (var trésor in Butin)
            sb.Append($"Butin : {trésor.Symbole} à la position {trésor.Pos} : {trésor.Nom}\n");
         return sb.ToString();
      }
      public string Espoirs => CréerEspoirs();
      string CréerEspoirs()
      {
         StringBuilder sb = new();
         foreach (var sortie in Espoir)
            sb.Append($"Sortie : {sortie.Symbole} à la position {sortie.Pos} : {sortie.Nom}\n");
         return sb.ToString();
      }
      string ConstruireRésultats()
      {
         StringBuilder sb = new();
         sb.Append("Partie terminée\n");
         foreach (var (p, trésor) in Possessions)
         {
            sb.Append($"{p.Nom} possède {trésor[0].Nom}");
            for (int i = 1; i < trésor.Count; ++i)
               sb.Append($", {trésor[i].Nom}");
            sb.Append('\n');
         }
         foreach (var (p, sortie) in Évadés)
            sb.Append($"{p.Nom} a fui par {sortie.Nom}\n");
         foreach (var (qui, parQuoi) in Dévorés)
            sb.Append($"{qui.Nom} a été dévoré par {parQuoi.Nom}\n");
         return sb.ToString();
      }
      public string Résultat => ConstruireRésultats();

      public override string ToString() =>
         $"{Présentation}{Carte}{Butins}{Espoirs}";
      public ConsoleColor TeinteDe(char c)
      {
         try
         {
            return Populace.TeinteDe(c);
         }
         catch(SymboleAbsentException)
         {
            var trésor = Algos.TrouverSi(Butin, p => p.Symbole == c);
            if (trésor != null)
               return trésor.Teinte;
            var sortie = Algos.TrouverSi(Espoir, p => p.Symbole == c);
            if (sortie != null)
               return sortie.Teinte;
         }
         throw new SymboleAbsentException();
      }
   }
}
