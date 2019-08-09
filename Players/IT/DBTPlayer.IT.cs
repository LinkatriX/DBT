using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        // constants to do with instant transmission range/speed/ki cost.
        const float INSTANT_TRANSMISSION_FRAME_KI_COST = 0.01f;
        const float INSTANT_TRANSMISSION_TELEPORT_MINIMUM_KI_COST = 1000f;

        // intensity is the camera pan power being used in this frame, distance is how far the camera is from the player using the power.
        public float GetInstantTransmissionFrameKiCost(float intensity, float distance)
        {
            // INSERT THINGS THAT REDUCE INSTANT TRANSMISSION COST HERE
            float costCoefficient = isInstantTransmission3Unlocked ? 0.5f : 1f;
            return INSTANT_TRANSMISSION_FRAME_KI_COST * (float)Math.Sqrt(intensity) * (float)Math.Sqrt(distance) * costCoefficient;
        }

        public float GetInstantTransmissionTeleportKiCost()
        {
            float costCoefficient = isInstantTransmission3Unlocked ? 0.5f : 1f;
            return INSTANT_TRANSMISSION_TELEPORT_MINIMUM_KI_COST;
        }

        const int INSTANT_TRANSMISSION_BASE_CHAOS_DURATION = 120;
        public int GetBaseChaosDuration()
        {
            int durationReduction = isInstantTransmission3Unlocked ? 2 : 1;
            return INSTANT_TRANSMISSION_BASE_CHAOS_DURATION / durationReduction;
        }

        public int GetChaosDurationByDistance(float distance)
        {
            int baseDurationOfDebuff = GetBaseChaosDuration();
            float debuffDurationCoefficient = isInstantTransmission2Unlocked ? 0.5f : 1f;
            int debuffIncrease = (int)Math.Ceiling(distance * debuffDurationCoefficient / 2000f);

            return baseDurationOfDebuff + debuffIncrease;
        }

        public void AddInstantTransmissionChaosDebuff(float distance)
        {
            // instant transmission 3 bypasses the debuff
            if (!isInstantTransmission3Unlocked)
                player.AddBuff(BuffID.ChaosState, GetChaosDurationByDistance(distance), true);
        }

        private bool _isReturningFromInstantTransmission = false;
        private float _trackedInstantTransmissionKiLoss = 0f;
        // bool handles the game feel of instant transmission by being a limbo flag.
        // the first time you press the IT key, this is set to true, but it can be set to false in mid swing to prevent further processing on the same trigger/keypress.
        private bool _isHandlingInstantTransmissionTriggers = false;
        public void HandleInstantTransmissionFreeform()
        {
            // don't mess with stuff if the map is open
            if (Main.mapFullscreen)
                return;

            // sadly this routine has to run outside the checks, because we don't know if we're allowed to IT to a spot unless we do this first.
            Vector2 screenMiddle = Main.screenPosition + (new Vector2(Main.screenWidth, Main.screenHeight) / 2f);
            Vector2 direction = Vector2.Normalize(Main.MouseWorld - screenMiddle);
            float distance = Vector2.Distance(Main.MouseWorld, player.Center);
            // throttle intensity by a lot
            float intensity = Math.Min(128f, (float)Vector2.Distance(Main.MouseWorld, screenMiddle)) / 2f;
            float kiCost = GetInstantTransmissionFrameKiCost(intensity, distance);

            // the one frame delay on handling instant transmission is to set up the limbo var.
            // this should also theoretically prevent fullscreen map transmission from double-firing ITs.
            if (!_isHandlingInstantTransmissionTriggers && DBTMod.Instance.instantTransmission.JustPressed)
            {
                _isHandlingInstantTransmissionTriggers = true;
            }
            if (_isHandlingInstantTransmissionTriggers && DBTMod.Instance.instantTransmission.Current && HasKi(kiCost + GetInstantTransmissionTeleportKiCost()))
            {
                // player is trying to IT and has the ki to do so.
                // set the limbo var to true until we stop handling
                _isReturningFromInstantTransmission = true;

                _trackedInstantTransmissionKiLoss += kiCost;
                ModifyKi(-kiCost);

                Main.zoomX += (direction * intensity).X;
                if (Main.zoomX + player.Center.X >= Main.maxTilesX * 16f)
                    Main.zoomX = (Main.maxTilesX * 16f) - player.Center.X;
                if (Main.zoomX + player.Center.X <= 0)
                    Main.zoomX = -player.Center.X;

                Main.zoomY += (direction * intensity).Y;
                if (Main.zoomY + player.Center.Y >= Main.maxTilesY * 16f)
                    Main.zoomY = (Main.maxTilesY * 16f) - player.Center.Y;
                if (Main.zoomY + player.Center.Y <= 0)
                    Main.zoomY = -player.Center.Y;

            }
            else if (_isHandlingInstantTransmissionTriggers && ((DBTMod.Instance.instantTransmission.JustReleased || (DBTMod.Instance.instantTransmission.Current && !HasKi(kiCost + GetInstantTransmissionTeleportKiCost()))) && HasKi(GetInstantTransmissionTeleportKiCost())))
            {
                // player has either let go of the instant transmission key or run out of ki. either way, disable further processing and try to teleport
                // if we fail, the player gets some ki back but the processing is still canceled.
                _isReturningFromInstantTransmission = true;
                _isHandlingInstantTransmissionTriggers = false;

                Vector2 target;
                target.X = Main.mouseX + Main.screenPosition.X;
                if (player.gravDir == 1f)
                {
                    target.Y = Main.mouseY + Main.screenPosition.Y - player.height;
                }
                else
                {
                    target.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                }

                if (TryTransmission(target, distance))
                {
                    // there's no need to "return" from IT, you succeeded.
                    // make sure we don't try to give the player back their ki
                    _trackedInstantTransmissionKiLoss = 0f;
                }
            }
            else if (_isReturningFromInstantTransmission)
            {
                ModifyKi(_trackedInstantTransmissionKiLoss);
                _trackedInstantTransmissionKiLoss = 0f;
                _isReturningFromInstantTransmission = false;
                _isHandlingInstantTransmissionTriggers = false;
                Main.zoomX = 0f;
                Main.zoomY = 0f;
            }
        }

        public bool TryTransmission(Vector2 target, float distance)
        {
            Vector2 originalPosition = player.Center;
            if (!HandleInstantTransmissionExitRoutine(target, distance))
            {
                ModifyKi(_trackedInstantTransmissionKiLoss);
                _trackedInstantTransmissionKiLoss = 0f;
                return false;
            }
            else
            {
                Projectile.NewProjectile(originalPosition.X, originalPosition.Y, 0f, 0f, DBTMod.Instance.ProjectileType("TransmissionLinesProj"), 0, 0, player.whoAmI);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, DBTMod.Instance.ProjectileType("TransmissionLinesProj"), 0, 0, player.whoAmI);

                ModifyKi(-GetInstantTransmissionTeleportKiCost());
                return true;
            }
        }

        public bool HandleInstantTransmissionExitRoutine(Vector2 target, float distance)
        {
            // unabashedly stolen from decompiled source for rod of discord.
            // find a suitable place to IT to, reversing the camera pan direction if necessary.
            target.Y -= 32f;
            if (target.X > 50f && target.X < (float)(Main.maxTilesX * 16 - 50) && target.Y > 50f && target.Y < (float)(Main.maxTilesY * 16 - 50))
            {
                int tileX = (int)(target.X / 16f);
                int tileY = (int)(target.Y / 16f);
                if (((Main.tile[tileX, tileY] != null && Main.tile[tileX, tileY].wall != 87) || (double)tileY <= Main.worldSurface || NPC.downedPlantBoss) && !Collision.SolidCollision(target, player.width, player.height))
                {
                    if (target.X < player.Center.X)
                        player.ChangeDir(-1);
                    else
                        player.ChangeDir(1);
                    player.Teleport(target, -1);
                    NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, target.X, target.Y, 1, 0, 0);
                    if (player.chaosState)
                    {
                        player.statLife -= player.statLife / 7;
                        PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                        if (Main.rand.Next(2) == 0)
                        {
                            damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
                        }
                        player.lifeRegenCount = 0;
                        player.lifeRegenTime = 0;
                    }
                    AddInstantTransmissionChaosDebuff(distance);
                    return true;
                }
            }
            return false;
        }
        
        public bool isInstantTransmission1Unlocked = false;
        public bool isInstantTransmission2Unlocked = false;
        public bool isInstantTransmission3Unlocked = false;
        public bool ITUnlocked { get; set; }
        public bool ITBeaconsUnlocked { get; set; }
        public bool ITTargetUnlocked { get; set; }
    }
}
