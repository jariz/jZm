using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    public class RemoteObject
    {
        protected int Position;
        protected RemoteMemory Mem;

        //debug purposes
        protected int Offset;
        protected int BaseOffset;

        protected void Move(int amount)
        {
            Position += amount;
            Offset = Position - BaseOffset;
        }

        protected int aInt()
        {
            Position += 4;
            return imove(4);
        }

        protected int aLong()
        {
            Position += 8;
            return imove(8);
        }

        protected int aVec(int type)
        {
            Position += 4 * type;
            return imove(4 * type);
        }

        protected int aShort()
        {
            Position += 2;
            return imove(2);
        }

        protected int aString()
        {
            Position += 16;
            return imove(16);
        }

        protected int imove(int z)
        {
            Offset = Position - BaseOffset;
            return Position - z;
        }

        object _state = null;
        public object State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
    }
}
