using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTest
{
    internal class Warehouse
    {
        private List<Pallet> pallets = new List<Pallet>();
        public Warehouse() { }

        public void AddPallet(Pallet pallet)
        {
            pallets.Add(pallet);
        }
        public Pallet GetPalletById(int id)
        {
            return pallets.Where(pallet => pallet.ID == id).FirstOrDefault();
        }
        public string GetOrderedPallets()
        {
            string output = "";
            var query = pallets.GroupBy(pallet => pallet.ExpirationDate)
                .Select(group => 
                new
                {
                    ExpirationDate = group.Key,
                    Pallets = group.OrderBy(pallet => pallet.Weight)
                })
                .OrderBy(group => group.Pallets.First().ExpirationDate);
            foreach(var group in query) 
            { 
                output += $"{group.ExpirationDate}\n";
                foreach (var pallet in group.Pallets)
                {
                    output += $"\tID: {pallet.ID}\n\tОбъем: {pallet.Volume}\n\tВес:{pallet.Weight}\n\n";
                }
            }
            return output;
        }
        public string GetPalletesWithMostExpDate()
        {
            string output = "";
            var query = pallets.OrderByDescending(pallet => pallet.ExpirationDate).Take(3).OrderBy(pallet => pallet.Volume);
            foreach (var pallet in query)
            {
                output += $"\nID: {pallet.ID}\nОбъем: {pallet.Volume}\nВес:{pallet.Weight}\nСрок годности: {pallet.ExpirationDate}";
            }
            return output;
        }
    }
}
