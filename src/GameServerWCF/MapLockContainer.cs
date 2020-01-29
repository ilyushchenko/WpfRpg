using System;
using System.Threading;
using Interfaces;

namespace GameServerWCF
{
    internal class CMapLockContainer
    {
        private readonly Object[,] _locks;

        public CMapLockContainer(Int32 width, Int32 height)
        {
            _locks = new Object[width, height];
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; y++)
                {
                    _locks[x, y] = new Object();
                }
            }
        }
        
        public Boolean TryLock(SPoint position)
        {
            return Monitor.TryEnter(_locks[position.X, position.Y]);
        }

        public void Unlock(SPoint position)
        {
            Monitor.Exit(_locks[position.X, position.Y]);
        }
    }
}