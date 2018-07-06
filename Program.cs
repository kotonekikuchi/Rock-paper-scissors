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
            Janken.PlayerNum(); // プレイヤーの人数を設定
            bool isRetry = true;
            while (isRetry)
            {
                bool isDrow = true; // あいこ判定
                while (isDrow)
                {
                    // ユーザの手を決める。
                    Janken.SelectUserHand();

                    // CPUの手を決める。
                    Janken.SelectCpuHand();

                    // じゃんけんの結果判定
                    isDrow = Janken.CheckJankenResult();
                }

                Janken.ResultOutput(); // 結果の出力を行う。
                isRetry = Janken.CheckRetry(); // リトライするかどうか
            }
        }
    }
}
