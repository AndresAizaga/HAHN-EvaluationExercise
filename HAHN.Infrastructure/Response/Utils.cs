using HAHN.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Text;

namespace HAHN.Infrastructure.Response
{
    public class Utils
    {
        public static async Task<ManagerResponse<List<T>>> PageList<T>(ManagerResponse<List<T>> ret, IQueryable<T> list, int? _page, int? _pageSize)
        {
            var count = await list.CountAsync();


            ret.RecordsCount = count;
            if (count == 0)
            {
                ret.Pages = 0;
                ret.Page = 1;
            }
            else if (_page.Value > 0 && _pageSize.Value > 0)
            {
                ret.Pages = Convert.ToInt32(Math.Ceiling((decimal)ret.RecordsCount / (decimal)_pageSize.Value));
                if (_page > ret.Pages)
                {
                    _page = ret.Pages;
                }

                ret.Page = _page.Value;

                //retObj = retObj.Skip<T>((_page.Value - 1) * _pageSize.Value).Take(_pageSize.Value).ToList();
                list = list.Skip((_page.Value - 1) * _pageSize.Value);
                list = list.Take(_pageSize.Value);

            }

            var retObj = await list.ToListAsync();

            return ret.SetData(retObj);
        }


        public static async Task SaveChangesAsync(DbContext context)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.Data)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.ToString());
                    sb.AppendLine();
                }


            }
            catch (DbException ex2)
            {
                throw new Exception(FlattenException(ex2));

            }
        }

        public static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
