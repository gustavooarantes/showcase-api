namespace FoodNet.Service;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}