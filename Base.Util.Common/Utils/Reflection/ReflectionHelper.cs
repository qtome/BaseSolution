using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Base.Util.Common.Utils.Reflection
{
    /// <summary>
    /// 反射 辅助类#
    /// </summary>
    public static class ReflectHelper
    {
        #region 对象相关

        /// <summary>
        /// 反射获取对象的属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static object ReflectGetter(this object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            object propertyValue = propertyInfo.GetValue(obj, null);
            return propertyValue;
        }

        /// <summary>
        /// 反射设置对象的属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns></returns>
        public static void ReflectSetter(this object obj, string propertyName, object propertyValue)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            propertyInfo.SetValue(obj, propertyValue, null);
        }

        /// <summary>
        /// 表达式获取对象的属性值
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static Func<T, object> ExpressionGetter<T>(string propertyName)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyName);

            // 对象实例
            ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "obj");

            // 转换参数为真实类型
            UnaryExpression unaryExpression = Expression.Convert(parameterExpression, type);

            // 调用获取属性的方法
            MethodCallExpression callMethod = Expression.Call(unaryExpression, property.GetGetMethod());
            Expression<Func<T, object>> expression = Expression.Lambda<Func<T, object>>(callMethod, parameterExpression);

            return expression.Compile();
        }

        /// <summary>
        /// 表达式设置对象的属性值
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static Action<T, object> ExpressionSetter<T>(string propertyName)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyName);

            ParameterExpression objectParameterExpression = Expression.Parameter(typeof(object), "obj");
            UnaryExpression objectUnaryExpression = Expression.Convert(objectParameterExpression, type);

            ParameterExpression valueParameterExpression = Expression.Parameter(typeof(object), "val");
            UnaryExpression valueUnaryExpression = Expression.Convert(valueParameterExpression, property.PropertyType);

            // 调用设置属性的方法
            MethodCallExpression body = Expression.Call(objectUnaryExpression, property.GetSetMethod(), valueUnaryExpression);
            Expression<Action<T, object>> expression = Expression.Lambda<Action<T, object>>(body, objectParameterExpression, valueParameterExpression);

            return expression.Compile();
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static TResult CreateInstance<TResult>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                //命名空间.类名,程序集
                string path = nameSpace + "." + className + "," + assemblyName;
                //加载类型
                Type type = Type.GetType(path);
                //根据类型创建实例
                object obj = Activator.CreateInstance(type, true);
                //类型转换并返回
                return (TResult)obj;
            }
            catch
            {
                //发生异常时，返回类型的默认值。
                return default;
            }
        }

        #endregion

        #region 方法相关

        /// <summary>
        /// 获取之前步骤方法信息
        /// </summary>
        /// <param name="index">第几个步骤前</param>
        /// <returns></returns>
        public static MethodBase GetMethodBy(int index = 1)
        {
            try
            {
                return new StackTrace().GetFrame(index).GetMethod();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取之前步骤方法信息
        /// </summary>
        /// <param name="index">第几个步骤前</param>
        /// <returns></returns>
        public static MethodBase GetMethodByFrame(int index = 1)
        {
            try
            {
                MethodBase methodBy = ReflectHelper.GetMethodBy(index);
                if (methodBy.Name.Contains("Start") && index >= 1)
                {
                    methodBy = ReflectHelper.GetMethodBy(index - 1);
                }

                if (methodBy.Name.Contains("MoveNext"))
                {
                    Regex regex = new Regex("<(\\w+)>.*");
                    var methodName = regex.Match(methodBy.DeclaringType.Name).Groups[1].Value;
                    for (int indexAdd = 1; indexAdd <= 5; indexAdd++)
                    {
                        if (methodName == methodBy.Name) break;
                        methodBy = ReflectHelper.GetMethodBy(index + indexAdd);
                    }
                }

                return methodBy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 步骤方法名
        /// </summary>
        /// <param name="index">第几个步骤前</param>
        /// <returns></returns>
        public static string GetMethodNameByFrame(int index = 1)
        {
            try
            {
                var method = GetMethodBy(index);
                if (method.Name.Contains("Start") && index >= 1)//判断生产环境
                {
                    method = GetMethodBy(index - 1);
                }
                if (method.Name.Contains("MoveNext"))//异步方法
                {
                    var regex = new Regex(@"<(\w+)>.*");
                    return regex.Match(method.DeclaringType.Name).Groups[1].Value;
                }
                return method.Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 调用方法实例
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
        public static TResult GetInvokeMethod<TResult>(string assemblyName, string nameSpace, string className, string methodName, object[] paras)
        {
            try
            {
                //命名空间.类名,程序集
                string path = nameSpace + "." + className + "," + assemblyName;
                //加载类型
                Type type = Type.GetType(path);
                //根据类型创建实例
                object obj = Activator.CreateInstance(type, true);
                //加载方法参数类型及方法
                MethodInfo method = null;
                if (paras != null && paras.Length > 0)
                {
                    //加载方法参数类型
                    Type[] paratypes = new Type[paras.Length];
                    for (int i = 0; i < paras.Length; i++)
                    {
                        paratypes[i] = paras[i].GetType();
                    }
                    //加载有参方法
                    method = type.GetMethod(methodName, paratypes);
                }
                else
                {
                    //加载无参方法
                    method = type.GetMethod(methodName);
                }
                //类型转换并返回
                return (TResult)method.Invoke(obj, paras);
            }
            catch
            {
                //发生异常时，返回类型的默认值。
                return default;
            }
        }

        /// <summary>
        /// 调用方法实例（异步）
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
        public static async Task<TResult> GetInvokeMethodAsync<TResult>(string assemblyName, string nameSpace, string className, string methodName, object[] paras)
        {
            try
            {
                //命名空间.类名,程序集
                string path = nameSpace + "." + className + "," + assemblyName;
                //加载类型
                Type type = Type.GetType(path);
                //根据类型创建实例
                object obj = Activator.CreateInstance(type, true);
                //加载方法参数类型及方法
                MethodInfo method = null;
                if (paras != null && paras.Length > 0)
                {
                    //加载方法参数类型
                    Type[] paratypes = new Type[paras.Length];
                    for (int i = 0; i < paras.Length; i++)
                    {
                        paratypes[i] = paras[i].GetType();
                    }
                    //加载有参方法
                    method = type.GetMethod(methodName, paratypes);
                }
                else
                {
                    //加载无参方法
                    method = type.GetMethod(methodName);
                }
                //类型转换并返回
                Task task = method.Invoke(obj, paras) as Task;
                await task;
                return (TResult)task.GetType().GetProperty("Result").GetValue(task, null);
            }
            catch
            {
                //发生异常时，返回类型的默认值。
                return default;
            }
        }

        /// <summary>
        /// 调用方法实例（异步 + 私有 + 泛型方法）
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
        public static async Task<TResult> GetInvokePrivateTMethodAsync<TResult, T>(string assemblyName, string nameSpace, string className, string methodName, object[] paras)
        {
            try
            {
                //命名空间.类名,程序集
                string path = nameSpace + "." + className + "," + assemblyName;
                //加载类型
                Type type = Type.GetType(path);
                //根据类型创建实例
                object obj = Activator.CreateInstance(type, true);
                //加载方法参数类型及方法
                MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).MakeGenericMethod(typeof(T));
                //类型转换并返回
                Task task = method.Invoke(obj, paras) as Task;
                await task;
                return (TResult)task.GetType().GetProperty("Result").GetValue(task, null);
            }
            catch
            {
                //发生异常时，返回类型的默认值。
                return default;
            }
        }

        #endregion

        #region 扩展相关

        /// <summary>
        /// 验证可空类型，获取基础类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type VerifyNullableType(Type type)
        {
            //泛型
            if (type.IsGenericType)
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                //Nullable<>
                if (genericTypeDefinition == typeof(Nullable<>)) return Nullable.GetUnderlyingType(type);
            }
            //非泛型
            return type;
        }

        /// <summary>
        /// 是否自定义类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsClass(Type type)
        {
            if (type.IsPrimitive) return false;
            if (type == typeof(string)) return false;
            if (IsCollection(type)) return false;
            if (type.IsClass)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否自定义类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCustomClass(Type type)
        {
            if (type.IsPrimitive) return false;
            if (type == typeof(string)) return false;
            if (IsCollection(type)) return false;
            if (type.IsClass && !type.IsGenericType)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否集合类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCollection(Type type)
        {
            if (type == typeof(string)) return false;
            if (type.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
            {
                return true;
            }
            // 检查Type是否直接实现了IEnumerable接口
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 转换数据类型，判断是否为空
        /// </summary>
        /// <param name="item">输入数据</param>
        /// <param name="type">目标类型</param>
        /// <returns></returns>
        public static object GetValueByChangeType(string item, Type type)
        {
            if (string.IsNullOrWhiteSpace(item)) return null;
            try
            {
                return Convert.ChangeType(item, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
