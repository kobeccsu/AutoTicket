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
    }
}
