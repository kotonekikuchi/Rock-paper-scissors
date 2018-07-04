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
            int result = 0;
            while (result == 0)
            {
                Console.WriteLine("じゃんけんの手を入力してください。1:グー 2:チョキ 3:パー");
                var usernum = Console.ReadLine();
                int hand;
                if (int.TryParse(usernum, out hand))
                {
                    if (hand > 0 && hand <= 4)
                    {
                        result = Janken(hand);
                    }
                    else
                    {
                        Console.WriteLine("1,2,3いずれかの数値を入力してください。");
                    }
                }
                else
                {
                    Console.WriteLine("1,2,3いずれかの数値を入力してください。");
                }

                if (result == 1)
                {
                    Console.WriteLine("あなたの負けです");
                    result = Retry();
                }
                else if (result == 2)
                {
                    Console.WriteLine("あなたの勝ちです");
                    result = Retry();
                }
         }
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

        public static int Retry()
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
