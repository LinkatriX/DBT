using System;
using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace DBT.Transformations.Appearance
{
    public abstract class TransformationAppearance
    {
        private Texture2D
            _customBody,
            _customFemaleBody,
            _customArms,
            _customEyes;

        private bool
            _customBodyChecked,
            _customFemaleBodyChecked,
            _customArmsChecked,
            _customEyesChecked;


        protected TransformationAppearance(AuraAppearance aura, HairAppearance hair, Color? generalColor, Color? eyeColor, TransformationAppearance manualFur = null)
        {
            Aura = aura;
            Hair = hair;
            
            EyeColor = eyeColor;
            GeneralColor = generalColor;

            ManualFur = manualFur;
        }


        private string GetPathForPart(string partName)
        {
            string path = this.GetType().GetRootPath();
            string[] splitPath = path.Split('/');

            return path + '/' + splitPath[splitPath.Length - 1] + '_' + partName;
        }

        private Texture2D GetPart(string partName, ref bool checkd, ref Texture2D texture2d)
        {
            if (ManualFur != null)
                return ManualFur.GetPart(partName, ref checkd, ref texture2d);

            if (texture2d != null)
                return texture2d;

            if (checkd)
                return null;

            string path = GetPathForPart(partName);

            checkd = true;

            Mod mod = this.GetType().GetModFromType();

            if (mod.TextureExists(path))
                texture2d = mod.GetTexture(path);

            return texture2d;
        }


        public AuraAppearance Aura { get; }

        public HairAppearance Hair { get; }

        public Color? GeneralColor { get; }

        public Color? EyeColor { get; }

        public TransformationAppearance ManualFur { get; }


        public Texture2D CustomArms => GetPart("Arms", ref _customArmsChecked, ref _customArms);
        public Texture2D CustomBody => GetPart("Body", ref _customBodyChecked, ref _customBody);
        public Texture2D CustomFemaleBody => GetPart("FemaleBody", ref _customFemaleBodyChecked, ref _customFemaleBody);
        public Texture2D CustomEyes => GetPart("Eyes", ref _customEyesChecked, ref _customEyes);

        public bool ShouldHideNormalSkin => CustomBody != null;
    }
}
