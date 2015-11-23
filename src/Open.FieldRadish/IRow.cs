using System;
using System.Collections;
using System.Data;

namespace FieldRadish
{
    public interface IRow
    {
        ITable Table { get; }
        Hashtable Data { get; }
        object Element { get; }        
        Type ElementType { get; }

        TResult Get<TResult>(int columnIndex);
        TResult Get<TResult>(string columnName);
    }
}
