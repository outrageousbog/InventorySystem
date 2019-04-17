﻿using System;
namespace InventorySystem.Models
{
    public class ProductSku
    {
        private int mSKUID;
        private long mSKUValue;

        public ProductSku(int mSKUID, long mSKUValue)
        {
            this.mSKUID = mSKUID;
            this.mSKUValue = mSKUValue;
        }

        public int SKUID { get => mSKUID; set => mSKUID = value; }
        public long SKUValue { get => mSKUValue; set => mSKUValue = value; }
    }
}