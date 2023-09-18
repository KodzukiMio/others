using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;

namespace KurzerMod.Content.Items.Weapons
{
    public class Sword0 : Terraria.ModLoader.ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 50;
            Item.knockBack = 6;
            Item.crit = 50;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.SetNameOverride("玛丽太太的烂面包");
            Item.shoot = ProjectileID.Bee;
            Item.shootSpeed = 8f;
            Item.material = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.Name == "Tooltip0")
                {
                    tooltipLine.Text = $"[c/00FF00:里面藏着许多蜜蜂].";
                }
            }
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.Heal(damageDone >> 1);
            target.AddBuff(BuffID.OnFire, 60);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int counts = (new Random()).Next(10, 30);
            for (int i = 0; i < counts; i++)
            {
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(1f), type, this.Item.damage >> 1, 3, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.DirtBlock, 6).AddIngredient(ItemID.Wood, 3).Register();
        }
    }
}