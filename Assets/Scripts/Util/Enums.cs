namespace Assets.Scripts.Util
{
    public class Enums
    {
        public enum Direction { Up, Down, Left, Right, None };
        public enum CardTypes { SwordVert, SwordHori, NaginataVert, NaginataHori, HammerVert, HammerHori, ThrowLight, ThrowMid, Shoot, ChiAttack, ChiStationary, Error, Fan, Kanobo, Tanto, Wakizashi, Tonfa, BoStaff };
        public enum FieldType { Blue, Red, Destroyed };
        public enum PlayerState
        {
            Idle = 0, MoveBegining, MoveEnding, Hit, Dead, BasicAttack, HoriSwingMid, VertiSwingHeavy, ThrowLight, ThrowMid, Shoot, ChiAttack, ChiStationary,
            TauntGokuStretch, TauntPointPoint, TauntThumbsDown, TauntWrasslemania, TauntYaMoves
        };
        public enum GameStates { Battle, CardSelection };
		public enum Element { Fire, Earth, Thunder, Water, Wood, None };
    }
}
