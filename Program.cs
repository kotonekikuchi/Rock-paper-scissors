// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int personCheck = 0; // 人数入力の数値チェック
            int usernum = 0; // ユーザの人数
            int cpunum = 0; // CPUの人数
            int[] hands = null; // 人数分のじゃんけんの手を保持する配列
            int[] results = null; // プレーヤーごとのじゃんけんの結果を保持する配列

            while (personCheck == 0)
            {
                // じゃんけんの参加人数を決める。
                Console.WriteLine("ユーザの人数を入力してください。");
                var userr = Console.ReadLine();
                Console.WriteLine("対戦したいCPUの数を入力してください。");
                var cpur = Console.ReadLine();
                if (int.TryParse(userr, out int un) && int.TryParse(cpur, out int cn))
                {
                    usernum = un;
                    cpunum = cn;
                    personCheck = 1;
                    hands = new int[un + cn];
                    results = new int[un + cn];
                }
                else
                {
                    personCheck = 0;
                    Console.WriteLine("数値を入力してください。");
                }
            }

            int battlefinish = 0; // あいこ判定
            while (battlefinish == 0)
            {
                // ユーザに手を決めてもらう。
                for (int i = 0; i < usernum; i++)
                {
                    int check = 0;
                    while (check == 0)
                    {
                        Console.WriteLine("ユーザ" + (i + 1) + "の手を入力してください。1:グー 2:チョキ 3:パー");
                        var uhand = Console.ReadLine();
                        check = CheckJankenNum(uhand);
                        if (check == 0)
                        {
                            Console.WriteLine("1~3の数値を入力してください。");
                        }
                        else if (check == 1)
                        {
                            hands[i] = int.Parse(uhand);
                        }
                    }
                }

                // CPUの手を決める。
                for (int i = 0; i < cpunum; i++)
                {
                    hands[usernum + i] = new System.Random().Next(3);
                }

                int rockNum = 0; // グーを出している人数
                int paperNum = 0; // パーを出している人数
                int scissorsNum = 0; // チョキを出している人数

                // じゃんけんの処理を行う。
                for (int i = 0; i < hands.Length; i++)
                {
                    switch (hands[i])
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

                // グー、チョキ、パーのいずれも使われている場合
                if ((rockNum > 0 && paperNum > 0 && scissorsNum > 0) || rockNum == usernum + cpunum || paperNum == usernum + cpunum || scissorsNum == usernum + cpunum)
                {
                    Console.WriteLine("あいこです。");
                    battlefinish = 0;
                }
                else
                {
                    // チョキが勝ちの場合
                    if (rockNum == 0)
                    {
                        for (int i = 0; i < hands.Length; i++)
                        {
                            if (hands[i] == 2)
                            {
                                results[i] = 1;
                            }
                            else if (hands[i] == 3)
                            {
                                results[i] = 0;
                            }
                        }
                    } // パーが勝ちの場合
                    else if (scissorsNum == 0)
                    {
                        for (int i = 0; i < hands.Length; i++)
                        {
                            if (hands[i] == 3)
                            {
                                results[i] = 1;
                            }
                            else if (hands[i] == 2)
                            {
                                results[i] = 0;
                            }
                        }
                    }// グーが勝ちの場合
                    else if (paperNum == 0)
                    {
                        for (int i = 0; i < hands.Length; i++)
                        {
                            if (hands[i] == 1)
                            {
                                results[i] = 1;
                            }
                            else if (hands[i] == 2)
                            {
                                results[i] = 0;
                            }
                        }
                    }

                    // ユーザごとの勝ち負けの判断をし、結果を出力する。
                    for (int i = 0; i < usernum; i++)
                    {
                        if (results[i] == 1)
                        {
                            Console.WriteLine("ユーザ" + (i + 1) + "は勝ちです。");
                        }
                        else if (results[i] == 0)
                        {
                            Console.WriteLine("ユーザ" + (i + 1) + "は負けです。");
                        }
                    }
                    int retryflg = 0;
                    while (retryflg == 0)
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
                                retryflg = 1;
                                if (re == 1)
                                {
                                    battlefinish = 0;
                                }
                                else if (re == 2)
                                {
                                    battlefinish = 1;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("1,2いずれかの数値を入力してください。");
                        }
                    }

                }
            }
        }


        // じゃんけんの手1~3を出している場合は1、それ以外を出している場合は0を返す。
        private static int CheckJankenNum(string input)
        {
            if (int.TryParse(input, out int i))
            {
                if (i > 0 && i < 4)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }
    }
}
