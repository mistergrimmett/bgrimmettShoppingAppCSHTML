using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bgrimmettShoppingAppCSHTML.Models.CodeFirst
{
    public class CartItem
    {
        public int Id { get; set;}
        public string CustomerId { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Item Item { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        //public decimal UnitTotal
        //{
        //    get
        //    {
        //        if(Item != null)
        //        { 
        //        return Count * Item.Price;
        //        }
        //        return 0;
        //    }
        //}
    }
}