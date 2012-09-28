using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceHelper
{
    //http://stackoverflow.com/questions/5593394/dynamic-queries-expando-dynamic-object-type
    //public interface IDataServiceCollection<T> : ICollection {
    //    void LoadAsync(Uri requestUri);
    //    void LoadAsync(IQueryable<T> query);
    //    event EventHandler<LoadCompletedEventArgs> LoadCompleted;
    //}
    //public class DataServiceCollectionWrapper {
    //    public static IDataServiceCollection<T> CreateDataServiceCollection<T>(DataServiceContext xpoContext) {
    //        return (IDataServiceCollection<T>)CreateGeneric<T>(typeof(MyDataServiceCollection<>), xpoContext);
    //    }
    //    public static object CreateGeneric<T>(Type generic, params object[] args) {
    //        System.Type specificType = generic.MakeGenericType(new System.Type[] { typeof(T) });
    //        return Activator.CreateInstance(specificType, args);
    //    }
    //    private class MyDataServiceCollection<T> : DataServiceCollection<T>, IDataServiceCollection<T> {
    //        public MyDataServiceCollection(DataServiceContext context) : base(context) { }
    //    }
    //}
    public interface IDataServiceCollection<T> : IDataServiceCollection {
        void LoadAsync(IQueryable<T> query);
    }
    public interface IDataServiceCollection : ICollection {
        void LoadAsync(Uri requestUri);
        event EventHandler<LoadCompletedEventArgs> LoadCompleted;
    }
    public class DataServiceCollectionWrapper {
        public static IDataServiceCollection CreateDataServiceCollection(Type ViewType, DataServiceContext xpoContext) {
            return (IDataServiceCollection)CreateGeneric(typeof(MyDataServiceCollection<>), ViewType, xpoContext);
        }
        private static object CreateGeneric(Type generic, Type innerType, params object[] args) {
            System.Type specificType = generic.MakeGenericType(new System.Type[] { innerType });
            return Activator.CreateInstance(specificType, args);
        }
        private class MyDataServiceCollection<T> : DataServiceCollection<T>, IDataServiceCollection<T> {
            public MyDataServiceCollection(DataServiceContext context) : base(context) { }
        }
    }
}
