using System.Data.SqlClient;
using EasyNetQ;

namespace outbox_profile_worker;


public class MyMessage
{
    public Guid Id { get; set; }
    public string Content { get; set; }
}


public class Worker : BackgroundService
{
    private IBus _bus;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        this._bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin");
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        this._bus.Dispose();
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=outbox-pattern;User Id=sa;Password=Test12345;MultipleActiveResultSets=True");
            SqlCommand command = new SqlCommand("SELECT Id, MessageContent FROM OUTBOX WHERE MESSAGETYPE='ProfileCreated' AND ISACTIVE = 1 ORDER BY CreatedDate", connection);

            if (command.Connection.State != System.Data.ConnectionState.Open)
                await command.Connection.OpenAsync();

            var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    MyMessage message = new MyMessage()
                    {
                        Id = (Guid)reader[0],
                        Content = (string)reader[1]
                    };

                    Console.WriteLine($"{message.Id} - {message.Content} was sent.");
                    await this._bus.SendReceive.SendAsync<MyMessage>("profileCreatedQueue", message).ConfigureAwait(false);

                    SqlCommand updateCommand = new SqlCommand("UPDATE OUTBOX SET IsActive = 0 WHERE Id=@Id", connection);
                    updateCommand.Parameters.Add("@Id", System.Data.SqlDbType.UniqueIdentifier).Value = message.Id;
                    var affectedRowCount = await updateCommand.ExecuteNonQueryAsync();
                    if (affectedRowCount == 0)
                        break;

                    Console.WriteLine($"{message.Id} was updated.");

                }
            }

            await reader.CloseAsync();
            await connection.CloseAsync();

            await Task.Delay(10000, stoppingToken);
        }
    }
}



