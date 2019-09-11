using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveNPCs(TagCompound tag)
        {
            tag.Add("AliveTownNPCs.Keys", AliveTownNPCs.Keys.ToList());
            tag.Add("AliveTownNPCs.Values", AliveTownNPCs.Values.ToList());
        }

        private void LoadNPCs(TagCompound tag)
        {
            var keys = tag.Get<List<int>>("AliveTownNPCs.Keys");
            var values = tag.Get<List<int>>("AliveTownNPCs.Values");
            AliveTownNPCs = keys.Zip(values, (key, value) => new { Key = key, Value = value }).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}