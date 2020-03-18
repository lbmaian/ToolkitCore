﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ToolkitCore.HarmonyPatches
{
    [StaticConstructorOnStartup]
    static class SavePatch
    {
        static SavePatch()
        {
            new Harmony("com.rimworld.mod.hodlhodl.toolkit.core")
                .Patch(
                    original: AccessTools.Method(
                            type: typeof(GameDataSaveLoader),
                            name: "SaveGame"),
                    postfix: new HarmonyMethod(typeof(SavePatch), nameof(SaveGame_PostFix))
                );
        }

        static void SaveGame_PostFix()
        {
            Log.Message("Saving Data");
            Database.DatabaseController.SaveToolkit();
            ToolkitData.globalDatabase.Write();
        }
    }
}
