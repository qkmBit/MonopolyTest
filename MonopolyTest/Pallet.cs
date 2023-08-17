using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MonopolyTest
{
    internal class Pallet
    {
        private int _id;
        private double _length;
        private double _width;
        private double _height;
        private List<Box> boxes = new List<Box>();

        public DateOnly ExpirationDate
        {
            get
            {
                if(boxes.Count == 0) throw new Exception("Паллета не содержит коробок");
                else return boxes.Min(x => x.ExpirationDate);
            }
        }

        public double Weight
        {
            get
            {
                if (boxes.Count == 0) throw new Exception("Паллета не содержит коробок");
                else return boxes.Sum(x => x.Weight);
            }
        }

        public double Volume
        {
            get
            {
                double volume = _length * _height * _width;
                if (boxes.Count > 0) volume += boxes.Sum(x => x.Volume);
                return volume;
            }
        }

        public int ID { get => _id; }

        public Pallet(int id, double length, double width, double height) 
        { 
            _id = id;
            _width = width;
            _height = height;
            _length = length;
        }

        public void AddBox(Box box)
        {
            double availableWidth = _width - (boxes.Sum(x => x.Width) + box.Width);
            double availableLength = _length - (boxes.Sum(x => x.Length) + box.Length);
            if (availableWidth >= 0 && availableLength >= 0) boxes.Add(box);
            else Console.WriteLine("Коробка не влезает в паллету");
        }
    }
}
