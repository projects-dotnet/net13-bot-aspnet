using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Logic
{
    public class UserProfileRepo : IUserProfileRepository
    {
        IUserProfileRepository _userProfileMongoDB;
        IUserProfileRepository _userProfileSQLDB;

        public UserProfileRepo()
        {
            _userProfileMongoDB = new UserProfileMongoRepo("mongodb://localhost:27017");
            _userProfileSQLDB = new UserProfileSQLRepo("Server=.;Database=SimpleBotDB;User Id=sa;Password=Pa$$w0rd;");
        }

        public UserProfile GetProfile(string id)
        {
            string _id = id;
            var visitas = 0;
            DateTime horarioRegistro = DateTime.Now;

            var profileMongoDB = GetProfileMongoDB(_id);

            var profileSQLDB = GetProfileSqlDB(_id);

            if (profileMongoDB.Visitas != profileSQLDB.Visitas)
            {
                if (profileSQLDB.HorarioRegistro > profileMongoDB.HorarioRegistro)
                {
                    visitas = profileSQLDB.Visitas;
                    horarioRegistro = profileSQLDB.HorarioRegistro;
                }
                else if (profileSQLDB.HorarioRegistro < profileMongoDB.HorarioRegistro)
                {
                    visitas = profileMongoDB.Visitas;
                    horarioRegistro = profileMongoDB.HorarioRegistro;
                }
                else
                {
                    visitas = profileSQLDB.Visitas;
                    //visitas = profileMongoDB.Visitas;
                }
            }
            else
            {
                visitas = profileSQLDB.Visitas;
                //visitas = profileMongoDB.Visitas;
            }

            return new UserProfile
            {
                Id = _id,
                Visitas = visitas,
                HorarioRegistro = horarioRegistro
            };
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var _profile = profile;
            var profileMongoDB = new UserProfile()
            {
                Id = _profile.Id,
                Visitas = _profile.Visitas,
                HorarioRegistro = _profile.HorarioRegistro
            };

            SetProfileMongoDB(id, profileMongoDB);

            var profileSQLDB = new UserProfile()
            {
                Id = _profile.Id,
                Visitas = _profile.Visitas,
                HorarioRegistro = _profile.HorarioRegistro
            };


            SetProfileSqlDB(id, profileSQLDB);
        }

        private UserProfile GetProfileMongoDB(string Id)
        {
            string id = Id;

            if (_userProfileMongoDB == null)
                _userProfileMongoDB = new UserProfileMongoRepo("mongodb://localhost:27017");

            var profileMongoDB = _userProfileMongoDB.GetProfile(id);

            if (profileMongoDB == null)
            {
                profileMongoDB = new UserProfile()
                {
                    Id = id,
                    Visitas = 0,
                    HorarioRegistro = DateTime.Now
                };
            }
            return profileMongoDB;
        }

        private void SetProfileMongoDB(string Id, UserProfile userProfile)
        {
            var id = Id;
            var profileMongoDB = userProfile;

            if (_userProfileMongoDB == null)
                _userProfileMongoDB = new UserProfileMongoRepo("mongodb://localhost:27017");

            _userProfileMongoDB.SetProfile(id, profileMongoDB);
        }

        private UserProfile GetProfileSqlDB(string Id)
        {
            string id = Id;

            if (_userProfileSQLDB == null)
                _userProfileSQLDB = new UserProfileSQLRepo("Server=.;Database=SimpleBotDB;User Id=sa;Password=Pa$$w0rd;");

            var profileSQLDB = _userProfileSQLDB.GetProfile(id);

            if (profileSQLDB == null)
            {
                profileSQLDB = new UserProfile()
                {
                    Id = id,
                    Visitas = 0,
                    HorarioRegistro = DateTime.Now
                };
            }
            return profileSQLDB;
        }

        private void SetProfileSqlDB(string Id, UserProfile userProfile)
        {
            var id = Id;
            var profileSQLDB = userProfile;

            if (_userProfileSQLDB == null)
                _userProfileSQLDB = new UserProfileSQLRepo("Server=.;Database=SimpleBotDB;User Id=sa;Password=Pa$$w0rd;");

            _userProfileSQLDB.SetProfile(id, profileSQLDB);
        }
    }
}