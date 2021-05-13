using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using BotDLL;

namespace Listforge_Control_panel
{
    public class MainViewModel : BaseModel
    {
        private ObservableCollection<LF_API_Uri> _lstAPI_URL;
        public ObservableCollection<LF_API_Uri> LstAPI_URL
        {
            get
            {
                return _lstAPI_URL;
            }
            set
            {
                _lstAPI_URL = value;
                onPropertyChanged("LstAPI_URL");
            }
        }
        private LF_API_Uri _selAPI_URL;
        public LF_API_Uri SelAPI_URL
        {
            get
            {
                return _selAPI_URL;
            }
            set
            {
                this._selAPI_URL = value;
                onPropertyChanged("SelAPI_URL");
            }
        }
        public MainViewModel()
        {
            this.LstAPI_URL = new ObservableCollection<LF_API_Uri>(LF_API_Uri.ReadAll());
        }
        public void RefreshMainViewModel()
        {
            this.LstAPI_URL = new ObservableCollection<LF_API_Uri>(LF_API_Uri.ReadAll());
        }
    }
}
