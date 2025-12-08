using System.Security.Cryptography;

namespace MicroserviceCourse.Discount.Api.Features.Discounts;

public static class DiscountCodeGenerator
{
    private const string Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GenerateDiscountCode(int length = 10)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

        char[] buffer = new char[length];
        for(int i = 0; i < length; i++)
        {
            int idx = RandomNumberGenerator.GetInt32(Allowed.Length);
            buffer[i] = Allowed[idx];
        }

        return new string(buffer);
    }
}
