﻿namespace Domain.Results;

public interface IDataResult<T>:IResult
{
    T Data { get; }
}
