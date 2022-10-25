using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RiskiMexico
{
    public class RiskiMexico : PhysicsGame
    {
        private PhysicsObject noppa1;
        private PhysicsObject noppa2;
        private IntMeter mexicot;
        private IntMeter heittoja;
        //private IntMeter jakovirheet;
        public override void Begin()
        {
            Camera.ZoomToLevel();
            AsetaOhjaimet();
            LuoLaskurit();  
        }


        void NopatVitteeoon()
        {
            noppa1.Hit(new Vector(0, -50000));
            noppa2.Hit(new Vector(0, -50000));
        }

        void AsetaOhjaimet()
        {
            Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
            Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "N‰yt‰ ohjeet");
            Keyboard.Listen(Key.Space, ButtonState.Pressed, HeitaNoppaa, "Heit‰ noppaa");
            Keyboard.Listen(Key.Up, ButtonState.Pressed, UusiHeitto, "Valmistaudu uuteen heittoon");
        }


        void LuoLaskurit()
        {
            mexicot = LuoPistelaskuri("Mexicot: ", Screen.Left + 1.0 / 3 * Screen.Width);
            heittoja = LuoPistelaskuri("Heittoja: ", Screen.Left + 2.0 / 3 * Screen.Width);
            //jakovirheet = LuoPistelaskuri("Jakovirheet: ", Screen.Right - 200);
        }


        void UusiHeitto()
        {
            noppa1.Destroy();
        }


        IntMeter LuoPistelaskuri(string otsikko, double sijainti)
        {
            IntMeter pistelaskuri = new IntMeter(0);

            Label pistenaytto = new Label();
            pistenaytto.X = sijainti;
            pistenaytto.Y = Screen.Top - 100;
            pistenaytto.TextColor = Color.Black;
            pistenaytto.Color = Color.White;
            pistenaytto.Title = otsikko;
            pistenaytto.Font.Size = 35;
            pistenaytto.BorderColor = Color.Black;

            pistenaytto.BindTo(pistelaskuri);
            Add(pistenaytto);
            return pistelaskuri;
        }


        void HeitaNoppaa()
        {
            int silmaluku1 = RandomGen.NextInt(1, 7);
            int silmaluku2 = RandomGen.NextInt(1, 7);
            noppa1 = LuoNoppa(silmaluku1, Screen.Right - 200, 75);
            noppa2 = LuoNoppa(silmaluku2, Screen.Right - 200, -75);

            if (silmaluku1 + silmaluku2 == 3)
            {
                mexicot.Value++;
                Label arriva = new Label("ARRIVAA! Juo shotti :)");
                arriva.TextColor = Color.White;
                arriva.Color = Color.Black;
                arriva.Y = 100;
                arriva.LifetimeLeft = TimeSpan.FromSeconds(5.0);
                Add(arriva);
            }

            if(silmaluku1 == silmaluku2)
            {
                heittoja.Value += silmaluku1;
            }

            if (heittoja.Value != 0)
            {
                heittoja.Value -= 1;
                if (heittoja.Value == 0)
                {
                    Label loppu = new Label($"Peli P‰‰ttyi! Heitit {mexicot.Value} mexicoa!");
                    loppu.TextColor = Color.White;
                    loppu.Color = Color.Black;
                    loppu.LifetimeLeft = TimeSpan.FromSeconds(2.0);
                    Add(loppu);
                    mexicot.Value = 0;
                }
            }
            Timer.SingleShot(2.0, NopatVitteeoon);
        }

        /// <summary>
        /// Aliohjelmassa luodaan annettujen parametrien perusteella kaksi noppaa
        /// </summary>
        /// <param name="koko">Nopan sivun pituus</param>
        /// <param name="silmaluku">Nopan osoittama silmaluku</param>
        /// <returns></returns>
        PhysicsObject LuoNoppa(int silmaluku, double x, double y)
        {
            int koko = 100;
            PhysicsObject noppa = new PhysicsObject(koko, koko, Shape.Rectangle);
            noppa.Color = Color.White;
            noppa.X = x;
            noppa.Y = y;
            noppa.KineticFriction = 0.6;
            noppa.Restitution = 0.5;
            noppa.LinearDamping = 3;
            noppa.AngularDamping = 1;
            this.Add(noppa);

            noppa.Hit(new Vector(-8000, 0));
            noppa.Hit(RandomGen.NextVector(0, 1000));
            noppa.AngularVelocity = RandomGen.NextInt(-5, 5);

            if (silmaluku == 1)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = x;
                silma1.Y = y;
            }
            if (silmaluku == 2)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = x + 1.0 / 4 * koko;
                silma1.Y = y + 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = x + -1.0 / 4 * koko;
                silma2.Y = y + -1.0 / 4 * koko;
            }
            if (silmaluku == 3)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = x + 1.0 / 4 * koko;
                silma1.Y = y + 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = x + -1.0 / 4 * koko;
                silma2.Y = y + -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
                silma3.X = x;
                silma3.Y = y;
            }
            if (silmaluku == 4)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = x + 1.0 / 4 * koko;
                silma1.Y = y + 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = x + -1.0 / 4 * koko;
                silma2.Y = y + -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
                silma3.X = x + 1.0 / 4 * koko;
                silma3.Y = y + -1.0 / 4 * koko;

                PhysicsObject silma4 = LuoSilma(koko);
                noppa.Add(silma4);
                silma4.X = x + -1.0 / 4 * koko;
                silma4.Y = y + 1.0 / 4 * koko;
            }
            if (silmaluku == 5)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = x + 1.0 / 4 * koko;
                silma1.Y = y + 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = x + -1.0 / 4 * koko;
                silma2.Y = y + -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
                silma3.X = x + 1.0 / 4 * koko;
                silma3.Y = y + -1.0 / 4 * koko;

                PhysicsObject silma4 = LuoSilma(koko);
                noppa.Add(silma4);
                silma4.X = x + -1.0 / 4 * koko;
                silma4.Y = y + 1.0 / 4 * koko;
                
                PhysicsObject silma5 = LuoSilma(koko);
                noppa.Add(silma5);
                silma5.X = x;
                silma5.Y = y;
            }
            if (silmaluku == 6)
            {
                PhysicsObject silma1 = LuoSilma(koko);
                noppa.Add(silma1);
                silma1.X = x + 1.0 / 4 * koko;
                silma1.Y = y + 1.0 / 4 * koko;

                PhysicsObject silma2 = LuoSilma(koko);
                noppa.Add(silma2);
                silma2.X = x + -1.0 / 4 * koko;
                silma2.Y = y + -1.0 / 4 * koko;

                PhysicsObject silma3 = LuoSilma(koko);
                noppa.Add(silma3);
                silma3.X = x + 1.0 / 4 * koko;
                silma3.Y = y + -1.0 / 4 * koko;

                PhysicsObject silma4 = LuoSilma(koko);
                noppa.Add(silma4);
                silma4.X = x + -1.0 / 4 * koko;
                silma4.Y = y + 1.0 / 4 * koko;

                PhysicsObject silma5 = LuoSilma(koko);
                noppa.Add(silma5);
                silma5.X = x;
                silma5.Y = y + 1.0 / 4 * koko;

                PhysicsObject silma6 = LuoSilma(koko);
                noppa.Add(silma6);
                silma6.X = x;
                silma6.Y = y + -1.0 / 4 * koko;
            }
            return noppa;
        }


        /// <summary>
        /// Aliohjelmassa luodaan pallo nopan silm‰kukuja varten
        /// </summary>
        /// <param name="koko">vakio joka m‰‰ritt‰‰ silm‰luvun muodostavien pallojen koon</param>
        /// <returns>palauttaa pallon</returns>
        PhysicsObject LuoSilma(int koko)
        {
            PhysicsObject silma = new PhysicsObject(koko / 5.0, koko / 5.0, Shape.Circle);
            silma.Color = Color.Red;
            Add(silma);
            return silma;
        }

        
    } 

}
