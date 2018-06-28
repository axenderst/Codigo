using SECI.ProviderData;
using SECI.Security;

namespace SECI.FactoryData
{
    public class BaseRepository
    {
        protected DataConsumer _ProviderDB;

        protected BaseRepository()
        {
            this._ProviderDB = DataFactory.GetNewInstance(
                                    GlobalConfiguration.StringConectionDB,
                                    GlobalConfiguration.ProviderDB);

            this._ProviderDB.AutoOpenAndCloseConnectionForDataReader = true;

           
        }
    }
}
