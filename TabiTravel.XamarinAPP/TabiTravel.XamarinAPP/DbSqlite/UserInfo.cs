using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace TabiTravel.XamarinAPP.DbSqlite
{
    public class UserInfo
    {
        [PrimaryKey]       
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MotherLanguage { get; set; }
        public string ImageUrl { get; set; }
        public bool IsGuide { get; set; }

    }
}