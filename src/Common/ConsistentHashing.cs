using System.Security.Cryptography;
using System.Text;

namespace Common;

public class ConsistentHashing
{
    private readonly SortedDictionary<int, string> _circle = new();

    public ConsistentHashing(IEnumerable<string> nodes)
    {
        foreach (var node in nodes)
        {
            var hash = GetHash(node);
            _circle[hash] = node;
        }
    }

    public string GetNode(string key)
    {
        var hash = GetHash(key);
        foreach (var nodeHash in _circle.Keys)
        {
            if (hash <= nodeHash)
                return _circle[nodeHash];
        }
        return _circle[_circle.Keys.First()];
    }

    private int GetHash(string key)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        return BitConverter.ToInt32(bytes, 0);
    }
}