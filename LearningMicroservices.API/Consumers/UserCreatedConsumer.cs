using MassTransit;
using MicroServices.Shared.Messages;

namespace OrderManagement.API.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var msg = context.Message;
            Console.WriteLine($"[OrderService] New user created: {msg.FullName} ({msg.Email})");

            // 🔹 Optionally save to local DB or cache
            // await _db.Users.AddAsync(new LocalUser { Id = msg.UserId, Name = msg.FullName, Email = msg.Email });
            // await _db.SaveChangesAsync();
        }
    }
}
