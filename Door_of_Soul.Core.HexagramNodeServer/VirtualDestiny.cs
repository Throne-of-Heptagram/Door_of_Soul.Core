﻿namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualDestiny
    {
        public static VirtualDestiny Instance { get; private set; }
        public static void Initial(VirtualDestiny instance)
        {
            Instance = instance;
        }
    }
}