using System.Security.Cryptography;
using System.Text;

string filePath;
long steamID;
byte[] steamIDBytes;

// Check if the file path is provided
if (args.Length < 1)
{
    Console.WriteLine("Usage: <program> <file_path> [steam_ID]");
    Console.WriteLine("Or drag and drop the file onto the executable and enter the Steam ID when prompted.");
    return;
}
else
{
    filePath = args[0];
}

if (!File.Exists(filePath))
{
    Console.WriteLine("File does not exist.");
    return;
}

// Check if Steam ID is provided, if not, ask for it
if (args.Length < 2 || string.IsNullOrEmpty(args[1]))
{
    Console.Write("Please enter the Steam ID: ");
    var steamIDInput = Console.ReadLine();
    if (!long.TryParse(steamIDInput, out steamID))
    {
        Console.WriteLine("Invalid Steam ID.");
        return;
    }
}
else
{
    if (!long.TryParse(args[1], out steamID))
    {
        Console.WriteLine("Invalid Steam ID provided.");
        return;
    }
}

steamIDBytes = BitConverter.GetBytes(steamID);


using (var f = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
{
    byte[] magic = new byte[7];
    f.Read(magic, 0, 7);
    string fileFormat = magic.SequenceEqual(Encoding.ASCII.GetBytes("HOI4bin")) ? "binary" : "text";

    long contentLength = f.Length - (fileFormat == "binary" ? 36 : 34);
    f.Seek(0, SeekOrigin.Begin);

    int upper = 0, lower = 0;
    for (int i = 0; i < contentLength; i++)
    {
        int byteValue = f.ReadByte();
        upper += byteValue;
        if (upper > 0xffff) upper -= 0xffff;
        lower += upper;
        if (lower > 0xffff) lower -= 0xffff;
    }

    int fletcher = (upper << 16) | lower;

    using (MD5 md5 = MD5.Create())
    {
        string hashInput = fletcher.ToString() + contentLength.ToString();
        byte[] inputBytes = Encoding.ASCII.GetBytes(hashInput);
        byte[] hashBytes = md5.ComputeHash(inputBytes.Concat(steamIDBytes).ToArray());

        f.Seek(-33 + (fileFormat == "binary" ? 1 : 0), SeekOrigin.End);
        f.Write(Encoding.ASCII.GetBytes(BitConverter.ToString(hashBytes).Replace("-", "").ToLower()), 0, 32);
    }
}

Console.WriteLine("Checksum and hash updated.");