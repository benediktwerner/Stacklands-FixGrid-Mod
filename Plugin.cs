using HarmonyLib;
using UnityEngine;

namespace FixGrid
{
    public class Plugin : Mod
    {
        public static ModLogger L;
        public static ConfigEntry<float> MinGridWidth;
        public static ConfigEntry<float> MinGridHeight;
        public static ConfigEntry<bool> AlignIslandGrid;

        private ConfigEntry<T> CreateConfig<T>(string name, T defaultValue, string description)
        {
            return Config.GetEntry<T>(name, defaultValue, new ConfigUI { Tooltip = description });
        }

        private void Awake()
        {
            L = Logger;

            MinGridWidth = CreateConfig("Minimum Grid Cell Width", 0.7f, "Vanilla is 0.75");
            MinGridHeight = CreateConfig("Minimum Grid Cell Height", 0.85f, "Vanilla is 0.85");
            AlignIslandGrid = CreateConfig(
                "Align Island Grid",
                true,
                "This fixes grid alignment on the island but as a trade-off disables the visible grid there since it can't be aligned properly"
            );

            Harmony.PatchAll(typeof(Plugin));
        }

        public void OnDestroy()
        {
            Harmony.UnpatchSelf();
        }

        [HarmonyPatch(typeof(WorldManager), "SnapCardsToGrid")]
        [HarmonyAfter("OffGridPlugin")]
        [HarmonyBefore("GridTogglePlugin")]
        [HarmonyPrefix]
        public static void SnapCardsToGrid(WorldManager __instance, out bool __runOriginal)
        {
            __runOriginal = false;

            if (__instance.CurrentBoard.Id != "island" || !AlignIslandGrid.Value)
                __instance.gridAlpha = 1f;

            var bounds = __instance.CurrentBoard.WorldBounds;

            var width = bounds.extents.x - 0.5f;
            var xCount = (int)(width / MinGridWidth.Value);
            __instance.GridWidth = width / xCount;

            var height = bounds.extents.z - 0.5f;
            var zCount = (int)(height / MinGridHeight.Value);
            __instance.GridHeight = height / zCount;

            var xOffset = AlignIslandGrid.Value ? bounds.center.x : 0;
            var zOffset = AlignIslandGrid.Value ? bounds.center.z : 0;

            foreach (GameCard gameCard in __instance.AllCards)
            {
                if (
                    gameCard.Parent == null
                    && gameCard.CardData is not Mob
                    && gameCard.MyBoard.IsCurrent
                    && !gameCard.BeingDragged
                    && (!gameCard.Velocity.HasValue || gameCard.Velocity.Value.magnitude < 0.01)
                )
                {
                    var position = gameCard.transform.position;
                    position.x =
                        (float)Mathf.RoundToInt((position.x - xOffset) / __instance.GridWidth) * __instance.GridWidth
                        + xOffset;
                    position.z =
                        (float)Mathf.RoundToInt((position.z - zOffset) / __instance.GridHeight) * __instance.GridHeight
                        + zOffset;
                    gameCard.TargetPosition = position;
                }
            }
        }
    }
}
