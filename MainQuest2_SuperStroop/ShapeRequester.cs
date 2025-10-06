using Microsoft.Xna.Framework;
using System;

namespace MainQuest2_SuperStroop
{
    internal class ShapeRequester
    {
        private Color[] _colours;
        private StroopShape[] _shapes;
        private Random _random = new Random();
        private int _shapeIndex;

        private Color _colour;
        private StroopShape _shape;

        public ShapeRequester(StroopShape[] shapes, Color[] colours)
        {
            _colours = colours;
            _shapes = shapes;
        }

        public void GetNewRequest()
        {
            _colour = _colours[_random.Next(_colours.Length)];
            _shape = _shapes[_random.Next(_shapes.Length)];
        }

        public Color Colour
        {
            get
            {
                return _colour;
            }
        }

        public StroopShape StroopShape
        {
            get
            {
                return _shape;
            }
        }
    }
}
