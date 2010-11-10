using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void bonusFunction(Bonus bon)
        {
            switch (bon.typBon)
            {
                case typBonusu.apteczka:
                    {
                        Game.zawodnik.getZycie += 40;
                        break;
                    }
                case typBonusu.bron:
                    {
                        switch (bon.getID)
                        {
                            case 0:
                                {
                                    Game.zawodnik.getAktualnaBron = new Bron("Fireball", 30, 0, 20);
                                    break;
                                }
                            case 1:
                                {
                                    Game.zawodnik.getAktualnaBron = new Bron("Ice Arrow", 10, 1, 2);
                                    break;
                                }
                            case 2:
                                {
                                    Game.zawodnik.getAktualnaBron = new Bron("Thunder", 60, 2, 50);
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
