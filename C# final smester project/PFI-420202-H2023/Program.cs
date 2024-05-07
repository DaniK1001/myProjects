using PFI_420202_H2023;
#nullable disable
///
/// Ce fichier a été laissé par l'architecte qui affirme qu'il
/// est complet et fonctionnel. Vous ne devez pas le modifier de
/// quelque manière que ce soit.
/// 
/// par Patrice Roy et Philippe Simard, 2023 (teachers)Jj 
/// ------------------------------------------------------------
/// 

Console.CursorVisible = false;
foreach (var nomFichier in args)
{
   Peintre peintre = new ();
   var partie = FabriquePartie.CréerPartie(nomFichier);
   Orchestrateur orchestrateur = new ();
   while (!orchestrateur.Fini)
      orchestrateur.Orchestrer(partie, peintre);
   Console.WriteLine(partie.Résultat);
   Console.Write("Pressez une touche pour continuer");
   Console.ReadKey(true);
}

class Orchestrateur
{
   Mode ModeCourant { get; set; } = Mode.Pause;
   IProtagoniste ProtagonisteCourant { get; set; } = null;
   // paramètre partie inutilisé pour le moment
   void MenuOptions(Partie partie)
   {
      Console.WriteLine("<espace> pour pause | jeu, <symbole> pour prise en charge");
   }
         
   void MenuPause(Partie partie, Peintre peintre)
   {
      Console.Clear();
      peintre.PeindreMenu($"{partie.Présentation}{partie.Butins}{partie.Espoirs}", partie);
      MenuOptions(partie);
      var k = Console.ReadKey(true);
      switch(k.Key)
      {
         case ConsoleKey.Escape: // fin de partie
            ModeCourant = Mode.Fini;
            break;
         case ConsoleKey.Spacebar:
            ModeCourant = Mode.Action;
            break;
         default:
            var prota = partie.TrouverSi(p => p.Symbole == k.KeyChar);
            if(prota != null && prota.AccepteContrôleManuel)
            {
               ProtagonisteCourant = prota;
               ModeCourant = Mode.Action;
            }
            break;
      }

   }
   void JeuNormal(Partie partie, Peintre peintre)
   {
      const int NB_DIRECTIONS = 4;
      GestionnaireMouvements gesMouvements = new();
      Thread[] fils = new []
      {
         // monstres
         new Thread(() =>
         {
            bool DoitDéplacer(Random r) => r.Next() % 4 == 0;
            Direction ChoisirDirection(Random r) => (Direction) (r.Next() % NB_DIRECTIONS);
            Random r = new();
            while (ModeCourant == Mode.Action)
            {
               var monstres = partie.Collecter(p => !p.AccepteContrôleManuel);
               foreach(var p in monstres)
                  if(DoitDéplacer(r))
                     gesMouvements.Ajouter(partie.DéplacerMonstre(p, ChoisirDirection(r)));
               Thread.Sleep(50);
            }
         }),
         // affichage
         new Thread(() =>
         {
            Console.Clear();
            peintre.Peindre(partie.Carte, partie);
            while (ModeCourant == Mode.Action)
            {
               var (modif, mode) = gesMouvements.Appliquer(partie, ModeCourant);
               if(modif)
               {
                  Console.SetCursorPosition(0, 0);
                  peintre.Peindre(partie.Carte, partie);
                  Thread.Sleep(10);
               }
               if(mode != Mode.Action)
                  ModeCourant = mode;
            }
         }),
         // lecture au clavier
         new Thread(() =>
         {
            PiloteClavier pilote = new();
            while (ModeCourant == Mode.Action)
            {
               var (dir, mode) = pilote.Lire(ModeCourant); // bloquant
               if (dir != Direction.Aucune)
               {
                  if (ProtagonisteCourant != null)
                     gesMouvements.Ajouter(partie.Déplacer(ProtagonisteCourant, dir));
               }
               else if (ModeCourant != Mode.Fini)
               {
                  ModeCourant = mode;
               }
            }
         })
      };
      foreach (var fil in fils) fil.Start();
      foreach (var fil in fils) fil.Join();
   }
   public void Orchestrer(Partie partie, Peintre peintre)
   {
      switch(ModeCourant)
      {
         case Mode.Pause:
            MenuPause(partie, peintre);
            break;
         case Mode.Action:
            JeuNormal(partie, peintre);
            break;
      }
   }
   public bool Fini => ModeCourant == Mode.Fini;
}
