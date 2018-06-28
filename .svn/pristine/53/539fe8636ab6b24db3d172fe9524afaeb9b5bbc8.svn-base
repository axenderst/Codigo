using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using SECI.ProviderData.Components;

namespace SECI.ProviderData.Providers
{
    internal sealed class CommonConsumer : DataConsumer
    {
        #region Variables

        private DbConnection dbConnection;
        private DbCommand dbCommand;
        private DbDataAdapter DAdapter;
        private DbTransaction dbTransaction;
        private string dbProvider = string.Empty;

        #endregion

        #region Propiedades Polimorficas Publicas

        public override string ServerVersion { get { return dbConnection.ServerVersion; } }
        public override string ConnectionString { get { return dbConnection.ConnectionString; } }
        public override int ConnectionTimeout { get { return dbConnection.ConnectionTimeout; } }
        public override string Database { get { return dbConnection.Database; } }
        public override string DataSource { get { return dbConnection.DataSource; } }
        public override string WorkstationId { get { return null; } }
        public override string ProviderOrDriver { get { return this.dbProvider; } }
        public override ConnectionState DbConnectionState { get { return dbConnection.State; } }
        public override Boolean AutoOpenAndCloseConnectionForDataReader { get; set; }

        #endregion

        #region Constructor

        public CommonConsumer(string UrlConn, string ProviderName)
        {
            try
            {
                this.AutoOpenAndCloseConnectionForDataReader = false;
                this.dbProvider = ProviderName;
                this.UrlConnection = UrlConn;
                this.dbConnection = Provider.GetDbFactory(ProviderName).CreateConnection();
                this.dbConnection.ConnectionString = this.UrlConnection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Metodos Polimorficos Publicos

        /// <summary>
        /// Abre la conexión de la bd
        /// </summary>
        public override void OpenConnection()
        {
            this.dbConnection.Open();
        }

        /// <summary>
        /// Cerrar la conexión ala BD
        /// </summary>
        public override void CloseConnection()
        {
            if (this.dbConnection.State == ConnectionState.Open &&
                this.dbConnection.State != ConnectionState.Executing)
            {
                this.dbConnection.Close();

                if (this.dbCommand != null)
                    this.dbCommand.Dispose();
            }
            else
                throw new Exception("No se puede cerrar la conexion por que no hay una conexion activa " +
                                    "o bien se esta realizando un proceso en este momento.");
        }

        /// <summary>
        /// Inicia una transacción
        /// </summary>
        public override void InitTransaction()
        {
            this.dbCommand = Provider.GetDbFactory(this.dbProvider).CreateCommand();

            this.dbCommand.Connection = this.dbConnection;

            if (this.dbCommand.Connection.State == ConnectionState.Open)
                this.dbTransaction = this.dbConnection.BeginTransaction();
            else
                throw new Exception(@"No se puede definir una transaccion ya que no hay una conexion abierta
                                      definida en DbCommand o DbCommand es nulo");
        }

        /// <summary>
        /// Deshace una transacción
        /// </summary>
        public override void RollbackTransaction()
        {
            this.dbTransaction.Rollback();
            /*
            if (this.dbTransaction.Connection != null)// && this.dbCommand.Transaction != null)
            {
                this.dbTransaction.Rollback();

            }
            else
                throw new Exception("No hay una transacción definida para el proceso actual.");
             */
        }

        /// <summary>
        /// Finaliza transacción y lo confirma
        /// </summary>
        public override void EndTransactionAndCommitDB()
        {
            this.dbTransaction.Commit();
        }

        /// <summary>
        /// CReación de un comando
        /// </summary>
        private void CreateCommand()
        {
            this.CreateCommand(CommandType.Text);
            this.dbCommand.CommandText = this.Query;
        }

        /// <summary>
        /// Indica como se interretará el comando
        /// </summary>
        /// <param name="commandtype"></param>
        private void CreateCommand(CommandType commandtype)
        {
            this.dbCommand = Provider.GetDbFactory(this.dbProvider).CreateCommand();
            this.dbCommand.CommandType = commandtype;
        }

        /// <summary>
        /// Crea un comando par agregar parametros al storeProcedure
        /// </summary>
        /// <param name="commandText">Texto del comando</param>
        /// <param name="commandType">Tipo de Comando</param>
        /// <param name="Parameters">Parametros</param>
        private void CreateCommand(string commandText, CommandType commandType, string Parameters)
        {
            this.CreateCommand(commandType);
            this.dbCommand.CommandText = commandText;

            this.AddParametersStoreProcedure(Parameters);
        }

        /// <summary>
        /// Agrega parametros a la colección
        /// </summary>
        /// <param name="commandText">Texto del comando</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="Parameters">Parametros</param>
        private void CreateCommand(string commandText, CommandType commandType, IDataParameter[] Parameters)
        {
            this.CreateCommand(commandType);
            this.dbCommand.CommandText = commandText;

            foreach (IDataParameter param in Parameters)
                this.dbCommand.Parameters.Add(param);
        }

        /// <summary>
        /// Crea un DataAdapter
        /// </summary>
        /// <returns>Regresa un objeto para actualizar un conjunto de datos</returns>
        private IDataAdapter CreateDataAdapter()
        {
            this.DAdapter = Provider.GetDbFactory(this.dbProvider).CreateDataAdapter();

            this.DAdapter.SelectCommand = this.dbCommand;
            this.DAdapter.SelectCommand.Connection = this.dbConnection;

            return (IDataAdapter)this.DAdapter;
        }

        /// <summary>
        /// Agrega o actualiza las filas para hacerlas coincidir con el origen de datos
        /// </summary>
        /// <returns>Regresa un DataTable</returns>
        private DataTable FillDataAdapter()
        {
            try
            {
                this.CreateDataAdapter();

                if (this.DAdapter == null)
                {
                    return null;
                }

                DataTable DT = new DataTable();
                this.DAdapter.Fill(DT);
                return DT;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.DAdapter != null)
                    this.DAdapter.Dispose();
            }
        }

        /// <summary>
        /// Verifica si esta el estado de la conexión
        /// </summary>
        private void VerifyOpenConnection()
        {
            if (this.AutoOpenAndCloseConnectionForDataReader)
                this.OpenConnection();

            if (this.dbConnection.State != ConnectionState.Open)
            {
                throw new Exception("No existe una conexión abierta.");
            }
        }

        /// <summary>
        /// Ejecuta un comando en la conexión y devuelve un objeto
        /// </summary>
        /// <returns>Regresa un objeto</returns>
        private IDataReader ExecuteReader()
        {
            if (this.AutoOpenAndCloseConnectionForDataReader)
                return (IDataReader)this.dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            else
                return (IDataReader)this.dbCommand.ExecuteReader();
        }

        /// <summary>
        /// Ejecuta la consulta y trae el rimer resultado, los demas se desechan
        /// </summary>
        /// <returns>Regresa la primera fila de los resultados de la columna y cierra la conexión</returns>
        private object ExecuteScalarQuery()
        {
            try
            {
                return this.dbCommand.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// Actualizar el origen de datos
        /// </summary>
        /// <returns>Regresa un DataAdapter</returns>
        public override IDataAdapter GetDataAdapter()
        {
            this.CreateCommand();

            return this.CreateDataAdapter();
        }

        /// <summary>
        /// Actualiza los datos que mande el store procedre
        /// </summary>
        /// <param name="StoreProcedure">Nombre del procedimeinto almacenado</param>
        /// <param name="Parameters">Paramentros</param>
        /// <returns>Regresa una DataAdapter para el store procedure</returns>
        public override IDataAdapter GetDataAdapter(string StoreProcedure, string Parameters)
        {
            this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

            return this.CreateDataAdapter();
        }

        public override IDataAdapter GetDataAdapter(string StoreProcedure, IDataParameter[] Parameters)
        {
            this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

            return this.CreateDataAdapter();
        }

        public override IDataReader GetDataReader()
        {
            this.CreateCommand();

            this.VerifyOpenConnection();

            this.dbCommand.Connection = this.dbConnection;

            return this.ExecuteReader();
        }

        public override IDataReader GetDataReader(string StoreProcedure, string Parameters)
        {
            this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

            this.VerifyOpenConnection();

            this.dbCommand.Connection = this.dbConnection;

            return this.ExecuteReader();
        }

        public override IDataReader GetDataReader(string StoreProcedure, IDataParameter[] Parameters)
        {
            this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

            this.VerifyOpenConnection();

            this.dbCommand.Connection = this.dbConnection;

            return this.ExecuteReader();
        }

        public override DataTable ExecuteQuery()
        {
            try
            {
                this.CreateCommand();

                return this.FillDataAdapter();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override DataTable ExecuteQuery(string StoreProcedure, string Parameters)
        {
            try
            {
                this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

                return this.FillDataAdapter();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override DataTable ExecuteQuery(string StoreProcedure, IDataParameter[] Parameters)
        {
            try
            {
                CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

                return FillDataAdapter();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override Object ExecuteScalar()
        {
            this.CreateCommand();

            this.VerifyOpenConnection();

            this.dbCommand.Connection = this.dbConnection;

            return this.ExecuteScalarQuery();
        }

        public override Object ExecuteScalar(string StoreProcedure, string Parameters)
        {
            this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

            this.VerifyOpenConnection();

            this.dbCommand.Connection = this.dbConnection;

            return this.ExecuteScalarQuery();
        }

        public override Object ExecuteScalar(string StoreProcedure, IDataParameter[] Parameters)
        {
            this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

            this.VerifyOpenConnection();

            this.dbCommand.Connection = this.dbConnection;

            return this.ExecuteScalarQuery();
        }

        public override Int32 ExecuteNonQuery()
        {
            try
            {
                this.CreateCommand();

                this.VerifyOpenConnection();

                this.dbCommand.Connection = this.dbConnection;

                return this.dbCommand.ExecuteNonQuery(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.CloseConnection();
            }
        }

        public override Int32 ExecuteNonQuery(string StoreProcedure, string Parameters)
        {
            try
            {
                this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

                this.VerifyOpenConnection();

                this.dbCommand.Connection = this.dbConnection;

                return this.dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.CloseConnection();
            }
        }

        public override Int32 ExecuteNonQuery(string QueryOrSP, IDataParameter[] Parameters, bool? IsStoreProcedure = true)
        {
            try
            {
                this.CreateCommand(QueryOrSP, ((bool)IsStoreProcedure) ? CommandType.StoredProcedure : CommandType.Text, Parameters);

                this.VerifyOpenConnection();

                this.dbCommand.Connection = this.dbConnection;

                return this.dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.CloseConnection();
            }
        }

        public override object ExecuteScalarTransaction(string StoreProcedure, IDataParameter[] Parameters)
        {
            if (this.dbCommand != null)
            {
                this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

                return this.dbCommand.ExecuteScalar(); ;
            }
            else
                throw new Exception("No se ha iniciado la transacción, debe definirla previamente.");
        }

        public override Int32 ExecuteNonQueryTransaction()
        {
            if (this.dbCommand != null)
            {
                this.CreateCommand();

                this.dbCommand.Connection = this.dbConnection;
                this.dbCommand.Transaction = this.dbTransaction;

                return this.dbCommand.ExecuteNonQuery();
            }
            else
                throw new Exception("No se ha iniciado la transacción, debe definirla previamente.");
        }

        public override Int32 ExecuteNonQueryTransaction(string StoreProcedure, string Parameters)
        {
            if (this.dbCommand != null)
            {
                this.CreateCommand(StoreProcedure, CommandType.StoredProcedure, Parameters);

                this.dbCommand.Connection = this.dbConnection;
                this.dbCommand.Transaction = this.dbTransaction;

                return this.dbCommand.ExecuteNonQuery();
            }
            else
                throw new Exception("No se ha iniciado la transacción. debe definirla previamente.");
        }

        public override Int32 ExecuteNonQueryTransaction(string QueryOrSP, IDataParameter[] Parameters, bool? IsStoreProcedure = true)
        {
            if (this.dbCommand != null)
            {
                this.CreateCommand(QueryOrSP, ((bool)IsStoreProcedure) ? CommandType.StoredProcedure : CommandType.Text, Parameters);

                this.dbCommand.Connection = this.dbConnection;
                this.dbCommand.Transaction = this.dbTransaction;

                return this.dbCommand.ExecuteNonQuery();
            }
            else
                throw new Exception("No se ha iniciado la transacción. debe definirla previamente.");
        }

        /// <summary>
        /// Método que libera memoria
        /// </summary>
        public override void Dispose()
        {
            if (this.dbTransaction != null)
                this.dbTransaction.Dispose();

            if (this.DAdapter != null)
                this.DAdapter.Dispose();

            if (this.dbCommand != null)
                this.dbCommand.Dispose();

            if (this.dbConnection != null)
                this.dbConnection.Dispose();

            GC.SuppressFinalize(this);

            ManagerComponents.DisposeComponent();
        }

        #endregion

        #region Metodos Privados

        private void AddParametersStoreProcedure(string Parameters)
        {
            if (Parameters.Length.Equals(0)) return;

            if (this.dbCommand != null)
            {
                this.dbCommand.Parameters.Clear();

                string[] detParam = Parameters.Split(',');

                foreach (string param in detParam)
                {
                    if (param.Equals(string.Empty)) continue;

                    string[] Item = param.Split('=');

                    if (Item.Length.Equals(2))
                    {
                        DbParameter parameter = Provider.GetDbFactory(this.dbProvider).CreateParameter();
                        parameter.ParameterName = Item[0];
                        parameter.Value = Item[1];
                        this.dbCommand.Parameters.Add(parameter);
                    }
                }
            }
            else
                throw new Exception("El objeto dbCommand es nulo, no es posible asignar los parametros del Store Procedure");
        }

        #endregion
    }
}