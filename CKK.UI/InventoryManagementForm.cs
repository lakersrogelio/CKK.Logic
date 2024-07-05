using CKK.Logic.Models;
using CKK.Persistance;
using System;
using System.Collections.Generic;
using CKK.Logic.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace CKK.UI
{
    public partial class InventoryManagementForm : Form
    {
        private IStore Store;
        private List<StoreItem> allItems; // Your list of store items
        private ListView listViewItems;
        private TextBox textBoxSearch;
        private List<StoreItem> filteredItems;
       



        public InventoryManagementForm(IStore Store)
        {
            Store = new FileStore();
            InitializeComponent();
            InitializePictureBox();
            allItems = Store.GetStoreItems();
            InitializeListView();
            this.Text = "Inventory Management";
            this.Size = new System.Drawing.Size(800, 600);
            this.BackColor = Color.DarkOliveGreen;

            Label welcomeLabel = new Label();
            welcomeLabel.Text = "Welcome to Corey's Knick Knacks";
            welcomeLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            welcomeLabel.AutoSize = true;
            welcomeLabel.Location = new Point(160, 10);
            this.Controls.Add(welcomeLabel);

            LinkLabel homeLink = new LinkLabel();
            homeLink.Text = "Home";
            homeLink.Size = new Size(50, 40);
            homeLink.Location = new Point(10, 60);
            this.Controls.Add(homeLink);

            LinkLabel shopLink = new LinkLabel();
            shopLink.Text = "Shop";
            shopLink.Size = new Size(50, 50);
            shopLink.Location = new Point(60, 60);
            this.Controls.Add(shopLink);

            LinkLabel loginLink = new LinkLabel();
            loginLink.Text = "Login";
            loginLink.Size = new Size(50, 40);
            loginLink.Location = new Point(105, 60);
            this.Controls.Add(loginLink);

            Button viewItemsButton = new Button();
            viewItemsButton.Text = "View All Items";
            viewItemsButton.Size = new Size(80, 30);
            viewItemsButton.Location = new System.Drawing.Point(50, 200);
            viewItemsButton.Click += new EventHandler(ViewItemsButton_Click);
            viewItemsButton.BackColor = Color.White;
            this.Controls.Add(viewItemsButton);

            Button addItemButton = new Button();
            addItemButton.Text = "Add New Item";
            addItemButton.Size = new Size(80, 30);
            addItemButton.Location = new System.Drawing.Point(50, 250);
            addItemButton.Click += new EventHandler(AddItemButton_Click);
            addItemButton.BackColor = Color.White;
            this.Controls.Add(addItemButton);

            Button removeItemButton = new Button();
            removeItemButton.Text = "Remove Item";
            removeItemButton.Size = new Size(80, 30);
            removeItemButton.Location = new System.Drawing.Point(50, 300);
            removeItemButton.Click += new EventHandler(RemoveItemButton_Click);
            removeItemButton.BackColor = Color.White;
            this.Controls.Add(removeItemButton);
        }
        private void InitializePictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile("grocery_store.jpg");
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(400, 300);
            pictureBox.Location = new Point(200, 60);
            this.Controls.Add(pictureBox);
        }
        private void ViewItemsButton_Click(object sender, EventArgs e)
        {
            if (Store == null)
            {
                Store = new Store();
            }
            List<StoreItem> items = Store.GetStoreItems();
            string message = "Items in Store:\n Apples\n Bannanas\n Flowers\n Milk\n Bread\n Soda\n Ham\n Cheese\n";
            foreach (var item in items)
            {
                message += $"ID: {item.Product.Id}, Name: {item.Product.Name}, Quantity: {item.Quantity}\n";
            }
            MessageBox.Show(message);
        }
        private void AddItemButton_Click(object sender, EventArgs e)
        {
            if (Store == null)
            {
                Store = new Store();
            }
            Product product = new Product { Name = "New Product" };
            int quantity = 10;
            Store.AddStoreItem(product, quantity);
            MessageBox.Show("Item added successfully!");
        }
        private void RemoveItemButton_Click(object sender, EventArgs e)
        {
            if (Store == null)
            {
                Store = new Store();
            }
            int id = 1;
            var product = Store.GetStoreItems().FirstOrDefault(item => item.Product.Id == id);
            if (product != null)
            {
                Store.RemoveStoreItem(id, 5);
                MessageBox.Show("Item removed successfully!");
            }
            else
            {
                MessageBox.Show("Product does not exist.");
            }
        }
        private void InitializeListView()
        {
            listViewItems = new ListView();
            listViewItems.Columns.Add("Name");
            listViewItems.Columns.Add("Quantity");
            listViewItems.Location = new Point(20, 370);
            listViewItems.Size = new Size(185, 175);
            listViewItems.View = View.Details;

            
            string items = "\nApples\nBannanas\nFlowers\nMilk\nBread\nSoda\nHam\nCheese";
            string[] itemNames = items.Split('\n');
            Random random = new Random();

            foreach (var itemName in itemNames)
            {
                if (!string.IsNullOrWhiteSpace(itemName))
                {
                    int randomQuantity = random.Next(1, 11); // Generate a random quantity (adjust the range as needed)
                    ListViewItem listViewItem = new ListViewItem(itemName.Trim());
                    listViewItem.SubItems.Add(randomQuantity.ToString()); // Add the random quantity
                    listViewItems.Items.Add(listViewItem);
                }
            }
            // Now add items to the ListView
            foreach (var itemName in itemNames)
            {
                if (!string.IsNullOrWhiteSpace(itemName))
                {
                    ListViewItem listViewItem = new ListViewItem(itemName.Trim());
                    listViewItems.Items.Add(listViewItem);
                }
              
            }

            
            this.Controls.Add(listViewItems);
            textBoxSearch = new TextBox();
            textBoxSearch.Size = new Size(140, 20);
            textBoxSearch.Location = new Point(15, 140);
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            this.Controls.Add(textBoxSearch);
            LoadItems();
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBoxSearch.Text.ToLower();
            
            //listViewItems.Items.Clear();


            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                DisplayItems(allItems);
            }
            else
            {
                filteredItems = allItems.Where(item => item.Product.Name.ToLower().Contains(searchQuery)).ToList();
                DisplayItems(filteredItems);
            }
        }
        private void DisplayItems(List<StoreItem> items)
        {
            //listViewItems.Items.Clear()
            foreach (var item in items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Product.Name);
                listViewItem.SubItems.Add(item.Quantity.ToString());
                listViewItems.Items.Add(listViewItem);
            }
        }
        private void InventoryManagementForm_Load(object sender, EventArgs e)
        {
            foreach (var item in allItems)
            {
                ListViewItem listViewItem = new ListViewItem(item.Product.Name);
                listViewItem.SubItems.Add(item.Quantity.ToString());
                listViewItems.Items.Add(listViewItem);
            }
        }
        public List<StoreItem> GetAllProductsByName(string name)
        {
            return Store.GetStoreItems().Where(item => item.Product.Name.Contains(name)).ToList();
        }
        public List<StoreItem> GetProductsByQuantity()
        {
            return Store.GetStoreItems().OrderByDescending(item => item.Quantity).ToList();
        }
        public List<StoreItem> GetProductsByPrice()
        {
            return Store.GetStoreItems().OrderByDescending(item => item.Product.Price).ToList();
        }
        private void LoadItems()
        {

        }
        
    }
}



















