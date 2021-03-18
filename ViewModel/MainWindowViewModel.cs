using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using SmiteAssistant.Model;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace SmiteAssistant.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private Boolean status = false;
        private String text = "";
        private String playerName = "";
        private SmitePlayer currentPlayer = null;
        private WebAssistant assistant = new WebAssistant();
        private ObservableCollection<String> foundPlayers = new ObservableCollection<String>();
        private CustomCommand findCommand;

        public Boolean Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public String Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public String PlayerName
        {
            get => playerName;
            set
            {
                playerName = value;
                OnPropertyChanged();
            }
        }
        public SmitePlayer CurrentPlayer
        {
            get => currentPlayer;
            set
            {
                currentPlayer = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<String> FoundPlayers 
        { 
            get=>foundPlayers;
            set 
            {
                foundPlayers = value;
                OnPropertyChanged();
            }
        }
        public CustomCommand FindCommand
        {
            get
            {
                return findCommand ?? new CustomCommand(obj =>
                {
                    //Text = assistant.Test();
                    FoundPlayers = assistant.TestFind(PlayerName);
                });
            }
        }

        public MainWindowViewModel()
        {
            Status = assistant.CheckServerStatus();
            FoundPlayers = new ObservableCollection<string>();

            DispatcherTimer serverStatusChecker = new DispatcherTimer();
            serverStatusChecker.Tick += (s, e) => Status = assistant.CheckServerStatus();
            serverStatusChecker.Interval = TimeSpan.FromSeconds(1);
            serverStatusChecker.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
