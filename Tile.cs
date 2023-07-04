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
        int x;
        int y;

        public Tile(int x, int y)
        {
            face = "##";
            this.x = x;
            this.y = y;
        }

        public string getFace() { return face; }
        public void SetFace(string face) { this.face = face; }
        public void SetFace()
        {
            if (isPath) { this.face = "[]"; }
            else { this.face = "##"; }
        }

        public void SetPath(bool x)
        { 
            this.isPath = x;
            this.SetFace();
        }
    }
}
