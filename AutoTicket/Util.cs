using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTicket
{
    public class Util
    {
        public static void ClassToDataRow<T>(DataTable table, T classObject) where T : class
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                //if (IsColumnByNameInvalid(table.Columns, property.Name)) { continue; }
                if (!table.Columns.Contains(property.Name))
                {
                    table.Columns.Add(property.Name);
                }
            }
        }

        public static void ClassToField<T>(DataTable table, DataRow row, T classObject) where T : class
        {
            //bool rowChanged = false;
            //DataRow row = table.NewRow();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                //if (IsColumnByNameInvalid(table.Columns, property.Name)) { continue; }

                //rowChanged = true;
                row[property.Name] = property.GetValue(classObject, null);

            }

            //if (!rowChanged) { return; }
            //table.Rows.Add(row);
        } 

        private static bool IsColumnByNameInvalid(DataColumnCollection columns, string propertyName)
        {
            return !columns.Contains(propertyName) || columns[propertyName] == null;
        }

        /// <summary>
        /// 于 javascript 的 escape 相同的后台方法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] ba = System.Text.Encoding.Unicode.GetBytes(s);
            for (int i = 0; i < ba.Length; i += 2)
            {
                if (ba[i + 1] == 0)
                {
                    //数字,大小写字母,以及"+-*/._"不变
                    if (
                          (ba[i] >= 48 && ba[i] <= 57)
                        || (ba[i] >= 64 && ba[i] <= 90)
                        || (ba[i] >= 97 && ba[i] <= 122)
                        || (ba[i] == 42 || ba[i] == 43 || ba[i] == 45 || ba[i] == 46 || ba[i] == 47 || ba[i] == 95)
                        )//保持不变
                    {
                        sb.Append(Encoding.Unicode.GetString(ba, i, 2));

                    }
                    else//%xx形式
                    {
                        sb.Append("%");
                        sb.Append(ba[i].ToString("X2"));
                    }
                }
                else
                {
                    sb.Append("%u");
                    sb.Append(ba[i + 1].ToString("X2"));
                    sb.Append(ba[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }
    }
}
