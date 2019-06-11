using LoveLive_Mahjong_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace UnitTestMahjong
{
    [TestClass]
    public class UnitTestMahjong
    {
        [TestMethod]
        public void TestLisiten()
        {
            LoveLive_MahjongClass.InitializeMahjongClass();

            // 设置一些要和的牌
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
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Aqours - 5 - 1],
            };

            Furu_Cards = new List<MahjongCardFuru>()
            {

            };

            MahjongLogic logic = new MahjongLogic();

            List<MahjongCard> waiting = logic.utIsWaiting(Hand_Cards, Furu_Cards);

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

            // 设置一些要和的牌
            List<MahjongCard> Hand_Cards;
            List<MahjongCardFuru> Furu_Cards;
            Hand_Cards = new List<MahjongCard>()
            {
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Rin - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Rin - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Maki - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Maki - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Nico - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Nico - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Nozomi - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Nozomi - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Hanayo - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Hanayo  - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Eli - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Eli - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Kotori - 1],
                LoveLive_MahjongClass.CardInfo[(int)MahjongCardName.Kotori - 1],
            };

            Furu_Cards = new List<MahjongCardFuru>()
            {
                 
            };

            MahjongLogic logic = new MahjongLogic();

            bool Hu = logic.utIsHu(Hand_Cards, Furu_Cards, out List<HuCard> huCards);
            Assert.IsTrue(Hu);

            List<MahjongYaku> yakus = logic.utCalcYaku(huCards);
            foreach (MahjongYaku yaku in yakus)
                Trace.WriteLine(yaku);

            Trace.WriteLine($"点数：{logic.utCalcHuPoints(huCards)}");
        }

        [TestMethod]
        public void TestFuruRon()
        {
            Semaphore semaphore = new Semaphore(0, 4);

            LoveLive_MahjongClass.InitializeMahjongClass();

            MahjongLogic mahjongLogic = new MahjongLogic();

            // 反射，强行配置玩家顺序
            FieldInfo field = mahjongLogic.GetType().GetField("order", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(mahjongLogic, new int[] { 0, 1, 2, 3 });
            field = mahjongLogic.GetType().GetField("playing", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(mahjongLogic, 3);

            // 配置一些可以副露和荣和的牌组
            // 玩家A（等待杠Ruby）
            mahjongLogic.player_info[0].card_onhand = new List<MahjongCard>()
            {
                LoveLive_MahjongClass.GetCard(MahjongCardName.Ruby),
                LoveLive_MahjongClass.GetCard(MahjongCardName.Ruby),
                LoveLive_MahjongClass.GetCard(MahjongCardName.Ruby),
            };

            // 玩家B（等待吃Ruby（年级））
            mahjongLogic.player_info[1].card_onhand = new List<MahjongCard>()
            {
                LoveLive_MahjongClass.GetCard(MahjongCardName.Yoshiko),
                LoveLive_MahjongClass.GetCard(MahjongCardName.Hanamaru),
            };

            // 玩家C（等待荣Ruby）
            mahjongLogic.player_info[2].waiting.Add(LoveLive_MahjongClass.GetCard(MahjongCardName.Ruby));

            // 当前D （打出了Ruby）
            mahjongLogic.player_info[3].card_played.Add(LoveLive_MahjongClass.GetCard(MahjongCardName.Ruby));

            // 设置回调
            mahjongLogic.PlayerActionResponseCallback = delegate (List<PlayerAction> actions)
            {
                foreach(PlayerAction act in actions)
                {
                    Trace.WriteLine(act.ToString());
                }
                semaphore.Release();
            };
            
            mahjongLogic.PlayerActionAcceptedCallback = delegate (int playerId, bool accept)
            {
                Trace.WriteLine($"ID = {playerId}, Accept = {accept}");
                semaphore.Release();
            };

            // 强行设定状态机
            mahjongLogic.StartGamingThread();
            mahjongLogic.gameStateMachine.SetState(MahjongLogic.GameStateMachine.State.SendPlayerAction);
            mahjongLogic.gameStateMachine.ReleaseSemaphore();

            // 等待回调执行完毕后再继续
            semaphore.WaitOne();

            // 模拟发送消息
            mahjongLogic.SendPlayerAction(new PlayerAction(2) { actionType = PlayerActionType.Cancel });
            //mahjongLogic.SendPlayerAction(new PlayerAction(1) { actionType = PlayerActionType.Cancel });
            mahjongLogic.SendPlayerAction(new PlayerAction(0) { actionType = PlayerActionType.Pong });

            // 等待回调执行完毕后再继续
            semaphore.WaitOne();
            semaphore.WaitOne();
            semaphore.WaitOne();
        }
    }
}
