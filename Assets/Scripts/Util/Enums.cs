namespace Assets.Scripts.Util
{
    public class Enums
    {
        public enum Direction { Up, Down, Left, Right, None };
        public enum CardTypes { SwordVert, SwordHori, NaginataVert, NaginataHori, ThrowLight, ThrowMid, Shoot, ChiAttack, ChiStationary, Error };
        public enum FieldType { Blue, Red, Destroyed };
        public enum PlayerState
        {
            Idle = 0, MoveBegining, MoveEnding, Hit, Dead, BasicAttack, HoriSwingMid, VertiSwingHeavy, ThrowLight, ThrowMid, Shoot, ChiAttack, ChiStationary,
            TauntGokuStretch, TauntPointPoint, TauntThumbsDown, TauntWrasslemania, TauntYaMoves
        };
        public enum GameStates { Battle, CardSelection };
    }
}
