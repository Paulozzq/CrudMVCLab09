using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudMVC.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime DateExpiration { get; set; }
    }
}