﻿namespace CodeFood.Data
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImagePath { get; set; }
    }
}
