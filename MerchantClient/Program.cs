var client = new ApiClient();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("1. Create Player");
    Console.WriteLine("2. Get Balance");
    Console.WriteLine("0. Exit");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("ExternalId: ");
            var externalId = Console.ReadLine();

            Console.Write("PartnerId: ");
            var partnerId = Console.ReadLine();

            await client.CreatePlayerAsync(
                externalId!,
                partnerId!);

            break;

        case "2":
            Console.Write("ExternalId: ");
            var id = Console.ReadLine();

            await client.GetBalanceAsync(id!);

            break;

        case "0":
            return;
    }
}