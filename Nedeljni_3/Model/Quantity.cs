using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_3.Model
{
    class Quantity
    {
        public string name { get; set; }
        public double quantity { get; set; }

        public Quantity(string n, double q)
        {
            name = n;
            quantity = q;
        }

        public static Quantity calculateOneIngredient(tblIngredient ing, int recipePersons,int number)
        {
            string n = ing.name;
            double res = (double)ing.quantity / recipePersons * number;
            return new Quantity(n, res);
        }
    }
}
