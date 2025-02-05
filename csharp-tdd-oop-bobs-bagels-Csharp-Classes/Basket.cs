﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using csharp_tdd_oop_bobs_bagels_Csharp_Classes;

namespace csharp_tdd_oop_bobs_bagels_Csharp_Classes
{
    public class Basket
    {
        Inventory inventory = new Inventory();
        public Basket() 
        {

        }

        public void TestData()
        {
            ShoppingBasket.Add( new ShopItem("BGLO", "Onion", 0.49m, "Bagel"));
            ShoppingBasket.Add( new ShopItem("BGLP", "Plain", 0.39m, "Bagel"));
            ShoppingBasket.Add( new ShopItem("BGLE", "Everything", 0.49m, "Bagel"));
            ShoppingBasket.Add(new ShopItem("BGLS", "Sesame", 0.49m, "Bagel"));
            foreach (ShopItem item in ShoppingBasket)
            {
                item.Amount = 1;
            }
        }
        public int itemId = 1;
        public bool AddItemToBasket(ShopItem item, int amount)
        {
            
            foreach(ShopItem i in inventory.InventoryList)
            {
                if (i.Variant == item.Variant) 
                {
                    if (this.ShoppingBasket.Count < this.ShoppingBasketMax)
                    {

                        ShoppingBasket.Add(new ShopItem(item.SKU, item.Variant, item.Price, item.Name) { Amount = amount, Id = itemId });

                        itemId++;
                        return true;
                    }
                    else if (this.ShoppingBasket.Count >= this.ShoppingBasketMax)
                    {
                        return false;
                    }
                }
                
            }

            return false;

        }

        public ShopItem SkuToShopItem(string Sku)
        {
            ShopItem empty = new ShopItem("","",0,"");
            foreach (ShopItem item in inventory.InventoryList)
            {
                if(item.SKU== Sku)
                {
                    return item;
                }
            }
            return empty;
        }

        public bool RemoveItemFromBasket(int id)
        {
            var itemToRemove = this.ShoppingBasket.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null) 
            {
                return this.ShoppingBasket.Remove(itemToRemove) ? true : false;
            }
            return false;
        }

        public void SetBasketMax(int value)
        {
            // if role is manager
            if(this.ShoppingBasket.Count < value)
            {
                ShoppingBasketMax = value;
            } else
            {
                Console.WriteLine("Basket size cant be smaller than current items in Basket");
            }
            
        }

        public decimal CalculateTotal()

        {
            
            decimal total = 0;
            foreach (var item in ShoppingBasket)
            {
                if (item.SKU == "BGLO" && item.Amount >= 6 || item.SKU == "BGLE" && item.Amount >= 6)

                {
                    
                    int modulo = item.Amount % 6;
                    int amountOfDiscounts = (item.Amount - modulo) / 6;
                    ShoppingBasketDiscountLeftOver.Add(new ShopItem(item.SKU, item.Variant, item.Price, item.Name) { Amount = modulo});
                    item.Costs += modulo * item.Price;
                    item.Costs += amountOfDiscounts * 2.49m;
                    item.Discount = item.Amount * item.Price - item.Costs;
                    total += amountOfDiscounts * 2.49m;
                    
                } else if (item.SKU == "BGLP" && item.Amount >= 12)
                {
                    int modulo = item.Amount % 12;
                    int amountOfDiscounts = (item.Amount - modulo) / 12;
                    ShoppingBasketDiscountLeftOver.Add(new ShopItem(item.SKU, item.Variant, item.Price, item.Name) { Amount = modulo });
                    item.Costs += modulo * item.Price;
                    item.Costs += amountOfDiscounts * 3.99m;
                    item.Discount = item.Amount * item.Price - item.Costs;
                    total += amountOfDiscounts * 3.99m;
                } else if (item.SKU != "COFB")
                {
                    ShoppingBasketDiscountLeftOver.Add(new ShopItem(item.SKU, item.Variant, item.Price, item.Name) { Amount = item.Amount });
                    item.Costs += item.Price * item.Amount;
                }  else if (item.SKU == "COFB")
                {
                    int Coffeeamount = item.Amount;
                    foreach (var p in this.ShoppingBasketDiscountLeftOver)
                    {
                        
                        if (p.Amount < Coffeeamount)
                        {
                            total += p.Amount * 1.25m;
                            p.Amount -= p.Amount;
                            Coffeeamount -= p.Amount;
                            
                        } if (p.Amount >= Coffeeamount)
                        {
                            total += Coffeeamount * 1.25m;
                            p.Amount -= Coffeeamount;
                            Coffeeamount -= Coffeeamount;
                            
                        }
                    }
                }

            }
            foreach (var item in ShoppingBasketDiscountLeftOver)
            {
                total += item.Amount * item.Price;
            }
            return total;

/*
            decimal total = 0;
            foreach (var item in this.ShoppingBasket)
            {
                total += item.Price * item.Amount;
            }
            return total;*/

        }

        public void AddFilling(ShopItem Bagel, string Filling)
        {
            
            foreach(ShopItem item in inventory.InventoryList)
            {
                if (item.SKU == Filling && Bagel.Name == "Bagel")
                {
                    ShoppingBasket.FirstOrDefault(Bagel).Extras.Add(item);
                }
            }

        }

      

        public List<ShopItem> ShoppingBasket { get; set; } = new List<ShopItem>();
        public List<ShopItem> ShoppingBasketDiscountLeftOver { get; set; } = new List<ShopItem>();

        public int ShoppingBasketMax { get; set; } = 4;

    }

    
}
