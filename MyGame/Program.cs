﻿using System;

namespace MyGame
{
    public static class Program
    {
        [STAThread]
        static void Main() 
        {
            using (var game = new GameRoot())
            {
                game.Run(); 
            }
        }
    }
}