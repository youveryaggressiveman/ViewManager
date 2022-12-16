using System.Text;

namespace API.Core.Crypter
{
    public class Test
    {
        private static string password1 = "qwert";
        private static string password2;
        private static string password3 = "123";
        private static string password4;
        private static string password5;

        public static string[] test()
        {
            string password = "";
            int num = 0;
            int c = 0; //временные переменные с, l, t
            int l = 0;
            int t = 0;
            int[] S = new int[256]; //рандомный массив на 256 символов
            int[] C = new int[16]; //контрольная сумма
            int[] X = new int[48]; //48-битовый буфер
            int Nblock = 0; //кол-во 16-значных блоков

            //переменные для шифрования и расшифровки
            int v = 0;        //две переменные для задания
            string vekt = ""; //вектора инициализации
            int temp = 0; //временная переменная
            string L = ""; //левый подблок
            string R = ""; //правый подблок
            int[] key = new int[16]; //ключ
            string shifr = ""; //результат функции блочного шифрования
            string login = ""; //открытый текст
            string itog = ""; //искомый результат - зашифрованный открытый текст
            string stroka = "";
            string text = "";
            password = password1;


            num = 16 - (password.Length % 16); //дополняем пароль до кратного 16 кол-ва символов
            for (int i = 0; i < num; i++)
            {
                password = password + num;
            }
            Nblock = password.Length / 16;



            Random rand = new Random();//рандомим S
            for (int i = 0; i < S.Length; i++)
                S[i] = rand.Next(255);
            for (int i = 0; i < C.Length; i++) //заполняем С
                C[i] = 0;
            for (int i = 0; i < Nblock; i++) //вычисляем контрольную сумму
            {
                for (int j = 0; j < C.Length; j++)
                {
                    c = Convert.ToInt32(password[i * 16 + j]);
                    int b;
                    b = c ^ l;
                    C[j] = C[j] ^ S[b];
                    l = C[j];
                }
            }
            for (int i = 0; i < C.Length; i++) //и прибавляем ее к паролю
                password += C[i];


            for (int i = 0; i < X.Length; i++) //обработка 48-битного буфера
                X[i] = 0;
            Nblock = password.Length / 16;
            for (int i = 0; i < Nblock; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    X[16 + j] = password[i * 16 + j];
                    X[32 + j] = Convert.ToInt32(X[16 + j]) ^ Convert.ToInt32(X[j]);
                }
                for (int j = 0; j < 18; j++)
                {
                    for (int a = 0; a < X.Length; a++)
                    {
                        X[a] = X[a] ^ S[t];
                        t = X[a];
                    }
                    t = (t + j) % 256;
                }
            }
            password = "";
            for (int i = 0; i < 16; i++) //получаем готовый хэш (хэш для дальнейшей работы брать из Х)
                password += X[i].ToString();
            password2 = password;



            Random r = new Random();//рандомно задаем вектор инициализации и приводим его к 2-х значному виду
            v = r.Next(0, 99);
            vekt = v.ToString();
            if (vekt.Length != 2)
                for (int i = 0; i < 2; i++)
                    if (i > vekt.Length)
                        vekt += '0';
            for (int i = 0; i < vekt.Length; i++) //задаем левый и правый блоки
            {
                if (i < vekt.Length / 2)
                    L = vekt[i].ToString();
                else
                    R = vekt[i].ToString();
            }
            for (int i = 0; i < 16; i++) //задаем ключ
                key[i] = X[i];
            login = password3;
            byte[] log = Encoding.Default.GetBytes(login); //здесь хранится ascii-код символов логина.
            int[] kod = new int[login.Length]; //здесь хранится зашифрованный логин
            for (int i = 0; i < 16; i++) //функция блочного шифрования (сеть Фейстеля)
            {
                temp = Convert.ToInt32(R) ^ (Convert.ToInt32(L) ^ key[i]);
                R = L;
                L = temp.ToString();
            }
            shifr = L + R; //результат функции блочного шифрования
            for (int j = 0; j < login.Length; j++) //обратная связь по шифротексту
            {
                kod[j] = Convert.ToInt32(log[j]) ^ Convert.ToInt32(shifr); //результат шифрования
            }
            for (int i = 0; i < login.Length; i++)
                itog += kod[i].ToString();
            password4 = itog;



            //расшифровка
            int kod1 = 0;
            for (int j = kod.Length - 1; j >= 0; j--)
            {
                kod1 = kod[j] ^ Convert.ToInt32(shifr);
                stroka += Convert.ToChar(kod1);
            }
            for (int i = stroka.Length - 1; i >= 0; i--)
                text += stroka[i];
            password5 = text;

            return new string[] {password1,password2,password3,password4,password5};
        }
    }
}
