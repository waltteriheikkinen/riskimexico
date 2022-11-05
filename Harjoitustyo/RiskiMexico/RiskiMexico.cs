using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using Silk.NET.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace RiskiMexico
{
    public class RiskiMexico : PhysicsGame
    {
        private PhysicsObject noppa1;
        private PhysicsObject noppa2;
        private PhysicsObject osoitin;
        private IntMeter mexicot;
        private IntMeter heittoja;
        private IntMeter jakovirheet;
        private int silmaluku1;
        private int silmaluku2;
        public override void Begin()
        {
            Camera.ZoomToLevel();
            AsetaOhjaimet();
            LuoLaskurit();
            LuoHeittoMittari();
        }


        public void LuoHeittoMittari()
        {
            LuoPalkki(0, Color.Lime);
            LuoPalkki(-100, Color.Yellow);
            LuoPalkki(-200, Color.Orange);
            LuoPalkki(-300, Color.Red);
            LuoPalkki(100, Color.Yellow);
            LuoPalkki(200, Color.Orange);
            LuoPalkki(300, Color.Red);

            osoitin = new PhysicsObject(25, 50);
            osoitin.IgnoresCollisionResponse = true;
            osoitin.Y = Level.Bottom + 100;
            osoitin.Color = Color.Black;
            osoitin.Oscillate(Vector.UnitX, 337.5, 0.2);
            this.Add(osoitin);
        }

        public PhysicsObject LuoPalkki(double x, Color vari)
        {
            PhysicsObject mittari = new PhysicsObject(100, 50);
            mittari.Y = Level.Bottom + 100;
            mittari.X = x;
            mittari.IgnoresCollisionResponse = true;
            mittari.Color = vari;
            this.Add(mittari);
            return mittari;
        }



        public void AsetaOhjaimet()
        {
            Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
            Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "N‰yt‰ ohjeet");
            Keyboard.Listen(Key.Space, ButtonState.Pressed, HeitaNoppaa, "Heit‰ noppaa");
        }


        public void LuoLaskurit()
        {
            mexicot = LuoPistelaskuri("Mexicot: ", Screen.Left + 1.0 / 4 * Screen.Width);
            heittoja = LuoPistelaskuri("Heittoja: ", 0);
            jakovirheet = LuoPistelaskuri("Jakovirheet: ", Screen.Right - 1.0 / 4 * Screen.Width);
        }


        public IntMeter LuoPistelaskuri(string otsikko, double sijainti)
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


        public void HeitaNoppaa()
        {
            silmaluku1 = RandomGen.NextInt(1, 7);
            silmaluku2 = RandomGen.NextInt(1, 7);
            noppa1 = LuoNoppa(silmaluku1, Screen.Right - 200, 75);
            noppa2 = LuoNoppa(silmaluku2, Screen.Right - 200, -75);


            if (-50 < osoitin.X && osoitin.X < 50)
            {
                LyoNoppaa(-12000, -10000, 10000);
            }
            if ((-150 <= osoitin.X && osoitin.X <= - 50) || (50 <= osoitin.X && osoitin.X <= 150))
            {
                LyoNoppaa(-20000, -15000, 15000);
            }
            if ((-250 <= osoitin.X && osoitin.X <= -150) || (150 <= osoitin.X && osoitin.X <= 250))
            {
                LyoNoppaa(-30000, -15000, 15000);
            }
            if ((-350 <= osoitin.X && osoitin.X <= -250) || (250 <= osoitin.X && osoitin.X <= 350))
            {
                LyoNoppaa(-40000, -15000, 15000);
            }           
            //noppa1.Hit(new Vector(0, 13000)); T‰‰ menee yli yl‰reunast
            noppa1.AngularVelocity = RandomGen.NextInt(-7, 7);
            noppa2.AngularVelocity = RandomGen.NextInt(-7, 7);

            Timer.SingleShot(2.0, NopatPysahtynyt);

        }


        public void NopatPysahtynyt()
        {
            int vanhatjakovirheet = jakovirheet.Value;
            if (noppa1.X < Screen.Left || noppa1.Y > Screen.Top || noppa1.Y < Screen.Bottom)
                jakovirheet.Value++;
            if (noppa2.X < Screen.Left || noppa2.Y > Screen.Top || noppa2.Y < Screen.Bottom)
                jakovirheet.Value++;
            if (jakovirheet.Value >= 3) JakovirheRankku();

            if(vanhatjakovirheet == jakovirheet.Value) PaivitaLaskurit();

            noppa1.Hit(new Vector(0, -50000));
            noppa2.Hit(new Vector(0, -50000));
        }


        public void JakovirheRankku()
        {
            jakovirheet.Value = jakovirheet.Value - 3;
            Label rankku = new Label("Jakovirheet t‰ynn‰! Suorita rangaistus.");
            rankku.TextColor = Color.White;
            rankku.Color = Color.Black;
            rankku.Y = 100;
            rankku.LifetimeLeft = TimeSpan.FromSeconds(5.0);
            Add(rankku);
        }
        public void PaivitaLaskurit()
        {
            //TODO: kolme samaa lukua putkeen! T‰‰ vois menn‰ taulukolla.
            if (heittoja.Value == 0)
            {
                if (silmaluku1 + silmaluku2 == 3)
                {
                    MexicoTuuletus();
                    LopetaPeli();
                }
                if (silmaluku1 == silmaluku2)
                {
                    heittoja.Value += silmaluku1;
                }
                return;
            }

            if (silmaluku1 + silmaluku2 == 3)
            {
                MexicoTuuletus();
            }

            if (silmaluku1 == silmaluku2)
            {
                heittoja.Value += silmaluku1;
            }

            heittoja.Value -= 1;

            if (heittoja.Value == 0)
            {
                LopetaPeli();
            }
        }


        public void LyoNoppaa(int voima, int tuurimin, int tuurimax)
        {
            noppa1.Hit(new Vector(voima, 0));
            noppa2.Hit(new Vector(voima, 0));
            noppa1.Hit(new Vector(RandomGen.NextInt(tuurimin, tuurimax), RandomGen.NextInt(tuurimin, tuurimax)));
            noppa2.Hit(new Vector(RandomGen.NextInt(tuurimin, tuurimax), RandomGen.NextInt(tuurimin, tuurimax)));
        }


        public void MexicoTuuletus()
        {
            mexicot.Value++;
            Label arriva = new Label("ARRIVAA! Juo shotti :)");
            arriva.TextColor = Color.White;
            arriva.Color = Color.Black;
            arriva.Y = 100;
            arriva.LifetimeLeft = TimeSpan.FromSeconds(5.0);
            Add(arriva);
        }


        public void LopetaPeli()
        {
            Label loppu = new Label($"Peli P‰‰ttyi! Heitit {mexicot.Value} mexicoa!");
            loppu.TextColor = Color.White;
            loppu.Color = Color.Black;
            loppu.LifetimeLeft = TimeSpan.FromSeconds(2.0);
            Add(loppu);
            mexicot.Value = 0;
            jakovirheet.Value = 0;
        }

        /// <summary>
        /// Aliohjelmassa luodaan annettujen parametrien perusteella kaksi noppaa
        /// </summary>
        /// <param name="koko">Nopan sivun pituus</param>
        /// <param name="silmaluku">Nopan osoittama silmaluku</param>
        /// <returns></returns>
        public PhysicsObject LuoNoppa(int silmaluku, double x, double y)
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

            if (silmaluku == 1)
            {
                PhysicsObject silma1 = LuoSilma(koko, x, y);
                noppa.Add(silma1);
            }
            if (silmaluku == 2)
            {
                PhysicsObject silma1 = LuoSilma(koko, x + 1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma1);

                PhysicsObject silma2 = LuoSilma(koko, x + -1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma2);
            }
            if (silmaluku == 3)
            {
                PhysicsObject silma1 = LuoSilma(koko, x + 1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma1);

                PhysicsObject silma2 = LuoSilma(koko, x + -1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma2);

                PhysicsObject silma3 = LuoSilma(koko, x, y);
                noppa.Add(silma3);
            }
            if (silmaluku == 4)
            {
                PhysicsObject silma1 = LuoSilma(koko, x + 1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma1);

                PhysicsObject silma2 = LuoSilma(koko, x + -1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma2);

                PhysicsObject silma3 = LuoSilma(koko, x + 1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma3);

                PhysicsObject silma4 = LuoSilma(koko, x + -1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma4);
            }
            if (silmaluku == 5)
            {
                PhysicsObject silma1 = LuoSilma(koko, x + 1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma1);

                PhysicsObject silma2 = LuoSilma(koko, x + -1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma2);

                PhysicsObject silma3 = LuoSilma(koko, x + 1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma3);

                PhysicsObject silma4 = LuoSilma(koko, x + -1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma4);
                
                PhysicsObject silma5 = LuoSilma(koko, x, y);
                noppa.Add(silma5);
            }
            if (silmaluku == 6)
            {
                PhysicsObject silma1 = LuoSilma(koko, x + 1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma1);

                PhysicsObject silma2 = LuoSilma(koko, x + -1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma2);

                PhysicsObject silma3 = LuoSilma(koko, x + 1.0 / 4 * koko, y + -1.0 / 4 * koko);
                noppa.Add(silma3);

                PhysicsObject silma4 = LuoSilma(koko, x + -1.0 / 4 * koko, y + 1.0 / 4 * koko);
                noppa.Add(silma4);

                PhysicsObject silma5 = LuoSilma(koko, x, y + 1.0 / 4 * koko);
                noppa.Add(silma5);

                PhysicsObject silma6 = LuoSilma(koko, x, y + -1.0 / 4 * koko);
                noppa.Add(silma6);
            }
            return noppa;
        }


        /// <summary>
        /// Aliohjelmassa luodaan pallo nopan silm‰kukuja varten
        /// </summary>
        /// <param name="koko">vakio joka m‰‰ritt‰‰ silm‰luvun muodostavien pallojen koon</param>
        /// <returns>palauttaa pallon</returns>
        public PhysicsObject LuoSilma(int koko, double x, double y)
        {
            PhysicsObject silma = new PhysicsObject(koko / 5.0, koko / 5.0, Shape.Circle);
            silma.Color = Color.Red;
            Add(silma);
            silma.X = x;
            silma.Y = y;
            return silma;
        }

        
    } 

}
