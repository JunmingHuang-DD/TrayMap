using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN3Automation.ProductSummary
{
    /// <summary>
    /// 使用Sqlite这个ORM: https://github.com/praeclarum/sqlite-net
    /// 需要.net 4.5, 添加的引用：
    /// SQLite-net
    /// SQLitePCLRaw.batteries_green
    /// SQLitePCLRaw.batteries.v2
    /// SQLitePCLRaw.core
    /// SQLitePCLRaw.provider.e_sqlite3
    /// 
    /// 以及chart控件的：System.Windows.Forms.DataVisualization
    /// 
    /// 对于sqlite3的dll，需要命名为e_sqlite3.dll
    /// </summary>
    public class ProductDataManage
    {
        SQLiteAsyncConnection _connect;

        public ProductDataManage(string datePath)
        {
            _connect = new SQLiteAsyncConnection(datePath);

            try
            {
                _connect.CreateTableAsync<ProductModel>();
            }
            catch (Exception)
            {

                throw;
            }          
        }


        #region 添加数据
        public async Task AddProduct(int pass, int fail)
        {
            var pd = new ProductModel()
            {
                Time = DateTime.Now,
                Pass = pass,
                Fail = fail
            };

            await _connect.InsertAsync(pd);
        }

        public async Task AddProduct(DateTime date, int pass, int fail)
        {
            var pd = new ProductModel()
            {
                Time = date,
                Pass = pass,
                Fail = fail
            };

            await _connect.InsertAsync(pd);
        }

        public async Task AddProduct(DateTime date, int pass, int fail, int skip, string error, string remark)
        {
            var pd = new ProductModel()
            {
                Time = date,
                Pass = pass,
                Fail = fail,
                Skip = skip,
                Error = error,
                Remark = remark
            };

            await _connect.InsertAsync(pd);
        }

        public async Task AddPass(int pass)
        {
            var pd = new ProductModel()
            {
                Time = DateTime.Now,
                Pass = pass,
                Fail = 0,
                Meterial=0
            };

            await _connect.InsertAsync(pd);
        }

        public async Task AddFail(int fail, string error = null)
        {
            var pd = new ProductModel()
            {
                Time = DateTime.Now,
                Pass = 0,
                Fail = fail,
                Error = error,
                Meterial=0
            };

            await _connect.InsertAsync(pd);
        }

        public async Task AddMeterial(int meterial, string error = null)
        {
            var pd = new ProductModel()
            {
                Time = DateTime.Now,
                Pass = 0,
                Fail = 0,
                Error = error,
                Meterial=1
            };

            await _connect.InsertAsync(pd);
        }
        #endregion

        #region 查询数据
        /// <summary>
        /// 获取一段时间内的每天的生产量数据
        /// </summary>
        /// <param name="fromDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public async  Task<List<DailySummary>> GetDailySummary(DateTime fromDate, DateTime endDate)
        {
            var sday = fromDate - TimeSpan.FromDays(1);
            var eday = endDate + TimeSpan.FromDays(1);


            var totalOutput = _connect.Table< ProductModel>()
                        .Where(p => p.Time > sday && p.Time < eday).OrderBy(p => p.Time);

            var totalList = await totalOutput.ToListAsync();

            var summary = from p in totalList
                          group p by new { yy = p.Time.Year, mm = p.Time.Month, dd = p.Time.Day }
                            into s
                          select new DailySummary()
                          {
                              Year = s.Key.yy,
                              Month = s.Key.mm,
                              Day = s.Key.dd,
                              Pass = (from ps in s select ps.Pass).Sum(),
                              Fail = (from ps in s select ps.Fail).Sum(),
                              Skip = (from ps in s select ps.Skip).Sum(),
                              Meterial = (from ps in s select ps.Meterial).Sum()
                          };

            return summary.ToList();
        }


        /// <summary>
        /// 获取某天按小时分布的数据
        /// </summary>
        /// <param name="date">查询日期</param>
        /// <returns></returns>
        public async Task<List<HourlySummary>> GetHourlySummary(DateTime date)
        {
            var sd = new DateTime(date.Year, date.Month, date.Day);
            var ed = new DateTime(date.Year, date.Month, date.Day) + TimeSpan.FromDays(1);

            var totalOutput = _connect.Table<ProductModel>()
                        .Where(p => p.Time > sd && p.Time < ed);
            var totalList = await totalOutput.ToListAsync();

            var hourly = from p in totalList
                         where p.Time < ed && p.Time > sd
                         orderby p.Time
                         group p by new { Day = p.Time.Day, Hour = p.Time.Hour } into s

                         select new HourlySummary()
                         {
                             Day = $"{s.Key.Day:D2}-{s.Key.Hour:D2}",
                             Hour = s.Key.Hour,
                             Pass = (from ps in s select ps.Pass).Sum(),
                             Fail = (from ps in s select ps.Fail).Sum(),
                             Skip = (from ps in s select ps.Skip).Sum(),
                             Meterial=(from ps in s select ps.Meterial).Sum()
                         };

            return hourly.ToList();
        }

        /// <summary>
        /// 获取一段时间按小时分布的数据
        /// </summary>
        /// <param name="start">查询开始日期</param>
        /// <param name="end">查询结束日期</param>
        /// <returns></returns>
        public async Task<List<HourlySummary>> GetHourlySummary(DateTime start, DateTime end)
        {
            var sd = new DateTime(start.Year, start.Month, start.Day);
            var ed = new DateTime(end.Year, end.Month, end.Day) + TimeSpan.FromDays(1);

            var totalOutput = _connect.Table<ProductModel>()
                        .Where(p => p.Time > sd && p.Time < ed);
            var totalList = await totalOutput.ToListAsync();

            var hourly = from p in totalList
                         where p.Time < ed && p.Time > sd
                         orderby p.Time
                         group p by new { Day = p.Time.Day, Hour = p.Time.Hour } into s

                         select new HourlySummary()
                         {
                             Day = $"{s.Key.Day:D2}-{s.Key.Hour:D2}",
                             Hour = s.Key.Hour,
                             Pass = (from ps in s select ps.Pass).Sum(),
                             Fail = (from ps in s select ps.Fail).Sum(),
                             Skip = (from ps in s select ps.Skip).Sum()
                         };

            return hourly.ToList();
        }
        #endregion
    }

    /// <summary>
    /// 指定对应的数据库类型，用于记录生产数据
    /// </summary>
    public class ProductModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public int Pass { get; set; }

        public int Fail { get; set; }

        public int Skip { get; set; }

        public int Meterial { get; set; }

        /// <summary>
        /// 类似于Error Code
        /// </summary>
        [MaxLength(40)]
        public string Error { get; set; }


        [MaxLength(60)]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 按照小时查询的数据
    /// </summary>
    public class HourlySummary
    {
        public string Day { get; set; }

        public int Hour { get; set; }

        public int Pass { get; set; }

        public int Fail { get; set; }

        public int Skip { get; set; }
        public int Meterial { get; set; }
    }

    /// <summary>
    /// 按照天的查询数据
    /// </summary>
    public class DailySummary
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Pass { get; set; }

        public int Fail { get; set; }

        public int Skip { get; set; }
        public int Meterial { get; set; }
    }
}
