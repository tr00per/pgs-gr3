﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class SerwerProtocol
    {

        static int offset = 0;

        int nickLenght = 16;
        private byte[] tablica;



        public SerwerProtocol()
        {
            offset = 0;

            tablica = new byte[255];
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
        public void addProtocolType(byte type)
        {
            offset = 0;
            byte[] tab = BitConverter.GetBytes(type);
            tab.CopyTo(tablica, offset);
            offset+=2;
        }

        /// <summary>
        /// funckja dodająca w drugim bicie protokołu wartość check sumy z całego protokołu
        /// </summary>
        /// <param name="sum">wartość sumy</param>
        public void addCheckSum(byte sum)
        {
            offset = 1;
            byte[] tab = BitConverter.GetBytes(sum);
            byte[] helpTab = new byte[1];
            helpTab[0] = tab[0];
            helpTab.CopyTo(tablica, offset);
            offset++;
        }

        /// <summary>
        /// funkcja dodająca do protokołu ilość graczy biorących udział w rozgrywce
        /// </summary>
        /// <param name="number">ilość graczy</param>
        public void addNumberOfPlayers(byte number)
        {
            byte[] tab = BitConverter.GetBytes(number);
            tab.CopyTo(tablica, offset);
            offset++;
        }



        public void addPlayerPosition(float x, float y)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 4;

            byte[] tab2 = BitConverter.GetBytes(y);
            tab2.CopyTo(tablica, offset);
            offset += 4;
        }

        public void addCursorPosition(float x, float y)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 4;

            byte[] tab2 = BitConverter.GetBytes(y);
            tab2.CopyTo(tablica, offset);
            offset += 4;

        }

        public void addPlayerHealth(short x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset+=2;
        }

        public void addPlayerNick(String s)
        {
            byte[] tab = Encoding.ASCII.GetBytes(s);
            tab.CopyTo(tablica, offset);
            offset += nickLenght;


        }

        public void addPlayerID(byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void addPlayerAvatar(byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void addPlayerPoints(short x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 2;
        }

        public void addRoundNumber(short x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 2;
        }

        public void addPlayerDeadNumber(short x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 2;
        }

        public void addSerwerInformation(String s)
        {
            byte[] tab = Encoding.ASCII.GetBytes(s);
            tab.CopyTo(tablica, offset);
            offset += s.Length;
        }

        /// <summary>
        /// funkcja dodająca do protokołu ilość pocisków aktualnie znajdujących się na mapie
        /// </summary>
        /// <param name="number">ilość pocisków</param>
        public void addNumberOfShots(byte number)
        {
            byte[] tab = BitConverter.GetBytes(number);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void addWeaponType(byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void addShotType(byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void addShotID(byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void addShotOwnerID(byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void addShotPosition(float x, float y)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset += 4;

            byte[] tab2 = BitConverter.GetBytes(y);
            tab2.CopyTo(tablica, offset);
            offset += 4;

        }


        public void unpack(byte[] tresc)
        {
            calculateCheckSum(tresc);

            byte typ = tresc[0];

            switch (typ)
            {
                case 0:
                    {
                        //otrzymano potwierdzenie od klienta, serwer kontynuuję prace

                        break;
                    }
                case 1:
                    {
                        // otrzymano negatywną odpowiedź od klienta, należy wysłac pakiet ponownie

                        break;
                    }
                case 2:
                    {
                        //wiadomość od klienta, który własnie się dołączył

                        //pobranie nazwy nowego gracza
                        String nick = Encoding.ASCII.GetString(tresc, 2, nickLenght);

                        //pobranie numeru avatara jaki wybral gracz
                        byte typAvatara = tresc[18];

                        // w odpowiedz serwer powienien wysłać potwierdzenie odbioru pakietu
                        // a nastepnie wysłać pakiet z przypisanym ID gracza
                        // i otrzymać kolejne potwierdzenie od klienta


                        break;
                    }
                case 3:
                    {
                        //serwer nie powinien otrzymywać  od klienta pakietów typu 3
                        //w takim wypadku należy albo zignorować pakiet albo zignorować pakiet :D

                        break;
                    }
                case 4:
                    {
                        //serwer otrzymuję od klienta informację w czasie trwania rozgrywki

                        //pobranie ID gracza
                        byte id_gracza = tresc[2];

                        //pobranie pozycji x i y gracza
                        Vector2 pozycja_gracza = new Vector2(BitConverter.ToSingle(tresc, 3), BitConverter.ToSingle(tresc, 7));

                        //pobranie pozycji x i y kursora gracza
                        Vector2 pozycja_kursora = new Vector2(BitConverter.ToSingle(tresc, 11), BitConverter.ToSingle(tresc, 15));

                        

                        //pobranie ilości pocisków
                        byte ilośćPocisków = tresc[19];

                        //pobranie typu pocisków
                        byte typPocisku = tresc[20];

                        break;
                    }
                case 5:
                    {
                        //serwer otrzymał od klienta informację o rezygnacji z gry więc powinien go usunąć

                        break;
                    }
                case 6:
                    {
                        //serwer nie powinien otrzymywać  od klienta pakietów typu 6
                        //w takim wypadku należy albo zignorować pakiet albo zignorować pakiet :D

                        break;
                    }
                default: break;
            }


        }
        
        /// <summary>
        /// Funkcja tworzaca pakiety typu 0-1, czyli potwierdzenia odbioru innych pakietow
        /// </summary>
        /// <param name="type">typ pakietu</param>
        public void createPackage(byte typ)
        {
            switch (typ)
            {

                case 0:
                    {
                        tablica = new byte[2];
                        addProtocolType(typ);
                        addCheckSum(0);
                        break;
                    }
                case 1:
                    {
                        tablica = new byte[2];
                        addProtocolType(typ);
                        addCheckSum(1);
                        break;
                    }
                default: break;
            }

        }
        /// <summary>
        /// Funkcja tworzy pakiety typu 2-5 
        /// </summary>
        /// <param name="listGracz">lista graczy biorocych udzial w rundzie</param>
        /// <param name="listPocisk">lista pociskow znajdujacych sie na mapie</param>
        /// <param name="typ">typ pakietu</param>
        /// <param name="nrRundy">aktulany numer rundy</param>
        public void createPackage(List<Gracz> listGracz, List<Pocisk> listPocisk, byte typ, byte nrRundy)
        {

            switch (typ)
            {
                
                case 2:
                    {
                        tablica = new byte[4];
                        addProtocolType(typ);
                        addPlayerID(listGracz.Last().getID);
                        addCheckSum(calculateCheckSum(tablica));
                        break;
                    }
                case 3:
                    {

                        tablica = new byte[5+30*listGracz.Count];
                        addProtocolType(typ);
                        addNumberOfPlayers((byte)listGracz.Count);

                        for (int i = 0; i < listGracz.Count; i++)
                        {
                            addPlayerID(listGracz.ElementAt(i).getID);
                            addPlayerNick(listGracz.ElementAt(i).getNick);
                            addPlayerAvatar(listGracz.ElementAt(i).getTypAvatara);
                            addPlayerPosition(listGracz.ElementAt(i).getPozycja.X, listGracz.ElementAt(i).getPozycja.Y);
                            addPlayerPoints(listGracz.ElementAt(i).getPunkty);
                            addPlayerDeadNumber(listGracz.ElementAt(i).getIloscZgonow);
                        }

                        addRoundNumber(nrRundy);

                        addCheckSum(calculateCheckSum(tablica));

                        break;
                    }
                case 4:
                    {
                        tablica = new byte[6 + 21 * listGracz.Count + 11*listPocisk.Count];
                        addProtocolType(typ);
                        addNumberOfPlayers((byte)listGracz.Count);

                        for (int i = 0; i < listGracz.Count; i++)
                        {
                            addPlayerID(listGracz.ElementAt(i).getID);
                            addPlayerPosition(listGracz.ElementAt(i).getPozycja.X, listGracz.ElementAt(i).getPozycja.Y);
                            addCursorPosition(listGracz.ElementAt(i).getPozycjaKursora.X, listGracz.ElementAt(i).getPozycjaKursora.Y);
                            addPlayerHealth(listGracz.ElementAt(i).getZycie);
                            addWeaponType(listGracz.ElementAt(i).getAktualnaBron.getTypBroni);
                        }

                        addNumberOfShots((byte)listPocisk.Count);

                        for (int i = 0; i < listPocisk.Count; i++)
                        {
                            addShotID(listPocisk.ElementAt(i).getID);
                            addShotOwnerID(listPocisk.ElementAt(i).getIDOwnera);
                            addShotType(listPocisk.First().getTypPocisku);
                            addShotPosition(listPocisk.ElementAt(i).getPozycja.X, listPocisk.ElementAt(i).getPozycja.Y);

                        }
                        addCheckSum(calculateCheckSum(tablica));

                        break;
                    }
                case 5:
                    {
                        tablica = new byte[2];
                        addProtocolType(typ);
                        addCheckSum(5);
                        break;
                    }
                
                default: break;
            }
        }
        /// <summary>
        /// Funkcja tworzy pakiety typu 6 - zawierające wiadomosci systemowe dla klienta
        /// </summary>
        /// <param name="wiadomosc">tresc wiadomosci</param>
        public void createPackage(String wiadomosc)
        {
            tablica = new byte[4 + wiadomosc.Length];
            addProtocolType(6);
            addSerwerInformation(wiadomosc);

            addCheckSum(calculateCheckSum(tablica));

        }

        /// <summary>
        /// Funkcja obliczajaca Check sume z pakietu jako sume jego wszystkich bitow
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public byte calculateCheckSum(byte[] tab)
        {
            byte suma = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                suma += tab[i];
            }
            return suma;
        }

        /// <summary>
        /// Funkcja sprawdzajaca poprawnosc pakietu na podstawie jego check sumy
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public bool CheckValueOfSum(byte[] tab)
        {
            byte suma = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                suma += tab[i];
            }
            suma -= tab[1];

            if (tab[1] == suma)
                return true;
            else
                return false;

        }







        #region GET - SET

        public byte[] getTablica
        {
            get { return tablica; }
        }

        #endregion 

    }
}
