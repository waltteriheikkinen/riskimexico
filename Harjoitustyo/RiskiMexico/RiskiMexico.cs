using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

namespace RiskiMexico
{
    public class RiskiMexico : PhysicsGame
    {
        public override void Begin()
        {
            LuoAlusta();
            PhysicsObject noppa1 = LuoNoppa(100, RandomGen.NextInt(1,6));
            noppa1.X = 200;
            noppa1.Y = 200;
            PhysicsObject noppa2 = LuoNoppa(100, RandomGen.NextInt(1, 6));

            noppa1.Hit(RandomGen.NextVector(0, 10000));
            noppa2.Hit(RandomGen.NextVector(0, 10000));


            PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
            Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        }

        void LuoAlusta()
        {
            Camera.ZoomToLevel();
            Level.CreateBorders();
        }

        PhysicsObject LuoNoppa(int koko, int silmaluku)
        {
            PhysicsObject noppa = new PhysicsObject(koko, koko, Shape.Rectangle);
            noppa.Color = Color.White;
            noppa.KineticFriction = 0.8;
            noppa.Restitution = 0.5;
            noppa.LinearDamping = 0.8;
            noppa.AngularDamping = 0.8;
            this.Add(noppa);

            if(silmaluku == 1)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
            }
            if (silmaluku == 2)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = 1.0 / 4 * koko;
                silma1.Y = 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = -1.0 / 4 * koko;
                silma2.Y = -1.0 / 4 * koko;
            }
            if (silmaluku == 3)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = 1.0 / 4 * koko;
                silma1.Y = 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = -1.0 / 4 * koko;
                silma2.Y = -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
            }
            if (silmaluku == 4)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = 1.0 / 4 * koko;
                silma1.Y = 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = -1.0 / 4 * koko;
                silma2.Y = -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
                silma3.X = 1.0 / 4 * koko;
                silma3.Y = -1.0 / 4 * koko;

                PhysicsObject silma4 = LuoSilma(koko);
                noppa.Add(silma4);
                silma4.X = -1.0 / 4 * koko;
                silma4.Y = 1.0 / 4 * koko;
            }
            if (silmaluku == 5)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = 1.0 / 4 * koko;
                silma1.Y = 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = -1.0 / 4 * koko;
                silma2.Y = -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
                silma3.X = 1.0 / 4 * koko;
                silma3.Y = -1.0 / 4 * koko;

                PhysicsObject silma4 = LuoSilma(koko);
                noppa.Add(silma4);
                silma4.X = -1.0 / 4 * koko;
                silma4.Y = 1.0 / 4 * koko;
                PhysicsObject silma5 = LuoSilma(koko);
                noppa.Add(silma5);
            }
            if (silmaluku == 6)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = 1.0 / 4 * koko;
                silma1.Y = 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = -1.0 / 4 * koko;
                silma2.Y = -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
                silma3.X = 1.0 / 4 * koko;
                silma3.Y = -1.0 / 4 * koko;

                PhysicsObject silma4 = LuoSilma(koko);
                noppa.Add(silma4);
                silma4.X = -1.0 / 4 * koko;
                silma4.Y = 1.0 / 4 * koko;

                PhysicsObject silma5 = LuoSilma(koko);
                noppa.Add(silma5);
                silma5.Y = 1.0 / 4 * koko;

                PhysicsObject silma6 = LuoSilma(koko);
                noppa.Add(silma6);
                silma6.Y = -1.0 / 4 * koko;
            }
            return noppa;
        }

        PhysicsObject LuoSilma(int koko)
        {
            PhysicsObject silma = new PhysicsObject(koko / 5.0, koko / 5.0, Shape.Circle);
            silma.Color = Color.Red;
            Add(silma);
            return silma;
        }

        
    } 

}
