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
    public class MaryBread : ModItem
    {
        public static readonly string MaryBreadName = "玛丽太太的美味面包";
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
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.SetNameOverride(MaryBreadName);
            Item.healLife = 5;
            Item.potion = false;
            Item.material = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // 请参考 https://tmodloader.github.io/tModLoader/html/class_terraria_1_1_mod_loader_1_1_tooltip_line.html 以获得常规工具提示行名称列表
            TooltipLine line = tooltips.FirstOrDefault(x => x.Mod == "Terraria" && x.Name == "HealLife");

            if (line != null)
            {
                line.Text = $"[c/00FF00:味道好极了!]\n玛丽太太美味的面包,使用时恢复[c/00FF00:最大生命的5%+{Item.healLife}*最大魔力的10%].[c/FF0000:没有使用冷却时间,但无法使用快速治疗]";
            }
        }

        public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
            healValue = (int)(player.statLifeMax * 0.05 + healValue * 0.1 * player.statManaMax);
        }
        public override bool? UseItem(Player player)
        {
            player.ClearBuff(BuffID.Cursed);
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
