using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieLib
{
    public class RemoteObject
    {
        public int Position;
        public RemoteMemory Mem;

        //debug purposes
        public int Offset;
        public int BaseOffset;

        public void Move(int amount)
        {
            Position += amount;
            Offset = Position - BaseOffset;
        }

        public int aInt()
        {
            Position += 4;
            return imove(4);
        }

        public int aVec(int type)
        {
            Position += 4 * type;
            return imove(4 * type);
        }

        public int aShort()
        {
            Position += 2;
            return imove(2);
        }

        public int aString()
        {
            Position += 16;
            return imove(16);
        }

        int imove(int z)
        {
            Offset = Position - BaseOffset;
            return Position - z;
        }
    }
}
