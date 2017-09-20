﻿using Door_of_Soul.Core.Protocol;
using System;

namespace Door_of_Soul.Core.Client
{
    public abstract class VirtualSystem : System
    {
        public event Action<OperationReturnCode, string> OnRegisterResponse;
        public event Action<OperationReturnCode, string> OnLoginResponse;

        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public abstract OperationReturnCode Register(string answerName, string basicPassword, out string errorMessage);
        public void RegisterResponse(OperationReturnCode returnCode, string operationMessage)
        {
            OnRegisterResponse?.Invoke(returnCode, operationMessage);
        }

        public abstract OperationReturnCode Login(string answerName, string basicPassword, out string errorMessage);
        public void LoginResponse(OperationReturnCode returnCode, string operationMessage)
        {
            OnLoginResponse?.Invoke(returnCode, operationMessage);
        }

        public abstract void LoadProxyAnswer(int answerId, string answerName, int[] soulIds);
    }
}
