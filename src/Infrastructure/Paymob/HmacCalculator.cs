using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Application.PaymobCommands.PaymobTransactionResponse;

namespace Infrastructure.Paymob;

public class HmacCalculator
{
   public bool VerifyHmac(PaymobTransactionResponseCommand paymob, string hash)
        {
            return CalculateHmac(paymob).Equals(hash, StringComparison.Ordinal);
        }
        private string CalculateHmac(PaymobTransactionResponseCommand paymob, string key = "")
        {
            string hmacPre = "";

            hmacPre += paymob.Transaction.AmountCents.ToString();
            hmacPre += paymob.Transaction.CreatedAt.ToString(CultureInfo.InvariantCulture);
            hmacPre += paymob.Transaction.Currency;
            hmacPre += paymob.Transaction.ErrorOccured.ToString().ToLower();
            hmacPre += paymob.Transaction.HasParentTransaction.ToString().ToLower();
            hmacPre += paymob.Transaction.Id.ToString();
            hmacPre += paymob.Transaction.IntegrationId.ToString();
            hmacPre += paymob.Transaction.Is3DSecure.ToString().ToLower();
            hmacPre += paymob.Transaction.IsAuth.ToString().ToLower();
            hmacPre += paymob.Transaction.IsCapture.ToString().ToLower();
            hmacPre += paymob.Transaction.IsRefunded.ToString().ToLower();
            hmacPre += paymob.Transaction.IsStandalonePayment.ToString().ToLower();
            hmacPre += paymob.Transaction.IsVoided.ToString().ToLower();
            hmacPre += paymob.Transaction.Order.Id.ToString();
            hmacPre += paymob.Transaction.Owner.ToString();
            hmacPre += paymob.Transaction.Pending.ToString().ToLower();
            hmacPre += paymob.Transaction.SourceData.Pan.ToString();
            hmacPre += paymob.Transaction.SourceData.SubType.ToString();
            hmacPre += paymob.Transaction.SourceData.Type.ToString();
            hmacPre += paymob.Transaction.Success.ToString().ToLower();
            
            Console.WriteLine(hmacPre);

            using(var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(hmacPre));
                var lowerCaseHex = String.Concat(hash.Select(b => b.ToString("x2"))).ToLower();

                Console.WriteLine(lowerCaseHex);
                return lowerCaseHex;
            }
        } 
}