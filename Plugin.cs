using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace FixGrid
{
    [BepInPlugin("de.benediktwerner.stacklands.fixgrid", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource L;
        public static ConfigEntry<float> MinGridWidth;
        public static ConfigEntry<float> MinGridHeight;
        public static ConfigEntry<bool> AlignIslandGrid;

        private void Awake()
        {
            L = Logger;

            MinGridWidth = Config.Bind("General", "Minimum Grid Cell Width", 0.7f, "Vanilla is 0.75");
            MinGridHeight = Config.Bind("General", "Minimum Grid Cell Height", 0.85f, "Vanilla is 0.85");
            AlignIslandGrid = Config.Bind(
                "General",
                "Align Island Grid",
                true,
                "This fixes grid alignemnt on the island but as a trade-off disables the visible grid there since it can't be aligned properly"
            );

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        public static void SnapCardsToGrid(WorldManager instance)
        {
            if (instance.CurrentBoard.Id != "island" || !AlignIslandGrid.Value)
                instance.gridAlpha = 1f;

            var bounds = instance.CurrentBoard.WorldBounds;

            var width = bounds.extents.x - 0.5f;
            var xCount = (int)(width / MinGridWidth.Value);
            instance.GridWidth = width / xCount;

            var height = bounds.extents.z - 0.5f;
            var zCount = (int)(height / MinGridHeight.Value);
            instance.GridHeight = height / zCount;

            var xOffset = AlignIslandGrid.Value ? bounds.center.x : 0;
            var zOffset = AlignIslandGrid.Value ? bounds.center.z : 0;

            foreach (GameCard gameCard in instance.AllCards)
            {
                if (gameCard.Parent == null && gameCard.CardData is not Mob && gameCard.MyBoard.IsCurrent)
                {
                    var position = gameCard.transform.position;
                    position.x =
                        (float)Mathf.RoundToInt((position.x - xOffset) / instance.GridWidth) * instance.GridWidth
                        + xOffset;
                    position.z =
                        (float)Mathf.RoundToInt((position.z - zOffset) / instance.GridHeight) * instance.GridHeight
                        + zOffset;
                    gameCard.TargetPosition = position;
                }
            }
        }

        [HarmonyPatch(typeof(WorldManager), nameof(WorldManager.Update))]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> CircumventSnapCardsToGridInline(
            IEnumerable<CodeInstruction> instructions
        )
        {
            return new CodeMatcher(instructions)
                .MatchForward(
                    false,
                    new CodeMatch(
                        OpCodes.Call,
                        AccessTools.Method(typeof(WorldManager), nameof(WorldManager.SnapCardsToGrid))
                    )
                )
                .ThrowIfInvalid("Didn't find SnapCardsToGrid call")
                .SetOperandAndAdvance(AccessTools.Method(typeof(Plugin), nameof(Plugin.SnapCardsToGrid)))
                .InstructionEnumeration();
        }
    }
}
