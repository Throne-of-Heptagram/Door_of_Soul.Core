﻿using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.HexagramEntranceServer
{
    public abstract class VirtualSystem : System
    {
        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public abstract OperationReturnCode DeviceRegister(int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage);
    }
}
