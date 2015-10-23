using Assets.Scripts.Util;

namespace Assets.Scripts.Util
{
    public class Elements
    {
        private static float[,] damageValues = new float[,] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };

        public static float GetDamageMultiplier(Enums.Element me, Enums.Element them)
        {
            return damageValues[(int)me, (int)them];
        }
    }
}
