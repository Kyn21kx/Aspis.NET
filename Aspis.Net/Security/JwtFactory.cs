using Microsoft.IdentityModel.Tokens;
using AspisNet.Utils.ApiOperations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace AspisNet.Security {
	public class JwtFactory : IJwtFactory {

        private string secret;
        private JwtSecurityTokenHandler tokenHandler;


        public JwtFactory(string secret)
        {
            this.secret = secret;
            this.tokenHandler = new JwtSecurityTokenHandler();
        }

        public string MakeWithDictionaryPayload(IDictionary<string, object> payload)
        {
            SecurityToken token = MakeTokenWithClaims(claimsDictionary: payload);
            return this.tokenHandler.WriteToken(token);
        }

        public string MakeWithReflectionPayload<T>(T payload)
        {
            //Get the claims using reflection
            IEnumerable<Claim> claims = GetClaimsFromObject(payload);
            SecurityToken token = MakeTokenWithClaims(claimsEnumerable: claims);
            return this.tokenHandler.WriteToken(token);
        }

        private SecurityToken MakeTokenWithClaims(IEnumerable<Claim>? claimsEnumerable = null, IDictionary<string, object>? claimsDictionary = null)
        {
            ValidateClaimsOrThrow(claimsEnumerable, claimsDictionary);
            byte[] symmetricKey = Convert.FromBase64String(this.secret);

            DateTime now = DateTime.UtcNow;

            //Get our key
            SymmetricSecurityKey key = new SymmetricSecurityKey(symmetricKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
                IssuedAt = now,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            //Set the claims accordingly
            if (claimsEnumerable != null)
            {
                tokenDescriptor.Subject = new ClaimsIdentity(claimsEnumerable);
            }
            else
            {
                tokenDescriptor.Claims = claimsDictionary;
            }
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        private IEnumerable<Claim> GetClaimsFromObject<T>(T obj)
        {
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            
            //Now, iterate over properties, and fields, and add them on
            List<Claim> claims = new List<Claim>(fields.Length + properties.Length);
            
            for (int i = 0; i < fields.Length; i++)
            {
                object? value = fields[i].GetValue(obj);
                if (value == null) continue;
                Claim c = new Claim(fields[i].Name, value.ToString());
                claims.Add(c);
            }

            return claims;
        }

        private void ValidateClaimsOrThrow(IEnumerable<Claim>? claimsEnumerable = null, IDictionary<string, object>? claimsDictionary = null)
        {
            if (claimsEnumerable == null && claimsDictionary == null)
            {
                throw new ApiOperationException(
                    "A token needs to contain claims in the form of an enumerable, or a dictionary and none were provided",
                    ApiOperationStatus.InternalError
                );
            }
            //If we have both as non null
            if (!(claimsDictionary != null ^ claimsEnumerable != null)) //NOR
            {
                throw new ApiOperationException(
                    "The claims provided to the token are in Enumerable and Dictionary format at the same time, please use only one parameter",
                    ApiOperationStatus.InternalError
                );
            }
        }

    }
}
