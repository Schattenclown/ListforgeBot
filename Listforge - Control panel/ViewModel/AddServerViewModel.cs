using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BotDLL;

namespace Listforge_Control_panel
{
    public class AddServerViewModel : BaseModel
    {
        private List<Game> _lstGame;
        public List<Game> LstGame
        {
            get
            {
                return _lstGame;
            }
            set
            {
                _lstGame = value;
                onPropertyChanged("LstGame");
            }
        }
        private LF_API_Uri _API_URL;
        public LF_API_Uri API_URL
        {
            get
            {
                return _API_URL;
            }
            set
            {
                _API_URL = value;
                onPropertyChanged("API_URL");
            }
        }
        public AddServerViewModel()
        {
            this._lstGame = new List<Game>();
            this.LstGame.Add(Game.ARK);
            this.LstGame.Add(Game.Conan);
            this.LstGame.Add(Game.CSGO);
            this.LstGame.Add(Game.Minecraft);
            this.LstGame.Add(Game.TeamSpeak);
            this.LstGame.Add(Game.Valheim);

            this.API_URL = new LF_API_Uri();
        }
    }
}
