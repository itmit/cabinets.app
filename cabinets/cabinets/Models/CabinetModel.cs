using System;
using System.Collections.Generic;
using System.Text;

namespace cabinets.Models
{
    public class CabinetModel
    {
        /// <summary>
        /// Возвращает номер кабинета
        /// </summary>
        public int Number 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает изображение кабинета
        /// </summary>
        public string Image 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает вместимость кабинета
        /// </summary>
        public int Сapacity
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает площадь кабинета
        /// </summary>
        public double Square 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает цвет флажка
        /// </summary>
        public string BoxColor 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает дату
        /// </summary>
        public string Date 
        {
            get; 
            set; 
        }
    }
}
