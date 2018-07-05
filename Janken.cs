// <copyright file="Janken.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1
{
    using System;

    public class Janken
    {
        private const int RockValue = 1; // グーの手
        private const int ScissorsValue = 2; // チョキの手
        private const int PaperValue = 3; // パーの手

        public static int UserNum { get; set; } = 0;

        public static int CpuNum { get; set; } = 0;

        public static int[] Hands { get; set; } = null;

        public static bool[] Results { get; set; } = null;

        public static int Rock { get => RockValue; }

        public static int Paper { get => PaperValue; }

        public static int Scissors { get => ScissorsValue; }

        public Janken()
        {
            UserNum = 0;
            CpuNum = 0;
            Hands = null;
            Results = null;

        }

        // じゃんけんの参加人数を決める。
        public static void PlayerNum()
        {
            bool isNumberSelected = false;
            while (!isNumberSelected)
            {
                Console.WriteLine("ユーザの人数を入力してください。");
                var userNum = Console.ReadLine();

                Console.WriteLine("対戦したいCPUの数を入力してください。");
                var cpuNum = Console.ReadLine();
                isNumberSelected = SetPlayerNum(userNum, cpuNum);
            }
        }

        // ユーザに手を決めてもらう。
        public static void SelectUserHand()
        {
            for (int i = 0; i < Janken.UserNum; i++)
            {
                bool isHandSelected = false;
                while (!isHandSelected)
                {
                    Console.WriteLine("ユーザ" + (i + 1) + "の手を入力してください。1:グー 2:チョキ 3:パー");
                    var uhand = Console.ReadLine();
                    isHandSelected = IsHandSelected(uhand);
                    if (!isHandSelected)
                    {
                        Console.WriteLine("1~3の数値を入力してください。");
                    }
                    else if (isHandSelected)
                    {
                        Janken.Hands[i] = int.Parse(uhand);
                    }
                }
            }
        }

        // CPUのじゃんけんの手を選ぶ
        public static void SelectCpuHand()
        {
            for (int i = 0; i < Janken.CpuNum; i++)
            {
                Janken.Hands[Janken.UserNum + i] = new System.Random().Next(1, 4);
            }
        }

        // じゃんけんの各手の人数を集計する
        public static bool CheckJankenResult()
        {
            int rockNum = 0; // グーを出している人数
            int scissorsNum = 0; // チョキを出している人数
            int paperNum = 0; // パーを出している人数

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

            return IsDrowResult(rockNum, scissorsNum, paperNum);
        }

        public static void ResultOutput() // 勝ち負けを出力する。
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

        public static bool CheckRetry() // リトライするかのチェック
        {
            Console.WriteLine("リトライしますか？ 1:はい 2:いいえ");
            var retry = Console.ReadLine();
            return IsRetry(retry);
        }

        public static bool IsRetry(string retry)
        {
            bool isSetRetry = false;
            while (!isSetRetry)
            {
                if (int.TryParse(retry, out int re) && (int.Parse(retry) == 1 || int.Parse(retry) == 2))
                {
                    isSetRetry = true;
                    if (re == 1)
                    {
                        return true;
                    }
                    else if (re == 2)
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("1,2いずれかの数値を入力してください。");
                    Console.WriteLine("リトライしますか？ 1:はい 2:いいえ");
                }
            }

            return false;
        }

        // Playerの人数をセットする
        public static bool SetPlayerNum(string userNum, string cpuNum)
        {
            if ((int.TryParse(userNum, out int un) && int.TryParse(cpuNum, out int cn))
                && (int.Parse(userNum) >= 1 && int.Parse(userNum) <= 10)
                && (int.Parse(cpuNum) >= 1 && int.Parse(cpuNum) <= 10))
            {
                Janken.UserNum = un;
                Janken.CpuNum = cn;
                Janken.Hands = new int[un + cn];
                Janken.Results = new bool[un + cn];
                return true;
            }
            else
            {
                Console.WriteLine("1以上10以下の数値を入力してください。");
            }

            return false;
        }

        // じゃんけんの手が正しく選択されているかどうか
        public static bool IsHandSelected(string input)
        {
            return int.TryParse(input, out int i) && (int.Parse(input) > 0 && int.Parse(input) < 4);
        }

        // じゃんけんがあいこなのか、勝負がついたのかを判定する
        public static bool IsDrowResult(int rockNum, int scissorsNum, int paperNum)
        {
            // あいこの場合
            if ((rockNum > 0 && paperNum > 0 && scissorsNum > 0)
                || rockNum == (Janken.UserNum + Janken.CpuNum)
                || scissorsNum == (Janken.UserNum + Janken.CpuNum)
                || paperNum == (Janken.UserNum + Janken.CpuNum))
            {
                Console.WriteLine("あいこです。");
                return true;
            }
            else
            {
                for (int i = 0; i < Janken.Hands.Length; i++)
                {
                    // チョキが勝ちの場合
                    if (rockNum == 0)
                    {
                        SetResult(i, Janken.Scissors, Janken.Paper);
                    }// パーが勝ちの場合
                    else if (scissorsNum == 0)
                    {
                        SetResult(i, Janken.Paper, Janken.Rock);
                    }// グーが勝ちの場合
                    else if (paperNum == 0)
                    {
                        SetResult(i, Janken.Rock, Janken.Scissors);
                    }
                }

                return false;
            }
        }

        // 誰がじゃんけんに勝ったのがを設定する
        public static void SetResult(int num, int winHand, int loseHand)
        {
            if (Janken.Hands[num] == winHand)
            {
                Janken.Results[num] = true;
            }
            else if (Janken.Hands[num] == loseHand)
            {
                Janken.Results[num] = false;
            }
        }
    }
}
