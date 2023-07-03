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
        

        public Tile()
        {
            face = "##";
            
        }

        public string getFace() { return face; }
        public void setFace(string face) { this.face = face; }

    }
}
