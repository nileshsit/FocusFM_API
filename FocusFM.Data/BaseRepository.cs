using Dapper;
using FocusFM.Common.Helpers;
using FocusFM.Model.Config;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace FocusFM.Data
{
    public abstract class BaseRepository
    {
        #region Fields
        public readonly IOptions<DataConfig> _connectionString;
        #endregion

        #region Constructor
        public BaseRepository(IOptions<DataConfig> connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region SQL Methods

        private async Task<T> WithConnectionAsync<T>(Func<SqlConnection, Task<T>> getData)
        {
            string decryptedConn = EncryptionDecryption.GetDecrypt(_connectionString.Value.DefaultConnection);
            using (SqlConnection con = new SqlConnection(decryptedConn))
            {
                await con.OpenAsync();
                return await getData(con);
            }
        }
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return WithConnectionAsync(con =>
                con.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure));
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return WithConnectionAsync(con =>
                con.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure));
        }

        public Task<int> ExecuteAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return WithConnectionAsync(con =>
                con.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure));
        }

        public Task<SqlMapper.GridReader> QueryMultipleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return WithConnectionAsync(con =>
                con.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure));
        }

        #endregion
    }
}