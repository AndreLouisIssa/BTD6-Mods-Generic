using MelonLoader;
using System;
using System.Reflection;
using HarmonyLib;
using System.Linq;
using Assets.Scripts.Unity.UI_New.Popups;
using TMPro;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(Cypheric.Main), "Cypheric", "1.0.0", "Magic Gonads")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Cypheric
{
    public class Main : MelonMod
    {
        internal static string modDir = $"{Environment.CurrentDirectory}\\Mods\\{Assembly.GetExecutingAssembly().GetName().Name}";

        public static int seed;

        public static HashSet<TextMeshProUGUI> seen = new HashSet<TextMeshProUGUI>();
        public static string Scramble(string input)
        {
            if (input == null) return input;
            return string.Concat(input.Select(x => (char)((x + seed) % 1024)));
        }

        //public static string Unscramble(string input)
        //{
        //    if (input == null) return input;
        //    return string.Concat(input.Select(x => (char)((x - seed)%1024)));
        //}

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Mod has finished loading");
            seed = 512+(new Random()).Next(-128, 128);
            MelonLogger.Msg($"seed: {seed}");
        }

        [HarmonyPatch(typeof(TMP_Text))]
        [HarmonyPatch(nameof(TMP_Text.text), MethodType.Setter)]
        internal class TMP_Text_set_text
        {
            [HarmonyPrefix]
            internal static void Prefix(ref string value)
            {
                value = Scramble(value);
            }
        }

        //[HarmonyPatch(typeof(TMP_Text))]
        //[HarmonyPatch(nameof(TMP_Text.text), MethodType.Getter)]
        //internal class TMP_Text_get_text
        //{
        //   [HarmonyPostfix]
        //    internal static void Postfix(ref string __result)
        //    {
        //        __result = Unscramble(__result);
        //    }
        //}

        //[HarmonyPatch(typeof(TextMeshProUGUI))]
        //[HarmonyPatch(nameof(TextMeshProUGUI.OnPreRenderCanvas))]
        //internal class TextMeshProUGUI_OnPreRenderCanvas
        //{
        //    [HarmonyPrefix]
        //    internal static void Prefix(TextMeshProUGUI __instance)
        //    {
        //        __instance.text = Scramble(__instance.text);
        //    }
        //}

    }

}