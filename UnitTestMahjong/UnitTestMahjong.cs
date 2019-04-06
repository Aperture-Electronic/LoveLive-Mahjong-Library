using LoveLive_Mahjong_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestMahjong
{
    [TestClass]
    public class UnitTestMahjong
    {
        [TestMethod]
        public void InitializeMahjongTest()
        {
            LoveLive_MahjongClass.InitializeMahjongClass();
            Assert.AreNotEqual(0, LoveLive_MahjongClass.CardInfo.Count);
            Trace.WriteLine(LoveLive_MahjongClass.CardInfo.Count);
        }

        [TestMethod]
        public void ANewMahjong()
        {
            LoveLive_MahjongClass.InitializeMahjongClass();

            MahjongLogic logic = new MahjongLogic();

            logic.NewGame_Handle(25000, 1);
            logic.NextScene();

            int[] order = logic.PlayerOrder;

            List<MahjongCard> cardA = logic.GetPlayerCardOnHand(order[0]);
            List<MahjongCard> cardB = logic.GetPlayerCardOnHand(logic.Playing);
            for(int i = 0; i < cardA.Count; i++)
            {
                Trace.WriteLine(cardA[i].name);
                Trace.WriteLine(cardB[i].name);
            }
        }

        [TestMethod]
        public void TestLisiten()
        {
            LoveLive_MahjongClass.InitializeMahjongClass();

            // ����һЩҪ�͵���
            List<MahjongCard> Hand_Cards;
            List<MahjongCardFuru> Furu_Cards;
            Hand_Cards = new List<MahjongCard>()
            {
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Honoka - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Chika - 1 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Ayu - 2 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Anju - 3 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Erena - 3 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Ria - 4 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Seira - 4 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Tsubasa - 3 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Aqours - 5 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Nijigasaki - 5 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.ARISE - 5 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.SaintSnow - 5 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Muse - 5 - 1],
            };

            Furu_Cards = new List<MahjongCardFuru>()
            {

            };

            MahjongLogic logic = new MahjongLogic();

            List<MahjongCard> waiting = new List<MahjongCard>();

            int count = LoveLive_MahjongClass.CardInfo.Count;
            for (int i = 0; i < count; i++)
            {
                List<MahjongCard> new_hand_cards = new List<MahjongCard>(Hand_Cards);
                new_hand_cards.Add(LoveLive_MahjongClass.CardInfo[i]);
                bool Hu = logic.Waiting(new_hand_cards, Furu_Cards);
                if (Hu) waiting.Add(LoveLive_MahjongClass.CardInfo[i]);
            }

            Trace.WriteLine($"You are waiting for {waiting.Count} cards.");
            foreach (MahjongCard card in waiting)
            {
                Trace.WriteLine(card);
            }
        }

        [TestMethod]
        public void TestYaku()
        {
            LoveLive_MahjongClass.InitializeMahjongClass();

            // ����һЩҪ�͵���
            List<MahjongCard> Hand_Cards;
            List<MahjongCardFuru> Furu_Cards;
            Hand_Cards = new List<MahjongCard>()
            {
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Hanayo - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Rin - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Maki - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Hanamaru - 1 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Yoshiko - 1 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Ruby - 1 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Shizuku - 2 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Rina - 2 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Kasumi - 2 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Nico  - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Eli - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Maki - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Kanan - 1 - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Kanan - 1 - 1],
            };

            Furu_Cards = new List<MahjongCardFuru>()
            {
                 
            };

            MahjongLogic logic = new MahjongLogic();

            bool Hu = logic.Waiting(Hand_Cards, Furu_Cards);
            Assert.IsTrue(Hu);

            List<MahjongYaku> yakus = logic.YAKU();
            foreach (MahjongYaku yaku in yakus)
                Trace.WriteLine(yaku);
        }
    }
}
