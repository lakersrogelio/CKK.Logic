using CKK.Logic.Exceptions;
//using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;


namespace CKK.Persistance
{
    public class FileStore :  ISavable, ILoadable
    {
        public string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
                       Path.DirectorySeparatorChar + "Persistance" + 
                       Path.DirectorySeparatorChar + "StoreItems.dat";
        
        private List<Product> Items;
        private int IdCounter = 1;
        public FileStore() 
        {
           
            Items = new List<Product>();
            CreatePath();
            Save();
            Load();
        }
        public StoreItem AddStoreItem(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new InventoryItemStockTooLowException("Inventory too low");
            }
            if (product.Id == 0)
            {
                product.Id = IdCounter++;
            }
            var existingItem = Items.FirstOrDefault(item => item.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity = existingItem.Quantity + quantity;
                return existingItem;
            }
            var newItem = new StoreItem(product, quantity);
            Items.Add(newItem);
            Save(); 
            return newItem;
        }
        public void DeleteStoreItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == id);
            if (item != null)
            {
                Items.Remove(item);
            }
            Save();
        }
        public StoreItem RemoveStoreItem(int productId, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("Quantity less than zero");
            }
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item == null)
            {
                throw new ProductDoesNotExistException("Product does not exist");
            }
            if (item.Quantity - quantity <= 0)
            {
                item.Quantity = 0;
            }
            else
            {
                item.Quantity = item.Quantity - quantity;
            }
            Save(); 
            return item;
        }
        public StoreItem? FindStoreItemById(int id)
        {
            if (id < 0)
            {
                throw new InvalidIdException("Id is invalid");
            }
            var item = Items.FirstOrDefault(item => item.Product.Id == id);
            if (item == null)
            {
                return null;
            }
            if (item.Quantity <= 0)
            {
                item.Quantity = 0;
            }
            return item;
        }
        public List<StoreItem> GetStoreItems()
        {
            return Items;
        }
        public void Save()
        {
                try
                {
                    using (Stream fileStream = File.OpenWrite(FilePath))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(fileStream, Items);
                    }
                    Console.WriteLine("Data saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving data: {ex.Message}");
                }
        }
        public void Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    using (Stream fileStream = File.OpenRead(FilePath))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        Items = formatter.Deserialize(fileStream) as List<StoreItem>;
                    }
                    Console.WriteLine("Data loaded successfully.");
                }
                else
                {
                    Console.WriteLine("File does not exist. No data loaded.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                Items = new List<StoreItem>();
            }
        }
        private void CreatePath()
        {
            try
            {
                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string persistenceFolder = Path.Combine(documentsFolder, "Persistance");

                if (!Directory.Exists(persistenceFolder))
                {
                    Directory.CreateDirectory(persistenceFolder);
                    Console.WriteLine("Created 'Persistance' folder.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating folder: {ex.Message}");
            }
        }
        public List<StoreItem> GetAllProductsByName(string name)
        {
            return Items.Where(item => item.Product.Name.Contains(name)).ToList();
           
        }

        public List<Product> GetProductsByQuantity()
        {

            for (int i = 0; i < Items.Count - 1; i++)
            {
                for (int j = 0; j < Items.Count - i - 1; j++)
                {
                    if (Items[j].Quantity < Items[j + 1].Quantity)
                    {
                        var temp = Items[j];
                        Items[j] = Items[j + 1];
                        Items[j + 1] = temp;
                    }
                }
            }
            return Items;

        }

        public List<StoreItem> GetProductsByPrice()
        {
            for (int i = 0; i < Items.Count - 1; i++)
            {
                for (int j = 0; j < Items.Count - i - 1; j++)
                {
                    if (Items[j].Product.Price < Items[j + 1].Product.Price)
                    {
                        var temp = Items[j];
                        Items[j] = Items[j + 1];
                        Items[j + 1] = temp;
                    }
                }
            }
            return Items;
        }
    }

}


            //return Items.OrderByDescending(item => item.Product.Price).ToList();



            //return Items.OrderByDescending(item => item.Quantity).ToList();



           


        


    
            






