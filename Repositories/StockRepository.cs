using System;
using System.Collections.Generic;
using Entities;
using System.IO;

namespace Repositories
{
    public class StockRepository
    {
        public List<Stock> Stocks = new List<Stock>();

        public StockRepository()
        {
            FetchStockFromFile();
        }

        public void FetchStockFromFile()
        {
            try
            {
                var stockLines = File.ReadAllLines("files//stock.txt");
                foreach (var stockLine in stockLines)
                {
                    var stock = Stock.StringToStock(stockLine);
                    Stocks.Add(stock);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void PrintStock(Stock stock)
        {
            Console.WriteLine($"Id: {stock.Id}, Created_At: {stock.CreatedAt}, Name: {stock.Name}, CostPrice: {stock.CostPrice}, Selling Price: {stock.SellingPrice}, SKU: {stock.SKU}, Variation: {stock.Variation}, Category Id: {stock.Category_Id}");
        }

        public void GetStocks()
        {
            foreach (Stock stock in Stocks)
            {
                PrintStock(stock);
            }
        }

        public List<Stock> ListAllStocks()
        {
            return Stocks;
        }


        public void AddStock(int id, string name, double costPrice, double sellingPrice, string sKU, int quantity, string variation, int category_Id)
        {
            var stockExist = FindStock(id);

            if (stockExist != null)
            {
                Console.WriteLine($"stock with {id} already exist");
            }

            Stock stock = new Stock(id, name, costPrice, sellingPrice, sKU, quantity, variation, category_Id);

            Stocks.Add(stock);

            TextWriter writer = new StreamWriter("Files//stock.txt", true);
            writer.WriteLine(stock.ToString());
            Console.WriteLine("Category added successfully!");
            writer.Close();
        }

        public void UpdateStock(int id, string name, double costPrice, double sellingPrice, string sKU, int quantity, string variation)
        {
            var category = FindStock(id);
            if (category == null)
            {
                Console.WriteLine($"stock with {id} does not exist");
            }
            category.Name = name;
            RefreshFile();
        }

        public void RefreshFile()
        {
            TextWriter writer = new StreamWriter("Files//stock.txt");
            foreach (var stock in Stocks)
            {
                writer.WriteLine(stock);
            }
            writer.Flush();
            writer.Close();
        }

        public void DeleteStock(int id)
        {
            var stock = FindStock(id);
            if (stock == null)
            {
                Console.WriteLine($"stock with {id} does not exist");
            }
            Stocks.Remove(stock);
            RefreshFile();
        }

        public Stock FindStock(int id)
        {
            return Stocks.Find(s => s.Id == id);
        }
        public void FindStockById()
        {
            Console.WriteLine("Enter the Id of Category you want to find: ");
            int id = int.Parse(Console.ReadLine());

            var stock = FindStock(id);

            if(stock == null)
            {
                Console.WriteLine($"Stock with Id \t {id} does not exist! ");
            }

            else
            {
                Console.WriteLine($"Id: {stock.Id} Name: {stock.Name} CostPrice: {stock.CostPrice} SellingPrice: {stock.SellingPrice} SKU: {stock.SKU} Quantity: {stock.Quantity} Variation: {stock.Variation} Category_Id: {stock.Category_Id}");
            }
        }
    }
}