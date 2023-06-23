using System;

namespace _01._Class_Box_Data
{
    public class Box
    {
        private double lenght;
        private double width;
        private double height;

        public Box(double lenght, double width, double height)
        {
            Lenght = lenght;
            Width = width;
            Height = height;
        }

        public double Lenght
        {
            get => lenght;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(String.Format(ExceptionalMessagecs.BoxParameterZeroOrNegative, nameof(this.Lenght)));
                }
                this.lenght = value;
            }
        }
        public double Width
        {
            get => width;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(String.Format(ExceptionalMessagecs.BoxParameterZeroOrNegative, nameof(this.Width)));
                }
                this.width = value;
            }
        }
        public double Height
        {
            get => height;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(String.Format(ExceptionalMessagecs.BoxParameterZeroOrNegative, nameof(this.Height)));
                }
                this.height = value;
            }
        }
        public double SurfaceArea() => 2 * Lenght * Width + LateralSurfaceArea();
        public double LateralSurfaceArea() => 2 * Lenght * Height + 2 * Width * Height;
        public double Volume() => Lenght * Width * Height;
    }
}