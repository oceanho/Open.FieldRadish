using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace FieldRadish
{
    public interface ITable : IEnumerable, ISerializable, IXmlSerializable
    {
        /// <summary>
        /// 定义一个数据行存储对象
        /// </summary>
        IRow[] Rows { get; }

        /// <summary>
        /// 定义一个表格列(头)对象
        /// </summary>
        DataColumn[] Columns { get; }

        /// <summary>
        /// 定义一个列与索引的映射关系Hashtable
        /// </summary>
        Hashtable ColumnMap { get; }

        /// <summary>
        /// 定义一个索引器，可以通过索引对应的值
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        IRow this[int rowIndex] { get; }

        /// <summary>
        /// 定义一个表示当前正在读取的数据行
        /// </summary>
        IRow Current { get; }

        /// <summary>
        /// 定义一个方法，该方法用于将ITable的数据指向下一条可读记录，有数据可读返回True,否则返回false
        /// </summary>
        /// <returns></returns>
        bool MoveNext();

        /// <summary>
        /// 定义一个方法,该方法重置读取器，当MoveNext()方法返回false后。通过此方法可以重置计数器,然后再此可以MoveNext()
        /// </summary>
        void Reset();
    }

    public interface ITable<TElement> : ITable, IEnumerable<TElement>
    {
    }
}
