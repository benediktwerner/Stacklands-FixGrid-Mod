using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace FixGrid
{
    [BepInPlugin(
        "de.benediktwerner.stacklands.fixgrid",
        PluginInfo.PLUGIN_NAME,
        PluginInfo.PLUGIN_VERSION
    )]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> MinGridWidth;
        public static ConfigEntry<float> MinGridHeight;

        private void Awake()
        {
            MinGridWidth = Config.Bind(
                "General",
                "Minimum Grid Cell Width",
                0.7f,
                "Vanilla is 0.75"
            );
            MinGridHeight = Config.Bind(
                "General",
                "Minimum Grid Cell Height",
                0.85f,
                "Vanilla is 0.85"
            );

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        [HarmonyPatch(typeof(WorldManager), nameof(WorldManager.Update))]
        [HarmonyPrefix]
        public static void FixGridSize(WorldManager __instance)
        {
            if (__instance.CurrentBoard == null)
                return;

            var width = __instance.CurrentBoard.WorldBounds.extents.x - 0.5f;
            var xCount = (int)(width / MinGridWidth.Value);
            __instance.GridWidth = width / xCount;

            var height = __instance.CurrentBoard.WorldBounds.extents.z - 0.5f;
            var zCount = (int)(height / MinGridHeight.Value);
            __instance.GridHeight = height / zCount;
        }
    }
}
