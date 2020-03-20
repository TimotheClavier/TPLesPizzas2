using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPLesPizzas2.Models;

namespace TPLesPizzas2.Controllers
{
    public class PizzaController : Controller
    {

        private static List<Ingredient> ingredients = Pizza.IngredientsDisponibles;
        private static List<Pate> pates = Pizza.PatesDisponibles;

        private static List<Pizza> pizzas = new List<Pizza>();


        // GET: Pizza
        public ActionResult Index()
        {
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            return View(pizzas.FirstOrDefault(p => p.Id == id));
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
           
            var pizzaVM = new PizzaVM();
            pizzaVM.setPates(pates);
            pizzaVM.setIngredients(ingredients);
            return View(pizzaVM);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM pizzaVM)
        {
            var pizzaVMCatch = new PizzaVM();
            pizzaVMCatch.setPates(pates);
            pizzaVMCatch.setIngredients(ingredients);
            try
            {
                if (ModelState.IsValid)
                {
                    if (pizzas.Any(n => n.Nom.ToUpper() == pizzaVM.Pizza.Nom.ToUpper()))
                    {
                        ModelState.AddModelError("", "Il existe déja une pizza avec ce nom");
                        return View(pizzaVMCatch);
                    }
                    else
                    {

                        Pizza pizza = pizzaVM.Pizza;
                        pizza.Pate = pates.FirstOrDefault(p => p.Id == pizzaVM.selectedPate);
                        
                        if (pizzaVM.selectedIngredients.Count() < 2 || pizzaVM.selectedIngredients.Count() > 5)
                        {
                            ModelState.AddModelError("", "Une pizza doit avoir entre 2 et 5 ingrédients");
                            return View(pizzaVMCatch);
                        }
                        else
                        {
                            foreach (var ingredient in pizzaVM.selectedIngredients)
                            {
                                pizza.Ingredients.Add(ingredients.FirstOrDefault(i => i.Id == ingredient));
                            }
                            if (pizzas.Count(p => p.Ingredients == pizza.Ingredients) > 1)
                            {
                                ModelState.AddModelError("", "2 pizzas ou plus ne peuvent pas avoir les mêmes ingrédients");
                                return View(pizzaVMCatch);
                            }
                            else
                            {
                               
                                pizza.Id = pizzas.Count() + 1;
                                pizzas.Add(pizza);
                                return RedirectToAction("Index");
                        }

                    }
                    }
                   
                }
                return View(pizzaVMCatch);
                
                
            }
            catch
            {
                
                return View(pizzaVMCatch);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            var pizzaVM = new PizzaVM();
            pizzaVM.setIngredients(ingredients);
            pizzaVM.setPates(pates);
            pizzaVM.Pizza = pizzas.FirstOrDefault(p => p.Id == id);
            return View(pizzaVM);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaVM pizzaVM)
        {
            try
            {
                var item = pizzas.FirstOrDefault(p => p.Id == pizzaVM.Pizza.Id);
                item.Nom = pizzaVM.Pizza.Nom;
                item.Pate = pates.FirstOrDefault(p => p.Id == pizzaVM.selectedPate);
                item.Ingredients.Clear();
                foreach (var ingredient in pizzaVM.selectedIngredients)
                {
                    item.Ingredients.Add(ingredients.FirstOrDefault(i => i.Id == ingredient));
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View(pizzas.FirstOrDefault(p => p.Id == id));
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                pizzas.Remove(pizzas.FirstOrDefault(p => p.Id == id));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}