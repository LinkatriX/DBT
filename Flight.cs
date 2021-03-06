﻿using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using DBT.Players;
using WebmilioCommons.Extensions;

namespace DBT
{
    public class Flight
    {
        public const int FLIGHT_KI_DRAIN = 4;
        public const float BURST_SPEED = 0.5f, FLIGHT_SPEED = 0.3f;

        public static void Update(DBTPlayer dbtPlayer)
        {
            if (Main.netMode == NetmodeID.Server) // Servers can't fly.
                return;

            if (!dbtPlayer.Flying) // Imagine wanting to run code without flying.
                return;

            Player player = dbtPlayer.player;

            if (player.dead || player.mount.Type != -1 || player.ropeCount != 0 || (dbtPlayer.StopFlightOnNoKi && dbtPlayer.Ki <= FLIGHT_KI_DRAIN))
            {
                dbtPlayer.Flying = false;
                AddKatchinFeetBuff(dbtPlayer);
                dbtPlayer.player.fallStart = (int)(dbtPlayer.player.position.Y / 16f);

                return;
            }

            player.DryCollision(true, true);
            player.fullRotationOrigin = new Vector2(11, 22);

            Vector2 rotationDirection = Vector2.Zero;

            float
                boostSpeed = BURST_SPEED * (dbtPlayer.Charging ? 1 : 0),

                flightCostMultiplier = (dbtPlayer.FlightT3 ? 0.25f : dbtPlayer.FlightDampenedFall ? 0.5f : 1f),
                totalFlightUsage = Math.Max(1f, FLIGHT_KI_DRAIN * dbtPlayer.FlightKiUsageModifier) * flightCostMultiplier,

                flightSpeedMultiplier = (1f + boostSpeed) * (dbtPlayer.FlightT3 ? 1.25f : dbtPlayer.FlightDampenedFall ? 1f : 0.75f),
                flightSpeed = FLIGHT_SPEED * flightSpeedMultiplier,

                totalHorizontalFlightSpeed = flightSpeed + player.moveSpeed + dbtPlayer.FlightSpeedModifier,
                totalVerticalFlightSpeed = flightSpeed + Player.jumpHeight / 5 + dbtPlayer.FlightSpeedModifier;

            dbtPlayer.ModifyKi(-totalFlightUsage * (1f + boostSpeed));

            if (dbtPlayer.UpHeld)
            {
                player.velocity.Y -= totalVerticalFlightSpeed / 3.8f; //3.8 original
                rotationDirection = Vector2.UnitY;
            }
            else if (dbtPlayer.DownHeld)
            {
                player.maxFallSpeed = 20f;
                player.velocity.Y += totalVerticalFlightSpeed / 3.8f; //3.6 original
                rotationDirection = -Vector2.UnitY;
            }

            if (dbtPlayer.RightHeld)
            {
                player.velocity.X += totalHorizontalFlightSpeed;
                rotationDirection += Vector2.UnitX;
            }
            else if (dbtPlayer.LeftHeld)

            {
                player.velocity.X -= totalHorizontalFlightSpeed;
                rotationDirection -= Vector2.UnitX;
            }

            player.velocity.X = MathHelper.Lerp(player.velocity.X, 0, 0.1f);
            player.velocity.Y = MathHelper.Lerp(player.velocity.Y, 0, 0.1f);

            player.velocity -= player.gravity * Vector2.UnitY;

            if (player.velocity.X > 0)
                player.legFrameCounter = -player.velocity.X;
            else
                player.legFrameCounter = player.velocity.X;

            player.fullRotation = MathHelper.Lerp(player.fullRotation, GetPlayerFlightRotation(rotationDirection, player), 0.1f);
        }

