﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerce.Shared.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool Visible { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        public bool Editing { get; set; } = false;

        [NotMapped]
        public bool IsNew { get; set; } = false;


    }
}
