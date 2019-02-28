using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScotYard.Graphe
{
    public class Case
    {
        // Listes des liens vers les autres cases, selon le moyen de transport
        private List<Case> listeTaxis = new List<Case>();
        private List<Case> listeBus = new List<Case>();
        private List<Case> listeMetros = new List<Case>();
        private List<Case> listeBateaux = new List<Case>();

        // La liste de toutes les cases. La position 122 dans la liste correspond à la case #122 sur la planche de jeu
        private static List<Case> _listeCases = new List<Case>();

        /// <summary>
        /// Liste de toutes les cases du jeu.  La position 122 dans la liste correspond à la case #122 sur la planche de jeu
        /// </summary>
        public static List<Case> ListeCases
        {
            get
            {
                return _listeCases;
            }
        }

        public List<Case> ListeTaxis {
            get {
                return listeTaxis;
            }
        }

        public List<Case> ListeBus {
            get {
                return listeBus;
            }
        }

        public List<Case> ListeMetros {
            get {
                return listeMetros;
            }
        }

        public List<Case> ListeBateaux {
            get {
                return listeBateaux;
            }
        }


        /// <summary>
        /// Vous devez appeler cette méthode avant de pouvoir utiliser ListeCases
        /// </summary>
        public static void CreerCases()
        {
            for (int i = 0; i <= 199; i++)
            {
                _listeCases.Add(new Case(i));
            }
            CreerGrapheCases();
        }

        /// <summary>
        /// Création du graphe des transports
        /// </summary>
        private static void CreerGrapheCases()
        {
            AssocierTaxis(1, 8, 9);
            AssocierBus(1, 58, 46);
            AssocierMetros(1, 46);
            AssocierTaxis(2, 10, 20);
            AssocierTaxis(3, 11, 12, 4);
            AssocierBus(3, 22, 23);
            AssocierTaxis(4, 3, 13);
            AssocierTaxis(5, 15, 16);
            AssocierTaxis(6, 29, 7);
            AssocierTaxis(7, 6, 17);
            AssocierBus(7, 42);
            AssocierTaxis(8, 1, 18, 19);
            AssocierTaxis(9, 1, 19, 20);
            AssocierTaxis(10, 2, 21, 34, 11);
            AssocierTaxis(11, 10, 3, 22);
            AssocierTaxis(12, 3, 23);
            AssocierTaxis(13, 4, 23, 14, 24);
            AssocierBus(13, 23, 14, 25);
            AssocierMetros(13, 46, 89, 67);
            AssocierTaxis(14, 13, 25, 15);
            AssocierBus(14, 13, 15);
            AssocierTaxis(15, 14, 5, 16, 28, 26);
            AssocierBus(15, 14, 29, 41);
            AssocierTaxis(16, 15, 5, 28, 29);
            AssocierTaxis(17, 29, 7, 30);
            AssocierTaxis(18, 8, 31, 43);
            AssocierTaxis(19, 8, 9, 32);
            AssocierTaxis(20, 9, 2, 33);
            AssocierTaxis(21, 33, 10);
            AssocierTaxis(22, 34, 11, 23, 35);
            AssocierBus(22, 34, 3, 23, 65);
            AssocierTaxis(23, 22, 12, 13, 37);
            AssocierBus(23, 22, 3, 13, 67);
            AssocierTaxis(24, 37, 13, 38);
            AssocierTaxis(25, 14, 39, 38);
            AssocierTaxis(26, 15, 27, 39);
            AssocierTaxis(27, 26, 27, 39);
            AssocierTaxis(28, 27, 15, 16, 41);
            AssocierTaxis(29, 16, 6, 17, 42, 41);
            AssocierBus(29, 15, 42, 55, 41);
            AssocierTaxis(30, 17, 42);
            AssocierTaxis(31, 18, 44, 43);
            AssocierTaxis(32, 19, 33, 45, 44);
            AssocierTaxis(33, 32, 20, 21, 46);
            AssocierTaxis(34, 47, 10, 22, 48);
            AssocierBus(34, 46, 22, 63);
            AssocierTaxis(35, 48, 22, 36, 65);
            AssocierTaxis(36, 35, 37, 49);
            AssocierTaxis(37, 36, 23, 24, 50);
            AssocierTaxis(38, 50, 24, 25, 51);
            AssocierTaxis(39, 25, 26, 52, 51);
            AssocierTaxis(40, 52, 27, 41, 53);
            AssocierTaxis(41, 40, 28, 29, 54);
            AssocierBus(41, 15, 29, 52);
            AssocierTaxis(42, 29, 30, 56, 72);
            AssocierBus(42, 29, 7, 72);
            AssocierTaxis(43, 18, 31, 57);
            AssocierTaxis(44, 31, 32, 58);
            AssocierTaxis(45, 58, 32, 46, 60, 59);
            AssocierTaxis(46, 45, 33, 47, 61);
            AssocierBus(46, 58, 1, 34, 78);
            AssocierMetros(46, 74, 1, 13, 79);
            AssocierTaxis(47, 46, 34, 62);
            AssocierTaxis(48, 62, 34, 35, 63);
            AssocierTaxis(49, 36, 50, 66);
            AssocierTaxis(50, 49, 37, 38);
            AssocierTaxis(51, 67, 38, 39, 52, 68);
            AssocierTaxis(52, 39, 40, 69, 51);
            AssocierBus(52, 13, 41, 86, 67);
            AssocierTaxis(53, 69, 40, 54);
            AssocierTaxis(54, 53, 41, 55, 70);
            AssocierTaxis(55, 54, 71);
            AssocierBus(55, 29, 89);
            AssocierTaxis(56, 42, 91);
            AssocierTaxis(57, 43, 58, 73);
            AssocierTaxis(58, 57, 44, 45, 59, 75, 74);
            AssocierBus(58, 1, 46, 77, 74);
            AssocierTaxis(59, 58, 45, 76, 75);
            AssocierTaxis(60, 45, 61, 76);
            AssocierTaxis(61, 76, 46, 62, 78);
            AssocierTaxis(62, 61, 47, 48, 79);
            AssocierTaxis(63, 79, 48, 64, 80);
            AssocierBus(63, 79, 34, 65, 100);
            AssocierTaxis(64, 63, 65, 81);
            AssocierTaxis(65, 64, 35, 66, 82);
            AssocierBus(65, 63, 22, 67, 82);
            AssocierTaxis(66, 65, 49, 67, 82);
            AssocierTaxis(67, 66, 51, 68, 84);
            AssocierBus(67, 23, 52, 102, 82, 65);
            AssocierMetros(67, 79, 13, 89, 111);
            AssocierTaxis(68, 67, 51, 69, 85);
            AssocierTaxis(69, 68, 52, 53, 86);
            AssocierTaxis(70, 54, 71, 87);
            AssocierTaxis(71, 70, 55, 72, 89);
            AssocierTaxis(72, 71, 42, 91, 90);
            AssocierBus(72, 42, 107, 105);
            AssocierTaxis(73, 57, 74, 92);
            AssocierTaxis(74, 73, 58, 92);
            AssocierBus(74, 58, 94);
            AssocierMetros(74, 46);
            AssocierTaxis(75, 74, 58, 59, 94);
            AssocierTaxis(76, 59, 60, 61, 77);
            AssocierTaxis(77, 95, 76, 78, 96);
            AssocierBus(77, 58, 78, 94, 124);
            AssocierTaxis(78, 77, 61, 79, 97);
            AssocierBus(78, 77, 46, 79);
            AssocierTaxis(79, 78, 62, 63, 98);
            AssocierBus(79, 78, 63);
            AssocierMetros(79, 93, 46, 67, 111);
            AssocierTaxis(80, 63, 100, 99);
            AssocierTaxis(81, 64, 82, 100);
            AssocierTaxis(82, 81, 65, 66, 101);
            AssocierBus(82, 65, 67, 140, 100);
            AssocierTaxis(83, 102, 101);
            AssocierTaxis(84, 67, 85);
            AssocierTaxis(85, 84, 68, 103);
            AssocierTaxis(86, 103, 69, 104);
            AssocierBus(86, 102, 52, 87, 116);
            AssocierTaxis(87, 70, 88);
            AssocierBus(87, 86, 41, 105);
            AssocierTaxis(88, 87, 89, 117);
            AssocierTaxis(89, 88, 71, 105);
            AssocierBus(89, 55, 105);
            AssocierMetros(89, 67, 13, 128, 140);
            AssocierTaxis(90, 72, 91, 105);
            AssocierTaxis(91, 72, 56, 107, 105, 90);
            AssocierTaxis(92, 73, 74, 93);
            AssocierTaxis(93, 92, 94);
            AssocierBus(93, 94);
            AssocierMetros(93, 79);
            AssocierTaxis(94, 93, 75, 95);
            AssocierBus(94, 93, 74, 77);
            AssocierTaxis(95, 94, 77, 122);
            AssocierTaxis(96, 77, 97, 109);
            AssocierTaxis(97, 96, 78, 98, 109);
            AssocierTaxis(98, 97, 79, 99, 110);
            AssocierTaxis(99, 98, 80, 112, 110);
            AssocierTaxis(100, 80, 81, 101, 113, 112);
            AssocierBus(100, 63, 82, 111);
            AssocierTaxis(101, 100, 82, 83, 114);
            AssocierTaxis(102, 83, 103, 115);
            AssocierBus(102, 67, 86, 127);
            AssocierTaxis(103, 102, 85, 86);
            AssocierTaxis(104, 86, 116);
            AssocierTaxis(105, 89, 90, 91, 106, 108);
            AssocierBus(105, 87, 89, 72, 107, 108);
            AssocierTaxis(106, 105, 107);
            AssocierTaxis(107, 106, 91, 119);
            AssocierBus(107, 105, 72, 161);
            AssocierTaxis(108, 117, 105, 119);
            AssocierBus(108, 116, 105, 135);
            AssocierBateaux(108, 115);
            AssocierTaxis(109, 96, 97, 110, 124);
            AssocierTaxis(110, 98, 99, 111, 109);
            AssocierTaxis(111, 110, 112, 124);
            AssocierBus(111, 124, 100);
            AssocierMetros(111, 163, 79, 67, 153);
            AssocierTaxis(112, 111, 99);
            AssocierTaxis(113, 125, 100, 114);
            AssocierTaxis(114, 113, 101, 115, 126, 132, 131);
            AssocierTaxis(115, 114, 102, 127, 126);
            AssocierBateaux(115, 108, 157);
            AssocierTaxis(116, 127, 104, 117, 118);
            AssocierBus(116, 127, 86, 108, 142);
            AssocierTaxis(117, 116, 88, 108, 129);
            AssocierTaxis(118, 134, 116, 129, 142);
            AssocierTaxis(119, 108, 107, 136);
            AssocierTaxis(120, 144, 121);
            AssocierTaxis(121, 120, 122, 145);
            AssocierTaxis(122, 121, 95, 123, 146);
            AssocierBus(122, 144, 123);
            AssocierTaxis(123, 122, 124, 149, 148, 137);
            AssocierBus(123, 122, 124, 165, 144);
            AssocierTaxis(124, 123, 109, 111, 130, 138);
            AssocierBus(124, 123, 77, 111, 153);
            AssocierTaxis(125, 112, 113, 131);
            AssocierTaxis(126, 114, 115, 127, 140);
            AssocierTaxis(127, 126, 115, 116, 134, 133);
            AssocierBus(127, 102, 116, 133);
            AssocierTaxis(128, 142, 143, 160, 188, 172);
            AssocierBus(128, 142, 135, 161, 199, 187);
            AssocierMetros(128, 140, 89, 185);
            AssocierTaxis(129, 118, 117, 135, 143, 142);
            AssocierTaxis(130, 124, 131, 139);
            AssocierTaxis(131, 130, 125, 114);
            AssocierTaxis(132, 114, 140);
            AssocierTaxis(133, 140, 127, 141);
            AssocierBus(133, 140, 127, 157);
            AssocierTaxis(134, 141, 127, 118, 142);
            AssocierTaxis(135, 129, 136, 161, 143);
            AssocierBus(135, 108, 161, 128);
            AssocierTaxis(136, 135, 119, 162);
            AssocierTaxis(137, 147, 123);
            AssocierTaxis(138, 150, 124, 152);
            AssocierTaxis(139, 130, 140, 154, 153);
            AssocierTaxis(140, 139, 132, 126, 133, 156, 154);
            AssocierBus(140, 154, 82, 133, 156);
            AssocierMetros(140, 153, 89, 128);
            AssocierTaxis(141, 133, 134, 142, 158);
            AssocierTaxis(142, 141, 134, 118, 143, 128, 158);
            AssocierBus(142, 116, 128, 157);
            AssocierTaxis(143, 142, 135, 160, 128);
            AssocierTaxis(144, 120, 145, 177);
            AssocierBus(144, 122, 123, 163);
            AssocierTaxis(145, 144, 121, 146);
            AssocierTaxis(146, 145, 122, 147, 163);
            AssocierTaxis(147, 146, 137, 164);
            AssocierTaxis(148, 164, 123, 149);
            AssocierTaxis(149, 148, 123, 150, 165);
            AssocierTaxis(150, 149, 138, 151);
            AssocierTaxis(151, 150, 152, 166, 165);
            AssocierTaxis(152, 138, 153, 151);
            AssocierTaxis(153, 152, 139, 154, 167, 166);
            AssocierBus(153, 124, 154, 184, 180);
            AssocierMetros(153, 163, 111, 140, 185);
            AssocierTaxis(154, 153, 139, 140, 155);
            AssocierBus(154, 153, 140, 156);
            AssocierTaxis(155, 154, 156, 168, 167);
            AssocierTaxis(156, 155, 140, 157, 169);
            AssocierBus(156, 154, 140, 157, 184);
            AssocierTaxis(157, 156, 133, 158, 170);
            AssocierBus(157, 156, 133, 142, 185);
            AssocierBateaux(157, 115, 194);
            AssocierTaxis(158, 157, 141, 142, 159);
            AssocierTaxis(159, 170, 158, 172, 198, 186);
            AssocierTaxis(160, 128, 143, 161, 173);
            AssocierTaxis(161, 160, 135, 136, 174);
            AssocierBus(161, 128, 135, 107, 199);
            AssocierTaxis(162, 136, 175);
            AssocierTaxis(163, 146, 177);
            AssocierBus(163, 144, 176, 191);
            AssocierMetros(163, 111, 153);
            AssocierTaxis(164, 147, 148, 179, 178);
            AssocierTaxis(165, 149, 151, 180, 179);
            AssocierBus(165, 123, 180, 191);
            AssocierTaxis(166, 151, 153, 183, 181);
            AssocierTaxis(167, 153, 155, 168, 183);
            AssocierTaxis(168, 167, 155, 184);
            AssocierTaxis(169, 156, 184);
            AssocierTaxis(170, 157, 159, 185);
            AssocierTaxis(171, 199, 173, 175);
            AssocierTaxis(172, 159, 128, 187);
            AssocierTaxis(173, 160, 174, 171, 188);
            AssocierTaxis(174, 173, 161, 175);
            AssocierTaxis(175, 174, 162, 171);
            AssocierTaxis(176, 177, 189);
            AssocierBus(176, 163, 190);
            AssocierTaxis(177, 176, 144, 163);
            AssocierTaxis(178, 164, 191, 189);
            AssocierTaxis(179, 164, 165, 191);
            AssocierTaxis(180, 165, 181, 193);
            AssocierBus(180, 165, 153, 184, 190);
            AssocierTaxis(181, 180, 166, 182, 193);
            AssocierTaxis(182, 181, 183, 195);
            AssocierTaxis(183, 182, 166, 167, 196);
            AssocierTaxis(184, 168, 169, 185, 197, 196);
            AssocierBus(184, 153, 156, 185, 180);
            AssocierTaxis(185, 184, 170, 186);
            AssocierBus(185, 184, 157, 187);
            AssocierMetros(185, 153, 128);
            AssocierTaxis(186, 185, 159, 198);
            AssocierTaxis(187, 172, 188, 198);
            AssocierBus(187, 185, 128);
            AssocierTaxis(188, 187, 128, 173, 199);
            AssocierTaxis(189, 176, 178, 190);
            AssocierTaxis(190, 189, 191, 192);
            AssocierBus(190, 176, 191, 180);
            AssocierTaxis(191, 190, 178, 179, 192);
            AssocierBus(191, 190, 163, 165);
            AssocierTaxis(192, 190, 191, 194);
            AssocierTaxis(193, 180, 181, 194);
            AssocierTaxis(194, 192, 193, 195);
            AssocierBateaux(194, 157);
            AssocierTaxis(195, 194, 182, 197);
            AssocierTaxis(196, 183, 184, 197);
            AssocierTaxis(197, 195, 196, 184);
            AssocierTaxis(198, 186, 187, 199);
            AssocierTaxis(199, 198, 188, 171);
            AssocierBus(199, 128, 161);
        }

        private static void AssocierTaxis(int maCase, params int[] tableau)
        {
            foreach(int position in tableau){
                _listeCases[maCase].listeTaxis.Add(_listeCases[position]);
            }
        }

        private static void AssocierBus(int maCase, params int[] tableau)
        {
            foreach (int position in tableau)
            {
                _listeCases[maCase].listeBus.Add(_listeCases[position]);
            }
        }

        private static void AssocierMetros(int maCase, params int[] tableau)
        {
            foreach (int position in tableau)
            {
                _listeCases[maCase].listeMetros.Add(_listeCases[position]);
            }
        }

        private static void AssocierBateaux(int maCase, params int[] tableau)
        {
            foreach (int position in tableau)
            {
                _listeCases[maCase].listeBateaux.Add(_listeCases[position]);
            }
        }

        // **************** Fin partie static

        /// <summary>
        /// Numéro de la case
        /// </summary>
        public int Numero
        {
            get;
            set;
        }

        /// <summary>
        /// Constructeur de Case
        /// </summary>
        /// <param name="numero">Numéro de la case</param>
        public Case(int numero)
        {
            Numero = numero;
        }       
    }
}
