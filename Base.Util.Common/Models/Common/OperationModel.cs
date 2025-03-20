using System.ComponentModel;

namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 操作 结果信息类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationModel<T>
        : OperationModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OperationModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        /// <param name="operation">操作类型</param>
        public OperationModel(T data, bool result, Operation operation)
        {
            Data = data;
            IsSuccessful = result;
            Type = operation.ToString();
        }

        /// <summary>
        /// 新增成功的数据信息
        /// </summary>
        public T Data { get; set; } = default;
        /// <summary>
        /// 操作 是否成功
        /// </summary>
        public bool IsSuccessful { get; set; } = false;
        /// <summary>
        /// 操作类型
        /// </summary>
        public string Type { get; set; } = Operation.无动作.ToString();

        /// <summary>
        /// 返回 新增 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        public static OperationModel<T> Post(T data, bool result)
        {
            return new OperationModel<T>(data, result, Operation.新增);
        }
        /// <summary>
        /// 返回 全量更新 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        public static OperationModel<T> Put(T data, bool result)
        {
            return new OperationModel<T>(data, result, Operation.全量更新);
        }
        /// <summary>
        /// 返回 部分更新 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        public static OperationModel<T> Patch(T data, bool result)
        {
            return new OperationModel<T>(data, result, Operation.部分更新);
        }
        /// <summary>
        /// 返回 删除 操作结果模型
        /// </summary>
        /// <param name="result">操作结果</param>
        public static OperationModel<T> Delete(bool result)
        {
            return new OperationModel<T>(default, result, Operation.删除);
        }
        /// <summary>
        /// 返回 查看 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        public static OperationModel<T> Get(T data)
        {
            return new OperationModel<T>(data, true, Operation.查看);
        }
        /// <summary>
        /// 返回 查询 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        public static OperationModel<T> Search(T data)
        {
            return new OperationModel<T>(data, true, Operation.查询);
        }
    }

    /// <summary>
    /// 操作 结果信息类
    /// </summary>
    public class OperationModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OperationModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        /// <param name="operation">操作类型</param>
        public OperationModel(object data, bool result, Operation operation)
        {
            Data = data;
            IsSuccessful = result;
            Type = operation.ToString();
        }

        /// <summary>
        /// 新增成功的数据信息
        /// </summary>
        public object Data { get; set; } = null;
        /// <summary>
        /// 操作 是否成功
        /// </summary>
        public bool IsSuccessful { get; set; } = false;
        /// <summary>
        /// 操作类型
        /// </summary>
        public string Type { get; set; } = Operation.无动作.ToString();

        /// <summary>
        /// 返回 新增 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        public static OperationModel Post(object data, bool result)
        {
            return new OperationModel(data, result, Operation.新增);
        }
        /// <summary>
        /// 返回 全量更新 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        public static OperationModel Put(object data, bool result)
        {
            return new OperationModel(data, result, Operation.全量更新);
        }
        /// <summary>
        /// 返回 部分更新 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="result">操作结果</param>
        public static OperationModel Patch(object data, bool result)
        {
            return new OperationModel(data, result, Operation.部分更新);
        }
        /// <summary>
        /// 返回 删除 操作结果模型
        /// </summary>
        /// <param name="result">操作结果</param>
        public static OperationModel Delete(bool result)
        {
            return new OperationModel(null, result, Operation.删除);
        }
        /// <summary>
        /// 返回 查看 操作结果模型
        /// </summary>
        /// <param name="data">数据</param>
        public static OperationModel Get(object data)
        {
            return new OperationModel(data, true, Operation.查看);
        }
        /// <summary>
        /// 返回 查询 操作结果模型
        /// </summary>
        public static OperationModel Search()
        {
            return new OperationModel(null, true, Operation.查询);
        }
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// NULL
        /// </summary>
        [Description("无动作")]
        无动作 = 0,
        /// <summary>
        /// Post
        /// </summary>
        [Description("新增")]
        新增 = 1,
        /// <summary>
        /// Put
        /// </summary>
        [Description("全量更新")]
        全量更新 = 2,
        /// <summary>
        /// Patch
        /// </summary>
        [Description("部分更新")]
        部分更新 = 3,
        /// <summary>
        /// Delete
        /// </summary>
        [Description("删除")]
        删除 = 4,
        /// <summary>
        /// Get
        /// </summary>
        [Description("查看")]
        查看 = 5,
        /// <summary>
        /// Get
        /// </summary>
        [Description("查询")]
        查询 = 6,
    }
}
