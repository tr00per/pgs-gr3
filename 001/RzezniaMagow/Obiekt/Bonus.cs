using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class Bonus : Obiekt
    {
        public enum typBonusu { apteczka, bron };

        private typBonusu typBon;
        

        


        public Bonus() : base()
        {
        }

        public Bonus(float x, float y, typBonusu bon): base(x, y)
        {
            typBon = bon;


        }

        //obsługa bonusów, przenieść potem do odpowiedniej klasy z logika gry

        public void bonusFunction(Bonus bon, Gracz gracz)
        {
            switch (bon.typBon)
            {
                case typBonusu.apteczka:
                    {
                        gracz.getZycie += 40;
                        break;
                    }
                case typBonusu.bron:
                    {
                        switch (bon.getID)
                        {
                            case 0:
                                {
                                    gracz.getAktualnaBron = new Bron("Fireball", 0, 20);
                                    break;
                                }
                            case 1:
                                {
                                    gracz.getAktualnaBron = new Bron("Ice Arrow", 1, 2);
                                    break;
                                }
                            case 2:
                                {
                                    gracz.getAktualnaBron = new Bron("Thunder", 2, 50);
                                    break;
                                }
                            default: break;
                        }
                        break;
                    }
            default: break;
            }
        }






    }
}
