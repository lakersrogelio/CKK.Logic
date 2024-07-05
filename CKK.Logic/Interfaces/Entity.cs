using CKK.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    [Serializable]
    public abstract class Entity
    {
        private int _id;
     public string Name { get; set; }
    
    

    public int Id
      {
        get { return _id; }
        set
        {
            if (value < 0)
            {
                throw new InvalidIdException("Invalid ID: " + value);
            }
            _id = value;
        }
      }
    }
    
}

        
        


