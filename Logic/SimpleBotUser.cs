using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Logic;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static IUserProfileRepository _userProfile;

        public SimpleBotUser()
        {
            _userProfile = new UserProfileRepo();
        }

        public static string Reply(Message message)
        {
            /*
             * Salva mensagens
             * 
            var doc = new BsonDocument
            {
                {"id",  message.Id},
                {"user", message.User },
                {"originalText", message.Text },
                {"returnedText", returnedText },
                {"messageDT", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") }
            };

            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("SimpleBotDB");
            var col = db.GetCollection<BsonDocument>("messagesTable");

            col.InsertOne(doc);

            UserProfile userProfile = GetProfile(message.Id);
            userProfile.Visitas += 1;
            SetProfile(userProfile.Id, userProfile);
            */

            var id = message.Id;

            var visitas = 0;
            DateTime horarioRegistro = DateTime.Now;

            var profile = _userProfile.GetProfile(id);

            visitas = profile.Visitas;
            visitas++;

            profile.Id = id;
            profile.Visitas = visitas;
            profile.HorarioRegistro = horarioRegistro;

            _userProfile.SetProfile(id, profile);


            return $"{message.User} disse '{message.Text}' e enviou {visitas} mensagens.";
        }

    }
}