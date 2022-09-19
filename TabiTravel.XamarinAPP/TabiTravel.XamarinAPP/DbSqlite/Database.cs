using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.DbSqlite
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        IUserService UserService;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);         
            _database.CreateTableAsync<UserInfo>();
            UserService = DependencyService.Get<IUserService>(DependencyFetchTarget.NewInstance);
        }

        public Task<UserInfo> GetAllUserInfo()
        {
            try
            {
                return _database.Table<UserInfo>().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task GenerateUserGuideInfo()
        {
            try
            {

                int userInfoCount = await _database.Table<UserInfo>().CountAsync();
                if (userInfoCount == 0)
                {
                    UserInfo userInfo = new UserInfo();
                    ProfilEditVM user = await UserService.GetById(Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542"), "EN");
                    userInfo.Firstname = user.Firstname;
                    userInfo.Lastname = user.Lastname;
                    userInfo.UserId = user.UserId;
                    userInfo.IsGuide = user.IsGuide;
                    userInfo.ImageUrl = user.Avatar;
                    string MotherLanguage = user.MotherLanguage;
                    userInfo.MotherLanguage = MotherLanguage;
                    await _database.InsertAsync(userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task GenerateUserTouristInfo()
        {
            try
            {
                int userInfoCount = await _database.Table<UserInfo>().CountAsync();
                if (userInfoCount == 0)
                {
                    UserInfo userInfo = new UserInfo();
                    ProfilEditVM user = await UserService.GetById(Guid.Parse("1046249e-1ab1-4c5f-8a08-42e1c9f07542"), "FR");
                    userInfo.Firstname = user.Firstname;
                    userInfo.Lastname = user.Lastname;
                    userInfo.UserId = user.UserId;
                    userInfo.IsGuide = user.IsGuide;
                    userInfo.ImageUrl = user.Avatar;
                    string MotherLanguage = user.MotherLanguage;
                    userInfo.MotherLanguage = MotherLanguage;
                    await _database.InsertAsync(userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public string GetUserMotherLanguage()
        {
            try
            {
                return _database.Table<UserInfo>().FirstOrDefaultAsync().Result.MotherLanguage;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Guid GetUserId()
        {
            try
            {
                return _database.Table<UserInfo>().FirstOrDefaultAsync().Result.UserId;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void UpdateUserInfo(string newMotherLanguage)
        {
            try
            {
                UserInfo resUser = _database.Table<UserInfo>().FirstOrDefaultAsync().Result;
                resUser.MotherLanguage = newMotherLanguage;
                await _database.UpdateAsync(resUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
