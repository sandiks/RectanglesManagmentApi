﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RectanglesManagmentApi.Dapper
{
    [ExcludeFromCodeCoverage]
    public class GenericListDataReader<T> : IDataReader where T : class
    {
        public GenericListDataReader(IEnumerable<T> data)
        {
            _ordinals = new Dictionary<string, int>();

            _enumerator = data.GetEnumerator();

            var ordinal = 0;

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                _properties.Add(propertyInfo);
                _ordinals.Add(propertyInfo.Name.ToUpper(), ordinal);
                ordinal++;
            }
        }

        public bool Read()
        {
            return _enumerator.MoveNext();
        }

        public void Dispose()
        {
            Close();
        }

        public int FieldCount => _properties.Count;

        public Type GetFieldType(int i)
        {
            return _properties[i].PropertyType;
        }

        public string GetName(int i)
        {
            return _properties[i].Name;
        }

        public object GetValue(int i)
        {
            return _properties[i].GetValue(_enumerator.Current);
        }

        public void Close()
        {
            _enumerator.Dispose();
        }

        public int GetOrdinal(string name)
        {
            return _ordinals[name.ToUpper()];
        }

        private readonly IEnumerator<T> _enumerator;
        private readonly List<PropertyInfo> _properties = new List<PropertyInfo>();
        private readonly Dictionary<string, int> _ordinals;

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
