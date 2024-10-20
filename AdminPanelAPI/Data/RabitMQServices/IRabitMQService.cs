namespace AdminPanelAPI.Data.RabitMQServices
{
    public interface IRabitMQService
    {
        public void SendMessage<T>(T message, string QueueName);
        public string ReceiveMessages(string QueueName);
    }
}
