using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bgrimmettShoppingAppCSHTML.Models.CodeFirst
{
    public class Item
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string MediaURL { get; set; }
        public string Description { get; set; }
    }
}