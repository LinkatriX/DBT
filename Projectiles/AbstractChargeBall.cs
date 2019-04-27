﻿using System;
using System.Collections.Generic;
using DBT.Helpers;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles
{
    // unabashedly stolen from blushie's laser example, and then customized WIP
    public abstract class AbstractChargeBall : ModProjectile
    {
        // CHARGE/BEAM SETTINGS, these are the things you can change to affect charge appearance behavior!

        #region Things you should definitely mess with

        // the maximum charge level of the ball     
        public float chargeLimit = 4;

        public float ChargeRatePerSecond()
        {
            return (float)Math.Pow(chargeLimit, 0.4f);
        }

        // this is the beam the charge beam fires when told to.
        public string beamProjectileName = "BaseBeamProj";

        // the type of dust that should spawn when charging or decaying
        public int dustType = 169;

        // the charge ball is just a single texture.
        // these two vars specify its draw origin and size, this is a holdover from when it shared a texture sheet with other beam components.
        public Point chargeOrigin = new Point(0, 0);
        public Point chargeSize = new Point(18, 18);

        // The sound effect used by the projectile when charging up.
        public string chargeSoundKey = "Sounds/EnergyWaveCharge";

        // The volume of the charge sound
        public float chargeSoundVolume = 1f;

        // The amount of delay between when the client will try to play the energy wave charge sound again, if the player stops and resumes charging.
        public int chargeSoundDelay = 120;

        // whether to try to put the player hand at their head when charging (eg. not firing)
        public bool isChargeAtHeadHeight = false;

        #endregion

        #region Things you probably should not mess with

        protected const float MAX_DISTANCE = 1000f;

        // vector to reposition the charge ball if it feels too low or too high on the character sprite
        public Vector2 channelingOffset = new Vector2(0, 4f);

        // rate at which charging produces dust
        protected float chargeDustFrequency = 0.4f;

        // rate at which dispersal of the charge ball (from weapon swapping) produces dust
        protected float disperseDustFrequency = 1.0f;

        // the amount of dust that tries to spawn when the charge orb disperses from weapon swapping.
        protected int disperseDustQuantity = 40;

        // Bigger number = slower movement. For reference, 60f is pretty fast. This doesn't have to match the beam speed.
        protected float rotationSlowness = 15f;

        // this field determines whether the beam tracks the player, "rooting" the tail origin to the player.
        protected bool isBeamOriginTracking = true;

        // the rate at which charge level increases while channeling
        protected float ChargeRate() { return ChargeRatePerSecond() / 60f; }

        // determines the frequency at which ki drain ticks. Bigger numbers mean slower drain.
        protected const int CHARGE_KI_DRAIN_WINDOW = 2;

        // Rate at which Ki is drained while channeling
        protected float ChargeKiDrainRate() { return projectile.damage * ChargeRatePerSecond() / (60f / CHARGE_KI_DRAIN_WINDOW); }

        // The sound slot used by the projectile to kill the sounds it's making
        protected KeyValuePair<uint, SoundEffectInstance> chargeSoundSlotId;

        #endregion

        #region Things you should not mess with

        // the maximum ChargeLimit of the attack after bonuses from gear (etc) are applied
        public float finalChargeLimit = 4;

        // the distance charge particle from the player center, as a factor of its size plus some padding.
        public float ChargeBallHeldDistance
        {
            get
            {
                return (chargeSize.Y / 2f) + 10f;
            }
        }

        // the amount of time the beam has been firing, used to track whether it has surpassed the minimum fire time the beam *has* to be fired.
        public float CurrentFireTime
        {
            get
            {
                return projectile.ai[0];
            }
            set
            {
                projectile.ai[0] = value;
                projectile.netUpdate = true;
            }
        }

        // the amount of charge currently in the ball, handles how long the player can fire and how much damage it deals
        public float ChargeLevel
        {
            get
            {
                return projectile.ai[1];
            }
            set
            {
                projectile.ai[1] = value;
                projectile.netUpdate = true;
            }
        }

        // Any nonzero number is on cooldown
        public float ChargeSoundCooldown
        {
            get
            {
                return projectile.localAI[0];
            }
            set
            {
                projectile.localAI[0] = value;
                projectile.netUpdate = true;
            }
        }

        // responsible for tracking if the player changed weapons in use, nullifying their charge immediately.
        private int _weaponBinding = -1;

        #endregion

        public override void SetDefaults()
        {
            projectile.width = chargeSize.X;
            projectile.height = chargeSize.Y;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        public bool IsFired = false;

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            // don't draw the ball when firing.
            if (IsFired)
                return false;
            float originalRotation = -1.57f;
            DrawChargeBall(spriteBatch, Main.projectileTexture[projectile.type], projectile.damage, originalRotation, projectile.scale, MAX_DISTANCE, Color.White);
            return false;
        }

        public Vector2 GetChargeBallPosition()
        {
            Vector2 positionOffset = channelingOffset + projectile.velocity * ChargeBallHeldDistance;
            return Main.player[projectile.owner].Center + positionOffset;
        }

        // The core function of drawing a charge ball
        public void DrawChargeBall(SpriteBatch spriteBatch, Texture2D texture, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color))
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            int radius = (int)Math.Ceiling(projectile.width / 2f * projectile.scale);
            DBZMOD.circle.ApplyShader(radius);
            float r = projectile.velocity.ToRotation() + rotation;
            spriteBatch.Draw(texture, GetChargeBallPosition() - Main.screenPosition,
                new Rectangle(0, 0, chargeSize.X, chargeSize.Y), color, r, new Vector2(chargeSize.X * .5f, chargeSize.Y * .5f), scale, 0, 0.99f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public void HandleChargeBallSize()
        {
            // the math square roots here cause the charge level to have a much more immediate size impact.
            projectile.scale = (float)(Math.Pow(ChargeLevel, 0.35f) / Math.Pow(chargeLimit, 0.35f)) * 1.3f;
        }

        private bool _wasCharging = false;

        public void HandleChargingKi(Player player)
        {
            bool isCharging = false;
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            finalChargeLimit = chargeLimit + dbtPlayer.SkillChargeLimitModifier;

            // stop channeling if the player is out of ki
            if (dbtPlayer.Ki < 1)
            {
                player.channel = false;
            }

            // keep alive routine.
            if (projectile.timeLeft < 4)
            {
                projectile.timeLeft = 10;
            }

            // charge the ball if the proper keys are held.
            // increment the charge timer if channeling and apply slowdown effect
            if (dbtPlayer.MouseLeftHeld && !IsFired)
            {
                // shoot some dust into the ball to show it's charging, and to look cool.
                // Also generates light.
                if (!IsFired)
                    ProjectileHelper.DoChargeDust(GetChargeBallPosition(), dustType, chargeDustFrequency, false, chargeSize.ToVector2());

                // the player can hold the charge all they like once it's fully charged up. Currently this doesn't incur a movespeed debuff either.
                if (ChargeLevel < finalChargeLimit && dbtPlayer.Ki >= ChargeKiDrainRate())
                {
                    isCharging = true;

                    // drain ki from the player when charging
                    if (DBTMod.IsTickRateElapsed(CHARGE_KI_DRAIN_WINDOW))
                        dbtPlayer.ModifyKi()

                    // increase the charge
                    ChargeLevel = Math.Min(finalChargeLimit, ChargeRate() + ChargeLevel);

                    // slow down the player while charging.
                    player.ApplyChannelingSlowdown();
                }
                else
                {
                    if (ChargeLevel == 0f)
                    {
                        projectile.Kill();
                    }
                }
            }

            // play the sound if the player just started charging and the audio is "off cooldown"
            if (!_wasCharging && isCharging && ChargeSoundCooldown == 0f)
            {
                if (!Main.dedServ)
                    chargeSoundSlotId = SoundHelper.PlayCustomSound(chargeSoundKey, projectile.Center, chargeSoundVolume);
                ChargeSoundCooldown = chargeSoundDelay;
            }
            else
            {
                ChargeSoundCooldown = Math.Max(0f, ChargeSoundCooldown - 1);
            }

            // set the wasCharging flag for proper tracking
            _wasCharging = isCharging;
        }

        public bool ShouldHandleWeaponChangesAndContinue(Player player)
        {
            if (player.HeldItem == null || player.dead)
            {
                projectile.Kill();
                return false;
            }

            if (_weaponBinding == -1)
            {
                _weaponBinding = player.HeldItem.type;
            }
            else
            {
                if (player.HeldItem.type != _weaponBinding)
                {
                    // do a buttload of decay dust
                    for (var i = 0; i < disperseDustQuantity; i++)
                    {
                        ProjectileHelper.DoChargeDust(GetChargeBallPosition(), dustType, disperseDustFrequency, true, chargeSize.ToVector2());
                    }
                    projectile.Kill();
                    return false;
                }
            }
            // weapon's correct, keep doing what you're doing.
            return true;
        }

        // helper field lets us limit mouse movement's impact on the charge ball rotation.
        private Vector2 _oldMouseVector = Vector2.Zero;

        // the old screen position helps us offset the MouseWorld vector by our screen position so it's more stable.
        private Vector2 _oldScreenPosition = Vector2.Zero;

        public override bool PreAI()
        {
            // capture the player instance so we can toss it around.
            Player player = Main.player[projectile.owner];

            // handles the initial binding to a weapon and determines if the player has changed items, which should kill the projectile.
            return ShouldHandleWeaponChangesAndContinue(player);
        }

        public virtual Vector2 DoControl(Player player)
        {
            // capture the current mouse vector, we're going to normalize movement prior to updating the charge ball location.
            Vector2 mouseVector = Main.MouseWorld;
            Vector2 screenPosition = Main.screenPosition;
            if (_oldMouseVector != Vector2.Zero)
            {
                Vector2 mouseMovementVector = (mouseVector - _oldMouseVector) / rotationSlowness;
                Vector2 screenChange = screenPosition - _oldScreenPosition;
                mouseVector = _oldMouseVector + mouseMovementVector + screenChange;
            }

            // handling the charge, literally - moving it around and stuff.
            UpdateChargeBallLocationAndDirection(player, mouseVector);

            // capture the current mouse vector as the previous mouse vector.
            _oldMouseVector = mouseVector;

            // capture the current screen position as the previous screen position.
            _oldScreenPosition = screenPosition;

            return mouseVector;
        }

        public void DoCharge(Player player)
        {
            // track whether charge level has changed by snapshotting it.
            float oldChargeLevel = ChargeLevel;

            // handle pouring ki into the ball (or decaying if not channeling)
            HandleChargingKi(player);

            // handle whether the ball should be visible, and how visible.
            HandleChargeBallSize();

            // If we just crossed a threshold, display combat text for the charge level increase.
            if (Math.Floor(oldChargeLevel) != Math.Floor(ChargeLevel) && oldChargeLevel != ChargeLevel)
            {
                Color chargeColor = oldChargeLevel < ChargeLevel ? new Color(51, 224, 255) : new Color(251, 74, 55);
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), chargeColor, (int)ChargeLevel, false, false);
            }
        }

        // The AI of the projectile
        public override void AI()
        {
            // capture the player instance so we can toss it around.
            Player player = Main.player[projectile.owner];

            // handle mouse movement and 
            DoControl(player);

            // handle charging (or not charging)
            DoCharge(player);

            // Handle Audio
            SoundHelper.UpdateTrackedSound(chargeSoundSlotId, projectile.Center);
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            if (projectile.owner != Main.myPlayer)
                return;
            var player = Main.player[projectile.owner];
            var modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.currentKiAttackChargeLevel = 0f;
            modPlayer.currentKiAttackMaxChargeLevel = 0f;
            modPlayer.isPlayerUsingKiWeapon = false;
        }

        public void UpdateChargeBallLocationAndDirection(Player player, Vector2 mouseVector)
        {
            var modPlayer = player.GetModPlayer<MyPlayer>();
            // custom channeling handler
            player.channel = true;

            // Multiplayer support here, only run this code if the client running it is the owner of the projectile
            if (projectile.owner == Main.myPlayer)
            {
                modPlayer.currentKiAttackChargeLevel = ChargeLevel;
                modPlayer.currentKiAttackMaxChargeLevel = finalChargeLimit;
                modPlayer.isPlayerUsingKiWeapon = true;
                Vector2 diff = mouseVector - player.Center;
                diff.Normalize();
                projectile.velocity = diff;
                projectile.direction = mouseVector.X > player.position.X ? 1 : -1;
                projectile.netUpdate = true;
            }
            if (isChargeAtHeadHeight)
            {
                projectile.position = player.Center;
            }
            else
            {
                projectile.position = player.Center + projectile.velocity * ChargeBallHeldDistance;
            }
            projectile.timeLeft = 10;
            player.itemTime = 10;
            player.itemAnimation = 10;
            int dir = projectile.direction;
            Vector2 rotationVector = projectile.velocity * dir;
            if (isChargeAtHeadHeight)
            {
                rotationVector = new Vector2(0, dir * -Main.screenHeight);
            }
            float itemRotation = (float)Math.Atan2(rotationVector.Y, rotationVector.X);
            player.itemRotation = itemRotation;
            // don't do this if the player is flying, we let the flight code handle it "manually" because it looks weird as shit
            if (!modPlayer.isFlying)
            {
                player.ChangeDir(dir);
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}