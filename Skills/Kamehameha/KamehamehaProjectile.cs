/*using DBT.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;

namespace DBT.Skills.Kamehameha
{
    public sealed class KamehamehaProjectile : SkillProjectile
    {
        public KamehamehaProjectile() : base(SkillDefinitionManager.Instance.Kamehameha, 12, 72)
        {
        }
    }

    public sealed class KamehamehaBeam : BaseBeam 
    {
        public override void SetDefaults()
        {
            // all beams tend to have a similar structure, there's a charge, a tail or "start", a beam (body) and a head (forwardmost point)
            // this is the structure that helps alleviate some of the logic burden by predefining the dimensions of each segment.
            tailOrigin = new Point(0, 0);
            tailSize = new Point(74, 71);
            beamOrigin = new Point(12, 72);
            beamSize = new Point(50, 2);
            headOrigin = new Point(12, 76);
            headSize = new Point(50, 24);

            // this determines how long the max fade in for beam opacity takes to fully "phase in", at a rate of 1f per frame. (This is handled by the charge ball)
            beamFadeOutTime = 30f;

            // Bigger number = slower movement. For reference, 60f is pretty fast. 180f is pretty slow.
            rotationSlowness = 60f;

            // vector to reposition the beam tail down if it feels too low or too high on the character sprite
            offsetY = new Vector2(0, 4f);

            // the maximum travel distance the beam can go
            maxBeamDistance = 2000f;

            // the speed at which the beam head travels through space
            beamSpeed = 15f;

            // the type of dust to spawn when the beam is firing
            dustType = 15;

            // the frequency at which to spawn dust when the beam is firing
            dustFrequency = 0.6f;

            // how many particles per frame fire while firing the beam.
            fireParticleDensity = 6;

            // the frequency at which to spawn dust when the beam collides with something
            collisionDustFrequency = 1.0f;

            // how many particles per frame fire when the beam collides with something
            collisionParticleDensity = 8;

            // The sound effect used by the projectile when firing the beam. (plays on initial fire only)
            beamSoundKey = "Sounds/BasicBeamFire";

            isEntityColliding = true;

            base.SetDefaults();
        }
    }

    public sealed class KamehamehaCharge : BaseBeamCharge 
    {
        public override void SetDefaults()
        {
            // the maximum charge level of the ball     
            chargeLimit = 6;

            // this is the beam the charge beam fires when told to.
            beamProjectileName = mod.ProjectileType<KamehamehaBeam>();

            // the type of dust that should spawn when charging or decaying
            dustType = 15;

            // Bigger number = slower movement. For reference, 60f is pretty fast. This doesn't have to match the beam speed.
            rotationSlowness = 15f;

            // the charge ball is just a single texture.
            // these two vars specify its draw origin and size, this is a holdover from when it shared a texture sheet with other beam components.
            chargeOrigin = new Point(0, 0);
            chargeSize = new Point(18, 18);

            // vector to reposition the charge ball if it feels too low or too high on the character sprite
            channelingOffset = new Vector2(0, 4f);

            // The sound effect used by the projectile when charging up.
            chargeSoundKey = "Sounds/EnergyWaveCharge";

            // The amount of delay between when the client will try to play the energy wave charge sound again, if the player stops and resumes charging.
            chargeSoundDelay = 120;

            base.SetDefaults();
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool CanHitPlayer(Player target)
        {
            return false;
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }
    }
}
*/