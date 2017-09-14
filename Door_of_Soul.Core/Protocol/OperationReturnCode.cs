namespace Door_of_Soul.Core.Protocol
{
    public enum OperationReturnCode : short
    {
        Successiful,
        ParameterCountError,
        NullObject,
        DbTransactionFailed,
        DbNoChanged,
        ParameterFormateError,
        Duplicated,
        NotExisted,
    }
}
