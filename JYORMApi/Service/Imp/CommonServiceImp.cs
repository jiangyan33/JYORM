using JYORMApi.Dao;
using JYORMApi.Entity;
using JYORMApi.Persistence;
using MyCommonTool.Utils;
using SqlSugar;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JYORMApi.Service.Imp
{
    public class CommonServiceImp : ICommonService
    {
        private readonly CommonMapper _commonMapper;
        private readonly SqlSugarClient _client;

        public CommonServiceImp(CommonMapper commonMapper, DefaultDBClient DB)
        {
            _commonMapper = commonMapper;
            _client = DB.GetInstance;
        }

        public async Task<List<Columns>> GetTableCreateDesc()
        {
            return await _commonMapper.GetTableCreateDesc();
        }

        public async Task TestTransaction()
        {
            var result = await _client.UseTranAsync(async () =>
             {
                 var sysUser = new SysUser();
                 sysUser.UserName = "江岩1";
                 sysUser.Password = "123456";
                 sysUser.CreateBy = 1;
                 sysUser.UpdateBy = 1;
                 await _client.Insertable(sysUser).IgnoreColumns(ignoreNullColumn: true).ExecuteCommandAsync();
                 var sysUser1 = new SysUser();
                 sysUser1.UserName = "江岩1";
                 sysUser1.Password = "123456";
                 sysUser1.CreateBy = 1;
                 sysUser1.UpdateBy = 1;
                 await _client.Insertable(sysUser1).IgnoreColumns(ignoreNullColumn: true).ExecuteCommandAsync();
             });
            if (!result.IsSuccess)
            {
                throw result.ErrorException;
            }
        }

        public async Task<bool> CreateModel(string nameStr)
        {
            var result = await _commonMapper.GetTableCreateDesc();
            var modelList = new List<KeyValuePair<string, string>>();
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var path = Path.Combine(basePath, "ModelTemplate.txt");
            var sourceTemplate = File.ReadAllText(path);
            foreach (var item in result.GroupBy(x => x.TableName))
            {
                var firstItem = item.First();
                var sourceItem = new
                {
                    NameStr = nameStr,
                    firstItem.TableComment,
                    TableName = firstItem.TableName.StrChange(),
                    Columns = item.Select(x =>
                    {
                        x.DataType = x.DataType.TypeChange(x.IsNullable == "YES");
                        x.ColumnName = x.ColumnName.StrChange();
                        return x;
                    }).ToList()
                };
                var template = new StringTemplate(sourceTemplate);
                var compileResult = template.Compile(sourceItem);

                modelList.Add(new KeyValuePair<string, string>(sourceItem.TableName, compileResult));
            }
            return true;
        }
    }
}
