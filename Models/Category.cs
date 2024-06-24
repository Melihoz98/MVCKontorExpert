using System;
using System.Collections.Generic;

namespace kontorExpert.Models
{
    public class Category
    {
        public Category() { }

        public Category(string name) 
        {
            CategoryName = name;
        }

        public Category(int id, string name) 
        {
            CategoryID = id;   
            CategoryName = name;
        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
