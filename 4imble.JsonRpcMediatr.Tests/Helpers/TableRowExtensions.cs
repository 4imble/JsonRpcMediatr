using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace _4imble.JsonRpcMediatr.Tests.Helpers
{
    public static class TableRowExtensions
    {
        public static T GetRowValue<T>(this TableRow row, string key)
        {
            if (IsDateTime<T>())
                return (T)Convert.ChangeType(DateTime.ParseExact(row[key], "dd/MM/yyyy", CultureInfo.InvariantCulture), typeof(T));

            if (!row.ContainsKey(key))
                return default(T);

            return (T)Convert.ChangeType(row[key], typeof(T));
        }

        private static bool IsDateTime<T>()
        {
            T checkType = default;
            return checkType is DateTime;
        }

        public static T GetTableValue<T>(this Table table, string key)
        {
            if (table.RowCount != 1)
                throw new Exception($"Unsupported row count ({table.RowCount})");

            return table.Rows[0].GetRowValue<T>(key);
        }

        public static TEnum GetTableEnumValue<TEnum>(this Table table, string key)
        {
            if (table.RowCount != 1)
                throw new Exception($"Unsupported row count ({table.RowCount})");

            return (TEnum) Enum.Parse(typeof(TEnum), table.Rows[0][key]);
        }
    }
}
