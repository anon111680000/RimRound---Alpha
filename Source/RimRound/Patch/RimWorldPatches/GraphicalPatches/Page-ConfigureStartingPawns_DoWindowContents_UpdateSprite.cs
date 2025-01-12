﻿using HarmonyLib;
using RimRound.Comps;
using RimRound.Utilities;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimRound.Patch
{
    [HarmonyPatch(typeof(Page_ConfigureStartingPawns))]
    [HarmonyPatch(nameof(Page_ConfigureStartingPawns.DoWindowContents))]
    class Page_ConfigureStartingPawns_DoWindowContents_UpdateSprite
    {
        public static void Postfix(Pawn ___curPawn) 
        {
            AssignBodyTypeCategoricalExemptionsForStarterPage();
           
        }

        static void AssignBodyTypeCategoricalExemptionsForStarterPage() 
        {
            foreach (Pawn p in Find.GameInitData.startingAndOptionalPawns)
            {
                if (p is null || (!p?.RaceProps.Humanlike ?? false))
                    continue;

                PawnBodyType_ThingComp comp = p.TryGetComp<PawnBodyType_ThingComp>();

                if (comp is null)
                    continue;

                comp.CategoricallyExempt = BodyTypeUtility.CheckExemptions(p);

                BodyTypeUtility.UpdatePawnSprite(p, false, false);
            }
        }

    }
}
