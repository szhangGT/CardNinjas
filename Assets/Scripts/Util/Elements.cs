using Assets.Scripts.Util;

namespace Assets.Scripts.Util
{
    public class Elements
    {
        private static float[,] damageValues = new float[,] {
            //Fire,Earth,Thund,Water, Wood, None
            { .75f,  .5f,    1,  .25f,   2, 1 },    //Fire
            {    1, .75f,    2,  .5f, .25f, 1 },    //Earth
            {    1, .25f, .75f,    2,  .5f, 1 },    //Thunder
            {    2,    1, .25f, .75f,  .5f, 1 },    //Water
            {  .5f,    2,    1, .25f, .75f, 1 },    //Wood
            {    1,    1,    1,    1,    1, 1 }  }; //None

        public static float GetDamageMultiplier(Enums.Element me, Enums.Element them)
        {
            return damageValues[(int)me, (int)them];
        }
    }
}
