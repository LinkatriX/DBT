using DBT.Commons;

namespace DBT.Managers
{
    public class SingletonManager<TManager, TManagerOf> : Manager<TManagerOf> where TManager : Manager<TManagerOf>, new() where TManagerOf : IHasUnlocalizedName 
    {
        private static TManager _instance;

        public static TManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TManager();

                if (!_instance.Initialized)
                    _instance.DefaultInitialize();

                return _instance;
            }
        }
    }
}