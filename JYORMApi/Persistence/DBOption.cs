namespace JYORMApi.Persistence
{
    public class DBOption
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string Uid { get; set; }

        public string Pwd { get; set; }

        public string Port { get; set; }

        override
        public string ToString()
        {
            return $"Server={Server};Database={Database};Uid={Uid};Pwd={Pwd};Port={Port}";
        }
    }
}
