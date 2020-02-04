namespace cabinets.Core.Models
{
    public class News
    {
        /// <summary>
        /// Возвращает картинку новости
        /// </summary>
        public string Image 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает заголовок новости
        /// </summary>
        public string Title 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает дату новости
        /// </summary>
        public string Date 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает детальный текст новостей
        /// </summary>
        public string DetailText 
        { 
            get; 
            set; 
        }
    }
}
