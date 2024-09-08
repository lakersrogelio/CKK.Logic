using CKK.Logic.Exceptions;
using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Product : Entity
    {
        //public decimal Price { get; set; }
        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid price: " + value);
                }
                _price = value;
            }
        }
    }
}

/* private int _id;
 private string? _name;
 private decimal _price;


public int Id { get; set; }
public string Name { get; set; }
public decimal Price { get; set; }

public int GetId()
{
    return Id;
}
public void SetId(int id)
{
    Id = id;
}

public string? GetName()
{
    return Name;
}

public void SetName(string name)
{
    Name = name;
}

public decimal GetPrice()
{
    return Price;

}
public void SetPrice(decimal price)
{
    Price = price;
}*/