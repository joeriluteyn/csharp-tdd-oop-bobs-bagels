﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace csharp_tdd_oop_bobs_bagels_Csharp_Classes
{
    public class ShopItem
    {
        private string _name;
        private decimal _price;
        private string _variant;
        private string _sku;
        private int _amount;
        private int _id;
        private decimal _costs;
        private decimal _discount;
        

        public string Name { get =>  _name; set { _name = value;} }
        public decimal Price { get => _price; set { _price = value;} }
        public string Variant { get => _variant; set { _variant = value;} }
        public string SKU { get => _sku; set { _sku = value; } }
        public int Amount { get => _amount; set { _amount = value;} }
        public int Id { get => _id; set { _id = value;} }
        public decimal Costs { get => _costs; set { _costs = value; } }
        public decimal Discount { get => _discount; set { _discount= value; } }



        public List<ShopItem> Extras = new List<ShopItem>();

        public ShopItem(string SKU, string Variant, decimal Price, string Name)
        {
            this.SKU = SKU;
            this.Price = Price;
            this.Name = Name;
            this.Variant = Variant;
            
        }

    }
}