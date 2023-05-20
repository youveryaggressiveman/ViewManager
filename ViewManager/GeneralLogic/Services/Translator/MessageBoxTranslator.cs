using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.Translator
{
    public static class MessageBoxTranslator
    {
        public static string GetTitle(string title, string lang)
        {
            var message = string.Empty;
            if (lang == "ru")
            {
                switch (title)
                {
                    case "Error":
                        message = "Ошибка";
                        break;
                    case "Warning":
                        message = "Предупреждение";
                        break;
                    case "Information":
                        message = "Информация";
                        break;
                    case "Confirm":
                        message = "Подтверждение";
                        break;
                    case "Ask":
                        message = "Вопрос";
                        break;
                    default:
                        break;
                }
            }
            
            return message;
        }

        public static string GetButtonName(string buttonName, string lang)
        {
            var message = string.Empty;
            if (lang == "ru")
            {
                switch (buttonName)
                {
                    case "Ok":
                        message = "Ок";
                        break;
                    case "No":
                        message = "Нет";
                        break;
                    case "Yes":
                        message = "Да";
                        break;
                    case "Cancel":
                        message = "Отменить";
                        break;
                    case "Confirm":
                        message = "Принять";
                        break;
                    default:
                        break;
                }
            }

            return message;
        }

    }
}
