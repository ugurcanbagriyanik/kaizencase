using KaizenCase.Helpers;
using KaizenCase.Interfaces;
using KaizenCase.Models;

namespace KaizenCase.Services
{

    public class CodeGeneratorService : ICodeGeneratorService
    {
        
        private readonly ILogger<CodeGeneratorService> _logger;
        private const string AllowedChars = "CDEFGHKLMNPRTXYZ234579"; // 'A' basamak sayisi esitlemek icin kullanilacagindan burada bulunmuyor.
        private static byte[] _key = new[] {(byte)125, (byte)127, (byte)12, (byte)15,(byte)125, (byte)127, (byte)12, (byte)15,(byte)12, (byte)15};
        private static int _createdCodeCount = 0;
        private static int _secretStartValue = 12437; // must be min 10000
        private const string Valid = "Valid";
        private const string NotValid = "NotValid";
        
        
        public CodeGeneratorService(ILogger<CodeGeneratorService> logger)
        {
            _logger = logger;
            _createdCodeCount = Int32.Parse(Environment.GetEnvironmentVariable("CreatedCodeCount") ?? "0");
            _secretStartValue = Int32.Parse(Environment.GetEnvironmentVariable("SecretStartValue") ?? "12437");
            var byteArrayString = Environment.GetEnvironmentVariable("SecretKey") ?? "125,127,12,15,125,127,12,15,12,15";
            _key = byteArrayString.Split(',').Select(byte.Parse).ToArray();
        }
        
        
        public async Task<List<string>> GenerateCode(int count)
        {
            Skip32Cipher skip32 = new Skip32Cipher(_key);

            var list = new List<string>();
            
            for (int i = 0; i < count; i++)
            {
                var encrypted = skip32.Encrypt(_secretStartValue + _createdCodeCount + i);
                list.Add(BaseConverter.Encode(encrypted, AllowedChars.ToCharArray()));
            }

            _createdCodeCount += count;

            return list;

        }        
        
        
        public async Task<string> CheckCode(string code)
        {
            try
            {
                var res=BaseConverter.Decode(code, AllowedChars.ToCharArray());
                if (res==0)
                {
                    return NotValid;
                }
            
                Skip32Cipher skip32 = new Skip32Cipher(_key);
                var decrypted = skip32.Decrypt(res);
            
                if (decrypted > _secretStartValue + _createdCodeCount || decrypted < 0)
                {
                    return NotValid;
                }
            }
            catch (Exception)
            {
                return NotValid;
            }
            return Valid;
        }
        
        public async Task<string> GenerateAndCheckAll(int count)
        {
            var failCount = 0;
            var codeList= await GenerateCode(count);

            foreach (var c in codeList)
            {
                var check=await CheckCode(c);
                if (check==NotValid)
                {
                    failCount++;
                }
            }
            
            return $"All Checked With {failCount} Fail Count ";

        }
        
    }
}