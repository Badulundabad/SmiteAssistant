using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SmiteAssistant.Model
{
    public class WebAssistant : INotifyPropertyChanged
    {
        private Boolean serverStatus = false;

        public Boolean ServerStatus
        {
            get => serverStatus;
            set
            {
                serverStatus = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SmitePlayer> Players { get; private set; }

        public WebAssistant()
        {

        }

        public Boolean CheckServerStatus()
        {
            String site = "http://status.hirezstudios.com/";
            HtmlDocument hirez = new HtmlDocument();
            using (WebClient webClient = new WebClient())
                hirez.LoadHtml(webClient.DownloadString(site));
            String status = hirez.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[2]/div[1]/div[2]/div[2]/div[1]/span[2]").InnerText;
            if (status.Contains("Operational"))
                return true;
            else return false;
        }
        public void CheckPlayersStatus()
        {

        }
        public ObservableCollection<SmitePlayer> FindPlayers(String name)
        {
            var hexName = "%" + BitConverter.ToString(Encoding.UTF8.GetBytes("super")).Replace('-', '%');
            var findString = $@"https://smite.guru/search?term={hexName}&type=Player";
            var guru = new HtmlDocument();
            using (var webClient = new WebClient())
                guru.LoadHtml(webClient.DownloadString(findString));
            var players = guru.DocumentNode.SelectNodes("/html/body/div[2]/div/div/div/section/div[2]/div/div");
            return new ObservableCollection<SmitePlayer> { new SmitePlayer(0, "", false, "") };
        }
        public ObservableCollection<String> TestFind(String name)
        {
            String hexName = "%" + BitConverter.ToString(Encoding.UTF8.GetBytes(name)).Replace('-', '%');
            String findString = $"https://smite.guru/search?term={hexName}&type=Player";
            HtmlDocument guru = new HtmlDocument();
            using (WebClient webClient = new WebClient())
                guru.LoadHtml(webClient.DownloadString(findString));
            var playerTable = guru.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div/section/div[2]/div/div");
            ObservableCollection<String> tests = new ObservableCollection<string>();
            foreach (var item in playerTable.ChildNodes)
            {
                string t = item.OuterHtml;
                tests.Add(t);
//                var array = playerTable.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[0].Attributes["href"].Value
//                    .Split(new char[] { '/', '-' });
//                string gameId = array[array.Length - 2];
            }
            return tests;
        }
        public String Test()
        {
            String hexName = "%" + BitConverter.ToString(Encoding.UTF8.GetBytes("super")).Replace('-', '%');
            String findString = $"https://smite.guru/search?term={hexName}&type=Player";
            HtmlDocument guru = new HtmlDocument();
            WebClient webClient = new WebClient();
            guru.LoadHtml(webClient.DownloadString(findString));
            var playerTable = guru.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div/section/div[2]/div/div");
            foreach (var item in playerTable.ChildNodes)
            {
                string t = item.ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerHtml;
            }
            var array = playerTable.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[0].Attributes["href"].Value
                .Split(new char[] { '/', '-' });
            string gameId = array[array.Length - 2];
            return playerTable.ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerHtml;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
