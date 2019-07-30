using System;
using System.Collections.Generic;
using DBT.Network;
using DBT.Transformations;
using Terraria;
using Terraria.ID;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void UpdateConditions()
        {

        }

        public bool CheckKiRequirement(int amount)
        {
            if (MaxKi >= amount)
                return true;

            return false;
        }
    }
}
