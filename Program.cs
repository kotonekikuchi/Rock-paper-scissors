// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1
{
    using System;

    internal class Program
    {
        private static void Main (string[] args)
        {
            int personCheck = 0; // 人数入力の数値チェック
            int usernum = 0;
            int cpunum = 0;
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

            // じゃんけんの処理を行う。（再帰みたいにできる？）
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
            if (rockNum > 0 && paperNum > 0 && scissorsNum > 0) 
            {
                Console.WriteLine("あいこです。");
            }
            else if (rockNum == usernum + cpunum || paperNum == usernum + cpunum || scissorsNum == usernum + cpunum)
            {
                Console.WriteLine("あいこです。");
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

         //   int result = 0;
         //   while (result == 0)
         //   {
         //       Console.WriteLine("じゃんけんの手を入力してください。1:グー 2:チョキ 3:パー");
         //       var userhand = Console.ReadLine();
         //       int hand;
         //       if (int.TryParse(userhand, out hand))
         //       {
         //           if (hand > 0 && hand < 4)
         //           {
         //               result = Janken(hand);
         //           }
         //           else
         //           {
         //               Console.WriteLine("1,2,3いずれかの数値を入力してください。");
         //           }
         //       }
         //       else
         //       {
         //           Console.WriteLine("1,2,3いずれかの数値を入力してください。");
         //       }

         //       if (result == 1)
         //       {
         //           Console.WriteLine("あなたの負けです");
         //           result = Retry();
         //       }
         //       else if (result == 2)
         //       {
         //           Console.WriteLine("あなたの勝ちです");
         //           result = Retry();
         //       }
         //}
        }

        public static int CheckJankenNum(string input)　// じゃんけんの手1~3を出している場合は1、それ以外を出している場合は0を返す。
        {
            if(int.TryParse(input, out int i))
            {
                if (i > 0 && i < 4)
                {
                    return 1;
                } else
                {
                    return 0;
                }
            }
            return 0;
        }
        public static int Janken(int userhand)
        {
            Random random = new Random();
            int cpuhand = random.Next(3);

            if (userhand == cpuhand)
            {
                Console.WriteLine("あいこです。もう一回手を入力してください。1:グー 2:チョキ 3:パー");
            }
            else
            {
                int result = (userhand + 3 - cpuhand) % 3;
                if (result == 1)
                {
                    return 1;
                }
                else if (result == 2)
                {
                    return 2;
                }
            }

            return 0;
        }

        public static int Retry() // 再度じゃんけんを行うかどうかチェックする。
        {
            int result;
            Console.WriteLine("再度じゃんけんをしますか？ 1:はい 2:いいえ"); // 1,2以外を入力した場合は終了させる。
            var retry = Console.ReadLine();

            int j;
            if (int.TryParse(retry, out j))
            {
                if (j == 1)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }
            } else
            {
                result = 1;
            }

            return result;
        }
    }
}
