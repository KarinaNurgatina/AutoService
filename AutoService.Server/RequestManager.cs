using System;
using System.Collections.Generic;

public class RequestManager : MarshalByRefObject
{
    private List<string> requests = new List<string>();

    public void AddRequest(string request)
    {
        requests.Add(request);
        Console.WriteLine($"Заявка добавлена: {request}");
    }

    public List<string> GetRequests()
    {
        return requests;
    }
}
