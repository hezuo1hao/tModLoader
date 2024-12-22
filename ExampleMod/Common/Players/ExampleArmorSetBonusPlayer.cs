using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExampleMod.Common.Players
{
	// This ModPlayer facilitates a set bonus effect. This example shows how either ArmorSetBonusActivated or ArmorSetBonusHeld can be used depending on how you want the player to interact with the set bonus effect. 
	public class ExampleArmorSetBonusPlayer : ModPlayer
	{
		public bool ExampleSetHood; // Indicates if the ExampleSet with ExampleHood is the active armor set.
		public int ShadowStyle = 0; // This is the shadow to use. Note that ExampleHood.ArmorSetShadows will only be called if the full armor set is visible.

		public static LocalizedText[] ShadowStyleText { get; private set; }

		public static LocalizedText CurrentShadowStyleText { get; private set; }

		public override void SetStaticDefaults() {
			string key = nameof(ExampleArmorSetBonusPlayer);
			ShadowStyleText = [Mod.GetLocalization($"{key}.ShadowStyle_0"), Mod.GetLocalization($"{key}.ShadowStyle_1"), Mod.GetLocalization($"{key}.ShadowStyle_2"), Mod.GetLocalization($"{key}.ShadowStyle_3")];
			CurrentShadowStyleText = Mod.GetLocalization($"{key}.CurrentShadowStyle");
		}

		public override void ResetEffects() {
			ExampleSetHood = false;
		}

		public override void ArmorSetBonusActivated() {
			if (!ExampleSetHood) {
				return;
			}

			if (ShadowStyle == 3) {
				ShadowStyle = 0;
			}
			ShadowStyle = (ShadowStyle + 1) % 3;
			ShowMessageForShadowStyle();
		}

		public override void ArmorSetBonusHeld(int holdTime) {
			if (!ExampleSetHood) {
				return;
			}

			if (holdTime == 60) {
				ShadowStyle = ShadowStyle == 3 ? 0 : 3;
				ShowMessageForShadowStyle();
			}
		}

		private void ShowMessageForShadowStyle() {
			string styleName = ShadowStyleText[0].Value;
			if (ShadowStyle > 0 && ShadowStyle < ShadowStyleText.Length)
				styleName = ShadowStyleText[ShadowStyle].Value;

			Main.NewText(CurrentShadowStyleText.Format(styleName));
		}
	}
}
