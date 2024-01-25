using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Services
{
    public class AddService
    {
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            return false;
        }
    }
}
