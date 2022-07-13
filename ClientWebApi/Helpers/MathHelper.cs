using System;

namespace ClientWebApi.Helpers
{
    public static class MathHelper
    {
        public static int Min(int x, int y, int z)
        {
            return Math.Min(x, Math.Min(y, z));
        }
    }
}
