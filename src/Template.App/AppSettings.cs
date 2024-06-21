namespace Template.App
{
    /// <summary>
    /// Набор настроек из appSetting.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// ewqecc <b>для внешних сервисов</b>
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Строки подключения
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }


    }
    public class ConnectionStrings
    {
        /// <summary>
        /// Подключение к RabbitMQ
        /// amqp://[username:password@]host1[:port1]/[vhost]
        /// </summary>
        public string RabbitConnection { get; set; }

        /// <summary>
        /// Подключение к MS SQL
        /// </summary>
        public string MsSqlConnection { get; set; }

        /// <summary>
        /// Подключение npgsql для Postgres
        /// </summary>
        /// <remarks>
        /// Формат
        /// <c>Server=[your server];Port=[your port];Database=[your database];User ID=[your user];Password=[your password]</c>
        /// <![CDATA[
        /// > [!NOTE]
        /// > Подробнее https://www.npgsql.org/doc/connection-string-parameters.html
        /// ]]></remarks>
        ///
        /// <example>
        /// <code>Server=postgres;Port=5432;Database=authorization;Username=authorization;Password=authorization</code>
        /// </example>
        /// <example>
        /// <code>Server=dotnet_postgres_test;Port=5432;Database=auth20;Username=authuser;Password=n1234</code>
        /// </example>
        public string PgSqlConnection { get; set; }
    }

}
