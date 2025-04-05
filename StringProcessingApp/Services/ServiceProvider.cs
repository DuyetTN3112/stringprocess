using StringProcessingApp.Database;
using StringProcessingApp.Interfaces;
using StringProcessingApp.Repositories;


namespace StringProcessingApp.Services
{
    public class ServiceProvider
    {
        private static ServiceProvider _instance;
        private static readonly object _lock = new object();

        public DatabaseContext DbContext { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IMessageRepository MessageRepository { get; private set; }
        public AuthService AuthService { get; private set; }
        public MessageService MessageService { get; private set; }

        private ServiceProvider()
        {
            // Initialize all services
            DbContext = new DatabaseContext();
            UserRepository = new UserRepository(DbContext);
            MessageRepository = new MessageRepository(DbContext);
            AuthService = new AuthService(UserRepository);
            MessageService = new MessageService(MessageRepository);
        }

        public static ServiceProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServiceProvider();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
