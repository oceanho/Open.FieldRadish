/***************************************************************
*       
* add by hehai 2015/11/23 15:16:24
*
****************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace FieldRadish
{
    public class DefaultRow : IRow
    {
        private ITable _table;
        private readonly object _element;
        private readonly Type _elementType;
        private readonly Hashtable _hashData;
        private readonly PropertyInfo[] _properties;
        public DefaultRow(ITable table, object element)
        {
            _table = table;
            _element = element;
            _hashData = new Hashtable();
            _elementType = element.GetType();

            _properties = _elementType.GetProperties();
            int _columnIndex = -1;
            foreach (var property in _properties)
            {
                foreach (var mapKey in table.ColumnMap.Keys)
                {
                    if (table.ColumnMap[mapKey].Equals(property.Name))
                    {
                        _columnIndex = (int)mapKey;
                    }
                }
                if (_columnIndex == -1)
                    throw new ArgumentException(string.Format(" Not exist {0} map", property.Name));
                _hashData.Add(_columnIndex, property.GetValue(_element, null));
            }
        }


        public TResult Get<TResult>(int columnIndex)
        {
            return (TResult)_hashData[columnIndex];
        }

        public TResult Get<TResult>(string columnName)
        {
            foreach (var item in Table.ColumnMap.Keys)
                if (Table.ColumnMap[item].Equals(columnName))
                    return Get<TResult>((int)item);
            throw new ArgumentException(string.Format("{0} map invalid",columnName));
        }

        public Type ElementType
        {
            get { return _elementType; }
        }

        public ITable Table
        {
            get { return _table; }
        }


        public Hashtable Data
        {
            get { return _hashData; }
        }

        public object Element
        {
            get { return _element; }
        }

        public override string ToString()
        {
            return "hello";
        }
    }
}
