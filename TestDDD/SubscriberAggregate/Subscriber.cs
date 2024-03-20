using TestDDD.SubscriberAggregate.Input;

namespace TestDDD.SubscriberAggregate
{
    public class Subscriber
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsDeleted { get; private set; }
        public Subscriber() { }
        
        public Subscriber(SubscriberInput input)
        {
            Name = input.Name;
        }
        public async Task SoftDelete()
        {
            this.IsDeleted = true;
        }
        public async Task UpdateSubscriber(SubscriberInput input)
        {
            Name = input.Name;
        }
    }
}
