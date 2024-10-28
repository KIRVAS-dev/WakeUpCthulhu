using System;

namespace CthulhuGame
{
    public static class DropProbability
    {      
        public static int Value
        {
            get
            {
                Random random = new();
                int randomNumber = random.Next(101);
                return randomNumber;
            }
        }
    }
}