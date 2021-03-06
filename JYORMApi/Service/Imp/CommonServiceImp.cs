using JYORMApi.Mapper;
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
                 var sysUser = new SysUser
                 {
                     UserName = "江岩1",
                     Password = "123456",
                     CreateBy = 1,
                     UpdateBy = 1
                 };
                 await _client.Insertable(sysUser).IgnoreColumns(ignoreNullColumn: true).ExecuteCommandAsync();
                 var sysUser1 = new SysUser
                 {
                     UserName = "江岩1",
                     Password = "123456",
                     CreateBy = 1,
                     UpdateBy = 1
                 };
                 await _client.Insertable(sysUser1).IgnoreColumns(ignoreNullColumn: true).ExecuteCommandAsync();
             });
            if (!result.IsSuccess)
            {
                throw result.ErrorException;
            }
        }

        public async Task<bool> CreateModel(string nameStr)
        {
            var filterField = typeof(BaseEntity).GetProperties().Select(x => x.Name).ToList();
            var result = await _commonMapper.GetTableCreateDesc();
            var modelList = new List<KeyValuePair<string, string>>();

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
                        x.DataType = x.DataType.MySqlTypeChange(x.IsNullable == "YES");
                        x.ColumnName = x.ColumnName.StrChange();
                        return x;
                    }).Where(x => !filterField.Contains(x.ColumnName)).ToList()
                };
                var template = new StringTemplate(StringTemplate.GetModelTemplate());
                var compileResult = template.Compile(sourceItem);

                modelList.Add(new KeyValuePair<string, string>(sourceItem.TableName, compileResult));
            }
            // 写入文件到本地
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            foreach (var item in modelList)
            {
                File.WriteAllText(Path.Combine(basePath, $"{item.Key}.cs"), item.Value);
            }
            return true;
        }
    }
}
