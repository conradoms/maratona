using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Maratona.Model
{
    /// <summary>
    /// Usuário
    /// </summary>
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
