﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;

namespace DBT
{
    public class DBTWorld : ModWorld
    {
        /*public static bool downedFriezaShip = false;

        public override void Initialize()
        {
            downedFriezaShip = false;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedFriezaShip) downed.Add("friezaShip");

            return new TagCompound {
                {"downed", downed}
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedFriezaShip = downed.Contains("friezaShip");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedFriezaShip = flags[0];
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedFriezaShip;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedFriezaShip = flags[0];
        }*/
    }
}
