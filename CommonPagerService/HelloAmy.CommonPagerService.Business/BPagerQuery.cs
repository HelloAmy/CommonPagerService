using HelloAmy.CommonPagerService.Factory;
using HelloAmy.CommonPagerService.IDAL;
using HelloAmy.CommonPagerService.Model.InParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAmy.CommonPagerService.Business
{
    public class BPagerQuery
    {
        public MPagerOutParam PagerQuery(MPagerInParam param)
        {
            if (param.PageIndex < 1)
            {
                param.PageIndex = 1;
            }

            int start = (param.PageIndex - 1) * param.PageSize;
            int end = param.PageIndex * param.PageSize;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT {0}", param.FieldNames).AppendLine();
            sql.AppendFormat("FROM {0}", param.TableName).AppendLine();
            sql.Append(param.Condition).Append(" ");
            if (!string.IsNullOrEmpty(param.Sort))
            {
                sql.Append("ORDER BY " + param.Sort).Append(" ");
            }

            sql.AppendFormat("LIMIT {0}, {1}", start, end);

            StringBuilder countsql = new StringBuilder();
            countsql.Append("SELECT COUNT(1)").AppendLine();
            countsql.AppendFormat("FROM {0}", param.TableName).AppendLine();
            countsql.Append(param.Condition);

            MPagerOutParam outParam = new MPagerOutParam();
            IPagerQuery dao = DALFactory.GetPagerQueryDao();
            using (var conn = ConnectionFactory.GetMySqlConnection(param.DataBaseName))
            {
                outParam.RowCount = dao.GetRowsCount(null, conn, countsql.ToString(), param.Parameters.ToArray());
                outParam.PageData = dao.GetDataSet(null, conn, sql.ToString(), param.Parameters.ToArray());
            }

            outParam.PageIndex = param.PageIndex;
            int result = outParam.RowCount / param.PageSize;
            outParam.PageCount = outParam.RowCount % param.PageSize == 0 ? result : result + 1;

            return outParam;
        }
    }
}