        /*public static void Update2(Player player)
        {
            // this might seem weird but the server isn't allowed to control the flight system. yes no fucking shit
            if (Main.netMode == NetmodeID.Server)
                return;

            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();

            //check for ki or death lol
            if ((modPlayer.Ki <= 0 || player.dead || player.mount.Type != -1 || player.ropeCount != 0) && modPlayer.Flying)
            {
                modPlayer.Flying = false;
                AddKatchinFeetBuff(player);
            }

            if (modPlayer.Flying)
            {
                // cancel platform collision
                player.DryCollision(true, true);

                //prepare vals
                player.fullRotationOrigin = new Vector2(11, 22);
                Vector2 mRotationDir = Vector2.Zero;

                //Input checks
                float boostSpeed = (BURST_SPEED) * (modPlayer.Charging ? 1 : 0);
                
                // handle ki drain
                float totalFlightUsage = Math.Max(1f, FLIGHT_KI_DRAIN * modPlayer.FlightKiUsageModifier);
                float flightCostMult = modPlayer.FlightT3 ? 0.25f : (modPlayer.FlightDampenedFall ? 0.5f : 1f);
                totalFlightUsage *= flightCostMult;
                modPlayer.ModifyKi(totalFlightUsage * (1f + boostSpeed) * -1);
                float flightSpeedMult = (1f + boostSpeed);
                flightSpeedMult *= modPlayer.FlightT3 ? 1.25f : (modPlayer.FlightDampenedFall ? 1f : 0.75f);
                float flightSpeed = FLIGHT_SPEED * flightSpeedMult;
                                    
                float totalHorizontalFlightSpeed = flightSpeed + (player.moveSpeed / 3) + modPlayer.FlightSpeedModifier;
                float totalVerticalFlightSpeed = flightSpeed + (Player.jumpSpeed / 2) + modPlayer.FlightSpeedModifier;

                if (modPlayer.UpHeld)
                {
                    // for some reason flying up is way, way faster than flying down.
                    player.velocity.Y -= (totalVerticalFlightSpeed / 3.8f);
                    mRotationDir = Vector2.UnitY;
                }
                else if (modPlayer.DownHeld)
                {
                    player.maxFallSpeed = 20f;
                    player.velocity.Y += totalVerticalFlightSpeed / 3.6f;
                    mRotationDir = -Vector2.UnitY;
                }

                if (modPlayer.RightHeld)
                {
                    player.velocity.X += totalHorizontalFlightSpeed;
                    mRotationDir += Vector2.UnitX;
                }
                else if (modPlayer.LeftHeld)
                {
                    player.velocity.X -= totalHorizontalFlightSpeed;
                    mRotationDir -= Vector2.UnitX;
                }

                //calculate velocity
                player.velocity.X = MathHelper.Lerp(player.velocity.X, 0, 0.1f);
                player.velocity.Y = MathHelper.Lerp(player.velocity.Y, 0, 0.1f);
                // keep the player suspended at worst.
                player.velocity = player.velocity - (player.gravity * Vector2.UnitY);                

                // handles keeping legs from moving when the player is in flight/moving fast/channeling.
                if (player.velocity.X > 0)
                {
                    player.legFrameCounter = -player.velocity.X;
                } else
                {
                    player.legFrameCounter = player.velocity.X;
                }                

                //calculate rotation
                float radRot = GetPlayerFlightRotation(mRotationDir, player);

                player.fullRotation = MathHelper.Lerp(player.fullRotation, radRot, 0.1f);
            }

            // altered to only fire once, the moment you exit flight, to avoid overburden of sync packets when moving normally.
            if (!modPlayer.Flying)
            {
                player.fullRotation = MathHelper.Lerp(player.fullRotation, 0, 0.1f);
            }
        }*///Obsolete flight code, remove before final commit

        public static Tuple<int, float> GetFlightFacingDirectionAndPitchDirection(DBTPlayer modPlayer)
        {
            int octantDirection = 0;
            int octantPitch = 0;
            // since the player is mirrored, there's really only 3 ordinal positions we care about
            // up angle, no angle and down angle
            // we don't go straight up or down cos it looks weird as shit
            switch (modPlayer.MouseWorldOctant)
            {
                case -3:
                case -2:
                case -1:
                    octantPitch = -1;
                    break;
                case 0:
                case 4:
                    octantPitch = 0;
                    break;
                case 1:
                case 2:
                case 3:
                    octantPitch = 1;
                    break;
            }

            // for direction we have to do things a bit different.
            Vector2 mouseVector = modPlayer.GetMouseVectorOrDefault();
            if (mouseVector == Vector2.Zero)
            {
                // we're probably trying to run direction on a player who isn't ours, don't do this. They can control their own dir.
                octantDirection = 0;
            }
            else
            {
                octantDirection = mouseVector.X < 0 ? -1 : 1;
            }

            return new Tuple<int, float>(octantDirection, octantPitch * 45f);
        }

        public static float GetPlayerFlightRotation(Vector2 mRotationDir, Player player)
        {
            float radRot = 0f;

            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();
            float leanThrottle = 180;
            // make sure if the player is using a ki weapon during flight, we're facing a way that doesn't make it look extremely goofy
            if (modPlayer.IsPlayerUsingKiWeapon)
            {
                var directionInfo = GetFlightFacingDirectionAndPitchDirection(modPlayer);
                // get flight rotation from octant
                var octantDirection = directionInfo.Item1;
                leanThrottle = directionInfo.Item2;

                int dir = octantDirection;
                if (dir != player.direction && player.whoAmI == Main.myPlayer)
                {                    
                    player.ChangeDir(dir);
                }                
            }

            if (modPlayer.IsPlayerUsingKiWeapon)
            {
                // we already got the lean throttle from above, and set the direction we needed to not look stupid
                if (player.direction == 1)
                    radRot = MathHelper.ToRadians(leanThrottle);
                else if (player.direction == -1)
                    radRot = MathHelper.ToRadians(-leanThrottle);
            }
            if (mRotationDir != Vector2.Zero)
            {
                mRotationDir.Normalize();
                radRot = (float)Math.Atan((mRotationDir.X / mRotationDir.Y));
                if (mRotationDir.Y < 0)
                {
                    if (mRotationDir.X > 0)
                        radRot += MathHelper.ToRadians(leanThrottle);
                    else if (mRotationDir.X < 0)
                        radRot -= MathHelper.ToRadians(leanThrottle);
                    else
                    {
                        if (player.velocity.X >= 0)
                            radRot = MathHelper.ToRadians(leanThrottle);
                        else if (player.velocity.X < 0)
                            radRot = MathHelper.ToRadians(-leanThrottle);
                    }
                }
            }

            return radRot;
        }

        public static void AddKatchinFeetBuff(DBTPlayer dbtPlayer)
        {
            // reset the player fall position here, even if they don't have flight dampening.
            dbtPlayer.player.fallStart = (int)(dbtPlayer.player.position.Y / 16f);

            // TODO Enable this when the buff is added.
            /*if (dbtPlayer.FlightDampenedFall)
                dbtPlayer.player.AddBuff<KatchinFeet>(600);*/
        }
    }
}

