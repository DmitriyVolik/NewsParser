using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Parser.Models;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using Fizzler;
using System.Collections.ObjectModel;
using System.Windows;

namespace Parser
{
    class NewsPageParser
    {

        HtmlNode document;


        public NewsPageParser(HtmlNode document)=> this.document = document;



        public List<News> GetNews()
        {
            List<News> result = new List<News>();

            foreach (var item in document.QuerySelectorAll("a.quicklink"))
            {
                var news_temp = new News();

                news_temp.Header = item.QuerySelector("div.quicklink__title")?.InnerText;

                news_temp.Date = item.QuerySelector("div.quicklink__date")?.InnerText;

                news_temp.Text = item.QuerySelector("div.quicklink__catchphrase")?.InnerText;

                news_temp.Url = item.Attributes["href"]?.Value;

                result.Add(news_temp);

            }

            //MessageBox.Show(result.Count.ToString());
            return result;
        }

        public string GetNextPageUrl()
        {
            return document.QuerySelector("div.search__next a").Attributes["href"].Value;
        }

        private int getCurrentPageNumber()
        {
            return Convert.ToInt32(document.QuerySelector("span.current").InnerText);
        }

        public bool IsNextPage()
        {
            //< span aria - current = "page" class="page-numbers current">51</span>

            //var currentPage = getCurrentPageNumber();

            return document.QuerySelector("div.search__next a") != null;


            //foreach (var item in document.QuerySelectorAll("a.page-number"))
            //{
            //    int page = Convert.ToInt32(item.InnerText);
            //    if (page > currentPage) return true;
            //}

        }
    }
}
