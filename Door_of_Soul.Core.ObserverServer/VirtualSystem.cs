﻿namespace Door_of_Soul.Core.ObserverServer
{
    public abstract class VirtualSystem : System
    {
        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }
    }
}
