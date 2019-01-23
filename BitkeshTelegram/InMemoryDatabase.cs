using System;
using System.Collections.Generic;

namespace BitkeshTelegram
{
    public class InMemoryDatabase
    {
        private static Dictionary<string, int> logins = new Dictionary<string, int>();
        private static Dictionary<int, string> mappings = new Dictionary<int, string>();

        static InMemoryDatabase(){
            logins.Add("27780956607", 1234);
            logins.Add("27760772568", 1234);
        }

        public InMemoryDatabase()
        {
        }

        public int ValidatePhoneNumber(string phoneNumber){
            if (logins.ContainsKey(phoneNumber))
                return 1;
            return 0;
        }

        public void RegisterMobileToChatId(string phoneNumber,int chatId){
            mappings[chatId] = phoneNumber;
        }

        public bool ValidateLoginsUsingChatId(int chatId,int pinSupplied){
            if ( mappings.ContainsKey(chatId)){
                var phoneNumber = mappings[chatId];
                var pin = logins[phoneNumber];
                if ( pin - pinSupplied == 0 ){
                    return true;
                }
            }
            return false;
        }
    }
}
