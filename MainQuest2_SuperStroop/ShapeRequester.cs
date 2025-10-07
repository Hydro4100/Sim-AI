using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace MainQuest2_SuperStroop
{
    internal class ShapeRequester
    {
        private Color[] _colours;
        private StroopShape[] _shapes;
        private string[] _colourNames;
        private Random _random = new Random();
        private int _shapeIndex;

        private Color _colour;
        private StroopShape _shape;
        private string _colourName;
        private Color _displayColour;

        public ShapeRequester(StroopShape[] shapes, Color[] colours, string[] colourNames)
        {
            _colours = colours;
            _shapes = shapes;
            _colourNames = colourNames;
        }

        public void GetNewRequest()
        {
            int index = _random.Next(_colours.Length);
            _colour = _colours[index];
            _colourName = _colourNames[index];
            _displayColour = _colours[_random.Next(_colourNames.Length)];
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

        public string ColourName
        {
            get
            {
                return _colourName;
            }
        }

        public Color DisplayColour
        {
            get
            {
                return _displayColour;
            }
        }
    }
}
