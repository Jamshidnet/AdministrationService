using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Extensions;

public static  class ExtensionMethods
{
    public static string GetHashedString(this string text)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashbytes = sha256.ComputeHash(bytes);
            text = Convert.ToBase64String(hashbytes);
        }
        return text;
    }


    private static Dictionary<string, int> tables = new()
    {
        ["CategoryTable"] = 1,
        ["ClientTable"] = 2,
        ["ClientAnswerTable"] = 3,
        ["ClientTypeTable"] = 4,
        ["DefaultAnswerTable"] = 5,
        ["DistrictTable"] =6,
        ["DocTable"]=  7,
        ["PermissionTable"]= 8,
        ["PeopleTable"] = 9,
        ["QuarterTable"] = 10,
        ["QuestionTable"] = 11,
        ["QuestionTypeTable"] = 12,
        ["RegionTable"] = 13,
        ["DocChangeLogTable"] = 15,
        ["SysTableTable"] =  16,
        ["UserActionTable"] = 17,
        ["UserTable"] = 18,
        ["UserTypeTable"] = 19
    };

    public static Dictionary<string, int> Tables { get => tables; set => tables = value; }
}
