using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClientApplication.ODataDemoService;

namespace TestClientApplication.Views {
    public class BaseListView : TestClientApplication.Common.LayoutAwarePage {
        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState) {

            Type objectType = pageState["ObjectType"] as Type;

            App.dataLoader.LoadCompleted += dataLoader_LoadCompleted;
            App.dataLoader.LoadItems(objectType);
        }
        private void dataLoader_LoadCompleted(object sender, System.Data.Services.Client.LoadCompletedEventArgs e) {
            App.dataLoader.LoadCompleted -= dataLoader_LoadCompleted;

            ObservableCollection<Object> collection = new ObservableCollection<object>();
            foreach(object item in (ICollection)sender) {
                collection.Add(item);
            }

            this.DefaultViewModel["Group"] = collection;
            this.DefaultViewModel["Items"] = collection;
        }
    }
}
