using System;

namespace GuitarTurner
{
    class Check
    {
        public static void IsTrue(bool criteria, string message)
        {
            if (criteria)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
