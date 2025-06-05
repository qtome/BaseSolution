namespace Base.Util.Redis
{
    /// <summary>
    /// Redis静态变量
    /// </summary>
    public class RedisConst
    {
        /// <summary>
        /// Redis配置类
        /// </summary>
        public static RedisConfig RedisConfig = null;

        #region 数据事务相关

        /// <summary>
        /// 系统错误记录信息使用redisDb(不可删除)
        /// </summary>
        public const int SystemErrorRedisDbNum = 9;
        /// <summary>
        /// 系统错误记录信息使用Key
        /// </summary>
        public const string SystemErrorRedisKey = "SystemError:";
        /// <summary>
        /// 过滤器使用记录日志
        /// </summary>
        public const string FilterLoggerRedisKey = "FilterLogger:";

        /// <summary>
        /// 权限数据缓存使用redisDb(不可删除)
        /// </summary>
        public const int PermissionRedisDbNum = 10;
        /// <summary>
        /// 权限数据缓存使用Key
        /// </summary>
        public const string PermissionRedisKey = "PermissionCache:";

        /// <summary>
        /// 数据验证，提交验证，事务完整相关的功能缓存使用redisDb(不可删除)
        /// </summary>
        public const int DBTransactionDbNum = 11;
        /// <summary>
        /// 数据库事务缓存使用Key
        /// </summary>
        public const string DBTransactionRedisKey = "DBTransaction:";

        #endregion

        #region 缓存数据相关

        /// <summary>
        /// 第三方服务缓存使用redisDb
        /// </summary>
        public const int BaseServerCacheRedisDbNum = 12;

        /// <summary>
        /// 服务缓存使用redisDb
        /// </summary>
        public const int BusinessCacheRedisDbNum = 13;

        /// <summary>
        /// 通用业务使用redisDb(下拉框选项相关)
        /// </summary>
        public const int CommonRedisDbNum = 15;
        /// <summary>
        /// 通用业务缓存使用Key
        /// </summary>
        public const string CommonCacheRedisKey = "CommonCache:";

        #endregion

    }
}
