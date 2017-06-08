using Maratona.Services.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Maratona.Droid.Services.ConfigDB))]

namespace Maratona.Droid.Services
{
    public class ConfigDB : IConfig
    {
        private string _sQLiteDirectory;
        private SQLite.Net.Interop.ISQLitePlatform _platform;

        public new string SQLiteDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_sQLiteDirectory))
                {
                    _sQLiteDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return _sQLiteDirectory;
            }
        }

        public new ISQLitePlatform Platform
        {
            get
            {
                if (_platform == null)
                {
                    _platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return _platform;
            }
        }
    }
}