using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace KurzerMod
{
    public class KurzerMod : Mod
    {
        static public KUR.V8 v8;
        public override void Load()
        {
            v8 = new KUR.V8();
        }
        public override void Unload()
        {
            v8 = null;
        }
    }
}