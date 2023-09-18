using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
namespace KurzerMod.Content.Items.Consumables
{
    public class Muogu : ModItem
    {
        public static LocalizedText RestoreLifeText { get; private set; }

        public override void SetStaticDefaults()
        {
            RestoreLifeText = this.GetLocalization(nameof(RestoreLifeText));

            Item.ResearchUnlockCount = 0;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.buyPrice(platinum: 1);
            Item.SetNameOverride("恶心的蘑菇");
            Item.potion = false;
            Item.material = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.Name == "Tooltip0")
                {
                    tooltipLine.Text = $"[c/FF0000:恶心的味道使你呕吐,清除所有Buff效果,部分物品将受到限制].";
                }
            }
        }
        public override bool? UseItem(Player player)
        {
            for (int i = 0; i < BuffID.Count; i++)
            {
                player.ClearBuff(i);
            }

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .Register();
        }
    }
}
