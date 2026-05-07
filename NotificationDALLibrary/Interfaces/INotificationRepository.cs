namespace NotificationDALLibrary.Interfaces{
    public interface INotificationRepository<T> where T : class{
        void Add(T entity);
        List<T> GetAll();
    }
}
