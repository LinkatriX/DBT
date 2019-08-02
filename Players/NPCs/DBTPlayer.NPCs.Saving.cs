using System.Collections.Generic;
using System.Linq;
using DBT.Transformations;
using DBT.UserInterfaces.CharacterMenus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private TagCompound SaveNPCs(TagCompound tag)
        {
            /*tag.Add("AliveTownNPCs.Keys", AliveTownNPCs.Keys);
            tag.Add("AliveTownNPCs.Values", AliveTownNPCs.Values);
            return tag;*/

            return new TagCompound
            {
                {"AliveTownNPCs.Keys", AliveTownNPCs.Keys.ToList()},
                {"AliveTownNPCs.Values", AliveTownNPCs.Values.ToList()}
            };

        }

        private void LoadNPCs(TagCompound tag) //_aliveTownNPCs.Values.GetEnumerator();
        {
            /*int i = 0;
            foreach (KeyValuePair<NPC, int> entry in AliveTownNPCs)
            {
                tag.GetInt(nameof(NPCFriendship));
                i++;
                /*tag.GetInt(nameof(entry.Value));
                Main.NewText("The value " + entry.Value + " has been loaded!", Color.OrangeRed);
            }*/
            //stats = tag.GetList<int>("stats");
            var keys = tag.Get<List<NPC>>("AliveTownNPCs.Keys");
            var values = tag.Get<List<int>>("AliveTownNPCs.Values");
            AliveTownNPCs = keys.Zip(values, (key, value) => new { Key = key, Value = value }).ToDictionary(x => x.Key, x => x.Value);
            Main.NewText("Echbegone", Color.OrangeRed);
        }
    }
}