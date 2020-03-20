using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPLesPizzas2.Models
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }


        public List<Pate> pates { get; set; } = new List<Pate>();
        public int selectedPate { get; set; }
        public List<int> selectedIngredients { get; set; } = new List<int>();

        public List<Ingredient> ingredients = new List<Ingredient>();

        public void setIngredients(List<Ingredient> ingredients)
        {
            this.ingredients = ingredients;
        }

        public void setPates(List<Pate> pates)
        {
            this.pates = pates;
        }
    }
}