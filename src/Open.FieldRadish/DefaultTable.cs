/***************************************************************
*       
* add by hehai 2015/11/23 15:01:19
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
    /// <summary>
    /// ITable默认实现类
    /// </summary>
    public class DefaultTable : ITable
    {
        private IRow[] _rows;
        private IRow _currentRow;

        private int _rowCount = 0;
        private DataColumn[] _columns;

        private int _currentIndex = -1;
        private Hashtable _columnsHashMap;

        private readonly IEnumerable _enumerable;
        public DefaultTable(IEnumerable enumerable)
        {
            _enumerable = enumerable;

            Type eleType = _enumerable.GetType();
            if (eleType.IsGenericType)
                _rowCount = (int)eleType.GetProperty("Count").GetValue(_enumerable, null);
            else
            {
                if (eleType.IsArray)
                    _rowCount = (int)eleType.GetProperty("Length").GetValue(_enumerable, null);
            }

            int _rowsIndex = -1;
            int _columnIndex = -1;
            PropertyInfo[] _properties = null;
            _rows = new DefaultRow[_rowCount];

            foreach (var item in _enumerable)
            {
                _rowsIndex++;
                if (_rowsIndex == 0)
                {
                    _columnsHashMap = new Hashtable();
                    _properties = item.GetType().GetProperties();
                    _columns = new DataColumn[_properties.Length];
                    foreach (var property in _properties)
                    {
                        _columnIndex++;
                        _columnsHashMap.Add(_columnIndex, property.Name);
                        _columns[_columnIndex] = new DataColumn(property.Name, property.ReflectedType);
                    }
                }
                _rows[_rowsIndex] = new DefaultRow(this, item);
            }
        }

        public IRow[] Rows
        {
            get { return _rows; }
        }

        public IRow this[int rowIndex]
        {
            get
            {
                if (_rows == null)
                    throw new ArgumentNullException();
                if (rowIndex >= _rows.Length)
                    throw new ArgumentException(" rowIndex over ", "");
                return _rows[rowIndex];
            }
        }

        public IRow Current
        {
            get { return _currentRow; }
        }

        public bool MoveNext()
        {
            _currentIndex++;
            _currentRow = (_currentIndex >= _rowCount) ? null : Rows[_currentIndex];
            return _currentRow != null;
        }

        public IEnumerator GetEnumerator()
        {
            return new RowEnumerator(Rows);
        }

        public DataColumn[] Columns
        {
            get { return _columns; }
        }

        public Hashtable ColumnMap
        {
            get { return _columnsHashMap; }
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    public class DefaultTable<TElement> : DefaultTable, ITable<TElement>
    {
        private IEnumerable<TElement> _elementEnumerable;
        public DefaultTable(IEnumerable<TElement> elementEnumerable)
            : base(elementEnumerable)
        {
            this._elementEnumerable = elementEnumerable;
        }
        public new IEnumerator<TElement> GetEnumerator()
        {
            return _elementEnumerable.GetEnumerator();
        }
    }

    public class RowEnumerator : IEnumerator
    {
        private IRow[] _rows;
        private int _currentIndex = -1;

        public RowEnumerator(IRow[] rows)
        {
            this._rows = rows;
        }
        public object Current
        {
            get { return _rows[_currentIndex]; }
        }

        public bool MoveNext()
        {
            _currentIndex++;
            return _currentIndex < _rows.Length;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }
    }
}
