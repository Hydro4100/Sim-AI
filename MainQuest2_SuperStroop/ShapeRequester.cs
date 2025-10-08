using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MainQuest2_SuperStroop
{
    internal class ShapeRequester
    {
        private Color[] _colours;
        private List<StroopShape> _shapes;
        private string[] _colourNames;
        private Random _random = new Random();

        private Color _colour;
        private StroopShape _shape;
        private string _colourName;
        private Color _displayColour;

        public ShapeRequester(List<StroopShape> shapes, Color[] colours, string[] colourNames)
        {
            _colours = colours;
            _shapes = shapes;
            _colourNames = colourNames;
        }

        public void GetNewRequest()
        {
            int index = _random.Next(_colours.Length);
            _shape = _shapes[_random.Next(_shapes.Count)];
            _colour = _shape.Colour;
            for (int i = 0; i < _colours.Length; i++)
            {
                if (_colours[i] == _shape.Colour)
                {
                    index = i;
                    break;
                }
            }
            _colourName = _colourNames[index];
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

        public string ColourName
        {
            get
            {
                return _colourName;
            }
        }
    }
}
