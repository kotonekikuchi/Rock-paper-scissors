// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1
{
    using System;

    internal static class Program
    {
        public static void Main(string[] args)
        {
            PlayerNum(); // プレイヤーの人数を設定
            bool retryFlg = true;
            while (retryFlg)
            {
                bool drowFlg = true; // あいこ判定
                while (drowFlg)
                {
                    // ユーザの手を決める。
                    SelectUserHand();

                    // CPUの手を決める。
                    SelectCpuHand();

                    // じゃんけんの結果判定
                    drowFlg = CheckJankenResult();
                }

                ResultOutput(); // 結果の出力を行う。
                retryFlg = CheckRetry(); // リトライするかどうか
            }
        }

        // じゃんけんの手1~3を出している場合は1、それ以外を出している場合は0を返す。
        private static bool CheckJankenNum(string input)
        {
            bool checkHand = false;
            if (int.TryParse(input, out int i))
            {
                if (i > 0 && i < 4)
                {
                    checkHand = true;
                }
                else
                {
                    checkHand = false;
                }
            }

            return checkHand;
        }

        // プレイヤーの人数を設定する。
        private static void PlayerNum()
        {
            bool personCheckFlg = false;
            while (!personCheckFlg)
            {
                // じゃんけんの参加人数を決める。
                Console.WriteLine("ユーザの人数を入力してください。");
                var userr = Console.ReadLine();
                Console.WriteLine("対戦したいCPUの数を入力してください。");
                var cpur = Console.ReadLine();
                if (int.TryParse(userr, out int un) && int.TryParse(cpur, out int cn))
                {
                    if (un != 0 && cn != 0)
                    {
                        Janken.UserNum = un;
                        Janken.CpuNum = cn;
                        personCheckFlg = true;
                        Janken.Hands = new int[un + cn];
                        Janken.Results = new bool[un + cn];
                    }
                    else
                    {
                        Console.WriteLine("1以上の数字を入力してください。");
                    }
                }
                else
                {
                    Console.WriteLine("数値を入力してください。");
                }
            }
        }

        // ユーザに手を決めてもらう。
        private static void SelectUserHand()
        {
            for (int i = 0; i < Janken.UserNum; i++)
            {
                bool check = false;
                while (!check)
                {
                    Console.WriteLine("ユーザ" + (i + 1) + "の手を入力してください。1:グー 2:チョキ 3:パー");
                    var uhand = Console.ReadLine();
                    check = CheckJankenNum(uhand);
                    if (!check)
                    {
                        Console.WriteLine("1~3の数値を入力してください。");
                    }
                    else if (check)
                    {
                        Janken.Hands[i] = int.Parse(uhand);
                    }
                }
            }
        }

        private static void SelectCpuHand()
        {
            for (int i = 0; i < Janken.CpuNum; i++)
            {
                Janken.Hands[Janken.UserNum + i] = new System.Random().Next(1, 4);
            }
        }

        private static bool CheckJankenResult()
        {
            int rockNum = 0; // グーを出している人数
            int paperNum = 0; // パーを出している人数
            int scissorsNum = 0; // チョキを出している人数
            bool drowFlg = true;

            // じゃんけんの処理を行う。
            for (int i = 0; i < Janken.Hands.Length; i++)
            {
                switch (Janken.Hands[i])
                {
                    case 1:
                        rockNum++;
                        break;
                    case 2:
                        scissorsNum++;
                        break;
                    case 3:
                        paperNum++;
                        break;
                }
            }

            drowFlg = CheckWinner(rockNum, paperNum, scissorsNum);
            return drowFlg;
        }

        private static bool CheckWinner(int rockNum, int paperNum, int scissorsNum)
        {
            bool drowFlg = true;

            // あいこの場合
            if ((rockNum > 0 && paperNum > 0 && scissorsNum > 0)
                || rockNum == (Janken.UserNum + Janken.CpuNum)
                || paperNum == (Janken.UserNum + Janken.CpuNum)
                || (scissorsNum == Janken.UserNum + Janken.CpuNum))
            {
                Console.WriteLine("あいこです。");
                drowFlg = true;
            }
            else
            {
                drowFlg = false;
                for (int i = 0; i < Janken.Hands.Length; i++)
                {
                    // チョキが勝ちの場合
                    if (rockNum == 0)
                    {
                        InputResult(i, Janken.scissors, Janken.Paper);
                    }// パーが勝ちの場合
                    else if (scissorsNum == 0)
                    {
                        InputResult(i, Janken.Paper, Janken.scissors);
                    }// グーが勝ちの場合
                    else if (paperNum == 0)
                    {
                        InputResult(i, Janken.rock, Janken.scissors);
                    }
                }
            }

            return drowFlg;
        }

        private static void InputResult(int num,int hand1, int hand2)
        {
            if (Janken.Hands[num] == hand1)
            {
                Janken.Results[num] = true;
            }
            else if (Janken.Hands[num] == hand2)
            {
                Janken.Results[num] = false;
            }
        }

        private static void ResultOutput() // 勝ち負けを出力する。
        {
            for (int i = 0; i < Janken.UserNum; i++)
            {
                if (Janken.Results[i])
                {
                    Console.WriteLine("ユーザ" + (i + 1) + "は勝ちです。");
                }
                else if (!Janken.Results[i])
                {
                    Console.WriteLine("ユーザ" + (i + 1) + "は負けです。");
                }
            }
        }

        private static bool CheckRetry() // リトライするかのチェック
        {
            bool retryFlg = false; // リトライするかどうか
            bool checkRetry = false; // リトライの入力が正しく行われているかどうか
            while (!checkRetry)
            {
                Console.WriteLine("リトライしますか？ 1:はい 2:いいえ");
                var retry = Console.ReadLine();
                if (int.TryParse(retry, out int re))
                {
                    if (re <= 0 || re >= 3)
                    {
                        Console.WriteLine("1,2いずれかの数値を入力してください。");
                    }
                    else
                    {
                        checkRetry = true;
                        if (re == 1)
                        {
                            retryFlg = true;
                        }
                        else if (re == 2)
                        {
                            retryFlg = false;
                        }
                    }
                }
                else
                {
                    checkRetry = false;
                    Console.WriteLine("1,2いずれかの数値を入力してください。");
                }
            }

            return retryFlg;
        }
    }
}
