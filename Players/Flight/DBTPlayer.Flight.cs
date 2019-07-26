using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void PostUpdateFlight()
        {
            if (!Flying) return;
        }

        public void ApplySkillChargeSlowdown()
        {
            float chargeMoveSpeedBonus = 0;

            if (Flying)
            {
                chargeMoveSpeedBonus = SkillChargeMoveSpeedModifier / 10f;
                float yVelocity = player.gravity + 0.001f;

                if (DownHeld || UpHeld)
                    yVelocity = player.velocity.Y / (1.2f - chargeMoveSpeedBonus);
                else
                    yVelocity = Math.Min(-0.4f, player.velocity.Y / (1.2f - chargeMoveSpeedBonus));
            }
            else
            {
                chargeMoveSpeedBonus = SkillChargeMoveSpeedModifier / 10f;

                player.velocity = new Vector2(player.velocity.X / (1.2f - chargeMoveSpeedBonus), Math.Max(player.velocity.Y, player.velocity.Y / (1.2f - chargeMoveSpeedBonus)));
            }
        }

        public Vector2 GetMouseVectorOrDefault()
        {
            if (Main.myPlayer != player.whoAmI)
                return Vector2.Zero;
            Vector2 mouseVector = Vector2.Normalize(Main.MouseWorld - player.Center);
            if (player.heldProj != -1)
            {
                // player has a projectile, check to see if it's a charge ball or beam, that hijacks the octant for style.
                //Will have to add when charging is finished - Skipping
                var proj = Main.projectile[player.heldProj];
                if (proj != null)
                {
                    if (proj.modProjectile != null)
                    {
                        mouseVector = proj.velocity;
                    }
                }
            }
            return mouseVector;
        }

        public void HandleMouseOctantAndSyncTracking()
        {
            // we only handle the local player's controls.
            if (Main.myPlayer != player.whoAmI)
                return;

            // this is why :p
            mouseWorldOctant = GetMouseWorldOctantFromRadians(GetMouseRadiansOrDefault());
        }

        public float GetMouseRadiansOrDefault()
        {
            var mouseVector = GetMouseVectorOrDefault();
            if (mouseVector == Vector2.Zero)
                return 0f;
            return mouseVector.ToRotation();
        }

        public int GetMouseWorldOctantFromRadians(float mouseRadians)
        {
            // threshold values for octants are 22.5 degrees in either direction of a 45 degree mark on a circle (perpendicular 90s, 180s and each midway point, in 22.5 degrees either direction).
            // to make this clear, we're setting up some offset vars to make the numbers a bit more obvious.
            float thresholdDegrees = 22.5f;
            float circumferenceSpan = 45f;
            // the 8 octants, starting at the EAST mark (0) and, presumably, rotating clockwise (positive) or counter clockwise (negative).
            // note that 4 and -4 are the same thing. It doesn't matter which you use, radian outcome is the same.
            int[] octants = { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
            foreach (int octant in octants)
            {
                float minRad = MathHelper.ToRadians((octant * circumferenceSpan) - thresholdDegrees);
                float maxRad = MathHelper.ToRadians((octant * circumferenceSpan) + thresholdDegrees);
                if (mouseRadians >= minRad && mouseRadians <= maxRad)
                {
                    // normalize octant -4 to 4, for sanity reasons. They really are the same octant, but this formula isn't good enough to figure that out for some reason.
                    return octant == -4 ? 4 : octant;
                }
            }

            // this shouldn't happen, who knows.
            return 0;
        }

        public bool IsPlayerImmobilized()
        {
            return player.frozen || player.stoned || player.HasBuff(BuffID.Cursed);
        }

        /*public void HandleChargeEffects()
        {
            
            // various effects while charging
            // if the player is flying and moving, charging applies a speed boost and doesn't recharge ki, but also doesn't slow the player.
            bool isAnyKeyHeld = LeftHeld || RightHeld || UpHeld || DownHeld;
            if (IsCharging)
            {
                bool isChargeBoostingFlight = isFlying && isAnyKeyHeld;
                bool shouldApplySlowdown = Ki < MaxKi && !isChargeBoostingFlight;
                // grant defense and a protective barrier visual if charging with baldur essentia
                if (!isChargeBoostingFlight)
                {
                    //TODO: Add in slowdown for Baldur Items

                    if (baldurEssentia || buldariumSigmite)
                    {
                        var defenseBoost = Math.Max(baldurEssentia ? 1.3f : 1f, buldariumSigmite ? 1.5f : 1f);
                        shouldApplySlowdown = true;
                        // only create the projectile if one doesn't exist already.
                        if (player.ownedProjectileCounts[mod.ProjectileType("BaldurShell")] == 0)
                            Projectile.NewProjectile(player.Center.X - 40, player.Center.Y + 90, 0, 0, mod.ProjectileType("BaldurShell"), 0, 0, player.whoAmI);
                        player.statDefense = (int)(player.statDefense * defenseBoost);
                    }
                    if (burningEnergyAmulet)
                    {
                        shouldApplySlowdown = true;
                        FireAura();
                    }
                    if (iceTalisman)
                    {
                        shouldApplySlowdown = true;
                        FrostAura();
                    }
                    if (pureEnergyCirclet)
                    {
                        shouldApplySlowdown = true;
                        PureEnergyAura();
                    }

                    // determine base regen rate and bonuses
                    AddKi(kiChargeRate + scarabChargeRateAdd, false, false);
                }

                if (shouldApplySlowdown)
                {
                    player.ApplyChannelingSlowdown();
                }
            }

            // grant multiplicative charge bonuses that grow over time if using either earthen accessories
            if (IsCharging && GetKi() < OverallKiMax() && (earthenScarab || earthenArcanium))
            {
                scarabChargeTimer++;
                if (scarabChargeTimer > 180 && scarabChargeRateAdd <= 5)
                {
                    scarabChargeRateAdd += 1;
                    scarabChargeTimer = 0;
                }
            }
            else
            {
                // reset scarab/earthen bonuses
                scarabChargeTimer = 0;
                scarabChargeRateAdd = 0;
            }
        }*/

        public bool isFlying { get; private set; }
        public bool Flying { get; internal set; }
        public bool FlightUnlocked { get; set; }
        public bool FlightDampenedFall { get; set; }
        public bool FlightT3 { get; set; }

        public bool isPlayerUsingKiWeapon = false;
        public float FlightSpeedModifier { get; set; }
        public float FlightKiUsageModifier { get; set; }
        public int mouseWorldOctant = -1;
    }
}