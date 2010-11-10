using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class ClientProtokol
    {


        static int offset = 0;

        int nickLenght = 16;
        private byte[] tablica;

        public ClientProtokol()
        {
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

        public void addShotType(byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tab.CopyTo(tablica, offset);
            offset++;
        }

        public void unpack(byte[] tresc)
        {
            if (CheckValueOfSum(tresc))
            {

                byte typ = tresc[0];

                switch (typ)
                {
                    case 0:
                        {
                            //otrzymano potwierdzenie od serwera,

                            break;
                        }
                    case 1:
                        {
                            // otrzymano negatywną odpowiedź od serwera, należy wysłac pakiet ponownie

                            break;
                        }
                    case 2:
                        {
                            //wiadomość od serwera z ID przypisanym do gracza



                            //pobranie ID_gracza
                            byte id_gracza = tresc[2];

                            // w odpowiedz serwer powienien wysłać potwierdzenie odbioru pakietu
                            // a nastepnie wysłać pakiet z przypisanym ID gracza
                            // i otrzymać kolejne potwierdzenie od klienta


                            break;
                        }
                    case 3:
                        {
                            // klient otrzymuje od serwera informację o wszystkich graczach biorących udział w grze
                            // na początku każdej rundy
                            // po otrzymaniu pakietu klient odsyła potwierdzenie
                            offset = 2;

                            byte iloscGraczy = tresc[offset];
                            offset++;

                            for (int i = 0; i < iloscGraczy; i++)
                            {
                                //pobranie ID gracza
                                byte gracz_ID = tresc[offset];
                                offset++;
                                //pobranie nicku gracza
                                String nick = Encoding.ASCII.GetString(tresc, offset, nickLenght);
                                offset += nickLenght;
                                //pobranie typu avatara
                                byte typAvatara = tresc[offset];
                                offset++;
                                //pobranie pozycji startowej
                                float x = BitConverter.ToSingle(tresc, offset);
                                offset += 4;
                                float y = BitConverter.ToSingle(tresc, offset);
                                offset += 4;
                                //pobranie ilosci punktow gracza
                                short punkty = BitConverter.ToInt16(tresc, offset);
                                offset += 2;
                                //pobranie ilości zgonów gracza
                                short smierci = BitConverter.ToInt16(tresc, offset);
                                offset += 2;
                            }
                            //pobranie numeru aktualnej rundy
                            short nrRundy = BitConverter.ToInt16(tresc, offset);

                            break;
                        }
                    case 4:
                        {
                            //klient otrzymuję od serwera informację w czasie trwania rozgrywki

                            offset = 2;

                            byte iloscGraczy = tresc[offset];
                            offset++;

                            for (int i = 0; i < iloscGraczy; i++)
                            {
                                //pobranie ID gracza
                                byte gracz_ID = tresc[offset];
                                offset++;

                                //pobranie pozycji startowej
                                float x = BitConverter.ToSingle(tresc, offset);
                                offset += 4;
                                float y = BitConverter.ToSingle(tresc, offset);
                                offset += 4;

                                //pobranie pozycji kursora
                                float xKursora = BitConverter.ToSingle(tresc, offset);
                                offset += 4;
                                float yKursora = BitConverter.ToSingle(tresc, offset);
                                offset += 4;

                                //pobranie ilości życia gracza
                                short życie = tresc[offset];
                                offset+=2;

                                byte typBroni = tresc[offset];
                                offset++;

                            }

                            //pobranie informacje o pociskach aktulanie znajdujących się na mapie
                            short iloscPocisków = BitConverter.ToInt16(tresc, offset);
                            offset++;

                            if (iloscPocisków > 0)
                            {
                                for (int i = 0; i < iloscPocisków; i++)
                                {
                                    //pobranie ID pocisku
                                    byte pocisk_ID = tresc[offset];
                                    offset++;
                                    //pobranie ID gracza, który wystrzelił pocisk
                                    byte pociskOwner = tresc[offset];
                                    offset++;
                                    //pobranie typu pocisku
                                    byte pociskType = tresc[offset];
                                    offset++;

                                    //pobranie pozycji pocisku
                                    float x = BitConverter.ToSingle(tresc, offset);
                                    offset += 4;
                                    float y = BitConverter.ToSingle(tresc, offset);
                                    offset += 4;
                                }
                            }

                            break;
                        }
                    case 5:
                        {
                            //klient otrzymał od serwera pakiet oznaczający koniec gry

                            break;
                        }
                    case 6:
                        {
                            //klient otrzymał od serwera jakąś wiadomość, którą należy wyświetlic na ekranie/konsoli

                            offset = 2;

                            String s = Encoding.ASCII.GetString(tresc, offset, tresc.Length - 2);

                            break;
                        }
                    default: break;
                }
            }
            else
            {

                //otrzymany pakiet zawiera błędy, nie zgadza sie check suma

            }

        }

        public void createPackage(Gracz gracz, byte typ)
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
                    case 2:
                        {
                            tablica = new byte[20];
                            addProtocolType(typ);
                            addPlayerNick(gracz.getNick);
                            addPlayerAvatar(gracz.getTypAvatara);
                            addCheckSum(calculateCheckSum(tablica));
                            break;
                        }
                    case 3:
                        {
                           
                            break;
                        }
                    case 4:
                        {
                            tablica = new byte[22];
                            addProtocolType(typ);
                            addPlayerID(gracz.getID);
                            addPlayerPosition(gracz.getPozycja.X, gracz.getPozycja.Y);
                            addCursorPosition(gracz.getPozycjaKursora.X, gracz.getPozycjaKursora.Y);
                            if(gracz.getListaPociskow.Count>0)
                            {
                                addNumberOfShots((byte)gracz.getListaPociskow.Count);
                                addShotType(gracz.getListaPociskow.First().getTypPocisku);
                                
                            }
                            else
                            {
                                addNumberOfShots(0);
                                addShotType(0);
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
                    case 6:
                        {
                            break;
                        }
                    default: break;
                }



        }

        public byte calculateCheckSum(byte[] tab)
        {
            byte suma = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                suma += tab[i];
            }
            return suma;
        }


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
