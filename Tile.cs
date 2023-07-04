using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    internal class Tile
    {
        public string face;
        public bool isPath;
        public byte x { get; private set; }
        public byte y { get; private set; }

        public Tile(byte x, byte y)
        {
            face = "##";
            this.x = x;
            this.y = y;
        }

        public void SetFace(string face) { this.face = face; }
        public void SetPath(bool x) { this.isPath = x; }
        public (byte,byte) GetCoords() { return (x,y); }
    }
}
