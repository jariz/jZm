using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    /// <summary>
    /// Represents a game object in the memory
    /// Provides several helper functions.
    /// </summary>
    public class RemoteObject
    {
        /// <summary>
        /// Current offset of the reader.
        /// </summary>
        protected int Position;

        /// <summary>
        /// The associated <see cref="RemoteMemory"/> object
        /// </summary>
        protected RemoteMemory Mem;

        /// <summary>
        /// The current offset from the start of the object
        /// </summary>
        protected int Offset;
        
        /// <summary>
        /// The current offset from the beginning
        /// </summary>
        protected int BaseOffset;

        /// <summary>
        /// Add xxx bytes to the current position
        /// </summary>
        /// <param name="amount">The amount of bytes to skip</param>
        protected void Move(int amount)
        {
            Position += amount;
            Offset = Position - BaseOffset;
        }

        /// <summary>
        /// Move 4 bytes
        /// </summary>
        /// <returns>The current address</returns>
        protected int aInt()
        {
            Position += 4;
            return imove(4);
        }

        /// <summary>
        /// Move 8 bytes
        /// </summary>
        /// <returns>The current address</returns>
        protected int aLong()
        {
            Position += 8;
            return imove(8);
        }

        /// <summary>
        /// Move 4 * VecType bytes
        /// </summary>
        /// <param name="type">Vector type (2, 3, 4)</param>
        /// <returns>The current address</returns>
        protected int aVec(int type)
        {
            Position += 4 * type;
            return imove(4 * type);
        }

        /// <summary>
        /// Move 2 bytes
        /// </summary>
        /// <returns>The current address</returns>
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
        /// <summary>
        /// Just a object that you can asso
        /// </summary>
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
