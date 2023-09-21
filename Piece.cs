using System;

namespace WpfApp1 
{
    public class Piece
    {
        public Piece()
        {

            public string name { get; set; }
            public string position { get; set; }
            public string colour { get; set; }

            public abstract void Piece(string name, string position, string colour)
            {
                this.name = name;
                this.position = position;
                this.colour = colour;
            }
        }
    }

}
