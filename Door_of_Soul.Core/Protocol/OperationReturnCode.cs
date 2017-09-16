namespace Door_of_Soul.Core.Protocol
{
    public enum OperationReturnCode : short
    {
        UndefinedError,
        Successiful,
        ParameterCountError,
        NullObject,
        DbTransactionFailed,
        DbNoChanged,
        ParameterFormateError,
        Duplicated,
        NotExisted,
        AuthenticationFailed,
    }
}
