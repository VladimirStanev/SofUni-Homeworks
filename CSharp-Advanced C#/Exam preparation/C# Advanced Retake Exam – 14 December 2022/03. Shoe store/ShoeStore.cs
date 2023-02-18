using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoeStore
{
    public class ShoeStore
    {
        private List<Shoe> shoes;

        public ShoeStore(string name, int storageCapacity)
        {
            Name = name;
            StorageCapacity = storageCapacity;
            shoes = new List<Shoe>();
        }

        public string Name { get; set; }
        public int StorageCapacity { get; set; }
        public List<Shoe> Shoes { get { return shoes; } }

        int Count { get { return shoes.Count; } }
        public string AddShoe(Shoe shoe)
        {
            if(StorageCapacity > Count)
            {
                Shoes.Add(shoe);
                return $"Successfully added {shoe.Type} {shoe.Material} pair of shoesByType to the store.";
            }
            return "No more space in the storage room.";
        }
        public int RemoveShoes(string material)
        {
            int count = 0;
            foreach(Shoe shoe in Shoes)
            {
                if (shoe.Material == material)
                {
                    count++;                   
                }
            }
            Shoes.RemoveAll(x => x.Material == material);

            return count;
        }
        public List<Shoe> GetShoesByType(string type)
        {
            List<Shoe> shoesByType = new();
            foreach(Shoe shoe in Shoes)
            {
                if(shoe.Type.ToLower() == type.ToLower())
                {
                    shoesByType.Add(shoe);
                }
            }
            return shoesByType;
        }
        public Shoe GetShoeBySize(double size)
        {
            return Shoes.FirstOrDefault(x => x.Size == size);
        }
        public string StockList(double size, string type)
        {
            List<Shoe> shoeByType = GetShoesByType(type);
            Shoe shoe = shoeByType.FirstOrDefault(x => x.Size == size);

            StringBuilder sb = new();
            if(shoe == null)
            {
                return "No matches found!";
            }
            else
            {
                shoeByType.RemoveAll(x =>x.Size != size);
                sb.AppendLine($"Stock list for size {size} - {type} shoes:");
                foreach (Shoe pair in shoeByType)
                {
                    sb.AppendLine(pair.ToString());
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}
