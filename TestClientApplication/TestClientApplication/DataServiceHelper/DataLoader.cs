using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.ComponentModel;

namespace DataServiceHelper {
    public class DataLoader {
        DataServiceContext context;
        public DataLoader(DataServiceContext context) {
            this.context = context;
        }

        public void LoadItems(Type objectType) {
            //MethodInfo createQuery = context.GetType().GetRuntimeMethod("CreateQueryT", new Type[] { typeof(string) });


            

            // context.GetType().getm
            //context.GetType().GetMethod("CreateQuery", BindingFlags.Instance | BindingFlags).MakeGenericMethod(type);

            IQueryable<INotifyPropertyChanged> query = (from p in context.CreateQuery<INotifyPropertyChanged>(objectType.Name) select p);
                        //IQueryable<T> query = (from p in context.CreateQuery<T>(typeof(T).Name) select p);
            Uri targetUri = ((System.Data.Services.Client.DataServiceQuery)(query)).RequestUri;
            //var query = (from p in (DataServiceQuery)createQuery.Invoke(context, new object[] { objectType.Name }) select p);
            IDataServiceCollection collection = DataServiceCollectionWrapper.CreateDataServiceCollection(objectType, context);
            collection.LoadCompleted += collection_LoadCompleted;
            collection.LoadAsync(targetUri);
        }
        private Dictionary<Type, DataServiceQuery> objectQueries = new Dictionary<Type, DataServiceQuery>();
        //private DataServiceQuery<T> CreateQueryT<T>(IList<String> includeMemberNames) {
        //    DataServiceQuery objectQuery = null;
        //    if(!objectQueries.TryGetValue(typeof(T), out objectQuery)) {
        //        objectQuery = context.CreateQuery<T>(GetEntitySetName(typeof(T))).OfType<T>();
        //        objectQueries.Add(typeof(T), objectQuery);
        //    }
        //    if(includeMemberNames != null) {
        //        foreach(String includeMemberName in includeMemberNames) {
        //            objectQuery = ((DataServiceQuery<T>)objectQuery).Include(includeMemberName);
        //        }
        //    }
        //    return (DataServiceQuery<T>)objectQuery;
        //}

        private void collection_LoadCompleted(object sender, LoadCompletedEventArgs e) {
            if(LoadCompleted != null) {
                LoadCompleted(sender, e);
            }
        }

        public event EventHandler<LoadCompletedEventArgs> LoadCompleted;
    }
}
