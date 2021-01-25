using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Parser.Models;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using Fizzler;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Data.SqlClient;

namespace Parser.Models
{

    public static class Helper
    {
        public static string MD5(this string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }
        }
    }

    


    class FormViewModel : INotifyPropertyChanged
    {

        public string _text;

        public ObservableCollection<News> News { get; set; }

        private News _selectedItem;

        public readonly Database database;

        public string _search;

        public string _newsView;


        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged("Search");

            }
        }


        public string NewsView { get => _newsView; set {
                _newsView = value;
                OnPropertyChanged("NewsView");
            } }

        public News SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NewsView = GetRtfString(SelectedItem.GetAsFlowDocument());
                OnPropertyChanged("SelectedItem");
            }
        }

        public FormViewModel()
        {
            News = new ObservableCollection<News>();


            NewsView = "";

            database = new Database(@"Data Source=.\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");

            database.GetNews().ForEach(i => News.Add(i));

        }
        

        public string GetRtfString(FlowDocument doc)
        {
            var tr = new System.Windows.Documents.TextRange(doc.ContentStart, doc.ContentEnd);
            var stream = new System.IO.MemoryStream();
            tr.Save(stream, System.Windows.DataFormats.Rtf);
            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");

            }
        }

        static HtmlDocument getHTML(string URL)
        {
            HtmlWeb web = new HtmlWeb();

            return web.Load(URL);
        }

        static NewsPageParser GetParser(string url)
        {
            var document = getHTML(url).DocumentNode;
            var parser = new NewsPageParser(document);
            return parser;
        }

        async void LoadAsync(string url)
        {
            NewsPageParser parser;

            do
            {
                parser = await Task<NewsPageParser>.Run(() => GetParser(url));

                parser.GetNews().ForEach(i => { 
                    if (!database.Contain(i))
                    {
                        database.Insert(i);
                        News.Add(i);
                    }
                });

              

                if (parser.IsNextPage())
                {
                    url = parser.GetNextPageUrl();
                }


            } while (parser.IsNextPage());
            
        }

        public RelayCommand LoadButton
        {
            get
            {
                return new RelayCommand(
                        obj =>
                        {

                            string url = "https://cashessentials.org/news/";
                            LoadAsync(url);
                        }
                    );
            }
        }

        public RelayCommand SearchButton
        {
            get
            {
                return new RelayCommand(
                        obj =>
                        {
                         
                            News.Clear();

                            database.GetNews(Search).ForEach(i => News.Add(i));

                        }
                    );
            }
        }

        public RelayCommand ResetButton
        {
            get
            {
                return new RelayCommand(
                        obj =>
                        {
                 
                            News.Clear();

                            Search = "";

                            database.GetNews().ForEach(i => News.Add(i));

                        }
                    );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
