using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RS.Models
{
    public class QueryParameter
    {
        private string _name;
        private System.Data.SqlDbType _type;
        private object _value;

        private QueryParameter()
        {
        }

        public static QueryParameter Create()
        {
            return new QueryParameter();
        }

        public QueryParameter Name(string name)
        {
            _name = name;
            return this;
        }

        public QueryParameter Type(System.Data.SqlDbType type)
        {
            _type = type;
            return this;
        }

        public QueryParameter Value(object value)
        {
            _value = value;
            return this;
        }

        public SqlParameter Build()
        {
            SqlParameter parameter = new SqlParameter(_name, _type);
            parameter.Value = _value;
            return parameter;
        }
    }
}