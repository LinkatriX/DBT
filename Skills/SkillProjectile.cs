using DBT.Extensions;
using DBT.Players;
using DBT.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills
{
    public abstract class SkillProjectile : KiProjectile
    {
        protected SkillProjectile(SkillDefinition definition, int width, int height) : base(definition.Characteristics.BaseDamage, definition.Characteristics.BaseKnockback, width, height) 
        {
            Definition = definition;
            Width = width;
            Height = height;
        }

        private bool ShouldFireSkill(DBTPlayer modPlayer)
        {
            if (!modPlayer.MouseLeftHeld) 
            {
                return true;
            }
            return false;
        }

        public override void SetDefaults()
        {
            projectile.width = Width;
            projectile.height = Height;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.velocity = Vector2.Zero;
            base.SetDefaults();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            DBTPlayer dbtPlayer = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();
            if (!UsesChargeBall)
            {
                if (dbtPlayer.MouseLeftHeld && !IsFired)
                {
                    ChargeAnimation(spriteBatch);
                    HandleCharging();
                }
            }
            return true;
        }

        public Vector2 GetChargeBallPosition()
        {
            return Main.player[projectile.owner].Center + channelingOffset;
        }
        // vector to reposition the charge ball if it feels too low or too high on the character sprite
        public Vector2 channelingOffset = new Vector2(0, 4f);

        public void HandleCharging()
        {
            DBTPlayer dbtPlayer = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();

            if (projectile.ai[1] == 0)
            {
                projectile.timeLeft = 999;
                projectile.position = GetChargeBallPosition();

                if (Definition.Characteristics.ChargeCharacteristics.CurrentCharge < Definition.Characteristics.ChargeCharacteristics.BaseMaxChargeLevel)
                {
                    ChargeTimer++;
                    if (ChargeTimer >= Definition.Characteristics.ChargeCharacteristics.BaseChargeTimer)
                    {
                        Definition.Characteristics.ChargeCharacteristics.CurrentCharge++;
                        PerChargeLevel();
                        ChargeTimer = 0;
                    }
                }
                if (dbtPlayer.MouseLeftHeld && !IsFired && Definition.Characteristics.ChargeCharacteristics.CurrentCharge <= Definition.Characteristics.ChargeCharacteristics.BaseMaxChargeLevel)
                    OnChargeAttack();

                if (!dbtPlayer.MouseLeftHeld)
                {
                    if (RequiresFullCharge)
                    {
                        if (Definition.Characteristics.ChargeCharacteristics.CurrentCharge >= Definition.Characteristics.ChargeCharacteristics.BaseMaxChargeLevel)
                            HandleFiring(Main.player[projectile.owner]);
                    }
                    else
                    {
                        if (Definition.Characteristics.ChargeCharacteristics.CurrentCharge > 0)
                            HandleFiring(Main.player[projectile.owner]);
                    }
                }
            }
        }

        public void HandleFiring(Player player)
        {
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();
            
            if (ShouldFireSkill(modPlayer))
            {
                OnFireAttack();
                

                // kill the charge sound if we're firing
                //chargeSoundSlotId = SoundHelper.KillTrackedSound(chargeSoundSlotId);

                if (UsesChargeBall)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == player.whoAmI)
                    {
                        // fire the laser!
                        //fire
                        KillCharge();
                    }
                }

                // beam is no longer sustainable, and neither is the charge ball
                /*if (Charge <= 0f)
                {
                    KillCharge();
                }*/
            }
        }

        /// <summary>A hook that is called per tick the skill is being charged.</summary>
        /// <returns></returns>
        public virtual void OnChargeAttack()
        {
        }
        /// <summary>A hook that is called whenever the skill is released.</summary>
        /// <returns></returns>
        public virtual void OnFireAttack()
        {
        }
        /// <summary>A hook that is called for every time the attack hits the next charge level.</summary>
        /// <returns></returns>
        public virtual void PerChargeLevel()
        {
        }

        public virtual void ChargeAnimation(SpriteBatch spriteBatch)
        {
        }

        public override bool PreAI()
        {
            Main.NewText("Projectile ai 1 is: " + projectile.ai[1]);
            Main.NewText("Projectile has fired? " + IsFired);
            // pre AI of the charge ball is responsible for telling us if the weapon has changed or the projectile should otherwise die

            bool isPassingPreAi = base.PreAI();
            if (!isPassingPreAi && MyProjectile != null)
            {
                MyProjectile.StartKillRoutine();
            }
            return isPassingPreAi;
        }

        public void KillCharge()
        {
            // kill the charge ball
            projectile.Kill();
        }

        public Projectile MyProjectile { get; set; } = null;
        public bool IsFired { get; set; } = false;
        public bool IsCharged { get; set; } = false;
        public SkillDefinition Definition { get; }
        public int Width { get; }
        public int Height { get; }
        public int ChargeTimer { get; set; }
        public bool UsesChargeBall { get; set; } = false;
        public bool RequiresFullCharge { get; set; } = false;
        public Texture2D ChargeOverrideTexture { get; set; } = null;
        
    }
}