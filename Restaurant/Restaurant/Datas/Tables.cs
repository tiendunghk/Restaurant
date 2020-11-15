using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Datas
{
    public static class Tables
    {
        public static List<Table> ListTables = new List<Table>
        {
            new Table{Id="table1",TableName="Bàn số 1",Status=TableStatus.CLEAN},
            new Table{Id="table2",TableName="Bàn số 2",Status=TableStatus.CLEAN},
            new Table{Id="table3",TableName="Bàn số 3",Status=TableStatus.DIRTY},
            new Table{Id="table4",TableName="Bàn số 4",Status=TableStatus.OCCUPIED},
            new Table{Id="table5",TableName="Bàn số 5",Status=TableStatus.DIRTY},
            new Table{Id="table6",TableName="Bàn số 6",Status=TableStatus.DIRTY},
            new Table{Id="table7",TableName="Bàn số 7",Status=TableStatus.CLEAN},
            new Table{Id="table8",TableName="Bàn số 8",Status=TableStatus.CLEAN},
        };
    }
}
