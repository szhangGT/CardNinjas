using Assets.Scripts.Util;

namespace Assets.Scripts.Player
{
    /* This file controls all of the transitions between states*/
    class PlayerStateMachine
    {
        private delegate Enums.PlayerState machine(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber);//function pointer
        private machine[] getNextState;//array of function pointers
        private Enums.PlayerState currState;
        private static float hold = 0;//used for delays
        private static bool die = false;

        public PlayerStateMachine()
        {
            currState = Enums.PlayerState.Idle;
            //fill array with functions
            getNextState = new machine[] { Idle, MoveBegining, MoveEnding, Hit, Dead, BasicAttack, HoriSwingMid, VertiSwingHeavy, ThrowLight, ThrowMid, Shoot, ChiAttack, ChiStaionary,
                                           TauntGokuStretch, TauntPointPoint, TauntThumbsDown, TauntWrasslemania, TauntYaMoves };
        }

        public Enums.PlayerState update(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            currState = getNextState[((int)currState)](hit, animDone, direction, type, handEmpty, playerNumber);//gets te next Enums.PlayerState
            return currState;
        }


        //The following methods control when and how you can transition between states

        private static Enums.PlayerState Idle(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (!handEmpty && CustomInput.BoolFreshPress(CustomInput.UserInput.UseCard, playerNumber))
            {
                switch (type)
                {
                    case Enums.CardTypes.SwordVert: return Enums.PlayerState.VertiSwingHeavy;
                    case Enums.CardTypes.SwordHori: return Enums.PlayerState.HoriSwingMid;
                    case Enums.CardTypes.NaginataVert: return Enums.PlayerState.VertiSwingHeavy;
                    case Enums.CardTypes.NaginataHori: return Enums.PlayerState.HoriSwingMid;
                    case Enums.CardTypes.ThrowLight: return Enums.PlayerState.ThrowLight;
                    case Enums.CardTypes.ThrowMid: return Enums.PlayerState.ThrowMid;
                    case Enums.CardTypes.Shoot: return Enums.PlayerState.Shoot;
                    case Enums.CardTypes.ChiAttack: return Enums.PlayerState.ChiAttack;
                    case Enums.CardTypes.ChiStationary: return Enums.PlayerState.ChiStationary;
                    case Enums.CardTypes.Error: return Enums.PlayerState.ChiAttack;
                }
            }
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack, playerNumber))
                return Enums.PlayerState.BasicAttack;
            if (direction != Enums.Direction.None)
                return Enums.PlayerState.MoveBegining;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Taunt, playerNumber))
            {
                float chance = UnityEngine.Random.Range(0f, 1f);
                if (chance < .225f)
                    return Enums.PlayerState.TauntPointPoint;
                else if (chance < .45f)
                    return Enums.PlayerState.TauntThumbsDown;
                else if (chance < .675f)
                    return Enums.PlayerState.TauntWrasslemania;
                else if (chance < .9f)
                    return Enums.PlayerState.TauntYaMoves;
                else
                    return Enums.PlayerState.TauntGokuStretch;
            }
            return Enums.PlayerState.Idle;
        }

        private static Enums.PlayerState MoveBegining(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.MoveEnding;
            return Enums.PlayerState.MoveBegining;
        }

        private static Enums.PlayerState MoveEnding(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.MoveEnding;
        }

        private static Enums.PlayerState Hit(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .4f)
            {
                hold = 0;
                if (die)
                    return Enums.PlayerState.Dead;
                return Enums.PlayerState.Idle;
            }
            return Enums.PlayerState.Hit;
        }

        //this is used to prevent the player character from doing any thing while dead
        private static Enums.PlayerState Dead(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            return Enums.PlayerState.Dead;
        }

        private static Enums.PlayerState BasicAttack(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack, playerNumber))
                hold = 1;
            if (animDone)
            {
                if (hold == 0)
                    return Enums.PlayerState.Idle;
                hold = 0;
                return Enums.PlayerState.BasicAttack;
            }
            return Enums.PlayerState.BasicAttack;
        }

        private static Enums.PlayerState HoriSwingMid(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.HoriSwingMid;
        }
        private static Enums.PlayerState VertiSwingHeavy(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.VertiSwingHeavy;
        }
        private static Enums.PlayerState ThrowLight(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.ThrowLight;
        }
        private static Enums.PlayerState ThrowMid(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.ThrowMid;
        }
        private static Enums.PlayerState Shoot(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.Shoot;
        }
        private static Enums.PlayerState ChiAttack(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.ChiAttack;
        }
        private static Enums.PlayerState ChiStaionary(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.ChiStationary;
        }
        private static Enums.PlayerState TauntGokuStretch(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.TauntGokuStretch;
        }
        private static Enums.PlayerState TauntPointPoint(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.TauntPointPoint;
        }
        private static Enums.PlayerState TauntThumbsDown(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.TauntThumbsDown;
        }
        private static Enums.PlayerState TauntWrasslemania(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.TauntWrasslemania;
        }
        private static Enums.PlayerState TauntYaMoves(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty, int playerNumber)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.TauntYaMoves;
        }

        internal void Die()
        {
            die = true;
        }

        internal void Revive()
        {
            currState = Enums.PlayerState.Idle;
            die = false;
        }
    }
}
