using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RzezniaMagow
{
    public class ClientProtokol
    {


        //int offset = 0;

        int nickLenght = 16;
        //private byte[] tablica;

        public ClientProtokol()
        {
           
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

        public void addShotType(ref byte[] tablica, ref int offset, byte x)
        {
            byte[] tab = BitConverter.GetBytes(x);
            tablica[offset] = tab[0];
            offset++;
        }

        public void unpack(byte[] tresc, int typ)
        {
            int offset = 0;
                switch (typ)
                {
                   
                    
                    case 8:
                        {
                            // klient otrzymuje od serwera informację o wszystkich graczach biorących udział w grze
                            // na początku każdej rundy
                            // po otrzymaniu pakietu klient odsyła potwierdzenie
                            Gracz gracz;
                            offset = 0;

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
                                
                                gracz = new Gracz(gracz_ID,nick,typAvatara);
                                gracz.getPozycja = new Vector2(x,y);

                                gracz.getPunkty = tresc[offset];
                                offset ++;

                                gracz.getIloscZgonow = tresc[offset];
                                offset ++;


                                Game.client.listaGraczy.Add(gracz);


                            }
                            //pobranie numeru aktualnej rundy
                            Game.client.getNrRundy = tresc[offset];

                           
                            break;
                        }
                    case 16:
                        {
                            //klient otrzymuję od serwera informację w czasie trwania rozgrywki

                            offset = 0;

                            byte iloscGraczy = tresc[offset];
                            offset++;

                           // byte[] listaIDgraczy = new byte[Game.client.listaGraczy.Count];

                            for (int m = 0; m < iloscGraczy; m++)
                            {
                                
                                //pobranie ID gracza
                                byte gracz_ID = tresc[offset];
                                offset++;
                                //listaIDgraczy[m] = gracz_ID;
                               

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
                                byte zycie = tresc[offset];
                                offset++;

                                byte typBroni = tresc[offset];
                                offset++;

                                for (int i = 0; i < Game.client.listaGraczy.Count; i++)
                                {
                                    if (Game.client.listaGraczy.ElementAt(i).getID == gracz_ID)
                                    {
                                        Game.client.listaGraczy.ElementAt(i).getPozycja = new Vector2(x, y);
                                        Game.client.listaGraczy.ElementAt(i).getPozycjaKursora = new Vector2(xKursora, yKursora);
                                        Game.client.listaGraczy.ElementAt(i).getZycie = zycie;
                                        Game.client.listaGraczy.ElementAt(i).getAktualnaBron.getTypBroni = typBroni;
                                    }
                                }
                            }

                            //if (iloscGraczy != Game.client.listaGraczy.Count)
                            //{
                            //    for (int i = Game.client.listaGraczy.Count; i > 0; i--)
                            //    {
                            //        bool flagaUsuniecia = true;
                            //        for (int j = 0; j < listaIDgraczy.Length; j++)
                            //        {
                            //            if(Game.client.listaGraczy.ElementAt(i).getID == listaIDgraczy[j])
                            //            flagaUsuniecia = false;
                            //        }
                            //        if (flagaUsuniecia)
                            //            Game.client.listaGraczy.RemoveAt(i);
                            //    }
                            //}


                            //pobranie informacje o pociskach aktulanie znajdujących się na mapie
                            byte iloscPocisków = tresc[offset];
                            offset++;
                            Game.client.listaPociskow = new List<Pocisk>();
                            if (iloscPocisków > 0)
                            {
                                //byte[] listaIDPociskow = new byte[iloscPocisków];

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

                                    Game.client.listaPociskow.Add(new Pocisk(x,y,pocisk_ID,pociskType,pociskOwner));

                                }
                            }


                                
                            
                           
                            break;
                        }
                   
                    case 64:
                        {
                            //klient otrzymał od serwera jakąś wiadomość, którą należy wyświetlic na ekranie/konsoli

                            offset = 0;

                            String s = Encoding.ASCII.GetString(tresc, offset, tresc.Length);

                            break;
                        }
                    default: break;
                }
            }

        public byte[] createPackage(Gracz gracz)
        {
            int offset = 0;
            byte[] tablica;
             
                   
                            tablica = new byte[20];
                            offset = 0;
                            
                            addPlayerID(ref tablica, ref offset, gracz.getID);
                            addPlayerPosition(ref tablica, ref offset, gracz.getPozycja.X, gracz.getPozycja.Y);
                            addCursorPosition(ref tablica, ref offset, gracz.getPozycjaKursora.X, gracz.getPozycjaKursora.Y);
                            if (gracz.getListaPociskow.Count > 0)
                            {
                                addNumberOfShots(ref tablica, ref offset,(byte)gracz.getListaPociskow.Count);
                                addShotType(ref tablica, ref offset, gracz.getListaPociskow.First().getTypPocisku);

                            }
                            else
                            {
                                addNumberOfShots(ref tablica, ref offset, 0);
                                addShotType(ref tablica, ref offset, 0);
                            }
                            gracz.getListaPociskow = new List<Pocisk>();
                            

                            return tablica; 
                       
                   
        }

        

    }
}
