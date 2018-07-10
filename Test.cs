using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NUnit.Framework;

namespace ConsoleApp1Test
{
    [TestFixture]
    public class Test
    {
        private ConsoleApp1.Janken janken;
        
        [SetUp]
        public void SetUp()
        {
            this.janken = new ConsoleApp1.Janken();
        }

        [TearDown]
        public void TearDown()
        {
            this.janken = null;
        }

        // SetPlayerNum
        [Test]
        public void SuccessSetPlayerNumTest境界値下限値()
        {
            bool isSetPlayerNum = ConsoleApp1.Janken.SetPlayerNum("1", "1");
            Assert.AreEqual(true, isSetPlayerNum);
        }
        [Test]
        public void SuccessSetPlayerNumTest境界上限値()
        {
            bool isSetPlayerNum = ConsoleApp1.Janken.SetPlayerNum("10", "10");
            Assert.AreEqual(true, isSetPlayerNum);
        }

        [Test]
        public void NgSetPlayerNumTest境界外下限値()
        {
            bool isSetPlayerNum = ConsoleApp1.Janken.SetPlayerNum("0", "1");
            Assert.AreEqual(false, isSetPlayerNum);
        }

        [Test]
        public void NgSetPlayerNumTest境界外上限値()
        {
            bool isSetPlayerNum = ConsoleApp1.Janken.SetPlayerNum("11", "1");
            Assert.AreEqual(false, isSetPlayerNum);
        }

        [Test]
        public void NgSetPlayerNumTest文字列入力()
        {
            bool isSetPlayerNum = ConsoleApp1.Janken.SetPlayerNum("aaa", "1");
            Assert.AreEqual(false, isSetPlayerNum);
        }

        //IsHandSelected
        [Test]
        public void SuccessIsHandSelectedTestグー()
        {
            bool isHandSelected = ConsoleApp1.Janken.IsHandSelected(ConsoleApp1.Janken.Rock.ToString());
            Assert.AreEqual(true, isHandSelected);
        }

        [Test]
        public void SuccessIsHandSelectedTestチョキ()
        {
            bool isHandSelected = ConsoleApp1.Janken.IsHandSelected(ConsoleApp1.Janken.Scissors.ToString());
            Assert.AreEqual(true, isHandSelected);
        }

        [Test]
        public void SuccessIsHandSelectedTestパー()
        {
            bool isHandSelected = ConsoleApp1.Janken.IsHandSelected(ConsoleApp1.Janken.Paper.ToString());
            Assert.AreEqual(true, isHandSelected);
        }

        [Test]
        public void NgIsHandSelectedTest0を入力()
        {
            bool isHandSelected = ConsoleApp1.Janken.IsHandSelected("0");
            Assert.AreEqual(false, isHandSelected);
        }

        [Test]
        public void NgIsHandSelectedTest4を入力()
        {
            bool isHandSelected = ConsoleApp1.Janken.IsHandSelected("4");
            Assert.AreEqual(false, isHandSelected);
        }

        [Test]
        public void NgIsHandSelectedTest文字列を入力()
        {
            bool isHandSelected = ConsoleApp1.Janken.IsHandSelected("aaa");
            Assert.AreEqual(false, isHandSelected);
        }

        [Test]
        public void SuccessIsDrowResultTest勝敗あり()
        {
            ConsoleApp1.Janken.UserNum = 2;
            ConsoleApp1.Janken.CpuNum = 2;
            ConsoleApp1.Janken.Hands = new int[4];
            bool isDrow = ConsoleApp1.Janken.IsDrowResult(2, 2, 0);
            Assert.AreEqual(false, isDrow);
        }

        [Test]
        public void SuccessIsDrowResultestすべての手がでてあいこ()
        {
            ConsoleApp1.Janken.UserNum = 2;
            ConsoleApp1.Janken.CpuNum = 2;
            ConsoleApp1.Janken.Hands = new int[4];
            bool isDrow = ConsoleApp1.Janken.IsDrowResult(2, 1, 1);
            Assert.AreEqual(true, isDrow);
        }

        [Test]
        public void SuccessIsDrowResultTest一つの手しかでなくてあいこ()
        {
            ConsoleApp1.Janken.UserNum = 2;
            ConsoleApp1.Janken.CpuNum = 2;
            ConsoleApp1.Janken.Hands = new int[4];
            bool isDrow = ConsoleApp1.Janken.IsDrowResult(4, 0, 0);
            Assert.AreEqual(true, isDrow);
        }

        //SetResult
        [Test]
        public void SuccessSetResultTestグーが勝ち()
        {
            ConsoleApp1.Janken.UserNum = 2;
            ConsoleApp1.Janken.CpuNum = 2;
            ConsoleApp1.Janken.Hands = new int[4];
            ConsoleApp1.Janken.Results = new bool[4];
            ConsoleApp1.Janken.Hands[0] = 1;
            ConsoleApp1.Janken.Hands[1] = 2;
            ConsoleApp1.Janken.SetResult(0,ConsoleApp1.Janken.Rock,ConsoleApp1.Janken.Scissors);
            ConsoleApp1.Janken.SetResult(1, ConsoleApp1.Janken.Rock, ConsoleApp1.Janken.Scissors);
            Assert.AreEqual(true, ConsoleApp1.Janken.Results[0]);
            Assert.AreEqual(false, ConsoleApp1.Janken.Results[1]);
        }

        [Test]
        public void SuccessSetResultTestチョキが勝ち()
        {
            ConsoleApp1.Janken.UserNum = 2;
            ConsoleApp1.Janken.CpuNum = 2;
            ConsoleApp1.Janken.Hands = new int[4];
            ConsoleApp1.Janken.Results = new bool[4];
            ConsoleApp1.Janken.Hands[0] = 2;
            ConsoleApp1.Janken.Hands[1] = 3;
            ConsoleApp1.Janken.SetResult(0, ConsoleApp1.Janken.Scissors, ConsoleApp1.Janken.Paper);
            ConsoleApp1.Janken.SetResult(1, ConsoleApp1.Janken.Rock, ConsoleApp1.Janken.Paper);
            Assert.AreEqual(true, ConsoleApp1.Janken.Results[0]);
            Assert.AreEqual(false, ConsoleApp1.Janken.Results[1]);
        }
        [Test]
        public void SuccessSetResultTestパーが勝ち()
        {
            ConsoleApp1.Janken.UserNum = 2;
            ConsoleApp1.Janken.CpuNum = 2;
            ConsoleApp1.Janken.Hands = new int[4];
            ConsoleApp1.Janken.Results = new bool[4];
            ConsoleApp1.Janken.Hands[0] = 3;
            ConsoleApp1.Janken.Hands[1] = 1;
            ConsoleApp1.Janken.SetResult(0, ConsoleApp1.Janken.Paper,ConsoleApp1.Janken.Rock);
            ConsoleApp1.Janken.SetResult(1, ConsoleApp1.Janken.Paper, ConsoleApp1.Janken.Rock);
            Assert.AreEqual(true, ConsoleApp1.Janken.Results[0]);
            Assert.AreEqual(false, ConsoleApp1.Janken.Results[1]);
        }

        //IsRetry
        [Test]
        public void SuccessIsRetryTestリトライする()
        {
            bool isRetry = ConsoleApp1.Janken.IsRetry("1");
            Assert.AreEqual(true, isRetry);
        }

        [Test]
        public void SuccessIsRetryTestリトライしない()
        {
            bool isRetry = ConsoleApp1.Janken.IsRetry("2");
            Assert.AreEqual(false, isRetry);
        }
    }
}
