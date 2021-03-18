using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmiteAssistant.Model
{
    public class SmitePlayer : INotifyPropertyChanged
    {
        private Int32 id = 0;
        private Int32 gameId = 0;
        private String name = "";
        private Boolean status = false;
        private String lastSeen = "";

        public Int32 Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public Int32 GameId
        {
            get => gameId;
            set
            {
                gameId = value;
                OnPropertyChanged();
            }
        }
        public String Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public Boolean Status 
        { 
            get => status;
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public String LastSeen 
        { 
            get => lastSeen;
            set
            {
                lastSeen = value;
                OnPropertyChanged();
            }
        }

        public SmitePlayer(Int32 gameId, String name, Boolean status, String lastSeen)
        {
            GameId = gameId;
            Name = name;
            Status = status;
            LastSeen = lastSeen;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
