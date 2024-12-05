using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Enums;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using CimaTerra.Content.Items.Projectiles;
using CimaTerra.Content.Items.Projectiles.BoletoGanador;
using Microsoft.Xna.Framework;
namespace CimaTerra.Content.Items.Mage.BoletoGanador
{
    internal class BoletoGanador : ModItem
    {

        public override void SetDefaults()
        {
            // Set default staff properties
            Item.DefaultToStaff(ModContent.ProjectileType<Projectiles.BoletoGanador.Casa>(), 7, 20, 11); // Default projectile
            Item.width = 34;
            Item.height = 40;
            Item.UseSound = SoundID.Item70;

            // Set weapon values
            Item.SetWeaponValues(25, 6, 32);

            // Set shop values
            Item.SetShopValues(ItemRarityColor.LightRed4, 10000);
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int chosenProjectileType = 0;
            float newSpeed = 0.1f; // Default speed
            int extraDamage = 0;  // Additional damage modifier

            switch (Main.rand.Next(3))
            {
                case 0:
                    chosenProjectileType = ModContent.ProjectileType<Projectiles.BoletoGanador.Casa>();
                    newSpeed = 1f;  // Modify speed for Casa
                    extraDamage =   20; // Add extra damage for Casa
                    break;

                case 1:
                    chosenProjectileType = ModContent.ProjectileType<Projectiles.BoletoGanador.Carro>();
                    newSpeed = 1.7f;  // Modify speed for Carro
                    extraDamage = 5; // Add extra damage for Carro
                    break;

                case 2:
                    chosenProjectileType = ModContent.ProjectileType<Projectiles.BoletoGanador.FajoBilletes>();
                    newSpeed = 2.3f;   
                    extraDamage = -6; 
                    break;
            }

            // Adjust velocity using the modified speed
            velocity *= newSpeed;

            // Spawn the projectile
            Projectile.NewProjectile(source, position, velocity, chosenProjectileType, damage + extraDamage, knockback, player.whoAmI);

            return false;
        }





        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        //public override void AddRecipes()
        //{
        //    CreateRecipe()
        //        .AddIngredient<ExampleItem>()
        //        .AddTile<Tiles.Furniture.ExampleWorkbench>()
        //        .Register();
        //}

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            // We can use ModifyManaCost to dynamically adjust the mana cost of this item, similar to how Space Gun works with the Meteor armor set.
            // See ExampleHood to see how accessories give the reduce mana cost effect.
            if (player.statLife < player.statLifeMax2 / 2)
            {
                mult *= 0.5f; // Half the mana cost when at low health. Make sure to use multiplication with the mult parameter.
            }
        }
    }
}
