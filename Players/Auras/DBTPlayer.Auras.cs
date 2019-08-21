using System.Collections.Generic;
using DBT.Auras;
using DBT.Transformations;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void PreUpdateMovementHandleAura()
        {
            AuraAppearance aura = GetAura();

            if (Aura != aura)
                Aura = aura;

            if (Aura == null) return;

            AuraFrameTimer += Aura.GetTicksPerFrameTimerTick(this);

            if (AuraFrameTimer >= Aura.Information.FramesTimer)
            {
                AuraFrameTimer = 0;
                AuraFrameIndex++;
            }

            if (AuraFrameIndex >= Aura.Information.FramesCount)
                AuraFrameIndex = 0;

            float[] rgbLightingRadiuses = aura.Lighting.GetRGBLightingRadiuses(this);
            Lighting.AddLight(player.Center + player.velocity * 8f, rgbLightingRadiuses[0], rgbLightingRadiuses[1], rgbLightingRadiuses[2]);
        }


        public AuraAppearance GetAura()
        {
            if (player.dead) return null;

			if (ActiveTransformations.Count == 0 && IsCharging)
				// TODO Change this to racial/trait aura.
				return AuraAnimationInformation.chargeAura;

            // TODO Support multiple auras.
            PlayerTransformation transformation = GetTransformation();
            if (transformation != null)
                return transformation.Definition.Appearance.Aura;

            return null;
        }

        internal void HandleAuraDrawLayers(List<PlayerLayer> layers)
        {
            AuraAnimationInformation.auraLayer.visible = true;
            layers.Insert(layers.FindIndex(l => l.Name == "MiscEffectsBack"), AuraAnimationInformation.auraLayer);
        }

        public AuraAppearance Aura { get; private set; }

        public int AuraFrameTimer { get; private set; }

        public int AuraFrameIndex { get; private set; }
    }
}
