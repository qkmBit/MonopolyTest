using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTest
{
    internal class Box
    {
        private int _id;
        private double _length;
        private double _width;
        private double _height;
        private double _weight;
        private Pallet _pallet;
        private DateOnly manufactureDate;
        public DateOnly ExpirationDate
        {
            get { return manufactureDate.AddDays(100); }
        }
        public double Weight
        {
            get { return _weight; }
        }
        public double Height
        {
            get { return _height; }
        }
        public double Length
        {
            get { return _length; }
        }
        public double Width
        {
            get { return _width; }
        }
        public double Volume
        {
            get { return _width * _height * _length; }
        }
        public Box(int id, double length, double width, double height, double weight, string dateString, Pallet pallet) 
        {
            _id = id;
            _width = width;
            _height = height;
            _length = length;
            _weight = weight;
            _pallet = pallet;
            _pallet.AddBox(this);
            manufactureDate = DateOnly.Parse(dateString);
        }

    }
}
