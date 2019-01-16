using System;
using System.Collections.Generic;
using System.Text;

namespace PrefeituraConecta.API.Dados
{
    public class DBConfiguration
    {
        private readonly string _DBConfigurationSQL = @"Data Source=.;Initial Catalog=DB_PREFEITURA_CONECTA;Integrated Security=True";

        public string DBConfigurationSQL { get { return _DBConfigurationSQL; } }

        public string GetconfigurationStringSQL()
        {

            return DBConfigurationSQL;
        }
    }
}
