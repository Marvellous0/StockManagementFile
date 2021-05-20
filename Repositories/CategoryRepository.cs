using System;
using System.Collections.Generic;
using Entities;
using System.IO;

namespace Repositories
{
    public class CategoryRepository
    {
        public List<Category> Categories = new List<Category>();

        public CategoryRepository()
        {
            FetchCategoryFromFile ();      
        }

        public void FetchCategoryFromFile()
        {
            try
            {
                var categoriesLines = File.ReadAllLines("files//category.txt");
                foreach (var categoryLine in categoriesLines)
                {
                    var category = Category.StringToCategory(categoryLine);
                    Categories.Add(category);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void PrintCategory (Category category)
        {
            Console.WriteLine($"{category.Id} {category.Name} {category.CreatedAt}");
        }

        public void GetCategories()
        {
            foreach (Category category in Categories)
            {
                PrintCategory(category);
            }
        }
        public void AddCategory(int id, string name)
        {
            var categoryExist = FindCategory(id);
            if (categoryExist != null)
            {
                Console.WriteLine($"category with {id} already exist");
            }
            Category category = new Category(id, name);
            Categories.Add(category);
            TextWriter writer = new StreamWriter ("category.txt", true);
            writer.WriteLine(category.ToString());
            Console.WriteLine("Category added successfully!");
            writer.Close();
        }

        public void UpdateCategory(int id, string name)
        {
            var category = FindCategory(id);
            if (category == null)
            {
                Console.WriteLine($"category with {id} does not exist");
            }
            category.Name = name;
            RefreshFile();
        }

        public void RefreshFile()
        {
            TextWriter writer = new StreamWriter ("category.txt");
            foreach(var category in Categories)
            {
                writer.WriteLine(category);
            }
            writer.Flush();
            writer.Close();
        }

        public void DeleteCategory(int id)
        {
            var category = FindCategory(id);
            if (category == null)
            {
                Console.WriteLine($"category with {id} does not exist");
            }
            Categories.Remove(category);
            RefreshFile();
        }

        public Category FindCategory (int id)
        {
            return Categories.Find(c => c.Id == id);
        }

        public List<Category> ListAllCategories()
        {
            return Categories;
        }
    }
}