﻿using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Models
{
    public class ProductModel : EntityBase
    {
        public string Description { get; set; }
        public decimal SellValue { get; set; }
        public int Stock { get; set; }
    }
}
