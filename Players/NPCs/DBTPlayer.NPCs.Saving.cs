using System.Collections.Generic;
using DBT.Transformations;
using DBT.UserInterfaces.CharacterMenus;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveNPCs(TagCompound tag)
        {
            tag.Add(nameof(_aliveTownNPCs.Keys), _aliveTownNPCs.Keys);
            tag.Add(nameof(_aliveTownNPCs.Values), _aliveTownNPCs.Values);
        }

        private void LoadNPCs(TagCompound tag)
        {
            _aliveTownNPCs.Values = tag.GetInt(nameof(_aliveTownNPCs.Values)); 
        }
    }
}