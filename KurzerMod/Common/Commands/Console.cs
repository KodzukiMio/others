using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace KurzerMod.Common.Commands
{
    public class Console : ModCommand
    {
        public override CommandType Type
            => CommandType.World;
        public override string Command
            => "console";
        public override string Usage
            => "/console javascript";
        public override string Description
            => "javascript console";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                throw new UsageException("At least one argument was expected.");
            }
            var v8 = KurzerMod.v8;
            Main.NewText(input);
        }
    }
}
