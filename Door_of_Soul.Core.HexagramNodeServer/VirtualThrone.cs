﻿namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualThrone
    {
        public static VirtualThrone Instance { get; private set; }
        public static void Initial(VirtualThrone instance)
        {
            Instance = instance;
        }
    }
}