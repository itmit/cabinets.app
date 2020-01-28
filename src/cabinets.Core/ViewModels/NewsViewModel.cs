using cabinets.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace cabinets.ViewModels
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            News = new List<NewsModel>
            {
                new NewsModel
                {
                    Image = "pic_news.png",
                    Title = "Уважаемые коллеги",
                    Date = "01.01.2020",
                    DetailText = "Команда @arendapsy от всей души поздравляет Вас с Днем психолога! Желаем вам удачи, сил, энергии, профессионального и личностного роста"
                },
                new NewsModel
                {
                    Image = "pic_news2.png",
                    Title = "Всемирный день ребенка",
                    Date = "02.01.2020",
                    DetailText = "— В послании Федеральному собранию РФ Владимир Путин призвал чиновников разработать и принять законы о правовых песочницах для внедрения новых технологий и о больших данных. Также он предложил сделать бесплатным доступ к социально важным ресурсам: https://tprg.ru/WV87"
                },
                new NewsModel
                {
                    Image = "pic_news3.png",
                    Title = "День логопеда",
                    Date = "03.01.2020",
                    DetailText = "Недавно учёные впервые смогли визуализировать атомарную структуру двумерного льда во время его образования. Выясним, какие секреты сокрыты в этом на первый взгляд простом физическом процессе: http://amp.gs/DDTt"
                }
        };
            
        }

        public List<NewsModel> News 
        { 
            get; 
            set; 
        }
    }
}
