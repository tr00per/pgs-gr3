using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class SerwerProtocol
    {

        //int offset = 0;

        int nickLenght = 16;
        //private byte[] tablica;



        public SerwerProtocol()
        {
            //offset = 0;

            //tablica = new byte[255];
        }
        /// <summary>
        /// Funkcja dodająca w pierwszym bicie protokołu wartość określającą jego typ, od którego zależy format pozostałych danych w protokole
        /// Typy protokołów
        /// Typ 0: potwierdzenie odbioru wiadomości od użytkownika, oprócz typu zawiera jedynie Checksume
        /// Typ 1: protokół zwracanany w momencie kiedy użytkownik nie otrzymał ważnego pakietu
        /// Typ 2: protokół wysyłany w momencie dołączania się nowego użytkownika:
        ///         - w wersji serwera zawiera kolejno pola: typ_protokolu, check_suma, ID_gracza
        ///         - w wersji klienta zawiera kolejno pola: typ_protokolu, check_suma, nick_gracza, wybrany_avatar
        ///        odbiory pakietów typu 2 wymagają wysłania potwierdzenia odbioru
        /// Typ 3: protokół wysyłany do wszystkich graczy na początku każdej rundy przez serwer
        ///         - zawiera kolejno pola: typ_protokolu, check_suma, ilosc_graczy,{ id_gracz, nick_gracza, avatar_gracza,
        ///                               pozycjaX_gracza, pozycjaY_gracza, punkty_gracza}(te zmienne występują x razy, gdzie x=ilosc_graczy), ilosc_smierci_gracza, numer_rundy  
        /// Typ 4 : protokół używany w trakcie rozgrywki do przesyłania informacji o graczach:
        ///         - w wersji klienta zawiera kolejno pola: typ_protokolu, check_suma, ID_gracza, pozycjaX_gracza, pozycjaY_gracza, pozycjaX_kursora, pozycjaY_kursora,typ_pocisku, ilosc_pociskow, 
        ///         - w wersji serwera zawiera kolejno pola: typ_protokolu, check_suma, ilosc_graczy,{ id_gracz, pozycjaX_gracza, pozycjaY_gracza, pozycjaX_kursora, pozycjaY_kursora, zycie_gracza}(te zmienne występują x razy, gdzie x=ilosc_graczy),
        ///                                                 ilosc_pociskow, {id_pocisku, id_ownera, typ_pocisku, pozycjaX_pocisku, pozycjaY_pocisku,}(te zmienne występują x razy, gdzie x=ilosc_pociskow)
        /// Typ 5 : protokół wysyłany w momencie zakończenia gry przez serwer lub kontrolowanego odłączenia się użytkownika
        ///         - zawiera kolejno pola: typ_protokolu, check_suma,
        /// Typ 6 : protokół używany do wysyłania tekstowych wiadomości od serwera do klientów
        ///         - zawiera kolejno pola typ_protokolu, check_suma, tresc,
        /// </summary>
        /// <param name="type"></param>
        //public void addProtocolType(byte type)
        //{
        //    offset = 0;
        //    byte[] tab = BitConverter.GetBytes(type);
        //    tab.CopyTo(tablica, offset);
        //    offset+=2;
        //}

        /// <summary>
        /// funckja dodająca w drugim bicie protokołu wartość check sumy z całego protokołu
        /// </summary>
        /// <param name="sum">wartość sumy</param>
        //public void addCheckSum(byte sum)
        //{
        //    offset = 1;
        //    byte[] tab = BitConverter.GetBytes(sum);
        //    byte[] helpTab = new byte[1];
        //    helpTab[0] = tab[0];
        //    helpTab.CopyTo(tablica, offset);
        //    offset++;
        //}

        /// <summary>
        /// funkcja dodająca do protokołu ilość graczy biorących udział w rozgrywce
        /// </summary>
        /// <param name="number">ilość graczy</param>
        public void addNumberOfPlayers(ref byte[] tablica, ref int offset, byte number)
        {
            byte[] tab = BitConverter.GetBytes(number);
            tablica[offset] = tab[0];
            offset++;
        }



        public void addPlayerPosition(ref byte[] tablica, ref int offset, float x, float y)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 4;

            byte[] tab2 = BitConverter.GetBytes(y);
            tab2.CopyTo(tablica, offset);
            offset += 4;
        }

        public void addCursorPosition(ref byte[] tablica, ref int offset, float x, float y)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 4;

            byte[] tab2 = BitConverter.GetBytes(y);
            tab2.CopyTo(tablica, offset);
            offset += 4;

        }

        public void addPlayerHealth(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addPlayerNick(ref byte[] tablica, ref int offset, String s)
        {
            byte[] tab = Encoding.ASCII.GetBytes(s);
            tab.CopyTo(tablica, offset);
            offset += nickLenght;


        }

        public void addPlayerID(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addPlayerAvatar(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addPlayerPoints(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset ++;
        }

        public void addRoundNumber(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset ++;
        }

        public void addPlayerDeadNumber(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset ++;
        }

        public void addSerwerInformation(ref byte[] tablica, ref int offset, String s)
        {
            byte[] tab = Encoding.ASCII.GetBytes(s);
            tab.CopyTo(tablica, offset);
            offset += s.Length;
        }

        public void addNumberOfBonus(ref byte[] tablica, ref int offset, byte number)
        {
            byte[] tab = BitConverter.GetBytes(number);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addBonusID(ref byte[] tablica, ref int offset, byte number)
        {
            byte[] tab = BitConverter.GetBytes(number);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addBonusFlag(ref byte[] tablica, ref int offset, byte number)
        {
            byte[] tab = BitConverter.GetBytes(number);
            tablica[offset] = tab[0];
            offset++;
        }
        /// <summary>
        /// funkcja dodająca do protokołu ilość pocisków aktualnie znajdujących się na mapie
        /// </summary>
        /// <param name="number">ilość pocisków</param>
        public void addNumberOfShots(ref byte[] tablica, ref int offset, byte number)
        {
            byte[] tab = BitConverter.GetBytes(number);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addWeaponType(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addShotType(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = new byte[1];
            tab[0] = x;
            tablica[offset] = tab[0];
            offset++;
        }

        public void addShotID(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }
        public void addShotHeadshot(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addShotOwnerID(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }

        public void addShotPosition(ref byte[] tablica, ref int offset, float x, float y)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 4;

            byte[] tab2 = BitConverter.GetBytes(y);
            tab2.CopyTo(tablica, offset);
            offset += 4;

        }


        public Gracz unpack(byte[] tresc)
        {

                        //serwer otrzymuję od klienta informację w czasie trwania rozgrywki
                        Gracz gracz;
           
                        //pobranie ID gracza
                        byte id_gracza = tresc[0];
                        //pobranie pozycji x i y gracza

                        float x = BitConverter.ToSingle(tresc, 1);
                        float y = BitConverter.ToSingle(tresc, 5);
                       // Vector2 pozycja_gracza = new Vector2(BitConverter.ToSingle(tresc, 3), BitConverter.ToSingle(tresc, 7));
                        gracz = new Gracz(x, y, id_gracza);


                        float xK = BitConverter.ToSingle(tresc, 9);
                        float yK = BitConverter.ToSingle(tresc, 13);
                        //pobranie pozycji x i y kursora gracza
                        //Vector2 pozycja_kursora = new Vector2(BitConverter.ToSingle(tresc, 11), BitConverter.ToSingle(tresc, 15));

                        //gracz.getPozycjaKursora = new Vector2(xK, yK);
                        gracz.getPozycjaKursora = moveCursor(x,xK,y,yK);

                       //pobranie ilości pocisków
                        byte ilośćPocisków = tresc[17];
                        byte typPocisku = tresc[18];

                        for (int i = 0; i < ilośćPocisków; i++)
                        {
                            gracz.getListaPociskow.Add(new Pocisk(x, y, gracz.getPozycjaKursora.X, gracz.getPozycjaKursora.Y, (byte)i, typPocisku, gracz.getID));

                        }
                        //pobranie typu pocisków
                        

                       return gracz;

        }

        /// <summary>
        /// Funkcja tworzy pakiety typu 2-5 
        /// </summary>
        /// <param name="listGracz">lista graczy biorocych udzial w rundzie</param>
        /// <param name="listPocisk">lista pociskow znajdujacych sie na mapie</param>
        /// <param name="typ">typ pakietu</param>
        /// <param name="nrRundy">aktulany numer rundy</param>
        public byte[] createPackage(List<Gracz> listGracz, List<Pocisk> listPocisk,List<Bonus> listBonus, byte typ, byte nrRundy)
        {
            byte[] tablica;
            int offset = 0;
            switch (typ)
            {

                case 8:
                    {

                        tablica = new byte[5+28*listGracz.Count];
                        //addProtocolType(typ);
                        offset = 0;
                        addNumberOfPlayers(ref tablica, ref offset, (byte)listGracz.Count);

                        for (int i = 0; i < listGracz.Count; i++)
                        {
                            addPlayerID(ref tablica, ref offset, listGracz.ElementAt(i).getID);
                            addPlayerNick(ref tablica, ref offset, listGracz.ElementAt(i).getNick);
                            addPlayerAvatar(ref tablica, ref offset, listGracz.ElementAt(i).getTypAvatara);
                            //addPlayerPosition(listGracz.ElementAt(i).getPozycja.X, listGracz.ElementAt(i).getPozycja.Y);
                            Random los= new Random();
                            float poz1 = 0;
                            float poz2 = 0;
                            switch (listGracz.ElementAt(i).getID)
                            {
                                case 1:
                                    {
                                        poz1 = (float)los.NextDouble() * 80 + 40;
                                        poz2 = (float)los.NextDouble() * 120 + 40;
                                        break;
                                    }
                                case 2:
                                    {
                                        poz1 = (float)los.NextDouble() * 100 + 100;
                                        poz2 = (float)los.NextDouble() * 100 + 840;
                                        break;
                                    }
                                case 3:
                                    {
                                        poz1 = (float)los.NextDouble() * 80 + 860;
                                        poz2 = (float)los.NextDouble() * 120 + 40;
                                        break;
                                    }
                                case 4:
                                    {
                                        poz1 = (float)los.NextDouble() * 120 + 800;
                                        poz2 = (float)los.NextDouble() * 160 + 800;
                                        break;
                                    }
                                default: break;
                            }
                           
                            addPlayerPosition(ref tablica, ref offset, poz1, poz2);

                            addPlayerPoints(ref tablica, ref offset, listGracz.ElementAt(i).getPunkty);
                            addPlayerDeadNumber(ref tablica, ref offset, listGracz.ElementAt(i).getIloscZgonow);
                        }

                        addRoundNumber(ref tablica, ref offset, nrRundy);

                       // addCheckSum(calculateCheckSum(tablica));
                       
                        return tablica;
                        
                    }
                case 16:
                    {
                        tablica = new byte[4 + 20 * listGracz.Count + 20*listPocisk.Count + 2 * listBonus.Count];

                        offset = 0;

                        addNumberOfPlayers(ref tablica, ref offset, (byte)listGracz.Count);

                        for (int i = 0; i < listGracz.Count; i++)
                        {
                            addPlayerID(ref tablica, ref offset, listGracz.ElementAt(i).getID);
                            addPlayerPosition(ref tablica, ref offset, listGracz.ElementAt(i).getPozycja.X, listGracz.ElementAt(i).getPozycja.Y);
                            addCursorPosition(ref tablica, ref offset, listGracz.ElementAt(i).getPozycjaKursora.X, listGracz.ElementAt(i).getPozycjaKursora.Y);
                            addPlayerHealth(ref tablica, ref offset, listGracz.ElementAt(i).getZycie);
                            addWeaponType(ref tablica, ref offset, listGracz.ElementAt(i).getAktualnaBron.getTypBroni);
                        }

                        addNumberOfShots(ref tablica, ref offset, (byte)listPocisk.Count);

                        for (int i = 0; i < listPocisk.Count; i++)
                        {
                            addShotID(ref tablica, ref offset, listPocisk.ElementAt(i).getID);
                            addShotOwnerID(ref tablica, ref offset, listPocisk.ElementAt(i).getIDOwnera);
                            addShotType(ref tablica, ref offset, listPocisk.ElementAt(i).getTypPocisku);
                            addShotHeadshot(ref tablica, ref offset, listPocisk.ElementAt(i).getTrafienie);
                            addShotPosition(ref tablica, ref offset, listPocisk.ElementAt(i).getPozycja.X, listPocisk.ElementAt(i).getPozycja.Y);
                            addCursorPosition(ref tablica, ref offset, listPocisk.ElementAt(i).getPozycjaKursora.X, listPocisk.ElementAt(i).getPozycjaKursora.Y);
                            
                        }
                        Game.serwer.removeBullets();

                        addNumberOfBonus(ref tablica, ref offset, (byte)listBonus.Count);
                        for (int i = 0; i < listBonus.Count; i++)
                        {
                            addBonusID(ref tablica, ref offset,listBonus.ElementAt(i).getID);
                            addBonusFlag(ref tablica, ref offset, listBonus.ElementAt(i).getCzyZlapany);
                        }

                       // addCheckSum(calculateCheckSum(tablica));
                       
                        return tablica;
                       
                    }


                default: return null;
            }
        }
        /// <summary>
        /// Funkcja tworzy pakiety typu 6 - zawierające wiadomosci systemowe dla klienta
        /// </summary>
        /// <param name="wiadomosc">tresc wiadomosci</param>
        public void createPackage(String wiadomosc)
        {
            byte[] tablica = new byte[4 + wiadomosc.Length];
            int offset = 0;
           // addProtocolType(6);
            addSerwerInformation(ref tablica, ref offset, wiadomosc);

            //addCheckSum(calculateCheckSum(tablica));

        }


        public Vector2 moveCursor(float x1, float x2 ,float y1, float y2)
        {
            float a, b;

            if (x2 - x1 != 0)
                a = (y2 - y1) / (x2 - x1);
            else
            {
                a = (y2 - y1 ) / (x2 - x1 + 0.001f);
                //return new Vector2(x2,y2);
            }


            b = y1 - a * x1;
            float xx = 0;
            float yy = 0;

            if (x1 < x2)
                xx = x2 * 100;
            else if (x2 < x1 && x2 > 0)
                xx = -20;
            else if (x1 == x2)
            {
                if (y2 < y1)
                {
                    yy = -20;
                    return new Vector2((yy - b) / a, yy);
                }
                else
                {
                    yy = 1100;
                    return new Vector2((yy - b) / a, yy);
                }

            }


            return new Vector2(xx,xx*a+b);

        }

    }
}
