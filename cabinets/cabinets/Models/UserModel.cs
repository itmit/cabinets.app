using System;
using System.Collections.Generic;
using System.Text;

namespace cabinets.Models
{
    public class UserModel
    {
        /// <summary>
        /// Возвращает полное имя пользователя
        /// </summary>
        public string FullName
        {
            get;
            set;
        } = "Пушкин Александр Сергеевич";

        /// <summary>
        /// Возвращает номер телефона пользователя
        /// </summary>
        public string Phone
        {
            get;
            set;
        } = "8(922)783-33-33";

        /// <summary>
        /// Возвращает электронную почту пользователя
        /// </summary>
        public string Email
        {
            get;
            set;
        } = "IvanIvanov@gmail.com";
    }
}
